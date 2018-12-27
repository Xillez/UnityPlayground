using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadSegment
{
    public Vector3 start;
    public Vector3 end;
    public float width;
    int parentIndex;

    public RoadSegment(Vector3 start, Vector3 end, int parent)
    {
        this.start = start;
        this.end = end;
        this.parentIndex = parent;
    }

    public float Length()
    {
        return (end - start).magnitude;
    }

    public Vector3 Direction()
    {
        return (end - start);
    }
}
