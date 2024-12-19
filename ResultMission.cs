using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultMission : MonoBehaviour          // Result Panel에 부착됨
{
	public TextMeshProUGUI resultText;           // 결과 창 텍스트
	public TextMeshProUGUI fallSucceseText;      // 성공 여부 텍스트

	public TextMeshProUGUI ToMainText;
	public TextMeshProUGUI OneMoreText;

	void OnEnable()
	{
		if (DataManager.instance.nowPlayer.KorOrEngBool == true)        // 한국어
		{
			resultText.text = $"최종 결과 : {MissionManager.instance.succeseCount} / 5";

			if (MissionManager.instance.succeseCount > 2)      // 5번 중 3번 이상 이겼을 경우
			{
				fallSucceseText.text = "성공!!";
			}
			else                                               // 5번 중 3번 이상 졌을 경우
			{
				fallSucceseText.text = "실패!!";
			}

			ToMainText.text = "메인으로";
			OneMoreText.text = "한 번 더!";
		}
		else                                        // 영어
		{
			resultText.text = $"Final Score : {MissionManager.instance.succeseCount} / 5";

			if (MissionManager.instance.succeseCount > 2)      // 5번 중 3번 이겼을 경우
			{
				fallSucceseText.text = "Success!!";
			}
			else                                               // 5번 중 3번 이상 졌을 경우
			{
				fallSucceseText.text = "Fail!!";
			}

			ToMainText.text = "To Main";
			OneMoreText.text = "Again!";
		}
	}

	public void Press_ToMain()        // 결과창 나오고 메인으로 돌아가는 (버튼)
	{
		AudioManager.instance.Mini_BackGroundSound_02();      // 메인 배경음악
		SceneManager.LoadScene("MainGame");
	}

	public void Press_OneMore()       // 결과창 나오고 한 번더 미션 게임을 할지(버튼)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     // 미션 게임 리로드
	}
}
