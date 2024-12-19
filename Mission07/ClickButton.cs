using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour   // 버튼에 부착되어 있음
{
    public bool isEnd;                   // 끝났는지 묻는 변수(버튼이 이미지에 도착하면 끝났다는 뜻임)
    public bool isHorizontal;          // 수평으로 갈 준비가 되어 있냐 묻는 변수

    public GameObject lineOutLinePanel;       // 라인만 가져주는 패널(버튼을 누르면 패널이 바로 없어지게 여기에도 가져왔다)

    Vector2 dir;        // 버튼의 위치(이동)을 받아줄 변수


	void OnEnable()
	{
        Application.targetFrameRate = 60;

    }

	void OnDisable()
	{
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        
    }

	public void Move()     // 버튼을 누르자마자 버튼을 출발
    {
        isEnd = false;         // 아직 버튼이 이미지에 도착하지 않았으니 false로 초기화
        isHorizontal = true;                      // 바로 내려가는 것이니, 수평이될 준비는 완료
        SeventhManager.isClick = true;                // 플레이어가 버튼을 클릭했으니 true로 변경

        AudioManager.instance.Mission_07_Click();     // 버튼 클릭 효과음

        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        if (lineOutLinePanel.activeSelf == true)        // 패널이 아직도 있는지 알아보고
        {
            lineOutLinePanel.SetActive(false);         // 패널이 있다면 비활성화 시킴
        }


        dir = Vector2.down;                   // 버튼을 내려보냄

        StartCoroutine(MoveCorutine());
    }




	IEnumerator MoveCorutine()     // 작은 시간초마다(사람 눈에는 안 보임) 아래로 이동 시킴
    {
        while (isEnd == false)         // 버튼이 이미지에 닿았다면 while문에서 빠져 나옴
        {
            gameObject.transform.Translate(dir * 50 * Time.deltaTime);          // 이 속도로 내려감

            yield return new WaitForSeconds(0.001f);
        }
    }

	public IEnumerator OnTriggerEnter2D(Collider2D collision)        // 버튼이 사다리랑 충돌했을 경우...
	{
        if (isHorizontal == false)                  // 수평으로 갈 준비가 되어 있지 않다면 버튼을 내리도록 함
        {
            yield return new WaitForSeconds(0.0005f);   // 오류가 있다면 여기를 보시오...
            isHorizontal = true;                    // 바로 수평으로 갈 준비가 되어 있다고 함

            AudioManager.instance.Mission_06_Click();     // 이동 효과음

            dir = Vector2.down;             // 버튼을 아래로 보냄
            yield break;
        }

        if (collision.name == "Left" && isHorizontal == true) // 수평으로 갈 준비가 되어 있고, 사다리가 왼쪽에 있다면..
        {
            yield return new WaitForSeconds(0.0005f);   // 오류가 있다면 여기를 보시오...
            isHorizontal = false;

            AudioManager.instance.Mission_06_Click();     // 이동 효과음

            dir = Vector2.right;                    // 버튼을 오른쪽으로 보냄
            yield break;
        }

        if (collision.name == "Right" && isHorizontal == true)  // 수평으로 갈 준비가 되어 있고, 사다리가 오른쪽에 있다면
        {
            yield return new WaitForSeconds(0.0005f);   // 오류가 있다면 여기를 보시오...
            isHorizontal = false;

            AudioManager.instance.Mission_06_Click();     // 이동 효과음

            dir = Vector2.left;                      // 버튼을 왼쪽으로 보냄
            yield break;
        }

        if (collision.CompareTag("Goal") == true)      // 버튼이 골 이미지(아무 곳이나)에 떨어지면...
        {
            yield return new WaitForSeconds(0.0005f);
            
            isEnd = true;                                 // 끝났다는 뜻
             
            SeventhManager.hitNum = int.Parse(collision.name.Substring(11, 1));    // 이미지의 이름을 따와서 숫자를 안 다음 정수를 넘긴다.

            yield break;
        }
    }
}
