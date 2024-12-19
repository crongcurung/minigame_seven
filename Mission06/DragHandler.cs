using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour,  IDragHandler, IPointerClickHandler, IEndDragHandler
{
	public bool isDrag = false;

	void OnEnable()
	{
		isDrag = false;
	}


	public void OnDrag(PointerEventData eventData)
	{

		isDrag = true;
		gameObject.transform.SetAsLastSibling();
		gameObject.transform.position = eventData.position;
		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		isDrag = false;

	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (isDrag == false)
		{
			AudioManager.instance.Mission_06_Click();          // È¿°úÀ½

			gameObject.transform.SetAsLastSibling();

			transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 45));
		}


	}

	
}
