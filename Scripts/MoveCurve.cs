using UnityEngine;
using System.Collections;

public class MoveCurve : MonoBehaviour
{

    public Vector3 t1;    //开始位置
    public GameObject t2;     //结束位置
    public float speed = 10;

    public bool isMove = false;
    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            //两者中心点
            Vector3 center = (t1 + t2.transform.position) * 0.5f;

            center -= new Vector3(0, 1, 0);

            Vector3 start = t1 - center;
            Vector3 end = t2.transform.position - center;

            //弧形插值
            transform.position = Vector3.Slerp(start, end, Time.deltaTime * speed);
            transform.position += center;

            if (Vector3.Distance(transform.position, t2.transform.position) < 0.1f)
                isMove = false;
        }
    }
}