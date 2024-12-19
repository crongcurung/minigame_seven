using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[Serializable]
public class TestVar      // FirstMission JSON ������ ������
{
    public string testText;    // ����
    public string num01;       // 1�� ��(����)
    public string num02;       // 2�� ��
    public string num03;       // 3�� ��
    public string num04;       // 4�� ��
}
[Serializable]
public class TestData   // FirstMission JSon ������ ��� ����Ʈ
{
    public List<TestVar> testData = new List<TestVar>();
}




public class FirstMission : MonoBehaviour
{
    public Slider slider;          // �ð� �����̴�
    public TextMeshProUGUI textText;    // ������ ��� �ؽ�Ʈ
    public TextMeshProUGUI[] answer;    // ��ư �ȿ� ��� ���� ��� �ؽ�Ʈ(�� 4��)

    bool isClick;                       // ��ư�� ������?(���� �����?) ��� ��. �ᱹ ������ �����ٴ� ��
    TestData data;                    // JSON ������ ���� ����
    int corretNum = -1;                  // ������ ��ư�� ������� ��� ����(-1�� �ʱ�ȭ)

    Color extistingColor = Color.white;      // ��ư�� ������ �ٽ� ������� �ǵ��� ���� ����

    bool finalCheck;



	void OnEnable()                    // �̼� ������ Ȱ��, ��Ȱ������ �ϱ� ������ Start �Լ� �� ��
    {
        slider.maxValue = 7;
        slider.value = 7;

        finalCheck = false;

        isClick = false;          // ó������ Ŭ���� �ȵǾ� �ִٰ� �ʱ�ȭ

        After_Press();         // ��ư Ŭ���� ������ ������� �ǵ����� ������, ù�Ǻ��� �ص� ��.

        int randNum;           // json ���Ͽ� �ִ� ������ �������� ���� ������ ����
        List<int> randInt = new List<int>() { 0, 1, 2, 3 };        //�ϴ� ����Ʈ ���� ��, 0������ 3������(�� 4��) ���� ����Ʈ
        int rand1;                                                 // 
        int rand2;                                                 // 

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)
        {
            TextAsset textAsset1 = Resources.Load<TextAsset>($"Data/FirstMission");      // ���̽� �ε�
            data = JsonUtility.FromJson<TestData>(textAsset1.text);                      // ���̽� �ε�
        }
        else
        {
            TextAsset textAsset1 = Resources.Load<TextAsset>($"Data/FirstMission_En");      // ���̽� �ε�
            data = JsonUtility.FromJson<TestData>(textAsset1.text);                      // ���̽� �ε�
        }

        randNum = UnityEngine.Random.Range(0, data.testData.Count);                   // ���� ����


        List<string> dataList = new List<string>() { data.testData[randNum].num01, data.testData[randNum].num02, data.testData[randNum].num03, data.testData[randNum].num04 };
        // �������� ���� ���ڸ� json ���� ����Ʈ�� ����, ���� �̾ƿ´�. �׸��� (��)����Ʈ�� ��´�.

        textText.text = data.testData[randNum].testText; //�̾ƿ� ������ �ؽ�Ʈ�� ������. 

        for (int i = 0; i < 4; i++)         // ���� 4���̰�, �̸� �� �������� �ٲ���ϱ� ������ �̷� �۾��� �Ѵ�.
        {
            rand1 = UnityEngine.Random.Range(0, randInt.Count); // �� 4��
            rand2 = UnityEngine.Random.Range(0, dataList.Count); // �� 4��
            answer[randInt[rand1]].text = dataList[rand2];       // �ϴ� �������� ���� ���� answer ����Ʈ�� ��Ƶд�.

            if (dataList[rand2] == data.testData[randNum].num01)   // ������ ���� Ȯ���ϰ� �����Ѵ�.
            {
                corretNum = randInt[rand1];           // ����� �������� �����Ѵ�.
            }

            randInt.RemoveAt(rand1);       // �ߺ� ����
            dataList.RemoveAt(rand2);      // �ߺ� ����
        }
    }

	void Update()
	{
        if (isClick == false)         // ��ư�� Ŭ���ϸ� �����̴��� ������ ���ϵ��� �Ѵ�.
        {
            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;       // ���δ�.
            }
            else
            {
                if (finalCheck == false)
                {
                    AudioManager.instance.Mission_01_Fail_Effect();       // �ð� ���� ȿ����

                    finalCheck = true;
                    for (int i = 0; i < 4; i++)
                    {
                        answer[i].GetComponentInParent<Button>().interactable = false;
                    }
                    slider.value = 0;
                    answer[this.corretNum].GetComponentInParent<Image>().color = Color.blue;   // ������ ��ư�� �Ķ��� ó��!
                    MissionManager.instance.failCount++;
                    MissionContinue();
                }
            }
        }
        else                            // ��ư�� Ŭ���ϸ� ��� ��ư�� ��Ȱ��ȭ �ȴ�.
        {
            for (int i = 0; i < 4; i++)
            {
                answer[i].GetComponentInParent<Button>().interactable = false;
            }
        }
	}

	public void Press_Button(int corretNum)       // ��ư�� ������ ���.. ��ư�� OnClick���� ���� �־���.(�Ķ���ʹ� �� ��ư�� ��ȣ�� ��Ҵ�.)
    {
        isClick = true;                        // ��ư�� ������ ����̱� ������ �ٷ� true�� �־���ȴ�.
        extistingColor = answer[this.corretNum].GetComponentInParent<Image>().color;    // �ϴ� ���� ������ �ִ°ſ��µ�, ���ֹ����� ������

        if (this.corretNum == corretNum)     // ���� ���� ��ȣ�� ��ư�� ��� ��ȣ�� ���ٸ�!
        {
            Debug.Log("����");
            AudioManager.instance.Mission_01_Success_Effect();            // �̼� ���� ȿ����
            answer[this.corretNum].GetComponentInParent<Image>().color = Color.blue;   // ������ ��ư�� �Ķ��� ó��!

            MissionManager.instance.succeseCount++;
            
        }
        else    // ���� ���� ��ȣ�� ��ư�� ��� ��ȣ�� Ʋ���ٸ�!
        {
            Debug.Log("Ʋ�Ƚ��ϴ�.");
            AudioManager.instance.Mission_01_Fail_Effect();            // �̼� ���� ȿ����
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.red;   // ���� ���� ��ư�� ������ ó��!
            //answer[this.corretNum].GetComponentInParent<Image>().color = Color.blue;   // ������ ��ư�� �Ķ��� ó��!

            MissionManager.instance.failCount++;
        }

        MissionContinue();

        return;
    }

    void After_Press()  // ��Ȱ��ȭ ��Ű�� �ٽ� Ȱ��ȭ�ϸ� ��ư�� ������ �������.. �ʱ�ȭ�ϴ� �Լ���.
    {
        for (int i = 0; i < 4; i++)
        {
            answer[i].GetComponentInParent<Image>().color = extistingColor;
            answer[i].GetComponentInParent<Button>().interactable = true;
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
