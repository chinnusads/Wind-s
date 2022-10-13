using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nissensai2022.Console
{
    internal sealed class ConsoleMethod : Attribute
    {
        public string command { get; private set; }

        public ConsoleMethod(string command)
        {
            this.command = command;
        }
    }
}