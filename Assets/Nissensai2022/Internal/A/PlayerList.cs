using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Nissensai2022.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Nissensai2022.Runtime.Logger;

namespace Nissensai2022.A
{
	public class PlayerList
	{
		// todo PlayerList
		private static Dictionary<int, Player> _dictionary = new Dictionary<int, Player>();

		public static List<Player> Players => _dictionary.Values.ToList();

		internal static void NewPlayer(int playerId)
		{
			if (_dictionary.ContainsKey(playerId))
				return;
			_dictionary.Add(playerId, new Player(playerId));
			CommandHandler.Instance.newPlayerHandler.Invoke(PlayerList.GetPlayerInfo(playerId));
		}

		public static Player GetPlayerInfo(int playerId)
		{
			if (!_dictionary.ContainsKey(playerId))
				return null;
			Player player = _dictionary[playerId];
			while (!player.IsReady)
			{
				player = _dictionary[playerId];
			}
			return player;
		}

		public static IEnumerator FetchAll()
		{
			var success = false;
			int tryTime = 0;
			do
			{
				tryTime++;
				var request =
					UnityWebRequest.Get($"{CommandHandler.BaseUrl}/api/player/all");
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

				List<JToken> list = result["list"].ToList();
				success = true;
				int count = list.Count;
				if (count > 0)
					Logger.Log($"Fetch all player list ({count})");
				foreach (var token in list)
				{
					int playerId = token["id"].Value<int>();
					string nickname = token["nickname"].Value<string>();
					int atk = token["ATK"].Value<int>();
					int def = token["DEF"].Value<int>();
					int spd = token["SPD"].Value<int>();
					int vit = token["VIT"].Value<int>();
					int avd = token["AVD"].Value<int>();
					int cmd = token["command"].Value<int>();
					_dictionary.Add(playerId, new Player(playerId, nickname, atk, def, spd, vit, avd, cmd));
				}
			} while (!success && tryTime < CommandHandler.Instance.retryTime);

			Loadding.LoaddingManager.Hide();
			if (!success)
			{
				Logger.Error("Failed to fetch player list.");
			}
		}

		public static void Update(int playerId)
		{
			if (_dictionary.ContainsKey(playerId))
				_dictionary[playerId].UpdatePlayerInfo();
			else
				_dictionary.Add(playerId, new Player(playerId));

			CommandHandler.Instance.playerParamaterChangedHandler.Invoke(PlayerList.GetPlayerInfo(playerId));
		}

		internal static string ListPlayer()
		{
			foreach (var player in _dictionary.Values)
			{
				Logger.Log(player.Print());
			}

			return "    Listing Over.";
		}
	}
}