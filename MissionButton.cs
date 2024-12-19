using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionButton : MonoBehaviour
{
    Animator boardAnim;


	void Start()
	{
		boardAnim = GetComponent<Animator>();
	}

	public void Press_MissionButton()
    {
		boardAnim.SetBool("BoardMove", true);

		AudioManager.instance.Mission_Enter();
	}

	public void Middle_Audio()
	{
		AudioManager.instance.Mission_Enter02();
	}

	public void MoveToMissionScene()
	{
		SceneManager.LoadScene("Mission");
	}
}
