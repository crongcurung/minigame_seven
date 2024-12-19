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



	string clickNumText;       // 누른 숫자를 받아오는 변수
	int clickNum;              // 누른 숫자를 받아오는 변수

	SecondMission secondMission;

	void Start()
	{
		secondMission = GameObject.Find("SecondPanel").GetComponent<SecondMission>();
		// 세컨드 미션을 받아오기
	}

	public void Press_Bubble()
	{
		clickNumText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
		// 누른 버블의 자식을 둘러보다 숫자를 받아온다.
		clickNum = int.Parse(clickNumText);  

		secondMission.sortNum.Add(clickNum);    // 내가 누른 숫자의 정보를 넘김
		

		secondMission.bubbleCount++;    // 현재 버블이 몇개 터졌는지 카운트하는 변수에 1를 더함
		secondMission.JudgeCheck();     // 방금 누른 버블이 순서대로 눌렀는지 확인하는 함수



		//Destroy(this.gameObject);   // 눌렀던 버블을 지운다.

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
