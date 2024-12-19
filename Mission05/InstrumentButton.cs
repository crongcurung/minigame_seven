using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InstrumentButton : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audioClip;     // 악기 오디오 리스트


    public List<GameObject> X_List;     // 다른거 누르거나, 아예 못 누르거나 하면 X 이미지가 나옴
    public List<Sprite> judgeImage_List;  // 0번은 BAD 이미지, 1번은 PERFECT 이미지


    public void Press_Instrument(int num)        // 버튼을 누르면 실행되는 함수, 파라미터는 각 버튼(악기)의 번호
    {
        audioSource = EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>();  // 일단 각 버튼의 악기 소리를 내도록 함
        audioSource.PlayOneShot(audioClip[num]);

        CurrentJudge(num);    // 버튼을 누른 시기에 판정이 어떻게 되는지 판단하는 함수
    }

    public void CurrentJudge(int num)  // 버튼을 누른 시기에 판정이 어떻게 되는지 판단하는 함수, 파라미터는 각 버튼(악기)의 번호
    {

        int buttonInt = FifthManager.noteLineInt;
        if (buttonInt > 3)
        {
            buttonInt = buttonInt - 4;
        }

        if (FifthManager.judge02 == true && num == buttonInt)      // 작은 영역에 들어있고, 악기가 맞는 경우(perfect 처리)
        {
            FifthManager.flameBool = false;
            Debug.Log("굿");

            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Image>().sprite = judgeImage_List[1];
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Animator>().SetBool("Judge", true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(0).gameObject.SetActive(false);

            return;
        }


        if (FifthManager.judge01 == true && num == buttonInt)          // 큰 영역에 들어있고, 악기가 맞는 경우 (bad 처리)
        {
            FifthManager.judgeInt += 1;
            FifthManager.flameBool = false;
            Debug.Log("별루");
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Image>().sprite = judgeImage_List[0];
            
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Animator>().SetBool("Judge", true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(0).gameObject.SetActive(false);

            return;
        }

        if (FifthManager.judge01 == false || num != buttonInt)      // 큰 영역에 들어있지 않거나, 악기가 맞지 않는 경우(그냥 리턴처리, 허공에다가 누른거라)
        {
            
            if (FifthManager.judge01 == true && num != buttonInt)    // 큰 영역에는 들어있지만, 악기가 맞지 않는 경우
            {
                FifthManager.judgeInt += 2;              // 바로 게임 종료
                Debug.Log("틀린거 누름");
                GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(2).gameObject.SetActive(true);
                FifthManager.flameBool = false;                             // x 표시 이미지 나옴
            }


            return;
        }
    }
}
