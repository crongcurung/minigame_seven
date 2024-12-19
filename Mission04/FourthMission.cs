using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FourthMission : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI alarmText;

    public Sprite backCardPrefabs;                           // ī�� �޸� �̹��� ������
    public List<Sprite> cardPrefabsList = new List<Sprite>();   // ī�� �̹��� �������� ��� ����Ʈ

    public List<Image> imageList = new List<Image>();  // �̹� �гο� �ִ� ī��(����ִ� ī��) ��ġ
    List<Image> copyImageList = new List<Image>();     // �������� ī���� ����Ʈ

    public GameObject cardWaitPanel;            // ó�� �����ϸ� ��ư ������ ���� ���� �г�

    public List<Sprite> saveCardSpriteList = new List<Sprite>();   // ó���� ���Դ� ī�带 ������ ����Ʈ
    public List<string> saveImageNameList = new List<string>();    // ī�尡 ��ġ�� ������Ʈ�� �̸��� ������ ����Ʈ
    public List<int> saveCardNumList = new List<int>();      // �� ���� ī�尡 ��� ������ ����Ǿ� �ִ��� �˾ƺ��� ����Ʈ(��������Ʈ�� ��ȣ�� �����Ͽ� ����)

    int randImageNum;   // ī�带 ��� ��ġ�� ���� ��, �������� �̴� ����

    public int currentCount;      // ������� �(�� ��) ����Ĵ� ����
    bool isEnd;

    void OnEnable()
    {
        currentCount = 0;
        copyImageList.Clear();
        saveCardSpriteList.Clear();
        saveImageNameList.Clear();
        saveCardNumList.Clear();

        isEnd = false;

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)      // �ѱ���..
        {
            alarmText.text = "ī�带 Ȯ�� ���ּ���!";
        }
        else         // ����..
        {
            alarmText.text = "Please Check These Card!";
        }
        slider.maxValue = 3.0f;                  // ù ��° ī�� Ȯ�ο� �ð�
        slider.value = 3.0f;
        slider.GetComponentInChildren<Image>().color = Color.black;    // ù ��° �ð��� ����������!


        for (int i = 0; i < 9; i++)
        {
            saveCardNumList.Add(i);              // 0���� 8����(�� 9��) ���ڸ� ����
        }

        for (int i = 0; i < 9; i++)
        {
            saveCardNumList.Add(i);              // 0���� 8����(�� 9��) ���ڸ� ����, �׷��� �� 18�� 0 ~ 8, 0 ~ 8�� ����
            cardPrefabsList.Add(cardPrefabsList[i]);   // ī�� ������ ����Ʈ�� �ڽ� �ȿ� �ִ� ���빰�� �� �����ؼ� 0 ~ 8��, 0 ~ 8������ ����
        }

        for (int i = 0; i < 18; i++)
        {
            copyImageList.Add(imageList[i]);  // ����ִ� ī�� ����Ʈ�� 18���� �ø���.
        }
        

        for (int i = 0; i < 18; i++)
        {
            randImageNum = UnityEngine.Random.Range(0, copyImageList.Count);  // 0���� 7���� �������� �̴´�.

            copyImageList[randImageNum].gameObject.GetComponent<CardImage>().cardNum = i;   // �������� ���� �� ����ִ� ����Ʈ�� �� ������ ��ȣ �ο�
            copyImageList[randImageNum].sprite = cardPrefabsList[i];          // �������� ���� �� ����ִ� ����Ʈ�� ī��������(��ȣ ����)�� ����

            saveImageNameList.Add(copyImageList[randImageNum].gameObject.name);  // �������� ���� �� ����ִ� ����Ʈ�� ���� ������Ʈ�� �̸��� ����Ʈ�� ��´�.
            saveCardSpriteList.Add(cardPrefabsList[i]);      // ī���������� ������ ����Ʈ�� ī���������� ����(������� �ؾ� ��)

            copyImageList.RemoveAt(randImageNum);    // �ߺ� ����
        }

        StartCoroutine(StartWait());  // ó���� �� �ʰ� �����ֱ� �������� ��������ϱ� ������ �ڷ�ƾ �Լ��� ��
    }


	void Update()
	{
        if (currentCount < 9 && isEnd == false)
        {

            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                isEnd = true;
                slider.value = 0;
                AudioManager.instance.Mission_01_Fail_Effect();       // �ð� ���� ȿ����
                for (int i = 0; i < imageList.Count; i++)
				{
                    imageList[i].gameObject.GetComponent<Button>().interactable = false;

                }

                MissionManager.instance.failCount++;
                MissionContinue();
            }
        }
        
        if(currentCount > 8.5 && isEnd == false) // ������
        {
            isEnd = true;

            MissionManager.instance.succeseCount++;
            MissionContinue();
        }
    }


	IEnumerator StartWait()  // ó���� �� �ʰ� �����ְ� ��¥ ������ ���۵Ǵ� ������
    {
        cardWaitPanel.SetActive(true);  // ���� �ð� ���� ��ư ������ ���� ���� �г��� ������

		yield return new WaitForSeconds(3.0f);

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)      // �ѱ���..
        {
            alarmText.text = "ī�带 ������, ���� ī�带 ã���ּ���.";
        }
        else         // ����..
        {
            alarmText.text = "Turn Over the Cards and Find the Same Card.";
        }
        slider.maxValue = 30.0f;
        slider.value = 30;                                           // �� ��°, ���� ���� �ð�
        slider.GetComponentInChildren<Image>().color = Color.red;    // �� ��° �ð��� ����������!

        for (int i = 0; i < imageList.Count; i++)            // 
        {
            imageList[i].sprite = backCardPrefabs;           // ����ִ� ī�� ����Ʈ�� ī�� �޸��� �ִ´�.
        }

        cardWaitPanel.SetActive(false);  // ��¥ ���� �����̱� ������ �г��� ����
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
