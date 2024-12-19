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

    public Sprite backCardPrefabs;                           // 카드 뒷면 이미지 프리팹
    public List<Sprite> cardPrefabsList = new List<Sprite>();   // 카드 이미지 프리팹을 담는 리스트

    public List<Image> imageList = new List<Image>();  // 이미 패널에 있는 카드(비어있는 카드) 위치
    List<Image> copyImageList = new List<Image>();     // 위에꺼를 카피한 리스트

    public GameObject cardWaitPanel;            // 처음 시작하면 버튼 누르는 것을 막을 패널

    public List<Sprite> saveCardSpriteList = new List<Sprite>();   // 처음에 나왔던 카드를 저장할 리스트
    public List<string> saveImageNameList = new List<string>();    // 카드가 위치한 오브젝트의 이름을 저장할 리스트
    public List<int> saveCardNumList = new List<int>();      // 다 섞인 카드가 어느 지점에 저장되어 있는지 알아보는 리스트(스프라이트의 번호를 생각하여 저장)

    int randImageNum;   // 카드를 어느 위치로 보낼 지, 랜덤으로 뽑는 변수

    public int currentCount;      // 현재까지 몇개(몇 쌍) 맞췄냐는 변수
    bool isEnd;

    void OnEnable()
    {
        currentCount = 0;
        copyImageList.Clear();
        saveCardSpriteList.Clear();
        saveImageNameList.Clear();
        saveCardNumList.Clear();

        isEnd = false;

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)      // 한국어..
        {
            alarmText.text = "카드를 확인 해주세요!";
        }
        else         // 영어..
        {
            alarmText.text = "Please Check These Card!";
        }
        slider.maxValue = 3.0f;                  // 첫 번째 카드 확인용 시간
        slider.value = 3.0f;
        slider.GetComponentInChildren<Image>().color = Color.black;    // 첫 번째 시간은 검정색으로!


        for (int i = 0; i < 9; i++)
        {
            saveCardNumList.Add(i);              // 0부터 8까지(총 9개) 숫자를 저장
        }

        for (int i = 0; i < 9; i++)
        {
            saveCardNumList.Add(i);              // 0부터 8까지(총 9개) 숫자를 저장, 그래서 총 18개 0 ~ 8, 0 ~ 8를 저장
            cardPrefabsList.Add(cardPrefabsList[i]);   // 카드 프리팹 리스트도 자신 안에 있던 내용물을 또 저장해서 0 ~ 8번, 0 ~ 8번으로 저장
        }

        for (int i = 0; i < 18; i++)
        {
            copyImageList.Add(imageList[i]);  // 비어있는 카드 리스트를 18까지 늘린다.
        }
        

        for (int i = 0; i < 18; i++)
        {
            randImageNum = UnityEngine.Random.Range(0, copyImageList.Count);  // 0부터 7까지 랜덤으로 뽑는다.

            copyImageList[randImageNum].gameObject.GetComponent<CardImage>().cardNum = i;   // 랜덤으로 뽑은 각 비어있는 리스트에 몇 번인지 번호 부여
            copyImageList[randImageNum].sprite = cardPrefabsList[i];          // 랜덤으로 뽑은 각 비어있는 리스트에 카드프리팹(번호 있음)을 넣음

            saveImageNameList.Add(copyImageList[randImageNum].gameObject.name);  // 랜덤으로 뽑은 각 비어있는 리스트를 가진 오브젝트의 이름을 리스트에 담는다.
            saveCardSpriteList.Add(cardPrefabsList[i]);      // 카드프리팹을 저장할 리스트에 카드프리팹을 저장(순서대로 해야 함)

            copyImageList.RemoveAt(randImageNum);    // 중복 방지
        }

        StartCoroutine(StartWait());  // 처음에 몇 초간 보여주기 다음으로 보여줘야하기 때문에 코루틴 함수를 씀
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
                AudioManager.instance.Mission_01_Fail_Effect();       // 시간 오버 효과음
                for (int i = 0; i < imageList.Count; i++)
				{
                    imageList[i].gameObject.GetComponent<Button>().interactable = false;

                }

                MissionManager.instance.failCount++;
                MissionContinue();
            }
        }
        
        if(currentCount > 8.5 && isEnd == false) // 성공시
        {
            isEnd = true;

            MissionManager.instance.succeseCount++;
            MissionContinue();
        }
    }


	IEnumerator StartWait()  // 처음에 몇 초간 보여주고 진짜 게임이 시작되는 순간임
    {
        cardWaitPanel.SetActive(true);  // 지연 시간 전에 버튼 누르는 것을 막을 패널을 실행함

		yield return new WaitForSeconds(3.0f);

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)      // 한국어..
        {
            alarmText.text = "카드를 뒤집어, 같은 카드를 찾아주세요.";
        }
        else         // 영어..
        {
            alarmText.text = "Turn Over the Cards and Find the Same Card.";
        }
        slider.maxValue = 30.0f;
        slider.value = 30;                                           // 두 번째, 게임 시작 시간
        slider.GetComponentInChildren<Image>().color = Color.red;    // 두 번째 시간은 빨강색으로!

        for (int i = 0; i < imageList.Count; i++)            // 
        {
            imageList[i].sprite = backCardPrefabs;           // 비어있는 카드 리스트에 카드 뒷면을 넣는다.
        }

        cardWaitPanel.SetActive(false);  // 진짜 게임 시작이기 때문에 패널을 없앰
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
