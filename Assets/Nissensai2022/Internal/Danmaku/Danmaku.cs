using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Logger = Nissensai2022.Runtime.Logger;

namespace Nissensai2022.Danmaku
{
	public class Danmaku
	{
		internal GameObject GameObject { get; private set; }
		private RectTransform _rectTransform;

		internal bool IsInWaitingArea => _rectTransform.anchoredPosition.x + _rectTransform.sizeDelta.x >= 0;

		internal bool IsAlive => _rectTransform.anchoredPosition.x + _rectTransform.sizeDelta.x >=
								 -DanmakuHandler.ScreenWidth;

		public static Coroutine AddDanmaku(string danmaku, bool sendToServer = false, int playerId = 0)
		{
			return DanmakuHandler.Instance.RunTask(Add(danmaku, sendToServer, playerId));
		}

		public static Coroutine SendLog(string danmaku, int playerId = 0)
		{
			return DanmakuHandler.Instance.RunTask(SendToServer(danmaku, playerId));
		}

		private static IEnumerator SendToServer(string danmaku, int playerId)
		{
			// todo batch log
			var success = false;
			int tryTime = 0;
			do
			{
				tryTime++;
				var request =
					UnityWebRequest.Get(
						$"{DanmakuHandler.BaseUrl}/api/game/log?playerId={playerId}&password={DanmakuHandler.Instance.password}&content={danmaku}");
				request.timeout = DanmakuHandler.Instance.timeout;
				Loadding.LoaddingManager.Show();
				yield return request.SendWebRequest();
				if (request.result != UnityWebRequest.Result.Success)
				{
					Logger.Warn(request.error);
					continue;
				}

				JObject result = JObject.Parse(request.downloadHandler.text);
				if (result["state"].Value<string>() != "ok")
				{
					Logger.Warn(result["msg"].Value<string>());
					continue;
				}

				success = true;
			} while (!success && tryTime < DanmakuHandler.Instance.retryTime);

			Loadding.LoaddingManager.Hide();
			if (!success)
			{
				Logger.Warn("Failed to send danmaku to server.");
			}
		}

		private static IEnumerator Add(string danmaku, bool sendToServer = false, int playerId = 0)
		{
			DanmakuHandler.AddDanmaku(danmaku);
			if (!sendToServer)
				yield break;
			yield return DanmakuHandler.Instance.RunTask(SendToServer(danmaku, playerId));

		}

		internal Danmaku(GameObject gameObject, string content)
		{
			GameObject = gameObject;
			_rectTransform = gameObject.transform as RectTransform;


			DanmakuHandler danmakuHandler = DanmakuHandler.Instance;

			Text text = gameObject.AddComponent<Text>();
			text.font = danmakuHandler.font;
			text.fontSize = danmakuHandler.fontSize;
			text.color = danmakuHandler.fontColor;
			text.text = content;

			ContentSizeFitter contentSizeFitter = GameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

			Color backgroundColor = danmakuHandler.backgroundColor;
			if (backgroundColor.a > 0)
			{
				GameObject imageObject = new GameObject("Background", typeof(RectTransform));
				imageObject.transform.parent = _rectTransform;
				RectTransform imageRectTransform = imageObject.transform as RectTransform;
				imageRectTransform.anchorMin = new Vector2(0, 0);
				imageRectTransform.anchorMax = new Vector2(1, 1);
				imageRectTransform.offsetMin = new Vector2(0, 0);
				imageRectTransform.offsetMax = new Vector2(0, 0);
				Image image = imageObject.AddComponent<Image>();
				image.color = danmakuHandler.backgroundColor;
			}
		}

		internal bool Update()
		{
			if (IsInWaitingArea) ;
			var pos = _rectTransform.anchoredPosition;
			pos.x -= DanmakuHandler.Speed * Time.deltaTime;
			_rectTransform.anchoredPosition = pos;
			return IsAlive;
		}
	}
}