using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugePosition : MonoBehaviour
{
	[SerializeField] private Vector2 offset;
	[SerializeField] private Transform follow;
	private RectTransform rectTransform;
	//public float aaa { get; private set; } = 0.5f;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}


	// Update is called once per frame
	void Update()
	{
		var gagePos = Camera.main.WorldToScreenPoint(follow.position);
		gagePos.x += offset.x;
		gagePos.y += offset.y;
		rectTransform.position = gagePos;
		//this.transform.position = new Vector3(this.transform.position.x, (54*(PlayerInfo.playerY)+70), 0f);
		//Debug.Log(this.transform.position);
	}
}
