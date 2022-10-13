using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nissensai2022.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
using Logger = Nissensai2022.Runtime.Logger;


namespace Nissensai2022.Internal
{
    public enum SystemStatus
    {
        Idle = 0,
        Playing = 1,
    }

    internal class SystemStatusManager : MonoBehaviour
    {
        internal static SystemStatusManager Instance;

        [Header("基本設定")] [Space(10)] [SerializeField]
        private LogLevel logLevel = LogLevel.Debug;

        [SerializeField] private string password = "********************************";
        [Space(10)] [SerializeField] private UnityEvent gameStartCallback;
        [SerializeField] private UnityEvent gameStopCallback;

        [Tooltip("手動でQRコードの表示の切り替えを行う")] [SerializeField]
        private bool qrCodeDisplayManually = false;

        [Space(50)] [Header("サーバー通信設定")] [Space(10)] [SerializeField]
        private string server = "nissensai.games";

        [SerializeField] private bool useSSL = true;
        [SerializeField] private float waitTime = 1f;
        [SerializeField] private int retryTime = 3;

        [Space(50)] [Header("ここからは触っちゃダメ！")] [Space(10)] [SerializeField]
        internal GameObject panel;

        [SerializeField] private Image qrCodeImage;
        [SerializeField] private InputField playerIdInput;


        private static string _gameToken = "";
        private static bool _isGameTokenReady = false;
        private static Animation _qrCodeAnm;

        internal static int RetryTime => Instance.retryTime;

        internal static string GameToken
        {
            get { return _isGameTokenReady ? _gameToken : ""; }
            private set { _gameToken = value; }
        }

        internal static Player CurrentPlayer { get; private set; }

        private static SystemStatus _status = SystemStatus.Idle;

        internal static SystemStatus Status
        {
            get => _status;
            set
            {
                if (_status == value)
                    return;

                if (_status == SystemStatus.Idle && value == SystemStatus.Playing)
                {
                    Logger.Log($"Game start for {CurrentPlayer.Name}");
                    Instance.gameStartCallback.Invoke();
                    if (!Instance.qrCodeDisplayManually)
                        Nissensai.HideQrCode();
                }
                else if (_status == SystemStatus.Playing && value == SystemStatus.Idle)
                {
                    Logger.Log($"Game end.");
                    Instance.gameStopCallback.Invoke();
                    RunTask(GetNewGameToken());
                    if (!Instance.qrCodeDisplayManually)
                        Nissensai.ShowQrCode();
                }

                _status = value;
            }
        }

        internal static string BaseUrl { get; private set; }

        private IEnumerator Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                yield break;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            Logger.Level = logLevel;
            BaseUrl = useSSL ? "https://" : "http://";
            BaseUrl += server;
            Logger.Log($"Base Url: {BaseUrl}");
            Status = SystemStatus.Idle;
            _qrCodeAnm = panel.GetComponent<Animation>();
            playerIdInput.onSubmit.AddListener((value) => { StartCoroutine(SendStart(Int32.Parse(value))); });
            Nissensai.AddConsoleMethod("GetNewToken", GetNewToken);
            Nissensai.AddConsoleMethod("SendResult", ResultUploader.SendResult);
            Nissensai.AddConsoleMethod("ReloadPlayerInfo", ReloadPlayerInfo);
            Nissensai.AddConsoleMethod("SetStatus", SetStatus);
            yield return StartCoroutine(GetNewGameToken());
            ShowQrCode();
            StartCoroutine(MainLoop());
        }

        private static string SetStatus(string para)
        {
            try
            {
                SystemStatus status = (SystemStatus)Convert.ToInt32(para);
                Status = status;
                return "    Changing...";
            }
            catch (Exception e)
            {
                return "    Invalid parameters. SetStatus 0 / SetStatus 1";
            }
        }

        private static string GetNewToken()
        {
            Nissensai.GetNewToken();
            return "    Fetching...";
        }

        private static string ReloadPlayerInfo()
        {
            RunTask(CurrentPlayer.UpdatePlayerInfo());
            return "    Fetching...";
        }

        private IEnumerator SendStart(int playerId)
        {
            playerIdInput.DeactivateInputField();
            playerIdInput.enabled = false;

            var url = $"{BaseUrl}/api/game/start" +
                      $"?gameToken={GameToken}" +
                      $"&playerId={playerId}";
            var request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Logger.Warn(request.error);
                playerIdInput.enabled = true;
                playerIdInput.ActivateInputField();
                yield break;
            }

            JObject result = JObject.Parse(request.downloadHandler.text);
            if (result["state"].Value<string>() != "ok")
            {
                Logger.Warn(result["msg"].Value<string>());
                playerIdInput.enabled = true;
                playerIdInput.ActivateInputField();
                yield break;
            }

            playerIdInput.text = "";
            playerIdInput.enabled = true;
            playerIdInput.ActivateInputField();
        }

        internal static void ShowQrCode()
        {
            Instance.playerIdInput.text = "";
            _qrCodeAnm.Stop();
            _qrCodeAnm.Play("QrCodeShow");
        }

        internal static void HideQrCode()
        {
            _qrCodeAnm.Stop();
            _qrCodeAnm.Play("QrCodeHide");
        }

        internal static Coroutine RunTask(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }

        internal static IEnumerator GetNewGameToken()
        {
            _isGameTokenReady = false;
            int tryTime = 0;
            do
            {
                tryTime++;
                var request = UnityWebRequest.Get($"{BaseUrl}/api/game/token?password={Instance.password}");
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Logger.Warn(request.error);
                    continue;
                }

                JObject result = JObject.Parse(request.downloadHandler.text);
                if (result["state"].Value<string>() != "ok")
                {
                    Logger.Warn(result["msg"].Value<string>());
                    continue;
                }

                GameToken = result["token"].Value<string>();
                _isGameTokenReady = true;
                Logger.Log($"Fetch new token ({GameToken})");
                Instance.qrCodeImage.sprite = QRCodeUtil.CreateSprite($"{BaseUrl}/start?gameToken={GameToken}");
            } while (!_isGameTokenReady && tryTime < RetryTime);

            if (!_isGameTokenReady)
            {
                Logger.Error("Failed to fetch new token.");
            }
        }

        private IEnumerator UpdateGameStatus()
        {
            yield return new WaitUntil(() => _isGameTokenReady);
            var request =
                UnityWebRequest.Get(
                    $"{SystemStatusManager.BaseUrl}/api/game/status?gameToken={SystemStatusManager.GameToken}");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Logger.Warn(request.error);
                yield break;
            }

            JObject result = JObject.Parse(request.downloadHandler.text);
            if (result["state"].Value<string>() != "ok")
            {
                Logger.Warn(result["msg"].Value<string>());
                yield break;
            }

            int playerId = result["playerId"].Value<int>();
            CurrentPlayer = new Player();
            yield return StartCoroutine(CurrentPlayer.UpdatePlayerInfo(playerId));

            if (CurrentPlayer.IsReady)
            {
                Status = (SystemStatus)result["status"].Value<int>();
            }
        }

        private IEnumerator MainLoop()
        {
            while (true)
            {
                if (Status == SystemStatus.Idle)
                {
                    yield return StartCoroutine(UpdateGameStatus());
                }

                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}