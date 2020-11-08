using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlagPoints : MonoBehaviour
{
    FlagPoint lastPoint;
    List<FlagPoint> points;

    private void Awake()
    {
        points = GetComponentsInChildren<FlagPoint>().ToList();
    }

    public Transform GetRandomPoint()
    {
        FlagPoint point = points[RandomNumberGenerator.Instance.Next(points.Count)];

        while (point == lastPoint)
            point = points[RandomNumberGenerator.Instance.Next(points.Count)];

        lastPoint = point;
        return point.transform;
    }
}
