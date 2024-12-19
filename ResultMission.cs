using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultMission : MonoBehaviour          // Result Panel�� ������
{
	public TextMeshProUGUI resultText;           // ��� â �ؽ�Ʈ
	public TextMeshProUGUI fallSucceseText;      // ���� ���� �ؽ�Ʈ

	public TextMeshProUGUI ToMainText;
	public TextMeshProUGUI OneMoreText;

	void OnEnable()
	{
		if (DataManager.instance.nowPlayer.KorOrEngBool == true)        // �ѱ���
		{
			resultText.text = $"���� ��� : {MissionManager.instance.succeseCount} / 5";

			if (MissionManager.instance.succeseCount > 2)      // 5�� �� 3�� �̻� �̰��� ���
			{
				fallSucceseText.text = "����!!";
			}
			else                                               // 5�� �� 3�� �̻� ���� ���
			{
				fallSucceseText.text = "����!!";
			}

			ToMainText.text = "��������";
			OneMoreText.text = "�� �� ��!";
		}
		else                                        // ����
		{
			resultText.text = $"Final Score : {MissionManager.instance.succeseCount} / 5";

			if (MissionManager.instance.succeseCount > 2)      // 5�� �� 3�� �̰��� ���
			{
				fallSucceseText.text = "Success!!";
			}
			else                                               // 5�� �� 3�� �̻� ���� ���
			{
				fallSucceseText.text = "Fail!!";
			}

			ToMainText.text = "To Main";
			OneMoreText.text = "Again!";
		}
	}

	public void Press_ToMain()        // ���â ������ �������� ���ư��� (��ư)
	{
		AudioManager.instance.Mini_BackGroundSound_02();      // ���� �������
		SceneManager.LoadScene("MainGame");
	}

	public void Press_OneMore()       // ���â ������ �� ���� �̼� ������ ����(��ư)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     // �̼� ���� ���ε�
	}
}
