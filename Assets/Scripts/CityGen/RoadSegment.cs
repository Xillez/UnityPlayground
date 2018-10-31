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
        Debug.Log("[" + this.GetType().Name + "]: RoadSegment() - entry");
        this.id = id;
        this.start = start;
        this.end = end;
    }

    public float Length()
    {
        Debug.Log("[" + this.GetType().Name + "]: Length() - entry");
        return (end - start).magnitude;
    }

    public Vector3 Direction()
    {
        Debug.Log("[" + this.GetType().Name + "]: Direction() - entry");
        return (end - start);
    }

    public bool Equals(RoadSegment other)
    {
        Debug.Log("[" + this.GetType().Name + "]: Equals() - entry");
        return (this.id == other.id);
    }
}
