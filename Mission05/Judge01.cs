using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge01 : MonoBehaviour
{

	public int parentNum;


	void OnTriggerEnter2D(Collider2D collision)   // ū ������ ������ ���
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.judge01 = true;
			FifthManager.flameBool = true;
			FifthManager.noteLineInt = gameObject.GetComponentInParent<Flame>().flameNum;
			FifthManager.currentFlameObjectName = gameObject.transform.parent.name;
		}
	}

	void OnTriggerExit2D(Collider2D collision)    // ū �������� ������ ���
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.judge01 = false;
			FifthManager.noteLineInt = -1;
			FifthManager.currentFlameObjectName = "";

			if (FifthManager.flameBool == true)
			{
				gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);
				FifthManager.judgeInt += 2;
			}
		}
	}
}
