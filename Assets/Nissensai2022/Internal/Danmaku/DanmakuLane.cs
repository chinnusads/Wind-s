using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nissensai2022.Danmaku
{
	internal class DanmakuLane
	{
		private GameObject _gameObject;
		private RectTransform _rectTransform;
		private List<Danmaku> _danmakus = new List<Danmaku>();

		internal bool HasSpace => _danmakus.TrueForAll(danmaku => !danmaku.IsInWaitingArea);

		internal void AddDanmaku(string content)
		{
			GameObject gameObject = new GameObject(content, typeof(RectTransform));
			gameObject.transform.SetParent(_rectTransform);
			RectTransform rectTransform = gameObject.transform as RectTransform;
			rectTransform.pivot = Vector2.up;
			rectTransform.anchorMin = new Vector2(0, 1);
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, DanmakuHandler.Instance.laneHeight);
			rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);


			Danmaku danmaku = new Danmaku(gameObject, content);
			_danmakus.Add(danmaku);
		}

		internal DanmakuLane(GameObject gameObject)
		{
			_gameObject = gameObject;
			_rectTransform = gameObject.transform as RectTransform;

			if (DanmakuHandler.Instance.debugMode)
				gameObject.AddComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		}

		internal void Update()
		{
			List<Danmaku> removeList = new List<Danmaku>();

			foreach (var danmaku in _danmakus)
			{
				if (danmaku.Update())
					continue;
				;
				removeList.Add(danmaku);
			}

			foreach (var danmaku in removeList)
			{
				UnityEngine.Object.Destroy(danmaku.GameObject);
				_danmakus.Remove(danmaku);
			}
		}
	}
}