using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour     // 메인 카메라에 부착
{
    public GameObject lineParent;
    public GameObject linePrefab;
    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    public RectTransform targetRectTr;
    public Camera mainCamera;

    public GameObject followImage;

    private Vector2 screenPoint;

    bool firstTouchBool = true;

    float rectWidth;

	void OnEnable()
	{
        rectWidth = this.gameObject.GetComponent<RectTransform>().rect.width;

    }

	void Update()
    {
        if (ThirdManager.isEnd == true && lineParent.transform.childCount > 2)
        {
            Destroy(lineParent.transform.GetChild(2).gameObject);

            return;
        }

        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(targetRectTr, Input.mousePosition, mainCamera)
            && ThirdManager.isEnd == false)
        {
            
            GameObject go = Instantiate(linePrefab, lineParent.transform);
            lr = go.GetComponent<LineRenderer>();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, Input.mousePosition, mainCamera, out screenPoint);


            followImage.transform.localPosition = new Vector3(screenPoint.x - rectWidth, screenPoint.y, 0);

            lr.positionCount = 0;
            
        }
        else if (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(targetRectTr, Input.mousePosition, mainCamera)
            && ThirdManager.isEnd == false)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, Input.mousePosition, mainCamera, out screenPoint);

            if (ThirdManager.currentTouchLine == false)
            {
                followImage.transform.localPosition = new Vector3(screenPoint.x - rectWidth, screenPoint.y, 0);
            }
            
            if (ThirdManager.currentTouchLine == true)
            {

                followImage.transform.localPosition = new Vector3(screenPoint.x - rectWidth, screenPoint.y, 0);
                Vector2 pos = new Vector2(screenPoint.x - rectWidth, screenPoint.y);

                if (firstTouchBool == true)
                {
                    points.Add(new Vector2(screenPoint.x - rectWidth, screenPoint.y));
                    lr.positionCount = 1;
                    lr.SetPosition(0, points[0]);

                    firstTouchBool = false;
                    return;
                }


                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (ThirdManager.currentTouchLine == true && ThirdManager.isEnd == false)
            {
                points.Clear();
                Destroy(lineParent.transform.GetChild(2).gameObject);

                AudioManager.instance.LoopNot_Effect();
                AudioManager.instance.Mission_03_Break_Effect();

                ThirdManager.touchFail = true;
                Debug.Log("마우스를 떼서 실패!!!");
            }
        }
        
    }

}


