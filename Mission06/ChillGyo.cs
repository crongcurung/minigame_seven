using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChillGyo : MonoBehaviour
{
    int rotateInt = 1;

    public void Press_ChillGyo()       // ��ư�� �� �� ���� ������ 45���� �þ�� �Լ�
    {
        

        rotateInt += 1;          

        EventSystem.current.currentSelectedGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45.0f * rotateInt));
    }
}
