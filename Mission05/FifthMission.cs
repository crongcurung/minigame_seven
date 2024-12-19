using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FifthMission : MonoBehaviour
{
    public TextMeshProUGUI alarmText;
    public TextMeshProUGUI countText;       // ó���� ���ڸ� ���� �ؽ�Ʈ

    public List<Sprite> spritePrefabs;     // �̹��� ������ �ִ� �͵� ��������Ʈ�� ��������

    public List<GameObject> flameList;       // �̹� ���� ��ġ�� 8���� ������ ��������
    public List<GameObject> copyFlameList;   // ������ ī���� ����Ʈ

    public List<int> flameIntList;
    public List<Sprite> flameImageList;   // ��������Ʈ �̹����� �޴� ����Ʈ

    public List<GameObject> lightList;    // ������ �޴� ����Ʈ

    public GameObject notePrefab;         // ��Ʈ ������
    public GameObject notePosition;       // ��Ʈ�� ������ ��ġ�� �ִ� ������Ʈ


    int randInstrument;
    int randFlame;
    int randDestory;            // �ڸ��� ����(1���� 8����)

    bool isEnd;
    bool failEnd;

	void OnEnable()
	{
        if (DataManager.instance.nowPlayer.KorOrEngBool)
        {
            alarmText.text = "�Ǳ⿡ �°� ��ư�� �����ּ���.";
        }
        else
        {
            alarmText.text = "Press Button According to Instrument.";
        }

        FifthManager.judgeInt = 0;             // �ʱ�ȭ
        FifthManager.judge01 = false;            // �ʱ�ȭ
        FifthManager.judge02 = false;              // �ʱ�ȭ
        FifthManager.isEnd = false;
        countText.gameObject.SetActive(true);           // ���ڰ� ������ ������Ʈ�� Ȱ��ȭ(���� Ȱ��ȭ���� �ٽ� �� ������ ����ɶ� �̷��� �ؾ���� ��)

        isEnd = false;
        failEnd = false;

        Instantiate(notePrefab, notePosition.transform.position, Quaternion.identity, notePosition.transform);
        // ��Ʈ �������� ��Ʈ ��ġ�� �����Ѵ�.

        lightList[0].SetActive(false);       // �ϴ� ������ ���ֱ�(�ʱ�ȭ, ������ �����־���)
        lightList[1].SetActive(false);       // �ϴ� ������ ���ֱ�(�ʱ�ȭ, ������ �����־���)

        copyFlameList.Clear();       // ī�Ǹ���Ʈ�� �ʱ�ȭ

        flameIntList.Clear();         //
        flameImageList.Clear();       //

        for (int i = 0; i < 4; i++)
        {
            flameImageList.Add(spritePrefabs[i]);       // ��������Ʈ �������� ����Ʈ�� ����
        }
        for (int i = 0; i < 4; i++)
        {
            flameImageList.Add(spritePrefabs[i]);        // �� �� �� �ؼ� 0 ~ 3, 0 ~ 3 ���� ����
        }

		for (int i = 0; i < 8; i++)
		{
            copyFlameList.Add(flameList[i]);           // �̹� ���� ��ġ�� ����Ʈ�� ī�� ����Ʈ�� ����

            flameIntList.Add(i);                       //
        }

        ArrangeInstrument();              // �̹��� ��ġ �Լ�

        countText.text = "3";               // �⺻ ��ٸ��� �ð�
        StartCoroutine(StartWaitText());

        AudioManager.instance.BackGround_Null();
    }

	void OnDisable()         // ������ ������
	{
        for (int i = 0; i < 8; i++)
        {
            flameList[i].SetActive(false);         //  �̹� ��ġ�� ���� ���� ��Ȱ��ȭ ��Ŵ
        }

        AudioManager.instance.PlayBackGroundSound_03();
    }

	IEnumerator StartWaitText()                    // ÷ �����ϰ� ���� �ؽ�Ʈ�� 3, 2, 1 �ϴ°� �ڷ�ƾ
    {
        yield return new WaitForSeconds(1.0f);
        countText.text = "2";

        yield return new WaitForSeconds(1.0f);
        countText.text = "1";

        yield return new WaitForSeconds(1.0f);
        countText.gameObject.SetActive(false);    // �ð��� �Ǹ� ���� �ؽ�Ʈ�� ��Ȱ��ȭ ��Ų��.
    }

	void Update()     // �� �� Ʋ�ȴ��� ������Ʈ �Լ��� �׻� Ȯ����
	{
        if (FifthManager.isEnd == true && FifthManager.judgeInt < 2 && isEnd == false)
        {
            isEnd = true;

            MissionManager.instance.succeseCount++;
            MissionContinue();
        }


        if (FifthManager.judgeInt == 0)
        {
            return;
        }
        if (FifthManager.judgeInt == 1)
        {
            lightList[0].SetActive(true);  // ���� �ϳ�
        }
        if (FifthManager.judgeInt >= 2 && isEnd == false)      // ���� ��
        {
            isEnd = true;

            lightList[0].SetActive(true);
            lightList[1].SetActive(true);
        }

        if (FifthManager.isEnd == true && FifthManager.judgeInt >= 2 && isEnd == true && failEnd == false)   // ���� ��
        {
            failEnd = true;
            MissionManager.instance.failCount++;
            MissionContinue();
        }
    }


	public void ArrangeInstrument()    // �̹��� ��ġ �Լ�
    {
        randDestory = UnityEngine.Random.Range(6, copyFlameList.Count + 1);     // �ּ� 6���� 8���� �ڸ�(�Ǳ�) �����

        for (int i = 0; i < randDestory; i++)   // ���� �ڸ������� �Ǳ� �̹��� ��ġ�ϱ�
        {
            randInstrument = UnityEngine.Random.Range(0, copyFlameList.Count);
            randFlame = UnityEngine.Random.Range(0, flameIntList.Count);     // �Ǳ� �����Լ�(0 ~ 7����)

            copyFlameList[flameIntList[randFlame]].SetActive(true);
            copyFlameList[flameIntList[randFlame]].GetComponent<Flame>().flameNum = randInstrument;
            copyFlameList[flameIntList[randFlame]].GetComponent<Image>().sprite = flameImageList[randInstrument];

            flameIntList.RemoveAt(randFlame);
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
