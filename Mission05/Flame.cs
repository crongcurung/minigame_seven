using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public int flameNum;

	void OnEnable()
	{
		gameObject.transform.GetChild(0).gameObject.SetActive(true);     // �ڽ� �Լ��� �ѵα� �ص־���
		gameObject.transform.GetChild(1).gameObject.SetActive(true);
	}

	void OnDisable()
	{
		gameObject.transform.GetChild(2).gameObject.SetActive(false);  // ������ ���� �� �ʱ�ȭ����!
		gameObject.transform.GetChild(3).gameObject.SetActive(false);

	}

}
