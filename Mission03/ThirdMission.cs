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

	public GameObject dalgonaPanel;   // 달고나 이미지들을 담고 있는 패널
	public GameObject completePanel;  // 성공한 달고나 이미지들으 담고 있는 패널
	public GameObject linePanel;      // 빨간선을 담고 있는 패널
	public Slider slider;
	public TextMeshProUGUI dalgonaText;  // 달고나 제시어
	int randDalgona;

	GameObject currentDalgona;     // 선택된 달고나 이미지를 받을 오브젝트
	GameObject currentComplete;     // 완성된 달고나 이미지를 받을 오브젝트

	bool isEnd;


	void OnEnable()
	{
		if (DataManager.instance.nowPlayer.KorOrEngBool == true)     // 한국어..
		{
			expl_Text.text = "한 번에, 달고나를 완성해 주세요.";

			TextAsset textAsset = Resources.Load<TextAsset>($"Data/DalgonaData");      // JSON 파일
			data = JsonUtility.FromJson<DalgonaData>(textAsset.text);
		}
		else         // 영어..
		{
			expl_Text.text = "Make dalgona at once.";

			TextAsset textAsset = Resources.Load<TextAsset>($"Data/DalgonaData_En");      // JSON 파일
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

		if (DataManager.instance.nowPlayer.KorOrEngBool == true)    // 한국어..
		{
			dalgonaText.text = "제시어 : " + data.dalgonaData[randDalgona].dalgonaText;
		}
		else          // 영어...
		{
			dalgonaText.text = "Word : " + data.dalgonaData[randDalgona].dalgonaText;
		}
		

		if (data.dalgonaData[randDalgona].dalgonaLevel == 0)  // 하(25초)
		{
			slider.maxValue = 25.0f;
			slider.value = 25.0f;
		}
		else if (data.dalgonaData[randDalgona].dalgonaLevel == 1)  // 중(60초)
		{
			slider.maxValue = 60.0f;
			slider.value = 60.0f;
		}
		else   // 상(100초)
		{
			slider.maxValue = 100.0f;
			slider.value = 100.0f;
		}
	}

	void Update()
	{
		if (ThirdManager.touchSuccess == true && isEnd == false)    // 미션 성공 시
		{
			isEnd = true;
			Debug.Log("미션 성공");
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

		if (ThirdManager.touchFail == true && ThirdManager.isEnd == false)  // 미션 실패 시
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
				AudioManager.instance.Mission_01_Fail_Effect();       // 시간 오버 효과음
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



	void MissionContinue()        // 이 미션이 끝났을 때 처리
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
