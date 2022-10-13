using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Nissensai2022.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Nissensai2022.Runtime.Logger;


namespace Nissensai2022.Runtime
{
    public enum ResultRank
    {
        E = 0,
        D = 1,
        C = 2,
        B = 3,
        A = 4
    }
}

namespace Nissensai2022.Internal
{
    internal class ResultUploader
    {
        internal static Coroutine UploadResult(ResultRank rank)
        {
            return SystemStatusManager.RunTask(Upload(rank));
        }

        internal static Coroutine UploadResult(int playerId, ResultRank rank)
        {
            return SystemStatusManager.RunTask(Upload(playerId, rank));
        }

        private static IEnumerator Upload(ResultRank rank)
        {
            yield return SystemStatusManager.RunTask(Upload(SystemStatusManager.CurrentPlayer.Id, rank));
        }

        internal static string SendResult(string para)
        {
            try
            {
                string[] paras = para.Split(',');
                int playerId = Convert.ToInt32(paras[0]);
                ResultRank rank = (ResultRank)Convert.ToInt32(paras[1]);
                SystemStatusManager.RunTask(Upload(playerId, rank));
                return "    Sending...";
            }
            catch (Exception e)
            {
                return "    Invalid parameters. SendResult PlayerId,ResultRank";
            }
        }

        private static IEnumerator Upload(int playerId, ResultRank rank)
        {
            bool isOk = false;
            int retryCount = 3;
            Logger.Log($"Sending play result {playerId} {rank}");
            do
            {
                var url =
                    $"{SystemStatusManager.BaseUrl}/api/game/result" +
                    $"?gameToken={SystemStatusManager.GameToken}" +
                    $"&playerId={playerId}" +
                    $"&rank={(int)rank}";
                var request = UnityWebRequest.Get(url);
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

                Logger.Log($"Upload result ok.");
                isOk = true;
                SystemStatusManager.Status = SystemStatus.Idle;
            } while (!isOk && retryCount < SystemStatusManager.RetryTime);

            if (!isOk)
            {
                Logger.Error($"Failed to send play result({(int)rank}) for player({playerId})");
            }
        }
    }
}