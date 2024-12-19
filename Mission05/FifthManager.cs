using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FifthManager
{
    public static bool judge01;             // true 면 judge01 영역(bad 판정)에 들어왔다는 뜻
    public static bool judge02;             // true 면 judge02 영역(perfact 판정)에 들어왔다는 뜻


    public static int noteLineInt;               // 판정 영역에 들어왔을 시, 그 영역의 부모(flame)가 가진 악기의 번호가 몇인지 받는 변수

    public static string currentFlameObjectName;   // 판정영역에 들어왔을 시, 부모(flame)의 이름을 받는 변수

    public static bool flameBool = true;            // 아무것도 못 누르고 판정영역 밖으로 노트가 나갈 경우, x 이미지를 표시하기 위한 변수


    public static int judgeInt = 0;               // 몇개 틀렸는지 받는 변수, 1개면 경고, 2개면 게임 끝

    public static bool isEnd;  // 오른쪽 끝에 콜라이더에 걸렸을 경우
}
