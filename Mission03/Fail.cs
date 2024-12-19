using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : MonoBehaviour
{
    public List<GameObject> piecese = new List<GameObject>();
   

	void Update()
    {
        if (ThirdManager.touchFail == true && ThirdManager.isEnd == false)
        {

            for (int i = 0; i < piecese.Count; i++)
            {
                piecese[i].SetActive(true);
                piecese[i].GetComponent<Animator>().SetBool("Fail", true);
            }
        }
    }

	void OnDisable()
	{
        for (int i = 0; i < piecese.Count; i++)
        {
            piecese[i].SetActive(false);
        }
    }
}
