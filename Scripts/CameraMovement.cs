using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour 
{
    private BezierGenerate bg;
    public float speed = 5;

	void Start () 
    {
		bg = GetComponent<BezierGenerate>();
	}

    public void DoBezierPath()
    {
        bg.GenerateCurvePoints();
        StartCoroutine(MoveToPath());
    }


    IEnumerator MoveToTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            //transform.LookAt(target);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator MoveToPath()
    {
        for (int i = 1; i < bg.coordinateList.Length; i++)
        {
            yield return StartCoroutine(MoveToTarget(bg.coordinateList[i]));

            if (i == bg.coordinateList.Length - 1)
            {
                SceneManager.LoadScene("InScene");
            }
        }
    }
}
