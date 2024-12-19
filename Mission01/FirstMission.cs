using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[Serializable]
public class TestVar      // FirstMission JSON 파일의 변수들
{
    public string testText;    // 문제
    public string num01;       // 1번 답(정답)
    public string num02;       // 2번 답
    public string num03;       // 3번 답
    public string num04;       // 4번 답
}
[Serializable]
public class TestData   // FirstMission JSon 파일을 담는 리스트
{
    public List<TestVar> testData = new List<TestVar>();
}




public class FirstMission : MonoBehaviour
{
    public Slider slider;          // 시간 슬라이더
    public TextMeshProUGUI textText;    // 문제를 담는 텍스트
    public TextMeshProUGUI[] answer;    // 버튼 안에 담긴 답을 담는 텍스트(총 4개)

    bool isClick;                       // 버튼을 눌렀냐?(답을 찍었냐?) 라는 것. 결국 게임이 끝났다는 뜻
    TestData data;                    // JSON 파일을 담을 변수
    int corretNum = -1;                  // 정답인 버튼이 몇번인지 담는 변수(-1로 초기화)

    Color extistingColor = Color.white;      // 버튼을 색깔을 다시 흰색으로 되돌릴 색깔 변수

    bool finalCheck;



	void OnEnable()                    // 미션 게임은 활성, 비활성으로 하기 때문에 Start 함수 안 씀
    {
        slider.maxValue = 7;
        slider.value = 7;

        finalCheck = false;

        isClick = false;          // 처음에는 클릭이 안되어 있다고 초기화

        After_Press();         // 버튼 클릭후 색깔을 원래대로 되돌리는 거지만, 첫판부터 해도 됨.

        int randNum;           // json 파일에 있는 문제를 랜덤으로 뽑을 숫자의 변수
        List<int> randInt = new List<int>() { 0, 1, 2, 3 };        //일단 리스트 선언 후, 0번부터 3번까지(총 4개) 담은 리스트
        int rand1;                                                 // 
        int rand2;                                                 // 

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)
        {
            TextAsset textAsset1 = Resources.Load<TextAsset>($"Data/FirstMission");      // 제이슨 로드
            data = JsonUtility.FromJson<TestData>(textAsset1.text);                      // 제이슨 로드
        }
        else
        {
            TextAsset textAsset1 = Resources.Load<TextAsset>($"Data/FirstMission_En");      // 제이슨 로드
            data = JsonUtility.FromJson<TestData>(textAsset1.text);                      // 제이슨 로드
        }

        randNum = UnityEngine.Random.Range(0, data.testData.Count);                   // 랜덤 실행


        List<string> dataList = new List<string>() { data.testData[randNum].num01, data.testData[randNum].num02, data.testData[randNum].num03, data.testData[randNum].num04 };
        // 랜덤으로 뽑은 숫자를 json 파일 리스트에 보내, 답을 뽑아온다. 그리고 (답)리스트에 담는다.

        textText.text = data.testData[randNum].testText; //뽑아온 문제를 텍스트로 보낸다. 

        for (int i = 0; i < 4; i++)         // 답은 4개이고, 이를 또 랜덤으로 바꿔야하기 때문에 이런 작업을 한다.
        {
            rand1 = UnityEngine.Random.Range(0, randInt.Count); // 총 4개
            rand2 = UnityEngine.Random.Range(0, dataList.Count); // 총 4개
            answer[randInt[rand1]].text = dataList[rand2];       // 일단 랜덤으로 돌린 답을 answer 리스트에 담아둔다.

            if (dataList[rand2] == data.testData[randNum].num01)   // 정답인 답을 확인하고 저장한다.
            {
                corretNum = randInt[rand1];           // 몇번이 정답인지 저장한다.
            }

            randInt.RemoveAt(rand1);       // 중복 방지
            dataList.RemoveAt(rand2);      // 중복 방지
        }
    }

	void Update()
	{
        if (isClick == false)         // 버튼을 클릭하면 슬라이더가 동작을 안하도록 한다.
        {
            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;       // 줄인다.
            }
            else
            {
                if (finalCheck == false)
                {
                    AudioManager.instance.Mission_01_Fail_Effect();       // 시간 오버 효과음

                    finalCheck = true;
                    for (int i = 0; i < 4; i++)
                    {
                        answer[i].GetComponentInParent<Button>().interactable = false;
                    }
                    slider.value = 0;
                    answer[this.corretNum].GetComponentInParent<Image>().color = Color.blue;   // 정답은 버튼을 파란색 처리!
                    MissionManager.instance.failCount++;
                    MissionContinue();
                }
            }
        }
        else                            // 버튼을 클릭하면 모든 버튼이 비활성화 된다.
        {
            for (int i = 0; i < 4; i++)
            {
                answer[i].GetComponentInParent<Button>().interactable = false;
            }
        }
	}

	public void Press_Button(int corretNum)       // 버튼을 눌렀을 경우.. 버튼에 OnClick으로 집어 넣었다.(파라미터는 각 버튼의 번호를 담았다.)
    {
        isClick = true;                        // 버튼을 눌렀을 경우이기 때문에 바로 true를 넣어버렸다.
        extistingColor = answer[this.corretNum].GetComponentInParent<Image>().color;    // 일단 기존 색깔을 넣는거였는데, 없애버릴까 생각중

        if (this.corretNum == corretNum)     // 만약 정답 번호랑 버튼에 담긴 번호가 같다면!
        {
            Debug.Log("정답");
            AudioManager.instance.Mission_01_Success_Effect();            // 미션 성공 효과음
            answer[this.corretNum].GetComponentInParent<Image>().color = Color.blue;   // 정답은 버튼을 파란색 처리!

            MissionManager.instance.succeseCount++;
            
        }
        else    // 만약 정답 번호랑 버튼에 담긴 번호가 틀리다면!
        {
            Debug.Log("틀렸습니다.");
            AudioManager.instance.Mission_01_Fail_Effect();            // 미션 실패 효과음
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.red;   // 내가 누른 버튼을 빨강색 처리!
            //answer[this.corretNum].GetComponentInParent<Image>().color = Color.blue;   // 정답은 버튼을 파란색 처리!

            MissionManager.instance.failCount++;
        }

        MissionContinue();

        return;
    }

    void After_Press()  // 비활성화 시키고 다시 활성화하면 버튼의 색깔이 담겨져서.. 초기화하는 함수임.
    {
        for (int i = 0; i < 4; i++)
        {
            answer[i].GetComponentInParent<Image>().color = extistingColor;
            answer[i].GetComponentInParent<Button>().interactable = true;
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
