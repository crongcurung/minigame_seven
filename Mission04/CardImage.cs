using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardImage : MonoBehaviour
{
    FourthMission fourthMission;  

    public int cardNum;             // 현재 이 스크립트를 가지고 있는 카드 이미지는 몇 번째 카드인가?
    public bool isRotate;   // 현재 이 스크립트를 가지고 있는 카드 이미지는 회전을 했었나?
    public bool isCorrect;

    int beforeNum;    // 방금 전(2 연속에서 첫 번째)에 눌렀던 카드의 번호가 몇 번인지 담는 변수

    int beforeI;
    int currentI;

	void OnEnable()
    {
        isRotate = false;
        isCorrect = false;
        fourthMission = GameObject.Find("FourthPanel").GetComponent<FourthMission>();
        gameObject.GetComponent<Button>().interactable = true;

    }

	void OnDisable()
	{
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

	public void CardButtonActive()            // 카드를 처음 눌렀을 때 실행(애니메이션)
    {
        isRotate = true;
        gameObject.GetComponent<Button>().interactable = false;


        for (int i = 0; i < fourthMission.imageList.Count; i++)
        {
            if (fourthMission.imageList[i].GetComponent<CardImage>() == this)
            {
                currentI = i;
            }

            if (fourthMission.imageList[i].GetComponent<CardImage>().isRotate == false || fourthMission.imageList[i].GetComponent<CardImage>() == this)
            {
                if (i == fourthMission.imageList.Count - 1)
                {
                    isCorrect = true;
                    return;     // 고정된 카드가 없다.
                }
            }
            else      // 이미 고정된 카드가 있다.
            {
                beforeNum = fourthMission.imageList[i].GetComponent<CardImage>().cardNum;
                // 이미 고정된 카드가 있으니, 방금 전에 눌렀던 번호를 일시적으로 담는다.
                beforeI = i;
                break;
            }
        }

        if (beforeNum > 8)            // 방금 전에 눌렀던 번호가 9 이상이라면
        {
            beforeNum = beforeNum - 9;  // 9를 빼줘 숫자를 맞춘다.
        }
        if (cardNum > 8)             // 현재 누른 번호가 9 이상이라면
        {
            cardNum = cardNum - 9;    // 9를 뺴줘 숫자를 맞춘다.
        }
        if (beforeNum == cardNum)     // 방금 전에 누른 것이랑 현재 번호가 같다면 정답 처리
        {
            Debug.Log("정답");

            fourthMission.currentCount++;
            isCorrect = true;      
            fourthMission.imageList[beforeI].GetComponent<CardImage>().isCorrect = true;   
            isRotate = false;    
            fourthMission.imageList[beforeI].GetComponent<CardImage>().isRotate = false;  

            return;
        }
        else   // 방금 전에 누른 것이랑 현재 번호가 틀리다면
        {
            Debug.Log("틀렸습니다.");
            isRotate = false;   
            fourthMission.imageList[beforeI].GetComponent<CardImage>().isRotate = false;
        }
    }


    public void CardRotateAfter()    // 다시 뒤집을때, 마지막 부분(애니메이션)
    {
        
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void CardBackRotate()     // 처음 돌릴 때, 마지막 부분(애니메이션)
    {
        if (isCorrect == true)   // 고정 시키기 위해
        {
            isCorrect = false;   // 다음 카드를 위해 초기화

            if (beforeNum == cardNum)
            {
                fourthMission.GetComponent<AudioSource>().Play();   
            }
            return;
        }


        fourthMission.imageList[beforeI].GetComponent<Animator>().SetBool("CardClick", false);     // 전 카드 다시 뒤집기
        gameObject.GetComponent<Animator>().SetBool("CardClick", false);                           // 현재 카드 다시 뒤집기
    }


    public void CardChange01()     // 처음 돌릴 때, 중간 부분(애니메이션)
    {
        gameObject.GetComponent<Image>().sprite = fourthMission.saveCardSpriteList[cardNum];
    }

    public void CardChange02()     // 되돌아올 때, 중간 부분(애니메이션)
    {
        gameObject.GetComponent<Image>().sprite = fourthMission.backCardPrefabs;
    }


    public void Press_CardImage()         // 카드를 누른다면...
    {
        AudioManager.instance.Mission_04_CardSlide();           // 카드 넘기는 효과음

        gameObject.GetComponent<Animator>().SetBool("CardClick", true);
        gameObject.GetComponent<Button>().interactable = false;
    }

}
