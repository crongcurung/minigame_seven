using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge02 : MonoBehaviour
{

	public int parentNum;


	void OnTriggerEnter2D(Collider2D collision)       // ���� ������ ������ ���
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.judge02 = true;
			FifthManager.noteLineInt = gameObject.GetComponentInParent<Flame>().flameNum;
		}
	}

	void OnTriggerExit2D(Collider2D collision)          // ���� �������� ������ ���
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.judge02 = false;
		}
	}
}
