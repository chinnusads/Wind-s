using UnityEngine;

namespace Nissensai2022.Console
{
    internal class ConsoleSwitch : MonoBehaviour
    {
        [SerializeField] GameObject console;
        [SerializeField] KeyCode key = KeyCode.F1;
        private static ConsoleSwitch Instance;

        private  void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            console.SetActive(false);
            console.transform.localScale = Vector3.one;
        }

        private void Update()
        {
            if (Input.GetKeyUp(key))
                console.SetActive(!console.activeSelf);
            else if (Input.GetKeyUp(KeyCode.Escape))
                console.SetActive(false);
            else
                return;
            ConsoleController.Instance.OnStart();
        }

        public static void ShowConsole()
        {
            Instance.console.SetActive(true);
        }
        
        public static void HideConsole()
        {
            Instance.console.SetActive(false);
        }
        
    }
}