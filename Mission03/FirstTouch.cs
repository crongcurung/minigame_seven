using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTouch : MonoBehaviour
{
	Vector2 firstPosition;


	void OnEnable()
	{
		firstPosition = this.gameObject.transform.position;
		this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
	}


	void OnDisable()
	{
		this.gameObject.transform.position = firstPosition;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Goal") && ThirdManager.colliderCount == ThirdManager.currentCount)
		{
			Debug.Log("터치 석세스1");
			ThirdManager.touchSuccess = true;
		}
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Goal") && ThirdManager.colliderCount == ThirdManager.currentCount)
		{
			Debug.Log("터치 석세스2");
			this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
			ThirdManager.touchSuccess = true;
		}
	}
}
