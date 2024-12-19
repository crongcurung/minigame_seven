using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour   // ��ư�� �����Ǿ� ����
{
    public bool isEnd;                   // �������� ���� ����(��ư�� �̹����� �����ϸ� �����ٴ� ����)
    public bool isHorizontal;          // �������� �� �غ� �Ǿ� �ֳ� ���� ����

    public GameObject lineOutLinePanel;       // ���θ� �����ִ� �г�(��ư�� ������ �г��� �ٷ� �������� ���⿡�� �����Դ�)

    Vector2 dir;        // ��ư�� ��ġ(�̵�)�� �޾��� ����


	void OnEnable()
	{
        Application.targetFrameRate = 60;

    }

	void OnDisable()
	{
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        
    }

	public void Move()     // ��ư�� �����ڸ��� ��ư�� ���
    {
        isEnd = false;         // ���� ��ư�� �̹����� �������� �ʾ����� false�� �ʱ�ȭ
        isHorizontal = true;                      // �ٷ� �������� ���̴�, �����̵� �غ�� �Ϸ�
        SeventhManager.isClick = true;                // �÷��̾ ��ư�� Ŭ�������� true�� ����

        AudioManager.instance.Mission_07_Click();     // ��ư Ŭ�� ȿ����

        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        if (lineOutLinePanel.activeSelf == true)        // �г��� ������ �ִ��� �˾ƺ���
        {
            lineOutLinePanel.SetActive(false);         // �г��� �ִٸ� ��Ȱ��ȭ ��Ŵ
        }


        dir = Vector2.down;                   // ��ư�� ��������

        StartCoroutine(MoveCorutine());
    }




	IEnumerator MoveCorutine()     // ���� �ð��ʸ���(��� ������ �� ����) �Ʒ��� �̵� ��Ŵ
    {
        while (isEnd == false)         // ��ư�� �̹����� ��Ҵٸ� while������ ���� ����
        {
            gameObject.transform.Translate(dir * 50 * Time.deltaTime);          // �� �ӵ��� ������

            yield return new WaitForSeconds(0.001f);
        }
    }

	public IEnumerator OnTriggerEnter2D(Collider2D collision)        // ��ư�� ��ٸ��� �浹���� ���...
	{
        if (isHorizontal == false)                  // �������� �� �غ� �Ǿ� ���� �ʴٸ� ��ư�� �������� ��
        {
            yield return new WaitForSeconds(0.0005f);   // ������ �ִٸ� ���⸦ ���ÿ�...
            isHorizontal = true;                    // �ٷ� �������� �� �غ� �Ǿ� �ִٰ� ��

            AudioManager.instance.Mission_06_Click();     // �̵� ȿ����

            dir = Vector2.down;             // ��ư�� �Ʒ��� ����
            yield break;
        }

        if (collision.name == "Left" && isHorizontal == true) // �������� �� �غ� �Ǿ� �ְ�, ��ٸ��� ���ʿ� �ִٸ�..
        {
            yield return new WaitForSeconds(0.0005f);   // ������ �ִٸ� ���⸦ ���ÿ�...
            isHorizontal = false;

            AudioManager.instance.Mission_06_Click();     // �̵� ȿ����

            dir = Vector2.right;                    // ��ư�� ���������� ����
            yield break;
        }

        if (collision.name == "Right" && isHorizontal == true)  // �������� �� �غ� �Ǿ� �ְ�, ��ٸ��� �����ʿ� �ִٸ�
        {
            yield return new WaitForSeconds(0.0005f);   // ������ �ִٸ� ���⸦ ���ÿ�...
            isHorizontal = false;

            AudioManager.instance.Mission_06_Click();     // �̵� ȿ����

            dir = Vector2.left;                      // ��ư�� �������� ����
            yield break;
        }

        if (collision.CompareTag("Goal") == true)      // ��ư�� �� �̹���(�ƹ� ���̳�)�� ��������...
        {
            yield return new WaitForSeconds(0.0005f);
            
            isEnd = true;                                 // �����ٴ� ��
             
            SeventhManager.hitNum = int.Parse(collision.name.Substring(11, 1));    // �̹����� �̸��� ���ͼ� ���ڸ� �� ���� ������ �ѱ��.

            yield break;
        }
    }
}
