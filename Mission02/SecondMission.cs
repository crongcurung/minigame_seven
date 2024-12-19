using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondMission : MonoBehaviour
{
    public Slider slider;             // �ð� �����̴�
    public TextMeshProUGUI testText;   // ���� ������ ����, ū ������ ���� �˷��ִ� �ؽ�Ʈ

    int rand;
    List<int> listInt = new List<int>();     // 1���� 99������ ���ڸ� �ϴ� ���� ����Ʈ
    List<int> sortInt = new List<int>();

    List<GameObject> bubble = new List<GameObject>();         // ���� �������� ��� ����Ʈ(�� 9��)

    public List<GameObject> Emp = new List<GameObject>();     // ������ ���� ����(�� 15��)
    List<GameObject> copyEmp = new List<GameObject>();        // ��Ȱ��ȭ���� Ȱ��ȭ �� �� ����Ʈ�� ���� ������ �־ ���� ����Ʈ�� ����� ����ٰ� �ִ´�.

    public GameObject bubblePrefab;                        // ���� ������
    public Sprite bubble_Errer;

    Vector3 bubbleScale;                            // ������ ũ�⸦ �޴� ����

    int textRand;   // ���� ������ ����, ū ������ ���� �������� ���ϱ�

    List<int> sortBubbleNum = new List<int>();    // ������ ������ ���ڸ� �������� �ѱ�
    public List<int> sortNum = new List<int>();  // ������ ������ �������� ���� ������ ���ڰ��� �ѱ�

    public int bubbleCount;  // ���� ������ � ������?

    bool isEnd;   // ������ �� ������?

    bool finalCheck;
    public bool judgeCheck;


    void OnEnable()
    {
        isEnd = false;           // �ϴ� ������ �� �ȴ�������, false
        bubbleCount = 0;          // � �����ĵ� �ʱ�ȭ
        finalCheck = false;
        judgeCheck = true;

        slider.maxValue = 10.0f;
        slider.value = 10.0f;

        copyEmp.Clear();           // ����Ʈ �ʱ�ȭ
        listInt.Clear();           
        sortInt.Clear();
        sortBubbleNum.Clear();    // ���� ���� ���ڸ� ��� ����Ʈ �ʱ�ȭ
        sortNum.Clear();           // ������ ������ �� ���ڸ� ��� ����Ʈ �ʱ�ȭ

        for (int i = 0; i < Emp.Count; i++)       // ��� ���� ����Ʈ���ٰ� ������ ����ִ´�.(�� 15��)
        {
            copyEmp.Add(Emp[i]);            
        }

        bubbleScale = bubblePrefab.transform.localScale;       // ������ �������� �ϴ� ���� �����ղ��� �Ѵ�.


        for (int i = 1; i < 100; i++)        // ���� ���� ���ڴ� 1���� 99������.
        {
            listInt.Add(i);           
        }

        for (int i = 0; i < 9; i++)   // 9�� ������ ���� ������ ������ ���� 9���̱� ������..
        {
            rand = UnityEngine.Random.Range(0, listInt.Count);   // 0���� 99������

            sortInt.Add(listInt[rand]);   // 0���� 99���������� 9���� �̾Ƽ� sortInt ����Ʈ�� ��Ƶд�.

            listInt.RemoveAt(rand);    // �ߺ� ���� listInt ����Ʈ��
        }
        Random_Text();     // ū������ ����, ���������� ���� �غ���
        Create_Circle();    // ���� â�� �Լ�

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
		
		if (bubbleCount > 8.5 && isEnd == false)      // ������ 9�� ������(�� ������)
        {
            Sort_Num();                       // ������ �´��� Ʋ�ȴ��� �Ǵ�
        }

        if (isEnd == false)                // �����ٸ� �ð� �� ��
        {
            if (slider.value > 0)                    
            {
                slider.value -= Time.deltaTime;
            }
            else
            {
                if (finalCheck == false)
                {
                    AudioManager.instance.Mission_01_Fail_Effect();       // �ð� ���� ȿ����
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
            AudioManager.instance.Mission_02_Fail_Effect();            // �̼� ���� ȿ����
            return;
        }

        if (sortNum[bubbleCount - 1] != sortBubbleNum[bubbleCount - 1])        // ���� ������ ������ ���� �ʾҴٸ�...
        {
            judgeCheck = false;

            AudioManager.instance.Mission_02_Fail_Effect();            // �̼� ���� ȿ����
            for (int i = 0; i < Emp.Count; i++)
            {
                if (Emp[i].transform.childCount == 1)
                {
                    //Emp[i].transform.GetChild(0).GetComponent<Image>().color = Color.yellow;        // Ʋ���� ��� ���� �ִ� ���� ���� ���������...

                    
                    Emp[i].transform.GetChild(0).GetComponent<Image>().sprite = bubble_Errer;
                }
            }
        }
        else                // ���� ������ ������ �´ٸ�
        {
            AudioManager.instance.Mission_02_Success_Effect();            // �̼� ���� ȿ����
        }
    }

    public void Random_Text()
    {
        textRand = UnityEngine.Random.Range(0, 2);     // ������, ū�� �ؽ�Ʈ


        if (DataManager.instance.nowPlayer.KorOrEngBool == true)      // �ѱ���..
        {
            if (textRand < 0.5f)      // 0�̶�� ����������
            {
                testText.text = "<color=#FF0000>���� ��</color>���� ��ġ�Ͻÿ�.";
            }
            else                       // 0�̶�� ū������
            {
                testText.text = "<color=#FF0000>ū ��</color>���� ��ġ�Ͻÿ�.";
            }
        }
        else              // ����..
        {
            if (textRand < 0.5f)      // 0�̶�� ����������
            {
                testText.text = "Touch it in <color=#FF0000>Small Order</color>.";
            }
            else                       // 0�̶�� ū������
            {
                testText.text = "Touch it in <color=#FF0000>High Order</color>.";
            }
        }
    }


    public void Create_Circle()          // ���� ���� �Լ�
    {
        //GameObject[] bubble = new GameObject[9];
        
        bubble.Clear();

        for (int i = 0; i < 9; i++)
        {
            float randFloat = UnityEngine.Random.Range(1.2f, 2.2f);     // ������ ����� ���� ���� ����
            bubbleScale.x = 1.0f;
            bubbleScale.y = 1.0f;
            bubbleScale.x = bubbleScale.x * randFloat;
            bubbleScale.y = bubbleScale.y * randFloat;

            bubble.Add(bubblePrefab);                                // ������ ��� ����Ʈ�� ������ ���
            bubble[i].transform.localScale = bubbleScale;            // ���� �����յ��� ����� �������� ���� ����� �ֱ�

            bubble[i].GetComponentInChildren<TextMeshProUGUI>().text = sortInt[i].ToString();  
            // ���� ����ִ� �� �ؽ�Ʈ�� �̾� ���� ���ڸ� ���� �ִ´�.

            sortBubbleNum.Add(sortInt[i]);   // ���� ������� ���ڸ� ����Ʈ�� ������ �°� ���� �ִ´�.

            int randEmpty = UnityEngine.Random.Range(0, copyEmp.Count);  //��ġ�� �������� �̴� ����

            Instantiate(bubble[i], copyEmp[randEmpty].transform.position, Quaternion.identity, copyEmp[randEmpty].transform);
            // ��ġ�� ��Ҵ� ����Ʈ�� �������� �̾Ƽ� ��ġ�� ������ ��, ������ ���� ����Ʈ�� �����ͼ� �ڽ����� �ɴ´�.

            copyEmp.RemoveAt(randEmpty);   // �ߺ� ����
        }

        

        if (textRand < 0.5f)              // ū��, ������ ���� ������ 0�̶��(���� ��)
        {
            sortBubbleNum.Sort();         // ����(���� ��)
        }
        else                                // ū��, ������ ���� ������ 1�̶��(ū ��)
        {
            sortBubbleNum.Sort();         // ����(���� ��) ��
            sortBubbleNum.Reverse();      // ����(ū ��)
        }
    }

    public void Sort_Num()
    {
        isEnd = true;
        

        for (int i = 0; i < 9; i++)        
        {
            if (sortNum[i] != sortBubbleNum[i])          // ������(ū ���̵�, ���� ���̵�) ����Ʈ�� ���� ���� ����Ʈ�� �ٸ��ٸ�
            {
                Debug.Log("Ʋ�Ƚ��ϴ�.");
                
                MissionManager.instance.failCount++;
                MissionContinue();
                return;
            }
        }

        Debug.Log("����");                               // ������(ū ���̵�, ���� ���̵�) ����Ʈ�� ���� ���� ����Ʈ�� ���ٸ�
        
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



    void MissionContinue()        // �� �̼��� ������ �� ó��
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
