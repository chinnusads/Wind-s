using System.Collections;
using System.Collections.Generic;
using Nissensai2022.A;
using UnityEngine;

internal class SampleNewCommandHandler : MonoBehaviour
{
    public void NewCommandHandler(Command command)
    {
        Nissensai2022.Runtime.Logger.Log($"New command event {command.PlayerId} {command.Cmd}");
    }
}
