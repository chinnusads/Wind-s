using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Nissensai2022.Runtime.Logger;

namespace Nissensai2022.A
{
    internal class CommandList
    {
        private int _currentPointer = 0;
        private readonly ConcurrentQueue<Command> _queue = new ConcurrentQueue<Command>();

        internal bool HasNext => _queue.Count > 0;

        internal Command Next
        {
            get
            {
                _queue.TryDequeue(out var command);
                return command;
            }
        }

        internal IEnumerator Init()
        {
            var success = false;
            int tryTime = 0;
            do
            {
                tryTime++;
                var request =
                    UnityWebRequest.Get($"{CommandHandler.BaseUrl}/api/player/commandlistlastid");
                request.timeout = CommandHandler.Instance.timeout;
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
                    Logger.Warn(result["msg"].Value<string>());
                    continue;
                }
                int lastId=result["last"].Value<int>();
                
                Logger.Log($"Fetch command history list last id({lastId})");
                _currentPointer = lastId;
                success = true;
            } while (!success && tryTime < CommandHandler.Instance.retryTime);

            Loadding.LoaddingManager.Hide();
            if (!success)
            {
                Logger.Error("Failed to fetch command history list last id.");
            }
        }

        internal IEnumerator Update()
        {
            var success = false;
            int tryTime = 0;
            do
            {
                tryTime++;
                var request =
                    UnityWebRequest.Get($"{CommandHandler.BaseUrl}/api/player/commandlist?after={_currentPointer}");
                request.timeout = CommandHandler.Instance.timeout;
                //Loadding.LoaddingManager.Show();//
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

                List<JToken> list = result["list"].ToList();
                success = true;
                int count = list.Count;
                if (count > 0)
                    Logger.Log($"Fetch new command list ({count})");
                foreach (var token in list)
                {
                    int playerId = token["playerId"].Value<int>();
                    int commandId = token["newCommand"].Value<int>();
                    _queue.Enqueue(new Command(playerId, commandId));
                    _currentPointer = token["id"].Value<int>();
                }
            } while (!success && tryTime < CommandHandler.Instance.retryTime);

            //Loadding.LoaddingManager.Hide();
            if (!success)
            {
                Logger.Error("Failed to fetch command list.");
            }
        }
    }
}