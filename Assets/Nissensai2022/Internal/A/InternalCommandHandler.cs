using System;
using Nissensai2022.A;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class InternalCommandHandler : MonoBehaviour
{
	public static InternalCommandHandler Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public static Coroutine RunTask(IEnumerator coroutine)
	{
		return Instance.StartCoroutine(coroutine);
	}
	

	public void NewCommandHandler(Command command)
	{
		try
		{
			switch (command.Cmd)
			{
				case CommandType.NewPlayer:
					StartCoroutine(PlayerList.NewPlayer(command.PlayerId));
					break;
				case CommandType.UpdatePlayer:
					StartCoroutine(PlayerList.Update(command.PlayerId));
					break;
				default:
					break;
			}
		}
		catch (Exception e)
		{
			Nissensai2022.Runtime.Logger.Warn(e.Message);
		}
		
	}
}
