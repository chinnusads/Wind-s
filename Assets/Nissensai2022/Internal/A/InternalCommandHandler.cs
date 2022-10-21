using Nissensai2022.A;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class InternalCommandHandler : MonoBehaviour
{
	public void NewCommandHandler(Command command)
	{
		switch (command.Cmd)
		{
			case CommandType.NewPlayer:
				PlayerList.NewPlayer(command.PlayerId);
				break;
			case CommandType.UpdatePlayer:
				PlayerList.Update(command.PlayerId);
				break;
			default:
				break;
		}
	}
}
