using System;

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