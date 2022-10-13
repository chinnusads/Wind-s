using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Nissensai2022.Internal;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Nissensai2022.Runtime.Logger;

namespace Nissensai2022.Runtime
{
    public class Player
    {
        /// <summary>
        /// プレイヤーID
        /// </summary>
        public int Id { get; private set; } = 0;

        /// <summary>
        /// プレイヤーネーム
        /// </summary>
        public string Name { get; private set; } = "";

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Atk { get; private set; } = 0;

        /// <summary>
        /// 防御力
        /// </summary>
        public int Def { get; private set; } = 0;

        /// <summary>
        /// 素早さ
        /// </summary>
        public int Spd { get; private set; } = 0;

        /// <summary>
        /// 体力
        /// </summary>
        public int Vit { get; private set; } = 0;

        /// <summary>
        /// 回避力
        /// </summary>
        public int Avd { get; private set; } = 0;

        /// <summary>
        /// データ更新完了したらTrue。Falseだと値を使っちゃダメ！
        /// </summary>
        public bool IsReady { get; private set; } = false;

        internal Player()
        {
        }

        /// <summary>
        /// コンストラクタ関数
        /// </summary>
        /// <param name="playerId">プレイヤーID</param>
        public Player(int playerId)
        {
            Id = playerId;
            SystemStatusManager.RunTask(UpdatePlayerInfo());
        }

        /// <summary>
        /// プレイヤーの情報を更新する
        /// </summary>
        /// <returns>コルーチン</returns>
        public IEnumerator UpdatePlayerInfo()
        {
            yield return UpdatePlayerInfo(Id);
        }

        internal IEnumerator UpdatePlayerInfo(int playerId)
        {
            IsReady = false;
            Id = playerId;
            int retryCount = 0;
            do
            {
                var request =
                    UnityWebRequest.Get($"{SystemStatusManager.BaseUrl}/api/player/status?playerId={playerId}");
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

                Name = result["nickname"].Value<string>();
                Atk = result["atk"].Value<int>();
                Def = result["def"].Value<int>();
                Spd = result["spd"].Value<int>();
                Vit = result["vit"].Value<int>();
                Avd = result["avd"].Value<int>();
                IsReady = true;
                if (Id != 1)
                    Logger.Log($"Player({Name}): {Atk}, {Def}, {Spd}, {Vit}, {Avd}");
            } while (!IsReady && retryCount < SystemStatusManager.RetryTime);

            if (!IsReady)
            {
                Logger.Error($"Failed to get player({playerId}) info");
            }
        }
    }
}