using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FifthMission : MonoBehaviour
{
    public TextMeshProUGUI alarmText;
    public TextMeshProUGUI countText;       // 처음에 숫자를 세는 텍스트

    public List<Sprite> spritePrefabs;     // 이미지 폴더에 있는 것들 스프라이트로 가져오기

    public List<GameObject> flameList;       // 이미 씬에 배치된 8개의 프레임 가져오기
    public List<GameObject> copyFlameList;   // 위에꺼 카피한 리스트

    public List<int> flameIntList;
    public List<Sprite> flameImageList;   // 스프라이트 이미지를 받는 리스트

    public List<GameObject> lightList;    // 경고들을 받는 리스트

    public GameObject notePrefab;         // 노트 프리팹
    public GameObject notePosition;       // 노트가 놓여질 위치에 있는 오브젝트


    int randInstrument;
    int randFlame;
    int randDestory;            // 자리수 랜덤(1부터 8까지)

    bool isEnd;
    bool failEnd;

	void OnEnable()
	{
        if (DataManager.instance.nowPlayer.KorOrEngBool)
        {
            alarmText.text = "악기에 맞게 버튼을 눌러주세요.";
        }
        else
        {
            alarmText.text = "Press Button According to Instrument.";
        }

        FifthManager.judgeInt = 0;             // 초기화
        FifthManager.judge01 = false;            // 초기화
        FifthManager.judge02 = false;              // 초기화
        FifthManager.isEnd = false;
        countText.gameObject.SetActive(true);           // 숫자가 나오는 오브젝트를 활성화(원래 활성화지만 다시 이 게임이 실행될때 이런걸 해야줘야 함)

        isEnd = false;
        failEnd = false;

        Instantiate(notePrefab, notePosition.transform.position, Quaternion.identity, notePosition.transform);
        // 노트 프리팹을 노트 위치에 생성한다.

        lightList[0].SetActive(false);       // 일단 경고등은 없애기(초기화, 원래는 꺼져있었음)
        lightList[1].SetActive(false);       // 일단 경고등은 없애기(초기화, 원래는 꺼져있었음)

        copyFlameList.Clear();       // 카피리스트만 초기화

        flameIntList.Clear();         //
        flameImageList.Clear();       //

        for (int i = 0; i < 4; i++)
        {
            flameImageList.Add(spritePrefabs[i]);       // 스프라이트 프리팹을 리스트에 삽입
        }
        for (int i = 0; i < 4; i++)
        {
            flameImageList.Add(spritePrefabs[i]);        // 한 번 더 해서 0 ~ 3, 0 ~ 3 으로 맞춤
        }

		for (int i = 0; i < 8; i++)
		{
            copyFlameList.Add(flameList[i]);           // 이미 씬에 배치된 리스트를 카피 리스트에 삽입

            flameIntList.Add(i);                       //
        }

        ArrangeInstrument();              // 이미지 배치 함수

        countText.text = "3";               // 기본 기다리는 시간
        StartCoroutine(StartWaitText());

        AudioManager.instance.BackGround_Null();
    }

	void OnDisable()         // 게임이 끝나면
	{
        for (int i = 0; i < 8; i++)
        {
            flameList[i].SetActive(false);         //  이미 배치된 씬을 전부 비활성화 시킴
        }

        AudioManager.instance.PlayBackGroundSound_03();
    }

	IEnumerator StartWaitText()                    // 첨 시작하고 숫자 텍스트가 3, 2, 1 하는거 코루틴
    {
        yield return new WaitForSeconds(1.0f);
        countText.text = "2";

        yield return new WaitForSeconds(1.0f);
        countText.text = "1";

        yield return new WaitForSeconds(1.0f);
        countText.gameObject.SetActive(false);    // 시간이 되면 숫자 텍스트를 비활성화 시킨다.
    }

	void Update()     // 몇 개 틀렸는지 업데이트 함수로 항상 확인함
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
            lightList[0].SetActive(true);  // 경고등 하나
        }
        if (FifthManager.judgeInt >= 2 && isEnd == false)      // 게임 끝
        {
            isEnd = true;

            lightList[0].SetActive(true);
            lightList[1].SetActive(true);
        }

        if (FifthManager.isEnd == true && FifthManager.judgeInt >= 2 && isEnd == true && failEnd == false)   // 실패 시
        {
            failEnd = true;
            MissionManager.instance.failCount++;
            MissionContinue();
        }
    }


	public void ArrangeInstrument()    // 이미지 배치 함수
    {
        randDestory = UnityEngine.Random.Range(6, copyFlameList.Count + 1);     // 최소 6부터 8까지 자리(악기) 만들기

        for (int i = 0; i < randDestory; i++)   // 나온 자리수까지 악기 이미지 배치하기
        {
            randInstrument = UnityEngine.Random.Range(0, copyFlameList.Count);
            randFlame = UnityEngine.Random.Range(0, flameIntList.Count);     // 악기 랜덤함수(0 ~ 7까지)

            copyFlameList[flameIntList[randFlame]].SetActive(true);
            copyFlameList[flameIntList[randFlame]].GetComponent<Flame>().flameNum = randInstrument;
            copyFlameList[flameIntList[randFlame]].GetComponent<Image>().sprite = flameImageList[randInstrument];

            flameIntList.RemoveAt(randFlame);
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
