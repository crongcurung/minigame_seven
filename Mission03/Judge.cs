using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
	public GameObject firstTouch;
	Vector2 firstPosition;

	bool isFirst = false;

	void OnEnable()
	{
		firstPosition = this.gameObject.transform.position;
	}

	void OnDisable()
	{
		isFirst = false;
		this.gameObject.transform.position = firstPosition;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Note"))
		{
			ThirdManager.currentTouchLine = true;
			firstTouch.transform.position = this.gameObject.transform.position;

		}
	}

	void OnTriggerExit2D(Collider2D collision)       // 빠져나왔을때?
	{
		if (collision.CompareTag("Note"))
		{
			if (ThirdManager.touchSuccess == false && ThirdManager.isEnd == false)
			{
				AudioManager.instance.LoopNot_Effect();
				AudioManager.instance.Mission_03_Break_Effect();

				ThirdManager.touchFail = true;
				Debug.Log("나가 버림");
			}
			return;
		}
	}

	

	void OnTriggerStay2D(Collider2D collision)        // 그릴때?
	{
		if (collision.CompareTag("Note"))
		{
			if (isFirst == true)
			{
				return;
			}

			isFirst = true;
			AudioManager.instance.Loop_Effect();
			AudioManager.instance.Mission_03_Pen_Effect();
		}
	}
}
