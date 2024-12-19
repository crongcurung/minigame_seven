using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge02 : MonoBehaviour
{

	public int parentNum;


	void OnTriggerEnter2D(Collider2D collision)       // 작은 영역에 들어오는 경우
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.judge02 = true;
			FifthManager.noteLineInt = gameObject.GetComponentInParent<Flame>().flameNum;
		}
	}

	void OnTriggerExit2D(Collider2D collision)          // 작은 영역에서 나가는 경우
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.judge02 = false;
		}
	}
}
