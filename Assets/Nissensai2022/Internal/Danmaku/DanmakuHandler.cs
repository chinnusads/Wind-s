using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nissensai2022.Danmaku
{
    internal class DanmakuHandler : MonoBehaviour
    {
        [Header("レイアウト設定")] [SerializeField] internal int laneHeight = 32;
        [SerializeField] private int marginTop = 10;
        [SerializeField] private float heightPercentage = 1.0f;

        [Header("描画設定")] [SerializeField] internal int fontSize = 28;
        [SerializeField] internal Font font;
        [SerializeField] internal Color fontColor;
        [SerializeField] internal Color backgroundColor;

        [Header("弾幕設定")] [SerializeField] internal float speed = 200f;
        [SerializeField] internal bool debugMode = false;

        [Space(50)] [Header("サーバー通信設定")] [Space(10)] [SerializeField]
        internal string password = "********************************";

        [SerializeField] private string server = "nissensai.games";
        [SerializeField] private bool useSSL = true;
        [SerializeField] internal int retryTime = 3;
        [SerializeField] internal int timeout = 5;

        internal static DanmakuHandler Instance;

        private static List<DanmakuLane> _lanes = new List<DanmakuLane>();
        internal static int ScreenWidth { get; private set; }
        internal static int ScreenHeight { get; private set; }
        internal static float Speed => Instance.speed;

        internal static string BaseUrl;

        internal Coroutine RunTask(IEnumerator task)
        {
            return StartCoroutine(task);
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            BaseUrl = useSSL ? "https://" : "http://";
            BaseUrl += server;

            ScreenHeight = Screen.height;
            ScreenWidth = Screen.width;
            InitLanes();
        }

        private void InitLanes()
        {
            int count = (int)(ScreenHeight * heightPercentage - marginTop) / laneHeight;
            for (int i = 0; i < count; i++)
            {
                int y = marginTop + laneHeight * i;
                GameObject gameObject = new GameObject($"lane{i}", typeof(RectTransform));
                RectTransform rectTransform = gameObject.transform as RectTransform;
                rectTransform.SetParent(transform);
                rectTransform.pivot = Vector2.up;
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, y, laneHeight);
                rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
                rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
                DanmakuLane lane = new DanmakuLane(gameObject);
                _lanes.Add(lane);
            }
        }

        internal static void AddDanmaku(string content)
        {
            foreach (var lane in _lanes)
            {
                if (!lane.HasSpace)
                    continue;
                lane.AddDanmaku(content);
                break;
            }
        }

        private void Update()
        {
            foreach (var lane in _lanes)
            {
                lane.Update();
            }
        }
    }
}