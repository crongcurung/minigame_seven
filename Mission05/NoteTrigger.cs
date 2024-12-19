using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Note"))
		{
			FifthManager.isEnd = true;
			Destroy(collision.gameObject.transform.parent.gameObject);
		}
	}
}
