using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FifthManager
{
    public static bool judge01;             // true �� judge01 ����(bad ����)�� ���Դٴ� ��
    public static bool judge02;             // true �� judge02 ����(perfact ����)�� ���Դٴ� ��


    public static int noteLineInt;               // ���� ������ ������ ��, �� ������ �θ�(flame)�� ���� �Ǳ��� ��ȣ�� ������ �޴� ����

    public static string currentFlameObjectName;   // ���������� ������ ��, �θ�(flame)�� �̸��� �޴� ����

    public static bool flameBool = true;            // �ƹ��͵� �� ������ �������� ������ ��Ʈ�� ���� ���, x �̹����� ǥ���ϱ� ���� ����


    public static int judgeInt = 0;               // � Ʋ�ȴ��� �޴� ����, 1���� ���, 2���� ���� ��

    public static bool isEnd;  // ������ ���� �ݶ��̴��� �ɷ��� ���
}
