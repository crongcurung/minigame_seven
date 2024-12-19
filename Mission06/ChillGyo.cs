using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChillGyo : MonoBehaviour
{
    int rotateInt = 1;

    public void Press_ChillGyo()       // 버튼을 한 번 누를 때마다 45도씩 늘어나는 함수
    {
        

        rotateInt += 1;          

        EventSystem.current.currentSelectedGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45.0f * rotateInt));
    }
}
