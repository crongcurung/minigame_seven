using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour    // �ݶ��̴� ������ �ִ� ��� ����Ʈ ����
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
