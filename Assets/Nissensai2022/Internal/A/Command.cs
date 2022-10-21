using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nissensai2022.A
{
    public class Command
    {
        public int PlayerId { get; private set; }
        public CommandType Cmd { get; private set; }

        public Command(int playerId, int commandId)
        {
            PlayerId = playerId;
            Cmd = (CommandType)commandId;
        }
    }
}