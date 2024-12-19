using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ChillGyoVar   
{             
    public string chillGyoText = null;   
}
[Serializable]
public class ChillGyoData
{
    public List<ChillGyoVar> chillGyoTextData = new List<ChillGyoVar>();
}

public class SixthMission : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI alarmText;

    public TextMeshProUGUI testText;       // 제시어를 담는 오브젝트
    public GameObject flamePanel;          // 그림들을 자식으로 가지고 있는 패널

    public List<GameObject> chillGyoList = new List<GameObject>();      // 칠교 도형 리스트
    List<Vector3> originalPosition = new List<Vector3>();               // 칠교 도형의 원래 위치를 담는 리스트
    List<Quaternion> originalRotation = new List<Quaternion>();         // 칠교 도형의 원래 회전을 담는 리스트

    int randNum;               // 어떤 그림이 나올지 랜덤 변수

    ChillGyoData data;              // JSON 데이터를 받는 변수

    bool isEnd;

    void OnEnable()
	{
        if (DataManager.instance.nowPlayer.KorOrEngBool == true)
        {
            TextAsset textAsset = Resources.Load<TextAsset>($"Data/ChillGyoData");      // JSON 파일
            data = JsonUtility.FromJson<ChillGyoData>(textAsset.text);

            alarmText.text = "그림에 맞게 도형을 배치 해주세요.";
        }
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>($"Data/ChillGyoData_En");      // JSON 파일
            data = JsonUtility.FromJson<ChillGyoData>(textAsset.text);

            alarmText.text = "Arrange Shapes According to Picture.";
        }

        SixthManager.currentCount = 0;                                   // 맞춘 도형의 카운트를 초기화
        isEnd = false;

        originalPosition.Clear();                   // 원래 위치 초기화
        originalRotation.Clear();

        for (int i = 0; i < chillGyoList.Count; i++)
        {
            originalPosition.Add(chillGyoList[i].transform.position);            // 도형의 위치와 회전을 리스트에 담는다.
            originalRotation.Add(chillGyoList[i].transform.rotation);
        }

        slider.maxValue = 30.0f;
        slider.value = 30.0f;

        randNum = UnityEngine.Random.Range(0, flamePanel.transform.childCount);             // 제시어와 리스트를 뽑는 랜덤 변수


        if (DataManager.instance.nowPlayer.KorOrEngBool == true)
        {
            testText.text = "제시어 : " + data.chillGyoTextData[randNum].chillGyoText;          // 해당되는 제시어 뽑기
        }
        else
        {
            testText.text = "Word :  " + data.chillGyoTextData[randNum].chillGyoText;          // 해당되는 제시어 뽑기
        }



        flamePanel.transform.GetChild(randNum).gameObject.SetActive(true);                 // 해당되는 그림 활성화
    }

	void OnDisable()                   
	{
        for (int i = 0; i < chillGyoList.Count; i++) 
        {
            chillGyoList[i].SetActive(true);                              // 비활성화된 도형을 전부 활성화시킴
            chillGyoList[i].transform.position = originalPosition[i];     // 위치를 원래대로 놔둠
            chillGyoList[i].transform.rotation = originalRotation[i];
        }

        flamePanel.transform.GetChild(randNum).gameObject.SetActive(false);

        for(int i = 0; i < 6; i++)
        {
            flamePanel.transform.GetChild(randNum).gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.black;
        }
    }

	void Update()
    {
        if (SixthManager.currentCount < 6 && isEnd == false)
        {
            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                isEnd = true;
                AudioManager.instance.Mission_01_Fail_Effect();       // 시간 오버 효과음
                slider.value = 0;

                MissionManager.instance.failCount++;
                MissionContinue();
            }
        }

        if (SixthManager.currentCount > 5.5f && isEnd == false)
        {
            isEnd = true;

            MissionManager.instance.succeseCount++;
            MissionContinue();
        }
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
