using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainGame : MonoBehaviour
{
    public void MoveToMainGame()        // Back Button OnClick()에 부착됨
    {
        AudioManager.instance.PlayBackSound();             // 뒤로 가기 효과음

        AudioManager.instance.PlayBackGroundSound_02();    // 배경 음악 바꾸기

        SceneManager.LoadScene("MainGame");                // 메인 게임으로 씬 이동
    }
}
