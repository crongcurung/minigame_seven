using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour    // 콜라이더 영역에 있는 흰색 포인트 지점
{
	void OnEnable()
	{
		ThirdManager.colliderCount++;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		ThirdManager.currentCount++;

		this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

	}

	void OnDisable()
	{
		this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}
}
