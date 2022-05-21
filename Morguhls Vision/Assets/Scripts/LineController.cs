using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer lr = null;
    Transform[] points;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    public void SetUpLine(Transform[] points)
    {
        lr.enabled = true;
        lr.positionCount = points.Length;
        this.points = points;
    }
    public void CancelLine()
    {
        lr.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        //if (dl.IsDraining)
        //{
        //    for(int i = 0; i < points.Length; i++)
        //    {
        //        lr.SetPosition(i, points[i].position);
        //    }
        //}
    }
}
