using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadSegment
{
    protected int id;
    public Vector3 start;
    public Vector3 end;
    public float width;

    public RoadSegment(int id, Vector3 start, Vector3 end)
    {
        this.id = id;
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

    public bool Equals(RoadSegment other)
    {
        return (this.id == other.id);
    }
}
