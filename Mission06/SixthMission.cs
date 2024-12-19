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

    public TextMeshProUGUI testText;       // ���þ ��� ������Ʈ
    public GameObject flamePanel;          // �׸����� �ڽ����� ������ �ִ� �г�

    public List<GameObject> chillGyoList = new List<GameObject>();      // ĥ�� ���� ����Ʈ
    List<Vector3> originalPosition = new List<Vector3>();               // ĥ�� ������ ���� ��ġ�� ��� ����Ʈ
    List<Quaternion> originalRotation = new List<Quaternion>();         // ĥ�� ������ ���� ȸ���� ��� ����Ʈ

    int randNum;               // � �׸��� ������ ���� ����

    ChillGyoData data;              // JSON �����͸� �޴� ����

    bool isEnd;

    void OnEnable()
	{
        if (DataManager.instance.nowPlayer.KorOrEngBool == true)
        {
            TextAsset textAsset = Resources.Load<TextAsset>($"Data/ChillGyoData");      // JSON ����
            data = JsonUtility.FromJson<ChillGyoData>(textAsset.text);

            alarmText.text = "�׸��� �°� ������ ��ġ ���ּ���.";
        }
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>($"Data/ChillGyoData_En");      // JSON ����
            data = JsonUtility.FromJson<ChillGyoData>(textAsset.text);

            alarmText.text = "Arrange Shapes According to Picture.";
        }

        SixthManager.currentCount = 0;                                   // ���� ������ ī��Ʈ�� �ʱ�ȭ
        isEnd = false;

        originalPosition.Clear();                   // ���� ��ġ �ʱ�ȭ
        originalRotation.Clear();

        for (int i = 0; i < chillGyoList.Count; i++)
        {
            originalPosition.Add(chillGyoList[i].transform.position);            // ������ ��ġ�� ȸ���� ����Ʈ�� ��´�.
            originalRotation.Add(chillGyoList[i].transform.rotation);
        }

        slider.maxValue = 30.0f;
        slider.value = 30.0f;

        randNum = UnityEngine.Random.Range(0, flamePanel.transform.childCount);             // ���þ�� ����Ʈ�� �̴� ���� ����


        if (DataManager.instance.nowPlayer.KorOrEngBool == true)
        {
            testText.text = "���þ� : " + data.chillGyoTextData[randNum].chillGyoText;          // �ش�Ǵ� ���þ� �̱�
        }
        else
        {
            testText.text = "Word :  " + data.chillGyoTextData[randNum].chillGyoText;          // �ش�Ǵ� ���þ� �̱�
        }



        flamePanel.transform.GetChild(randNum).gameObject.SetActive(true);                 // �ش�Ǵ� �׸� Ȱ��ȭ
    }

	void OnDisable()                   
	{
        for (int i = 0; i < chillGyoList.Count; i++) 
        {
            chillGyoList[i].SetActive(true);                              // ��Ȱ��ȭ�� ������ ���� Ȱ��ȭ��Ŵ
            chillGyoList[i].transform.position = originalPosition[i];     // ��ġ�� ������� ����
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
                AudioManager.instance.Mission_01_Fail_Effect();       // �ð� ���� ȿ����
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
