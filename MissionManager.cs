using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance = null;
	public int succeseCount = 0;                    // ������� ������ �̼� ����
	public int failCount = 0;                       // ������� ������ �̼� ����


	public GameObject canvasObject;          // 1, 3, 4, 5, 6

	public GameObject etcCanvasObject;    // ī��Ʈ �� ���â ���� ĵ����
	public GameObject canvasWaitPanel;    // �̼� ���� �����ϱ� ���� ī��Ʈ�� ���� �г�
	public GameObject resultPanel;        // 5���� ������ ���â

	public GameObject dalgonaCanvasObject;   // 2
	public GameObject thirdCanvasObject;
	public GameObject lineCanvasObject;      // 7
    public GameObject seventhCanvasObject;

	

    public List<GameObject> oneToSixthPanel = new List<GameObject>();

	TextMeshProUGUI gameTile;     // �̼� ���� ���� ����� �гο��� ���� ���� �˷��ֱ�
	TextMeshProUGUI waitCount;    // �̼� ���� ���� ����� �гο��� ī��Ʈ ����

	public bool endMission;

    int missionRandNum = 0;


	void Awake()        // �̱���ȭ
	{
		if (instance == null)
		{
			instance = this;
		}

		endMission = false;

		gameTile = canvasWaitPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		waitCount = canvasWaitPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

		MissionContinue();     // ó������ ���� �������� �Ѿ�� �Ѵ�.

		AudioManager.instance.PlayBackGroundSound_03();    // ��� ���� �ٲٱ�
	}


	void Update()
	{
		if (succeseCount + failCount == 10 && endMission == false)
		{
			endMission = true;    // �̼� ������ ������.
		}

	}

	public void FinalResult()                    // 5���� ������ ������ ���â�� �����ִ� �Լ�
	{
		canvasObject.SetActive(false);                // ĵ���� ��Ȱ��ȭ

		for (int i = 0; i < oneToSixthPanel.Count; i++)
		{
			oneToSixthPanel[i].SetActive(false);
		}

		dalgonaCanvasObject.SetActive(false);     // ĵ���� ��Ȱ��ȭ
		thirdCanvasObject.SetActive(false);       // ĵ���� ��Ȱ��ȭ

		lineCanvasObject.SetActive(false);        // ĵ���� ��Ȱ��ȭ
		seventhCanvasObject.SetActive(false);     // ĵ���� ��Ȱ��ȭ

		etcCanvasObject.SetActive(true);          //
		canvasWaitPanel.SetActive(false);         // � �̼ǰ������� �˷��ִ� �г� ��Ȱ��ȭ
		resultPanel.SetActive(true);              //
	}

	public void MissionContinue()       // ���� �������� �Ѿ�� �Լ�
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


		missionRandNum = UnityEngine.Random.Range(1, 8);                // ���� ������ � �ɷ� ���� �������� �̴´�.
		//missionRandNum = 7;                                           // �̰ɷ� �������� ����!!!!!!!!!!!!!!!!
		Debug.Log(missionRandNum);

		etcCanvasObject.SetActive(true);      // ���� ������ ��� �˷��ٷ��� Ȱ��ȭ
		canvasWaitPanel.SetActive(true);

		waitCount.text = "3";      // 3�� ī��Ʈ

		//DataManager.instance.nowPlayer.KorOrEngBool = true;    // ��Ʈ������
		if (DataManager.instance.nowPlayer.KorOrEngBool == true)        // �ѱ���...
		{
			if (missionRandNum == 3)
			{
				gameTile.text = "<color=blue>'�̾ƺ���?'</color> ������ �غ�!!";
			}
			else if (missionRandNum == 7)
			{
				gameTile.text = "<color=blue>'Ÿ������?'</color> ������ �غ�!!";
			}
			else                                        // 3�� 7���� ������ ������ �̼� ���ӵ�...
			{
				if (missionRandNum == 1)
				{
					gameTile.text = "<color=blue>'Ǯ���?'</color> ������ �غ�!!";
				}
				else if (missionRandNum == 2)
				{
					gameTile.text = "<color=blue>'�����ƴ�?'</color> ������ �غ�!!";
				}
				else if (missionRandNum == 4)
				{
					gameTile.text = "<color=blue>'��������?'</color> ������ �غ�!!";
				}
				else if (missionRandNum == 5)
				{
					gameTile.text = "<color=blue>'���纼��?'</color> ������ �غ�!!";
				}
				else
				{
					gameTile.text = "<color=blue>'��������?'</color> ������ �غ�!!";
				}
			}
		}
		else        // ����
		{
			if (missionRandNum == 3)
			{
				gameTile.text = "Get Ready to Start <color=blue>'Shall We Pick?'</color>!!";
			}
			else if (missionRandNum == 7)
			{
				gameTile.text = "Get Ready to Start <color=blue>'Shall We Ride?'</color>!!";
			}
			else                                        // 3�� 7���� ������ ������ �̼� ���ӵ�...
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

	public IEnumerator WaitFirst()            // ī��Ʈ
	{
		yield return new WaitForSeconds(1.0f);
		waitCount.text = "2";

		StartCoroutine(WaitSecond());
	}

	public IEnumerator WaitSecond()            // ī��Ʈ
	{
		yield return new WaitForSeconds(1.0f);
		waitCount.text = "1";

		StartCoroutine(WaitThird());
	}

	public IEnumerator WaitThird()            // ī��Ʈ(������)
	{
		yield return new WaitForSeconds(1.0f);

		etcCanvasObject.SetActive(false);

		if (missionRandNum == 3)           // �ް� ������ �ɷȴٸ�..
		{
			dalgonaCanvasObject.SetActive(true);
			thirdCanvasObject.SetActive(true);
		}
		else if (missionRandNum == 7)      // ��ٸ� ������ �ɷȴٸ�..
		{
			lineCanvasObject.SetActive(true);
			seventhCanvasObject.SetActive(true);
		}
		else                                        // 3�� 7���� ������ ������ �̼� ���ӵ�...
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
