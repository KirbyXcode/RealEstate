using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 贝塞尔曲线生成器
/// </summary>
public class BezierGenerate : MonoBehaviour
{
    public Transform beginTF;
    public Transform controlTF01; 
    public Transform controlTF02;
    public Transform endTF;

    private void Awake()
    {
        coordinateList = new Vector3[nodeCount];
    }

    //private void Update()
    //{
    //    GenerateCurvePoints();
    //    DrawCurve();
    //}

    public static Vector3 CreatePoint(Vector3 beginPos,Vector3 controlPos01,Vector3 controlPos02,Vector3 endPos,float ratio)
    {
        return beginPos * Mathf.Pow(1 - ratio, 3) + 3 * controlPos01 * ratio * Mathf.Pow(1 - ratio, 2) + 3 * controlPos02 * Mathf.Pow(ratio, 2) * (1 - ratio) + endPos * Mathf.Pow(ratio, 3);
    }

    [Tooltip("节点数量")]
    public int nodeCount = 4;
    public Vector3[] coordinateList;

    public void GenerateCurvePoints()
    {
        float t = 0;
        float interval = 1f / (nodeCount - 1);
        for (int i = 0; i < nodeCount; i++)
        {
            Vector3 pos = CreatePoint(beginTF.position, controlTF01.position, controlTF02.position, endTF.position, t);
            coordinateList[i] = pos;
            t += interval;
        }
    }
     
    private void DrawCurve()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer)
        {
            lineRenderer.SetVertexCount(coordinateList.Length);
            lineRenderer.SetPositions(coordinateList);
        }
    }

}
