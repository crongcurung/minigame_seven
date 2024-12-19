using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public int flameNum;

	void OnEnable()
	{
		gameObject.transform.GetChild(0).gameObject.SetActive(true);     // 자식 함수를 켜두긴 해둬야함
		gameObject.transform.GetChild(1).gameObject.SetActive(true);
	}

	void OnDisable()
	{
		gameObject.transform.GetChild(2).gameObject.SetActive(false);  // 게임이 끝날 때 초기화하자!
		gameObject.transform.GetChild(3).gameObject.SetActive(false);

	}

}
