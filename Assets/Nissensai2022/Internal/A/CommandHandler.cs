using System.Collections;
using System.Collections.Generic;
using Nissensai2022.Runtime;
using UnityEngine;
using UnityEngine.Events;
using Logger = Nissensai2022.Runtime.Logger;

namespace Nissensai2022.A
{
	internal class CommandHandler : MonoBehaviour
	{
		internal static CommandHandler Instance { get; private set; } = null;
		internal static string BaseUrl { get; private set; }

		[Header("基本設定")]
		[Space(10)]
		[SerializeField]
		internal string password = "********************************";

		[Space(10)][SerializeField] private UnityEvent<Command> newCommandHandler;
		[SerializeField] internal UnityEvent<Player> newPlayerHandler;
		[SerializeField] internal UnityEvent<Player> playerParamaterChangedHandler;

		[Space(50)]
		[Header("サーバー通信設定")]
		[Space(10)]
		[SerializeField]
		private string server = "nissensai.games";

		[SerializeField] private bool useSSL = true;
		[SerializeField] private float waitTime = 1f;
		[SerializeField] internal int retryTime = 3;
		[SerializeField] internal int timeout = 5;

		private static CommandList _commandToHandleList = new CommandList();
		private static Dictionary<int, CommandType> _commandDic = new Dictionary<int, CommandType>();

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

			BaseUrl = useSSL ? "https://" : "http://";
			BaseUrl += server;

			newCommandHandler.AddListener(command =>
			{
				int key = command.PlayerId;
				if (_commandDic.ContainsKey(key))
					_commandDic[key] = command.Cmd;
				else
					_commandDic.Add(key, command.Cmd);
			});

			Nissensai.AddConsoleMethod("ListCommand", ListCommand);
			Nissensai.AddConsoleMethod("ListPlayer", PlayerList.ListPlayer);

			StartCoroutine(MainLoop());
		}

		private static string ListCommand()
		{
			foreach (var keypair in _commandDic)
			{
				Logger.Log($"{keypair.Key} : {keypair.Value}");
			}

			return "    Listing Over.";
		}

		private IEnumerator MainLoop()
		{
			yield return _commandToHandleList.Init();
			yield return PlayerList.FetchAll();
			var players = PlayerList.Players;
			foreach (var player in players)
			{
				_commandDic.Add(player.Id, (CommandType)player.Cmd);
				newPlayerHandler.Invoke(player);
			}

			while (true)
			{
				yield return _commandToHandleList.Update();
				while (_commandToHandleList.HasNext)
				{
					newCommandHandler.Invoke(_commandToHandleList.Next);
				}

				yield return new WaitForSeconds(waitTime);
			}
		}
	}
}