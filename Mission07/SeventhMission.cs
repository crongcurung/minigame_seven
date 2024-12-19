using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeventhMission : MonoBehaviour
{
	public Slider slider;
	public GameObject outLinePanel;                // 처음에 시작할때 아무 버튼도 못 누르게 하는 패널
	public GameObject lineOutLinePanel;            // 라인만 지우는 패널
	public TextMeshProUGUI testText;               // 문제 내는 텍스트

	public List<Button> clickButton = new List<Button>();              // 위에 알파벳 버튼(누르면 내려감)
	public List<Image> goalButton = new List<Image>();                 // 아래 숫자 이미지(도착 지점)

	public List<GameObject> verseList01 = new List<GameObject>();      // 라인 1번에서 오른쪽으로 나 있는 사다리
	public List<GameObject> verseList02 = new List<GameObject>();      // 라인 2번에서 오른쪽으로 나 있는 사다리
	public List<GameObject> verseList03 = new List<GameObject>();      // 라인 3번에서 오른쪽으로 나 있는 사다리
	public List<GameObject> verseList04 = new List<GameObject>();      // 라인 4번에서 오른쪽으로 나 있는 사다리

	List<GameObject> copyVerseList01 = new List<GameObject>();      // 라인 1번에서 오른쪽으로 나 있는 사다리
	List<GameObject> copyVerseList02 = new List<GameObject>();      // 라인 2번에서 오른쪽으로 나 있는 사다리
	List<GameObject> copyVerseList03 = new List<GameObject>();      // 라인 3번에서 오른쪽으로 나 있는 사다리
	List<GameObject> copyVerseList04 = new List<GameObject>();      // 라인 4번에서 오른쪽으로 나 있는 사다리


	int rand01;       // 라인 1번에서 사다리를 몇개 뽑을 건지에 대한 랜덤 변수
	int rand02;       // 라인 2번에서 사다리를 몇개 뽑을 건지에 대한 랜덤 변수
	int rand03;       // 라인 3번에서 사다리를 몇개 뽑을 건지에 대한 랜덤 변수  
	int rand04;       // 라인 4번에서 사다리를 몇개 뽑을 건지에 대한 랜덤 변수

	int currentRand;             // 정답을 담는 랜덤 변수
	Color originalColor;         // 기존 버튼 컬러를 받는 변수

	bool isEnd = false;        // 버튼이 아래로 다 내려왔는지 확인하는 변수

	void OnEnable()
	{
		SeventhManager.hitNum = 0;

		copyVerseList01.Clear();      // 카피 사다리 리스트 초기화
		copyVerseList02.Clear();
		copyVerseList03.Clear();
		copyVerseList04.Clear();

		SeventhManager.isClick = false;
		isEnd = false;                 // 버튼이 아래로 다 내려왔는지 확인하는 변수 초기화
		currentRand = 0;             // 정답을 담는 랜덤 변수

		originalColor = goalButton[0].color;  // 기존 버튼 색깔을 받는다.

		for (int i = 0; i < 5; i++)       // 사다리 전부 비활성화
		{
			verseList01[i].SetActive(false);
			verseList02[i].SetActive(false);
			verseList03[i].SetActive(false);
			verseList04[i].SetActive(false);
		}

		for (int i = 0; i < 5; i++)
		{
			copyVerseList01.Add(verseList01[i]);
			copyVerseList02.Add(verseList02[i]);
			copyVerseList03.Add(verseList03[i]);
			copyVerseList04.Add(verseList04[i]);
		}

		slider.maxValue = 10.0f;
		slider.value = 10.0f;
		outLinePanel.SetActive(true);

		rand01 = UnityEngine.Random.Range(1, 6);   // 라인 1번에서 사다디를 몇개 놓을 것인가(최소 1개, 최대 5개)
		rand02 = UnityEngine.Random.Range(1, 6);   // 라인 2번에서 사다디를 몇개 놓을 것인가(최소 1개, 최대 5개)
		rand03 = UnityEngine.Random.Range(1, 6);   // 라인 3번에서 사다디를 몇개 놓을 것인가(최소 1개, 최대 5개)
		rand04 = UnityEngine.Random.Range(1, 6);   // 라인 4번에서 사다디를 몇개 놓을 것인가(최소 1개, 최대 5개)

		for (int i = 0; i < rand01; i++)                // 뽑은 숫자대로 사다리를 만든다.
		{
			int rand01 = UnityEngine.Random.Range(0, copyVerseList01.Count);      // 위에 뽑은 숫자에서 랜덤으로 어느 사다리가 생길지를 담는 랜덤 변수 
			copyVerseList01[rand01].SetActive(true);
			copyVerseList01.RemoveAt(rand01);  // 중복방지
		}

		for (int i = 0; i < rand02; i++)                // 뽑은 숫자대로 사다리를 만든다.
		{
			int rand02 = UnityEngine.Random.Range(0, copyVerseList02.Count);      // 위에 뽑은 숫자에서 랜덤으로 어느 사다리가 생길지를 담는 랜덤 변수
			copyVerseList02[rand02].SetActive(true);
			copyVerseList02.RemoveAt(rand02);  // 중복방지
		}

		for (int i = 0; i < rand03; i++)                // 뽑은 숫자대로 사다리를 만든다.
		{
			int rand03 = UnityEngine.Random.Range(0, copyVerseList03.Count);      // 위에 뽑은 숫자에서 랜덤으로 어느 사다리가 생길지를 담는 랜덤 변수
			copyVerseList03[rand03].SetActive(true);
			copyVerseList03.RemoveAt(rand03);  // 중복방지
		}

		for (int i = 0; i < rand04; i++)                // 뽑은 숫자대로 사다리를 만든다.
		{
			int rand04 = UnityEngine.Random.Range(0, copyVerseList04.Count);       // 위에 뽑은 숫자에서 랜덤으로 어느 사다리가 생길지를 담는 랜덤 변수
			copyVerseList04[rand04].SetActive(true);
			copyVerseList04.RemoveAt(rand04);  // 중복방지
		}

		StartCoroutine(StartWait());
	}

	void OnDisable()
	{
		SeventhManager.hitNum = 0;

		for (int i = 0; i < 5; i++)       // 사다리 전부 비활성화
		{
			goalButton[i].color = originalColor;  // 컬러를 원래대로 해두기
			clickButton[i].interactable = true;
		}
	}


	void Update()
	{
		if (SeventhManager.isClick == true)         // 버튼을 클릭했는지
		{
			for (int i = 0; i < 5; i++)
			{
				clickButton[i].interactable = false;        // 버튼을 클릭했으면 버튼 전부를 비활성화 시킴
			}
		}


		if (SeventhManager.isClick == false && isEnd == false)
		{
			if (slider.value > 0)
			{
				slider.value -= Time.deltaTime;
			}
			else
			{
				isEnd = true;
				AudioManager.instance.Mission_01_Fail_Effect();       // 시간 오버 효과음

				for (int i = 0; i < 5; i++)
				{
					clickButton[i].interactable = false;        // 시간이 다 지나면, 버튼 전부를 비활성화 시킴
				}

				slider.value = 0;
				MissionManager.instance.failCount++;
				MissionContinue();
			}
		}


		if (SeventhManager.hitNum > 0 && isEnd == false)  // hitNum이 0이상이란 것은 이미지에 닿았다는 뜻
		{
			if (SeventhManager.hitNum == currentRand)        // hitNum이 정답과 같다면
			{
				isEnd = true;          // 게임 끝
				Debug.Log("정답");

				AudioManager.instance.Mission_01_Success_Effect();     // 성공 효과음

				SeventhManager.hitNum = 0;

				MissionManager.instance.succeseCount++;
				MissionContinue();
				return;
			}
			else               // hitNum이 정답과 다르다면
			{
				isEnd = true;
				Debug.Log("틀렸습니다.");

				AudioManager.instance.Mission_01_Fail_Effect();     // 실패 효과음

				SeventhManager.hitNum = 0;

				MissionManager.instance.failCount++;
				MissionContinue();   // 3초 후 이 게임 끝내기
				return;
			}
		}

		
	}


	IEnumerator StartWait()
	{
		yield return new WaitForSeconds(10.0f);        // 사다리를 보기위한 10초가 끝나면...

		currentRand = UnityEngine.Random.Range(1, 6);

		outLinePanel.SetActive(false);                      // 전체 패널은 지우고
		lineOutLinePanel.SetActive(true);                  //
		goalButton[currentRand - 1].color = Color.red;

		if (DataManager.instance.nowPlayer.KorOrEngBool == true)    // 한국어..
		{
			testText.text = $"<color=#FF0000>{currentRand}번</color> 구역으로 가는 사다리를 누르세요!";
		}
		else     // 영어..
		{
			testText.text = $"Press Ladder to Area <color=#FF0000>{currentRand}</color>!";
		}
		slider.maxValue = 10.0f;
		slider.value = 10;
		slider.GetComponentInChildren<Image>().color = Color.red;    // 여기부터 바로 정답인 이미지를 빨강색을 바꿈

		StartCoroutine(AgainWait());
	}

	IEnumerator AgainWait()    // 10초 동안 버튼을 누를 기회를 주는 코루틴 함수
	{
		yield return new WaitForSeconds(10.0f);  
		lineOutLinePanel.SetActive(false);          // 라인만 가리는 패널 활성화
	}



	void MissionContinue()        // 이 미션이 끝났을 때 처리
	{
		StartCoroutine(EndGameWait());
	}

	public IEnumerator EndGameWait()      // 3초 후 이 게임 끝내기
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
