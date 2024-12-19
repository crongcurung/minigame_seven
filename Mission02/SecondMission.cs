using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondMission : MonoBehaviour
{
    public Slider slider;             // 시간 슬라이더
    public TextMeshProUGUI testText;   // 작은 순으로 할지, 큰 순으로 할지 알려주는 텍스트

    int rand;
    List<int> listInt = new List<int>();     // 1부터 99까지의 숫자를 일단 담은 리스트
    List<int> sortInt = new List<int>();

    List<GameObject> bubble = new List<GameObject>();         // 버블 프리팹을 담는 리스트(총 9개)

    public List<GameObject> Emp = new List<GameObject>();     // 버블을 넣을 공간(총 15개)
    List<GameObject> copyEmp = new List<GameObject>();        // 비활성화에서 활성화 될 때 리스트에 남는 문제가 있어서 따로 리스트를 만들어 여기다가 넣는다.

    public GameObject bubblePrefab;                        // 버블 프리팹
    public Sprite bubble_Errer;

    Vector3 bubbleScale;                            // 버블의 크기를 받는 변수

    int textRand;   // 작은 순으로 할지, 큰 순으로 할지 랜덤으로 정하기

    List<int> sortBubbleNum = new List<int>();    // 생성된 버블의 숫자를 이쪽으로 넘김
    public List<int> sortNum = new List<int>();  // 버블이 터질때 이쪽으로 터진 버블의 숫자값을 넘김

    public int bubbleCount;  // 현재 버블이 몇개 터졌냐?

    bool isEnd;   // 버블을 다 눌렀냐?

    bool finalCheck;
    public bool judgeCheck;


    void OnEnable()
    {
        isEnd = false;           // 일단 버블을 다 안눌렀으니, false
        bubbleCount = 0;          // 몇개 터졌냐도 초기화
        finalCheck = false;
        judgeCheck = true;

        slider.maxValue = 10.0f;
        slider.value = 10.0f;

        copyEmp.Clear();           // 리스트 초기화
        listInt.Clear();           
        sortInt.Clear();
        sortBubbleNum.Clear();    // 버블에 들어가는 순자를 담는 리스트 초기화
        sortNum.Clear();           // 버블이 터졌을 때 숫자를 담는 리스트 초기화

        for (int i = 0; i < Emp.Count; i++)       // 대신 써줄 리스트에다가 공간을 집어넣는다.(총 15개)
        {
            copyEmp.Add(Emp[i]);            
        }

        bubbleScale = bubblePrefab.transform.localScale;       // 버블의 스케일을 일단 버블 프리팹껄로 한다.


        for (int i = 1; i < 100; i++)        // 버블에 들어가는 숫자는 1부터 99까지임.
        {
            listInt.Add(i);           
        }

        for (int i = 0; i < 9; i++)   // 9인 이유는 실제 버블이 나오는 곳이 9곳이기 때문에..
        {
            rand = UnityEngine.Random.Range(0, listInt.Count);   // 0부터 99번까지

            sortInt.Add(listInt[rand]);   // 0부터 99번까지에서 9개를 뽑아서 sortInt 리스트로 담아둔다.

            listInt.RemoveAt(rand);    // 중복 방지 listInt 리스트로
        }
        Random_Text();     // 큰순으로 갈지, 작은순으로 갈지 해보기
        Create_Circle();    // 버블 창조 함수

    }

	void OnDisable()
	{
        for (int i = 0; i < Emp.Count; i++)       
        {
            if (Emp[i].transform.childCount == 1)
            {
                Destroy(Emp[i].transform.GetChild(0).gameObject);
            }
        }
    }

	void Update()
    {
		
		if (bubbleCount > 8.5 && isEnd == false)      // 버블이 9개 터졌다(다 끝났다)
        {
            Sort_Num();                       // 정답이 맞는지 틀렸는지 판단
        }

        if (isEnd == false)                // 끝났다면 시간 안 셈
        {
            if (slider.value > 0)                    
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                if (finalCheck == false)
                {
                    AudioManager.instance.Mission_01_Fail_Effect();       // 시간 오버 효과음
                    slider.value = 0;
                    finalCheck = true;
                    MissionManager.instance.failCount++;
                    LateBubbleCheck();
                    MissionContinue();
                }
            }
        }
    }

    public void JudgeCheck()
    {
        if (judgeCheck == false)
        {
            AudioManager.instance.Mission_02_Fail_Effect();            // 미션 실패 효과음
            return;
        }

        if (sortNum[bubbleCount - 1] != sortBubbleNum[bubbleCount - 1])        // 누른 버블이 순서에 맞지 않았다면...
        {
            judgeCheck = false;

            AudioManager.instance.Mission_02_Fail_Effect();            // 미션 실패 효과음
            for (int i = 0; i < Emp.Count; i++)
            {
                if (Emp[i].transform.childCount == 1)
                {
                    //Emp[i].transform.GetChild(0).GetComponent<Image>().color = Color.yellow;        // 틀렸을 경우 남아 있는 버블 전부 노란색으로...

                    
                    Emp[i].transform.GetChild(0).GetComponent<Image>().sprite = bubble_Errer;
                }
            }
        }
        else                // 누른 버블이 순서와 맞다면
        {
            AudioManager.instance.Mission_02_Success_Effect();            // 미션 성공 효과음
        }
    }

    public void Random_Text()
    {
        textRand = UnityEngine.Random.Range(0, 2);     // 작은순, 큰순 텍스트


        if (DataManager.instance.nowPlayer.KorOrEngBool == true)      // 한국어..
        {
            if (textRand < 0.5f)      // 0이라면 작은순으로
            {
                testText.text = "<color=#FF0000>작은 순</color>으로 터치하시오.";
            }
            else                       // 0이라면 큰순으로
            {
                testText.text = "<color=#FF0000>큰 순</color>으로 터치하시오.";
            }
        }
        else              // 영어..
        {
            if (textRand < 0.5f)      // 0이라면 작은순으로
            {
                testText.text = "Touch it in <color=#FF0000>Small Order</color>.";
            }
            else                       // 0이라면 큰순으로
            {
                testText.text = "Touch it in <color=#FF0000>High Order</color>.";
            }
        }
    }


    public void Create_Circle()          // 버블 생성 함수
    {
        //GameObject[] bubble = new GameObject[9];
        
        bubble.Clear();

        for (int i = 0; i < 9; i++)
        {
            float randFloat = UnityEngine.Random.Range(1.2f, 2.2f);     // 버블의 사이즈를 만들 랜덤 변수
            bubbleScale.x = 1.0f;
            bubbleScale.y = 1.0f;
            bubbleScale.x = bubbleScale.x * randFloat;
            bubbleScale.y = bubbleScale.y * randFloat;

            bubble.Add(bubblePrefab);                                // 버블을 담는 리스트에 프리팹 담기
            bubble[i].transform.localScale = bubbleScale;            // 담은 프리팹들의 사이즈를 랜덤으로 뽑은 사이즈를 넣기

            bubble[i].GetComponentInChildren<TextMeshProUGUI>().text = sortInt[i].ToString();  
            // 버블에 들어있는 각 텍스트에 뽑아 놓은 숫자를 집어 넣는다.

            sortBubbleNum.Add(sortInt[i]);   // 위에 집어넣은 숫자를 리스트에 순서에 맞게 집어 넣는다.

            int randEmpty = UnityEngine.Random.Range(0, copyEmp.Count);  //위치를 랜덤으로 뽑는 변수

            Instantiate(bubble[i], copyEmp[randEmpty].transform.position, Quaternion.identity, copyEmp[randEmpty].transform);
            // 위치를 담았던 리스트를 랜덤으로 뽑아서 위치를 가져온 후, 버블을 담은 리스트를 가져와서 자식으로 심는다.

            copyEmp.RemoveAt(randEmpty);   // 중복 방지
        }

        

        if (textRand < 0.5f)              // 큰순, 작은순 랜덤 변수가 0이라면(작은 순)
        {
            sortBubbleNum.Sort();         // 정렬(작은 순)
        }
        else                                // 큰순, 작은순 랜덤 변수가 1이라면(큰 순)
        {
            sortBubbleNum.Sort();         // 정렬(작은 순) 후
            sortBubbleNum.Reverse();      // 역순(큰 순)
        }
    }

    public void Sort_Num()
    {
        isEnd = true;
        

        for (int i = 0; i < 9; i++)        
        {
            if (sortNum[i] != sortBubbleNum[i])          // 정렬한(큰 순이든, 작은 순이든) 리스트와 내가 누른 리스트가 다르다면
            {
                Debug.Log("틀렸습니다.");
                
                MissionManager.instance.failCount++;
                MissionContinue();
                return;
            }
        }

        Debug.Log("정답");                               // 정렬한(큰 순이든, 작은 순이든) 리스트와 내가 누른 리스트가 같다면
        
        MissionManager.instance.succeseCount++;


        MissionContinue();
    }

    public void LateBubbleCheck()
    {
        for (int i = 0; i < Emp.Count; i++)
        {
            if (Emp[i].transform.childCount == 1)
            {
                Emp[i].transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
                Emp[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
            }
        }
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
