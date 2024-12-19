using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ThirdManager
{
    public static int colliderCount;  // 이번 판에 사용된 콜라이더의 개수를 받는 변수
    public static int currentCount = 0;   // 현재 지나간 콜라이더의 개수를 받는 변수

    public static bool currentTouchLine;

    public static bool touchFail;
    public static bool touchSuccess;

    public static bool isEnd;
}
