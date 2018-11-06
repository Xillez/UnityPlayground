using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadSegment
{
    public Vector3 start;
    public Vector3 end;
    public float width;

    public RoadSegment(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
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
