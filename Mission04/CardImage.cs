using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardImage : MonoBehaviour
{
    FourthMission fourthMission;  

    public int cardNum;             // ���� �� ��ũ��Ʈ�� ������ �ִ� ī�� �̹����� �� ��° ī���ΰ�?
    public bool isRotate;   // ���� �� ��ũ��Ʈ�� ������ �ִ� ī�� �̹����� ȸ���� �߾���?
    public bool isCorrect;

    int beforeNum;    // ��� ��(2 ���ӿ��� ù ��°)�� ������ ī���� ��ȣ�� �� ������ ��� ����

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

	public void CardButtonActive()            // ī�带 ó�� ������ �� ����(�ִϸ��̼�)
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
                    return;     // ������ ī�尡 ����.
                }
            }
            else      // �̹� ������ ī�尡 �ִ�.
            {
                beforeNum = fourthMission.imageList[i].GetComponent<CardImage>().cardNum;
                // �̹� ������ ī�尡 ������, ��� ���� ������ ��ȣ�� �Ͻ������� ��´�.
                beforeI = i;
                break;
            }
        }

        if (beforeNum > 8)            // ��� ���� ������ ��ȣ�� 9 �̻��̶��
        {
            beforeNum = beforeNum - 9;  // 9�� ���� ���ڸ� �����.
        }
        if (cardNum > 8)             // ���� ���� ��ȣ�� 9 �̻��̶��
        {
            cardNum = cardNum - 9;    // 9�� ���� ���ڸ� �����.
        }
        if (beforeNum == cardNum)     // ��� ���� ���� ���̶� ���� ��ȣ�� ���ٸ� ���� ó��
        {
            Debug.Log("����");

            fourthMission.currentCount++;
            isCorrect = true;      
            fourthMission.imageList[beforeI].GetComponent<CardImage>().isCorrect = true;   
            isRotate = false;    
            fourthMission.imageList[beforeI].GetComponent<CardImage>().isRotate = false;  

            return;
        }
        else   // ��� ���� ���� ���̶� ���� ��ȣ�� Ʋ���ٸ�
        {
            Debug.Log("Ʋ�Ƚ��ϴ�.");
            isRotate = false;   
            fourthMission.imageList[beforeI].GetComponent<CardImage>().isRotate = false;
        }
    }


    public void CardRotateAfter()    // �ٽ� ��������, ������ �κ�(�ִϸ��̼�)
    {
        
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void CardBackRotate()     // ó�� ���� ��, ������ �κ�(�ִϸ��̼�)
    {
        if (isCorrect == true)   // ���� ��Ű�� ����
        {
            isCorrect = false;   // ���� ī�带 ���� �ʱ�ȭ

            if (beforeNum == cardNum)
            {
                fourthMission.GetComponent<AudioSource>().Play();   
            }
            return;
        }


        fourthMission.imageList[beforeI].GetComponent<Animator>().SetBool("CardClick", false);     // �� ī�� �ٽ� ������
        gameObject.GetComponent<Animator>().SetBool("CardClick", false);                           // ���� ī�� �ٽ� ������
    }


    public void CardChange01()     // ó�� ���� ��, �߰� �κ�(�ִϸ��̼�)
    {
        gameObject.GetComponent<Image>().sprite = fourthMission.saveCardSpriteList[cardNum];
    }

    public void CardChange02()     // �ǵ��ƿ� ��, �߰� �κ�(�ִϸ��̼�)
    {
        gameObject.GetComponent<Image>().sprite = fourthMission.backCardPrefabs;
    }


    public void Press_CardImage()         // ī�带 �����ٸ�...
    {
        AudioManager.instance.Mission_04_CardSlide();           // ī�� �ѱ�� ȿ����

        gameObject.GetComponent<Animator>().SetBool("CardClick", true);
        gameObject.GetComponent<Button>().interactable = false;
    }

}
