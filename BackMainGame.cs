using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainGame : MonoBehaviour
{
    public void MoveToMainGame()        // Back Button OnClick()�� ������
    {
        AudioManager.instance.PlayBackSound();             // �ڷ� ���� ȿ����

        AudioManager.instance.PlayBackGroundSound_02();    // ��� ���� �ٲٱ�

        SceneManager.LoadScene("MainGame");                // ���� �������� �� �̵�
    }
}
