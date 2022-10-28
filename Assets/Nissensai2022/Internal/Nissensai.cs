using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using Nissensai2022.Internal;
using UnityEngine.Networking;

namespace Nissensai2022.Runtime
{
    public class Nissensai
    {
        /// <summary>
        /// 現在のプレイヤー情報
        /// </summary>
        public static Player CurrentPlayer => SystemStatusManager.CurrentPlayer;

        /// <summary>
        /// ゲームのリザルト(5段階評価)をサーバーに送信する。
        /// </summary>
        /// <param name="rank">5段階評価。ResultRank.AやResultRank.Eのように指定する。</param>
        public static void SendResult(ResultRank rank)
        {
            ResultUploader.UploadResult(rank);
        }

        /// <summary>
        /// 途中でゲームを中止した場合はこれを使ってサーバーの状態を更新してください。これを使わないとQRコードが表示されない。
        /// </summary>
        public static void GiveUp()
        {
            ResultUploader.UploadResult(ResultRank.E);
        }

        /// <summary>
        /// 手動でQRコードを表示する。
        /// </summary>
        public static void ShowQrCode()
        {
            SystemStatusManager.ShowQrCode();
        }

        /// <summary>
        /// 手動でQRコードを非表示する。
        /// </summary>
        public static void HideQrCode()
        {
            SystemStatusManager.HideQrCode();
        }

        /// <summary>
        /// 現在のプレイヤーの名前を取得
        /// </summary>
        /// <returns>string 現在のプレイヤーの名前</returns>
        public static string GetPlayerName()
        {
            return SystemStatusManager.CurrentPlayer.Name;
        }

        /// <summary>
        /// 新しいゲームトークンを取得する
        /// </summary>
        public static void GetNewToken()
        {
            SystemStatusManager.RunTask(SystemStatusManager.GetNewGameToken());
        }

        /// <summary>
        /// コンソールコマンドを新規追加する。
        /// </summary>
        /// <param name="command">コマンドの文字列(なんと打てば実行されるか)</param>
        /// <param name="method">文字列を返すメソッド。例えば：string Method(){}</param>
        public static void AddConsoleMethod(string command, Func<string> method)
        {
            Nissensai2022.Console.Console.AddMethod(command, method);
        }
        
        /// <summary>
        /// コンソールコマンドを新規追加する。
        /// </summary>
        /// <param name="command">コマンドの文字列(なんと打てば実行されるか)</param>
        /// <param name="method">例えば：string Method(string paras){}</param>
        public static void AddConsoleMethod(string command, Func<string,string> method)
        {
            Nissensai2022.Console.Console.AddMethod(command, method);
        }

        public static void ChangeCommand(int playerId, int commandId)
        {
            SystemStatusManager.RunTask(ChangeCommandCoroutine(playerId, commandId));
        }

        private static IEnumerator ChangeCommandCoroutine(int playerId, int commandId)
        {
            bool isOk = false;
            int retryCount = 3;
            Logger.Log($"Sending command change {playerId} {commandId}");
            do
            {
                var url =
                    $"{SystemStatusManager.BaseUrl}/api/player/command" +
                    $"?playerId={playerId}" +
                    $"&command={commandId}";
                var request = UnityWebRequest.Get(url);
                request.timeout = SystemStatusManager.Instance.timeout;
                Loadding.LoaddingManager.Show();
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Logger.Warn(request.error);
                    continue;
                }

                JObject result = JObject.Parse(request.downloadHandler.text);
                if (result["state"].Value<string>() != "ok")
                {
                    Logger.Error(result["msg"].Value<string>());
                    continue;
                }

                Logger.Log($"Command change ok.");
                isOk = true;
            } while (!isOk && retryCount < SystemStatusManager.RetryTime);

            Loadding.LoaddingManager.Hide();
            if (!isOk)
            {
                Logger.Error($"Failed to send command change({commandId}) for player({playerId})");
            }
        }
        
    }
}