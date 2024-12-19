using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitManager : MonoBehaviour
{

    public int hitCount = 0;
	public int childNum = 0;   // 자식의 개수가 3개인지, 4개인지 판단

	void OnEnable()
	{
		childNum = gameObject.transform.childCount;
	}

	void OnDisable()
	{
		this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.name == "ChillGyo04" && gameObject.name == "ChillGyo05")
		{
			if (hitCount == childNum)
			{
				AudioManager.instance.Mission_06_Success();          // 성공 효과음

				collision.gameObject.transform.position = this.gameObject.transform.position;
				this.gameObject.GetComponent<Image>().sprite = collision.gameObject.GetComponent<Image>().sprite;
				this.gameObject.GetComponent<Image>().color = Color.white;
				collision.gameObject.SetActive(false);
				this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				SixthManager.currentCount++;
			}
		}

		if (collision.name == "ChillGyo05" && gameObject.name == "ChillGyo04")
		{
			if (hitCount == childNum)
			{
				AudioManager.instance.Mission_06_Success();          // 성공 효과음

				collision.gameObject.transform.position = this.gameObject.transform.position;
				this.gameObject.GetComponent<Image>().sprite = collision.gameObject.GetComponent<Image>().sprite;
				this.gameObject.GetComponent<Image>().color = Color.white;
				collision.gameObject.SetActive(false);
				this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				SixthManager.currentCount++;
			}
		}

		if (collision.name == gameObject.name)
		{
			if (hitCount == childNum)
			{
				AudioManager.instance.Mission_06_Success();          // 성공 효과음

				collision.gameObject.transform.position = this.gameObject.transform.position;
				this.gameObject.GetComponent<Image>().color = Color.white;
				collision.gameObject.SetActive(false);
				this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				SixthManager.currentCount++;
			}
		}
	}
}
