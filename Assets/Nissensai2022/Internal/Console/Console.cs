using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nissensai2022.Console
{
    internal static class Console
    {
        #region core

        private static Dictionary<string, MethodInfo> commands = new Dictionary<string, MethodInfo>();

        private static int position = -1;
        private static List<string> consoleHistory = new List<string>();

        public static void Init()
        {
            Type finfo = typeof(Console);
            foreach (MethodInfo m in finfo.GetMethods(BindingFlags.Static | BindingFlags.NonPublic))
            {
                ConsoleMethod cmd = m.GetCustomAttribute<ConsoleMethod>(false);
                if (cmd != null)
                    commands.Add(cmd.command, m);
            }
        }

        public static void AddMethod(string command, MethodInfo method)
        {
            commands.Add(command, method);
        }

        public static void AddMethod(string command, Func<string> method)
        {
            commands.Add(command, method.Method);
        }
        
        public static void AddMethod(string command, Func<string,string> method)
        {
            commands.Add(command, method.Method);
        }

        public static string Execute(string input)
        {
            List<string> args = new List<string>(input.Split(' '));
            consoleHistory.Add(input);
            position = consoleHistory.Count;
            string output = null;
            if (commands.ContainsKey(args[0]))
            {
                object[] paras = new object[args.Count - 1];
                for (int i = 0; i < paras.Length; ++i)
                    paras[i] = args[i + 1];
                try
                {
                    output = commands[args[0]].Invoke(null, paras).ToString();
                }
                catch (TargetParameterCountException e)
                {
                    output = "Wrong parameter count.";
                }
                catch (ArgumentException e)
                {
                    output = "Wrong parameter type.";
                }
            }
            else
                output = "No such command.";

            return output;
        }

        public static string Last()
        {
            if (position == -1)
                return null;
            position -= 1;
            if (position < 0)
                position = 0;
            return consoleHistory[position];
        }

        public static string Next()
        {
            if (position == -1)
                return null;
            position += 1;
            if (position >= consoleHistory.Count)
                position = consoleHistory.Count - 1;
            return consoleHistory[position];
        }

        public static string Remind(string input)
        {
            input = input.Substring(0, 1).ToUpper() + input.Substring(1);
            List<string> reminds = new List<string>();
            string all = "";
            foreach (string cmd in commands.Keys)
            {
                if (cmd.StartsWith(input))
                {
                    reminds.Add(cmd);
                    all += cmd + "\t";
                }
            }

            if (reminds.Count == 1)
                return reminds[0];
            else if (reminds.Count == 0)
                Logger.Remind("No such command.");
            else
                Logger.Remind(all);
            return "";
        }

        #endregion

        #region universal method

        [ConsoleMethod("Help")]
        private static string Help()
        {
            string output = "    ";
            foreach (string cmd in commands.Keys)
                output += cmd + "\n    ";
            return output;
        }

        [ConsoleMethod("Cls")]
        private static string Clear()
        {
            position = -1;
            consoleHistory.Clear();
            return "cls";
        }

        [ConsoleMethod("SetResolution")]
        private static string SetResolution(string width, string height, string fullScreen)
        {
            try
            {
                int w = Convert.ToInt32(width);
                int h = Convert.ToInt32(height);
                bool f = Convert.ToBoolean(fullScreen);
                Screen.SetResolution(w, h, f);
                return "    Done.";
            }
            catch (FormatException e)
            {
                return "    Wrong parameter type.";
            }
        }

        [ConsoleMethod("ForceQuit")]
        private static string ForceQuit()
        {
            Application.Quit();
            return "    Quitting...";
        }

        [ConsoleMethod("LoadScene")]
        private static string LoadScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
            return "    Loading...";
        }

        #endregion
    }
}