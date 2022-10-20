using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Nissensai2022.ExceptionHandler
{
    public class ExceptionHandler : MonoBehaviour
    {
        [SerializeField] private bool isQuitWhenException = true;
        private string _logPath;
        private string _bugExePath;

#if !UNITY_EDITOR
        void Awake()
        {
            _logPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"))+"/log";
            _bugExePath = Application.dataPath;
            _bugExePath = _bugExePath.Substring(0, _bugExePath.LastIndexOf("/")) +
                          "/ExceptionHandler.exe";
            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
            Application.logMessageReceived += Handler;
            //Application.RegisterLogCallback( Handler );  
            
            Debug.Log(_bugExePath);

            DontDestroyOnLoad(gameObject);
        }

        void Handler(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Exception || type == LogType.Assert)
            {
                string logPath = _logPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".log";
                if (Directory.Exists(_logPath))
                {
                    File.AppendAllText(logPath, "[time] " + DateTime.Now.ToString() + "\r\n");
                    File.AppendAllText(logPath, "[type] " + type.ToString() + "\r\n\r\n");
                    File.AppendAllText(logPath, "[exception message]\r\n" + logString + "\r\n\r\n");
                    File.AppendAllText(logPath, "[stack trace]\r\n" + stackTrace + "\r\n");
                }

                if (File.Exists(_bugExePath))
                {
                    ProcessStartInfo pros = new ProcessStartInfo();
                    pros.FileName = _bugExePath;
                    pros.Arguments = "\"" + logPath + "\"";
                    Process pro = new Process();
                    pro.StartInfo = pros;
                    pro.Start();
                }

                if (isQuitWhenException)
                {
                    Application.Quit();
                }
            }
        }
#endif
    }
}