using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeventhMission : MonoBehaviour
{
	public Slider slider;
	public GameObject outLinePanel;                // ó���� �����Ҷ� �ƹ� ��ư�� �� ������ �ϴ� �г�
	public GameObject lineOutLinePanel;            // ���θ� ����� �г�
	public TextMeshProUGUI testText;               // ���� ���� �ؽ�Ʈ

	public List<Button> clickButton = new List<Button>();              // ���� ���ĺ� ��ư(������ ������)
	public List<Image> goalButton = new List<Image>();                 // �Ʒ� ���� �̹���(���� ����)

	public List<GameObject> verseList01 = new List<GameObject>();      // ���� 1������ ���������� �� �ִ� ��ٸ�
	public List<GameObject> verseList02 = new List<GameObject>();      // ���� 2������ ���������� �� �ִ� ��ٸ�
	public List<GameObject> verseList03 = new List<GameObject>();      // ���� 3������ ���������� �� �ִ� ��ٸ�
	public List<GameObject> verseList04 = new List<GameObject>();      // ���� 4������ ���������� �� �ִ� ��ٸ�

	List<GameObject> copyVerseList01 = new List<GameObject>();      // ���� 1������ ���������� �� �ִ� ��ٸ�
	List<GameObject> copyVerseList02 = new List<GameObject>();      // ���� 2������ ���������� �� �ִ� ��ٸ�
	List<GameObject> copyVerseList03 = new List<GameObject>();      // ���� 3������ ���������� �� �ִ� ��ٸ�
	List<GameObject> copyVerseList04 = new List<GameObject>();      // ���� 4������ ���������� �� �ִ� ��ٸ�


	int rand01;       // ���� 1������ ��ٸ��� � ���� ������ ���� ���� ����
	int rand02;       // ���� 2������ ��ٸ��� � ���� ������ ���� ���� ����
	int rand03;       // ���� 3������ ��ٸ��� � ���� ������ ���� ���� ����  
	int rand04;       // ���� 4������ ��ٸ��� � ���� ������ ���� ���� ����

	int currentRand;             // ������ ��� ���� ����
	Color originalColor;         // ���� ��ư �÷��� �޴� ����

	bool isEnd = false;        // ��ư�� �Ʒ��� �� �����Դ��� Ȯ���ϴ� ����

	void OnEnable()
	{
		SeventhManager.hitNum = 0;

		copyVerseList01.Clear();      // ī�� ��ٸ� ����Ʈ �ʱ�ȭ
		copyVerseList02.Clear();
		copyVerseList03.Clear();
		copyVerseList04.Clear();

		SeventhManager.isClick = false;
		isEnd = false;                 // ��ư�� �Ʒ��� �� �����Դ��� Ȯ���ϴ� ���� �ʱ�ȭ
		currentRand = 0;             // ������ ��� ���� ����

		originalColor = goalButton[0].color;  // ���� ��ư ������ �޴´�.

		for (int i = 0; i < 5; i++)       // ��ٸ� ���� ��Ȱ��ȭ
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

		rand01 = UnityEngine.Random.Range(1, 6);   // ���� 1������ ��ٵ� � ���� ���ΰ�(�ּ� 1��, �ִ� 5��)
		rand02 = UnityEngine.Random.Range(1, 6);   // ���� 2������ ��ٵ� � ���� ���ΰ�(�ּ� 1��, �ִ� 5��)
		rand03 = UnityEngine.Random.Range(1, 6);   // ���� 3������ ��ٵ� � ���� ���ΰ�(�ּ� 1��, �ִ� 5��)
		rand04 = UnityEngine.Random.Range(1, 6);   // ���� 4������ ��ٵ� � ���� ���ΰ�(�ּ� 1��, �ִ� 5��)

		for (int i = 0; i < rand01; i++)                // ���� ���ڴ�� ��ٸ��� �����.
		{
			int rand01 = UnityEngine.Random.Range(0, copyVerseList01.Count);      // ���� ���� ���ڿ��� �������� ��� ��ٸ��� �������� ��� ���� ���� 
			copyVerseList01[rand01].SetActive(true);
			copyVerseList01.RemoveAt(rand01);  // �ߺ�����
		}

		for (int i = 0; i < rand02; i++)                // ���� ���ڴ�� ��ٸ��� �����.
		{
			int rand02 = UnityEngine.Random.Range(0, copyVerseList02.Count);      // ���� ���� ���ڿ��� �������� ��� ��ٸ��� �������� ��� ���� ����
			copyVerseList02[rand02].SetActive(true);
			copyVerseList02.RemoveAt(rand02);  // �ߺ�����
		}

		for (int i = 0; i < rand03; i++)                // ���� ���ڴ�� ��ٸ��� �����.
		{
			int rand03 = UnityEngine.Random.Range(0, copyVerseList03.Count);      // ���� ���� ���ڿ��� �������� ��� ��ٸ��� �������� ��� ���� ����
			copyVerseList03[rand03].SetActive(true);
			copyVerseList03.RemoveAt(rand03);  // �ߺ�����
		}

		for (int i = 0; i < rand04; i++)                // ���� ���ڴ�� ��ٸ��� �����.
		{
			int rand04 = UnityEngine.Random.Range(0, copyVerseList04.Count);       // ���� ���� ���ڿ��� �������� ��� ��ٸ��� �������� ��� ���� ����
			copyVerseList04[rand04].SetActive(true);
			copyVerseList04.RemoveAt(rand04);  // �ߺ�����
		}

		StartCoroutine(StartWait());
	}

	void OnDisable()
	{
		SeventhManager.hitNum = 0;

		for (int i = 0; i < 5; i++)       // ��ٸ� ���� ��Ȱ��ȭ
		{
			goalButton[i].color = originalColor;  // �÷��� ������� �صα�
			clickButton[i].interactable = true;
		}
	}


	void Update()
	{
		if (SeventhManager.isClick == true)         // ��ư�� Ŭ���ߴ���
		{
			for (int i = 0; i < 5; i++)
			{
				clickButton[i].interactable = false;        // ��ư�� Ŭ�������� ��ư ���θ� ��Ȱ��ȭ ��Ŵ
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
				AudioManager.instance.Mission_01_Fail_Effect();       // �ð� ���� ȿ����

				for (int i = 0; i < 5; i++)
				{
					clickButton[i].interactable = false;        // �ð��� �� ������, ��ư ���θ� ��Ȱ��ȭ ��Ŵ
				}

				slider.value = 0;
				MissionManager.instance.failCount++;
				MissionContinue();
			}
		}


		if (SeventhManager.hitNum > 0 && isEnd == false)  // hitNum�� 0�̻��̶� ���� �̹����� ��Ҵٴ� ��
		{
			if (SeventhManager.hitNum == currentRand)        // hitNum�� ����� ���ٸ�
			{
				isEnd = true;          // ���� ��
				Debug.Log("����");

				AudioManager.instance.Mission_01_Success_Effect();     // ���� ȿ����

				SeventhManager.hitNum = 0;

				MissionManager.instance.succeseCount++;
				MissionContinue();
				return;
			}
			else               // hitNum�� ����� �ٸ��ٸ�
			{
				isEnd = true;
				Debug.Log("Ʋ�Ƚ��ϴ�.");

				AudioManager.instance.Mission_01_Fail_Effect();     // ���� ȿ����

				SeventhManager.hitNum = 0;

				MissionManager.instance.failCount++;
				MissionContinue();   // 3�� �� �� ���� ������
				return;
			}
		}

		
	}


	IEnumerator StartWait()
	{
		yield return new WaitForSeconds(10.0f);        // ��ٸ��� �������� 10�ʰ� ������...

		currentRand = UnityEngine.Random.Range(1, 6);

		outLinePanel.SetActive(false);                      // ��ü �г��� �����
		lineOutLinePanel.SetActive(true);                  //
		goalButton[currentRand - 1].color = Color.red;

		if (DataManager.instance.nowPlayer.KorOrEngBool == true)    // �ѱ���..
		{
			testText.text = $"<color=#FF0000>{currentRand}��</color> �������� ���� ��ٸ��� ��������!";
		}
		else     // ����..
		{
			testText.text = $"Press Ladder to Area <color=#FF0000>{currentRand}</color>!";
		}
		slider.maxValue = 10.0f;
		slider.value = 10;
		slider.GetComponentInChildren<Image>().color = Color.red;    // ������� �ٷ� ������ �̹����� �������� �ٲ�

		StartCoroutine(AgainWait());
	}

	IEnumerator AgainWait()    // 10�� ���� ��ư�� ���� ��ȸ�� �ִ� �ڷ�ƾ �Լ�
	{
		yield return new WaitForSeconds(10.0f);  
		lineOutLinePanel.SetActive(false);          // ���θ� ������ �г� Ȱ��ȭ
	}



	void MissionContinue()        // �� �̼��� ������ �� ó��
	{
		StartCoroutine(EndGameWait());
	}

	public IEnumerator EndGameWait()      // 3�� �� �� ���� ������
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
