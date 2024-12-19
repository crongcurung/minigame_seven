using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance = null;
	public int succeseCount = 0;                    // 현재까지 성공한 미션 게임
	public int failCount = 0;                       // 현재까지 실패한 미션 게임


	public GameObject canvasObject;          // 1, 3, 4, 5, 6

	public GameObject etcCanvasObject;    // 카운트 및 결과창 전용 캔버스
	public GameObject canvasWaitPanel;    // 미션 게임 시작하기 전에 카운트를 세는 패널
	public GameObject resultPanel;        // 5개가 끝나면 결과창

	public GameObject dalgonaCanvasObject;   // 2
	public GameObject thirdCanvasObject;
	public GameObject lineCanvasObject;      // 7
    public GameObject seventhCanvasObject;

	

    public List<GameObject> oneToSixthPanel = new List<GameObject>();

	TextMeshProUGUI gameTile;     // 미션 게임 들어갈때 생기는 패널에서 게임 제목 알려주기
	TextMeshProUGUI waitCount;    // 미션 게임 들어갈때 생기는 패널에서 카운트 세기

	public bool endMission;

    int missionRandNum = 0;


	void Awake()        // 싱글톤화
	{
		if (instance == null)
		{
			instance = this;
		}

		endMission = false;

		gameTile = canvasWaitPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		waitCount = canvasWaitPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

		MissionContinue();     // 처음부터 다음 게임으로 넘어가야 한다.

		AudioManager.instance.PlayBackGroundSound_03();    // 배경 음악 바꾸기
	}


	void Update()
	{
		if (succeseCount + failCount == 10 && endMission == false)
		{
			endMission = true;    // 미션 게임을 끝낸다.
		}

	}

	public void FinalResult()                    // 5판이 끝나고 마지막 결과창을 보여주는 함수
	{
		canvasObject.SetActive(false);                // 캔버스 비활성화

		for (int i = 0; i < oneToSixthPanel.Count; i++)
		{
			oneToSixthPanel[i].SetActive(false);
		}

		dalgonaCanvasObject.SetActive(false);     // 캔버스 비활성화
		thirdCanvasObject.SetActive(false);       // 캔버스 비활성화

		lineCanvasObject.SetActive(false);        // 캔버스 비활성화
		seventhCanvasObject.SetActive(false);     // 캔버스 비활성화

		etcCanvasObject.SetActive(true);          //
		canvasWaitPanel.SetActive(false);         // 어떤 미션게임인지 알려주는 패널 비활성화
		resultPanel.SetActive(true);              //
	}

	public void MissionContinue()       // 다음 게임으로 넘어가는 함수
	{
		canvasObject.SetActive(false);

		for (int i = 0; i < oneToSixthPanel.Count; i++)
		{
			oneToSixthPanel[i].SetActive(false);
		}

		etcCanvasObject.SetActive(false);
		canvasWaitPanel.SetActive(false);

		dalgonaCanvasObject.SetActive(false);
		thirdCanvasObject.SetActive(false);

		lineCanvasObject.SetActive(false);
		seventhCanvasObject.SetActive(false);


		missionRandNum = UnityEngine.Random.Range(1, 8);                // 다음 게임은 어떤 걸로 할지 랜덤으로 뽑는다.
		//missionRandNum = 7;                                           // 이걸로 스테이지 조정!!!!!!!!!!!!!!!!
		Debug.Log(missionRandNum);

		etcCanvasObject.SetActive(true);      // 다음 게임이 어떤지 알려줄려고 활성화
		canvasWaitPanel.SetActive(true);

		waitCount.text = "3";      // 3초 카운트

		//DataManager.instance.nowPlayer.KorOrEngBool = true;    // 포트폴리오
		if (DataManager.instance.nowPlayer.KorOrEngBool == true)        // 한국어...
		{
			if (missionRandNum == 3)
			{
				gameTile.text = "<color=blue>'뽑아볼래?'</color> 시작할 준비!!";
			}
			else if (missionRandNum == 7)
			{
				gameTile.text = "<color=blue>'타러갈래?'</color> 시작할 준비!!";
			}
			else                                        // 3번 7번을 제외한 나머지 미션 게임들...
			{
				if (missionRandNum == 1)
				{
					gameTile.text = "<color=blue>'풀어볼래?'</color> 시작할 준비!!";
				}
				else if (missionRandNum == 2)
				{
					gameTile.text = "<color=blue>'순서아니?'</color> 시작할 준비!!";
				}
				else if (missionRandNum == 4)
				{
					gameTile.text = "<color=blue>'뒤집을래?'</color> 시작할 준비!!";
				}
				else if (missionRandNum == 5)
				{
					gameTile.text = "<color=blue>'맞춰볼래?'</color> 시작할 준비!!";
				}
				else
				{
					gameTile.text = "<color=blue>'끼워볼래?'</color> 시작할 준비!!";
				}
			}
		}
		else        // 영어
		{
			if (missionRandNum == 3)
			{
				gameTile.text = "Get Ready to Start <color=blue>'Shall We Pick?'</color>!!";
			}
			else if (missionRandNum == 7)
			{
				gameTile.text = "Get Ready to Start <color=blue>'Shall We Ride?'</color>!!";
			}
			else                                        // 3번 7번을 제외한 나머지 미션 게임들...
			{
				if (missionRandNum == 1)
				{
					gameTile.text = "Get Ready to Start <color=blue>'Shall We Solve?'</color>!!";
				}
				else if (missionRandNum == 2)
				{
					gameTile.text = "Get Ready to Start <color=blue>'Shall We Know?'</color>!!";
				}
				else if (missionRandNum == 4)
				{
					gameTile.text = "Get Ready to Start <color=blue>'Shall We Flip?'</color>!!";
				}
				else if (missionRandNum == 5)
				{
					gameTile.text = "Get Ready to Start <color=blue>'Shall We Guess?'</color>!!";
				}
				else
				{
					gameTile.text = "Get Ready to Start <color=blue>'Shall We Put?'</color>!!";
				}
			}
		}

		StartCoroutine(WaitFirst());
	}

	public IEnumerator WaitFirst()            // 카운트
	{
		yield return new WaitForSeconds(1.0f);
		waitCount.text = "2";

		StartCoroutine(WaitSecond());
	}

	public IEnumerator WaitSecond()            // 카운트
	{
		yield return new WaitForSeconds(1.0f);
		waitCount.text = "1";

		StartCoroutine(WaitThird());
	}

	public IEnumerator WaitThird()            // 카운트(마지막)
	{
		yield return new WaitForSeconds(1.0f);

		etcCanvasObject.SetActive(false);

		if (missionRandNum == 3)           // 달고나 게임이 걸렸다면..
		{
			dalgonaCanvasObject.SetActive(true);
			thirdCanvasObject.SetActive(true);
		}
		else if (missionRandNum == 7)      // 사다리 게임이 걸렸다면..
		{
			lineCanvasObject.SetActive(true);
			seventhCanvasObject.SetActive(true);
		}
		else                                        // 3번 7번을 제외한 나머지 미션 게임들...
		{
			canvasObject.SetActive(true);

			if (missionRandNum == 1)
			{
				oneToSixthPanel[0].SetActive(true);
			}
			else if (missionRandNum == 2)
			{
				oneToSixthPanel[1].SetActive(true);
			}
			else if (missionRandNum == 4)
			{
				oneToSixthPanel[2].SetActive(true);
			}
			else if (missionRandNum == 5)
			{
				oneToSixthPanel[3].SetActive(true);
			}
			else
			{
				oneToSixthPanel[4].SetActive(true);
			}
		}
	}

}
