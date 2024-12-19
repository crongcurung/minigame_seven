using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InstrumentButton : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audioClip;     // �Ǳ� ����� ����Ʈ


    public List<GameObject> X_List;     // �ٸ��� �����ų�, �ƿ� �� �����ų� �ϸ� X �̹����� ����
    public List<Sprite> judgeImage_List;  // 0���� BAD �̹���, 1���� PERFECT �̹���


    public void Press_Instrument(int num)        // ��ư�� ������ ����Ǵ� �Լ�, �Ķ���ʹ� �� ��ư(�Ǳ�)�� ��ȣ
    {
        audioSource = EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>();  // �ϴ� �� ��ư�� �Ǳ� �Ҹ��� ������ ��
        audioSource.PlayOneShot(audioClip[num]);

        CurrentJudge(num);    // ��ư�� ���� �ñ⿡ ������ ��� �Ǵ��� �Ǵ��ϴ� �Լ�
    }

    public void CurrentJudge(int num)  // ��ư�� ���� �ñ⿡ ������ ��� �Ǵ��� �Ǵ��ϴ� �Լ�, �Ķ���ʹ� �� ��ư(�Ǳ�)�� ��ȣ
    {

        int buttonInt = FifthManager.noteLineInt;
        if (buttonInt > 3)
        {
            buttonInt = buttonInt - 4;
        }

        if (FifthManager.judge02 == true && num == buttonInt)      // ���� ������ ����ְ�, �ǱⰡ �´� ���(perfect ó��)
        {
            FifthManager.flameBool = false;
            Debug.Log("��");

            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Image>().sprite = judgeImage_List[1];
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Animator>().SetBool("Judge", true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(0).gameObject.SetActive(false);

            return;
        }


        if (FifthManager.judge01 == true && num == buttonInt)          // ū ������ ����ְ�, �ǱⰡ �´� ��� (bad ó��)
        {
            FifthManager.judgeInt += 1;
            FifthManager.flameBool = false;
            Debug.Log("����");
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Image>().sprite = judgeImage_List[0];
            
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(3).GetComponent<Animator>().SetBool("Judge", true);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(0).gameObject.SetActive(false);

            return;
        }

        if (FifthManager.judge01 == false || num != buttonInt)      // ū ������ ������� �ʰų�, �ǱⰡ ���� �ʴ� ���(�׳� ����ó��, ������ٰ� �����Ŷ�)
        {
            
            if (FifthManager.judge01 == true && num != buttonInt)    // ū �������� ���������, �ǱⰡ ���� �ʴ� ���
            {
                FifthManager.judgeInt += 2;              // �ٷ� ���� ����
                Debug.Log("Ʋ���� ����");
                GameObject.Find(FifthManager.currentFlameObjectName).transform.GetChild(2).gameObject.SetActive(true);
                FifthManager.flameBool = false;                             // x ǥ�� �̹��� ����
            }


            return;
        }
    }
}
