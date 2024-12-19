using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    public float noteSpeed;

    bool startBool = false;

	void OnEnable()
	{

        noteSpeed = UnityEngine.Random.Range(400, 600);

        StartCoroutine(StartWait());
    }

	void Update()
    {
        if (startBool == true)
        {
            transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
        }
    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(3.0f);

        startBool = true;
    }
}
