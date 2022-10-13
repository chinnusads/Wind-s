using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nissensai2022.Runtime
{
    internal enum LogLevel
    {
        Debug,
        Warn,
        Error,
        None
    }

    public class Logger
    {
        internal static LogLevel Level = LogLevel.Debug;

        public static void Log(object msg)
        {
            switch (Level)
            {
                case LogLevel.Debug:
                    Debug.Log($"[Nissensai2022] {msg}");
                    Console.Logger.Log($"[Nissensai2022] {msg}");
                    break;
                case LogLevel.Warn:
                case LogLevel.Error:
                case LogLevel.None:
                    break;
            }
        }

        public static void Warn(object msg)
        {
            switch (Level)
            {
                case LogLevel.Debug:
                case LogLevel.Warn:
                    Debug.LogWarning($"[Nissensai2022] {msg}");
                    Console.Logger.Warn($"[Nissensai2022] {msg}");
                    break;
                case LogLevel.Error:
                case LogLevel.None:
                    break;
            }
        }

        public static void Error(object msg)
        {
            switch (Level)
            {
                case LogLevel.Debug:
                case LogLevel.Warn:
                case LogLevel.Error:
                    Debug.LogError($"[Nissensai2022] {msg}");
                    Console.Logger.Error($"[Nissensai2022] {msg}");
                    Console.ConsoleSwitch.ShowConsole();
                    break;
                case LogLevel.None:
                    break;
            }
        }
    }
}