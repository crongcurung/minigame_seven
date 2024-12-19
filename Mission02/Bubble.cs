using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Bubble : MonoBehaviour
{
	public Sprite endBubble_1;
	public Sprite endBubble_2;
	public Sprite endBubble_3;
	public Sprite endBubble_4;
	public Sprite endBubble_5;



	string clickNumText;       // ���� ���ڸ� �޾ƿ��� ����
	int clickNum;              // ���� ���ڸ� �޾ƿ��� ����

	SecondMission secondMission;

	void Start()
	{
		secondMission = GameObject.Find("SecondPanel").GetComponent<SecondMission>();
		// ������ �̼��� �޾ƿ���
	}

	public void Press_Bubble()
	{
		clickNumText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
		// ���� ������ �ڽ��� �ѷ����� ���ڸ� �޾ƿ´�.
		clickNum = int.Parse(clickNumText);  

		secondMission.sortNum.Add(clickNum);    // ���� ���� ������ ������ �ѱ�
		

		secondMission.bubbleCount++;    // ���� ������ � �������� ī��Ʈ�ϴ� ������ 1�� ����
		secondMission.JudgeCheck();     // ��� ���� ������ ������� �������� Ȯ���ϴ� �Լ�



		//Destroy(this.gameObject);   // ������ ������ �����.

		GetComponent<Button>().interactable = false;
		StartCoroutine("EndCoroutine");
	}

	IEnumerator EndCoroutine()
	{

		GetComponent<Image>().sprite = endBubble_5;
		yield return new WaitForSeconds(0.3f);

		Destroy(this.gameObject);
	}
}
