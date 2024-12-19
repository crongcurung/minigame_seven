using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHit : MonoBehaviour
{
	HitManager hitManager;

	void OnEnable()
	{
		hitManager = gameObject.transform.parent.GetComponent<HitManager>();
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("트리거");

		if (collision.name == "ChillGyo01" && gameObject.transform.parent.name == "ChillGyo07")
		{
			hitManager.hitCount++;
			return;
		}

		if (collision.name == "ChillGyo07" && gameObject.transform.parent.name == "ChillGyo01")
		{
			hitManager.hitCount++;
			return;
		}

		if (collision.name == "ChillGyo04" && gameObject.transform.parent.name == "ChillGyo05")
		{
			hitManager.hitCount++;
			return;
		}

		if (collision.name == "ChillGyo05" && gameObject.transform.parent.name == "ChillGyo04")
		{
			hitManager.hitCount++;
			return;
		}

		if (collision.name == gameObject.transform.parent.name)
		{
			hitManager.hitCount++;
			return;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("나감");

		if (collision.name == "ChillGyo01" && gameObject.transform.parent.name == "ChillGyo07")
		{
			hitManager.hitCount--;
			return;
		}

		if (collision.name == "ChillGyo07" && gameObject.transform.parent.name == "ChillGyo01")
		{
			hitManager.hitCount--;
			return;
		}

		if (collision.name == "ChillGyo04" && gameObject.transform.parent.name == "ChillGyo05")
		{
			hitManager.hitCount--;
			return;
		}

		if (collision.name == "ChillGyo05" && gameObject.transform.parent.name == "ChillGyo04")
		{
			hitManager.hitCount--;
			return;
		}

		if (collision.name == gameObject.transform.parent.name)
		{
			hitManager.hitCount--;
		}
	}
}
