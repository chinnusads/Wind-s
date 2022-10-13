using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace Nissensai2022.Console
{
    internal class ConsoleController : MonoBehaviour
    {
        [SerializeField] InputField inputField;
        [SerializeField] Text ouputText;
        [SerializeField] Scrollbar scrollbar;
        public static ConsoleController Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            Cls();
            Console.Init();
        }

        private void Cls()
        {
            if (ouputText == null)
                return;
            ouputText.text =
                "\n\n<color=#ffc0cbff><b>----------------------------------------------------------------------------\n" +
                "<size=48>" + GameInfo.GAME_NAME + "</size>\n\n" +
                "Version: " + GameInfo.GAME_VERSION + "\n" +
                "Author: " + GameInfo.AUTHOR + "\n" +
                "----------------------------------------------------------------------------\n" +
                ">>Debug Console\n" +
                ">>Type \"Help\" to get command list.\n" +
                "------------------------------------------------\n" +
                /*">>\n" +
                ">>\n" +
                ">>\n" +
                ">>\n" +
                ">>\n" +
                ">>\n" +
                ">>\n" +*/
                "\n</b></color>";
        }

        internal void OnStart()
        {
            inputField.ActivateInputField();
            inputField.text = "";
        }

        private IEnumerator ScrollToBottom()
        {
            yield return new WaitUntil(() => { return scrollbar.size != 1; });
            yield return new WaitForEndOfFrame();
            scrollbar.value = 0f;
        }


        internal void Error(object obj)
        {
            ouputText.text += "<color=#ff3333><b>>>" + obj.ToString() + "</b></color>\n\n";
            if (gameObject.activeSelf)
                StartCoroutine(ScrollToBottom());
        }

        internal void Warn(object obj)
        {
            ouputText.text += "<color=#ffcc00><b>>>" + obj.ToString() + "</b></color>\n\n";
            if (gameObject.activeSelf)
                StartCoroutine(ScrollToBottom());
        }

        internal void Log(object obj)
        {
            ouputText.text += "<color=#999999><b>>>" + obj.ToString() + "</b></color>\n\n";
            if (gameObject.activeSelf)
                StartCoroutine(ScrollToBottom());
        }

        internal void Remind(object obj)
        {
            ouputText.text += "<color=#fefffeff><b>>>" + obj.ToString() + "</b></color>\n\n";
            if (gameObject.activeSelf)
                StartCoroutine(ScrollToBottom());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string input = inputField.text;
                if (!input.Equals(""))
                {
                    ouputText.text += "<color=#ffc0cbff><b>>>" + input + "</b></color>\n";
                    string output = Console.Execute(input);
                    if (output != null)
                    {
                        if (output.Equals("cls"))
                            Cls();
                        else
                            ouputText.text += "<color=#fefffeff><b>" + output + "</b></color>\n\n";
                    }

                    inputField.text = "";
                    StartCoroutine(ScrollToBottom());
                    inputField.ActivateInputField();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                inputField.text = Console.Last();
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                inputField.text = Console.Next();
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                string cmd = Console.Remind(inputField.text);
                if (!string.IsNullOrEmpty(cmd))
                {
                    inputField.text = cmd;
                    inputField.MoveTextEnd(false);
                }
            }
        }
    }
}