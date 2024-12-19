using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class DalgonaVar
{
	public string dalgonaText = null;
	public int dalgonaLevel = 0;
}
[Serializable]
public class DalgonaData
{
	public List<DalgonaVar> dalgonaData = new List<DalgonaVar>();
}

public class ThirdMission : MonoBehaviour
{
	DalgonaData data;

	public TextMeshProUGUI expl_Text;

	public GameObject dalgonaPanel;   // �ް� �̹������� ��� �ִ� �г�
	public GameObject completePanel;  // ������ �ް� �̹������� ��� �ִ� �г�
	public GameObject linePanel;      // �������� ��� �ִ� �г�
	public Slider slider;
	public TextMeshProUGUI dalgonaText;  // �ް� ���þ�
	int randDalgona;

	GameObject currentDalgona;     // ���õ� �ް� �̹����� ���� ������Ʈ
	GameObject currentComplete;     // �ϼ��� �ް� �̹����� ���� ������Ʈ

	bool isEnd;


	void OnEnable()
	{
		if (DataManager.instance.nowPlayer.KorOrEngBool == true)     // �ѱ���..
		{
			expl_Text.text = "�� ����, �ް��� �ϼ��� �ּ���.";

			TextAsset textAsset = Resources.Load<TextAsset>($"Data/DalgonaData");      // JSON ����
			data = JsonUtility.FromJson<DalgonaData>(textAsset.text);
		}
		else         // ����..
		{
			expl_Text.text = "Make dalgona at once.";

			TextAsset textAsset = Resources.Load<TextAsset>($"Data/DalgonaData_En");      // JSON ����
			data = JsonUtility.FromJson<DalgonaData>(textAsset.text);
		}


		

		ThirdManager.colliderCount = 0;
		ThirdManager.currentCount = 0;
		ThirdManager.currentTouchLine = false;
		ThirdManager.touchFail = false;
		ThirdManager.touchSuccess = false;
		ThirdManager.isEnd = false;

		isEnd = false;

		randDalgona = UnityEngine.Random.Range(0, dalgonaPanel.transform.childCount);

		currentDalgona = dalgonaPanel.transform.GetChild(randDalgona).gameObject;
		currentDalgona.SetActive(true);

		dalgonaText.gameObject.SetActive(true);

		if (DataManager.instance.nowPlayer.KorOrEngBool == true)    // �ѱ���..
		{
			dalgonaText.text = "���þ� : " + data.dalgonaData[randDalgona].dalgonaText;
		}
		else          // ����...
		{
			dalgonaText.text = "Word : " + data.dalgonaData[randDalgona].dalgonaText;
		}
		

		if (data.dalgonaData[randDalgona].dalgonaLevel == 0)  // ��(25��)
		{
			slider.maxValue = 25.0f;
			slider.value = 25.0f;
		}
		else if (data.dalgonaData[randDalgona].dalgonaLevel == 1)  // ��(60��)
		{
			slider.maxValue = 60.0f;
			slider.value = 60.0f;
		}
		else   // ��(100��)
		{
			slider.maxValue = 100.0f;
			slider.value = 100.0f;
		}
	}

	void Update()
	{
		if (ThirdManager.touchSuccess == true && isEnd == false)    // �̼� ���� ��
		{
			isEnd = true;
			Debug.Log("�̼� ����");
			ThirdManager.isEnd = true;
			MissionManager.instance.succeseCount++;

			AudioManager.instance.LoopNot_Effect();
			AudioManager.instance.Effect_Null();
			currentComplete = completePanel.transform.GetChild(randDalgona).gameObject;

			currentDalgona.SetActive(false);
			currentComplete.SetActive(true);


			MissionContinue();

			return;
		}

		if (ThirdManager.touchFail == true && ThirdManager.isEnd == false)  // �̼� ���� ��
		{
			ThirdManager.isEnd = true;
			currentDalgona.SetActive(false);
			MissionManager.instance.failCount++;


			MissionContinue();
		}

		if (ThirdManager.isEnd == false)               
		{
			if (slider.value > 0)
			{
				slider.value -= Time.deltaTime;
			}
			else
			{
				AudioManager.instance.Mission_01_Fail_Effect();       // �ð� ���� ȿ����
				ThirdManager.isEnd = true;
				slider.value = 0;
				MissionManager.instance.failCount++;
				MissionContinue();
			}
		}
	}

	void OnDisable()
	{
		if (currentDalgona != null)
		{
			currentDalgona.SetActive(false);
		}

		if (currentComplete != null)
		{
			currentComplete.SetActive(false);
		}

		if (linePanel.transform.childCount > 2)
		{
			for (int i = 0; i < linePanel.transform.childCount; i++)
			{
				if (i == 0 || i == 1)
				{
					continue;
				}

				Destroy(linePanel.transform.GetChild(i).gameObject);
			}
		}

		dalgonaText.gameObject.SetActive(false);
	}



	void MissionContinue()        // �� �̼��� ������ �� ó��
	{
		StartCoroutine(EndGameWait());
	}

	public IEnumerator EndGameWait()
	{
		yield return new WaitForSeconds(3.0f);

		if (MissionManager.instance.endMission == false)
		{
			MissionManager.instance.MissionContinue();
		}
		else
		{
			MissionManager.instance.FinalResult();
		}
	}

}
