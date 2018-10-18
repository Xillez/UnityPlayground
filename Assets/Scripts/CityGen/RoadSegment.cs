using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseRoadSegment
{
    protected int id;
}

public class RoadSegment : BaseRoadSegment
{
    public Vector3 start;
    public Vector3 end;
    public float width;

    public RoadSegment(int id)
    {
        this.id = id;
    }

    public bool Equals(RoadSegment other)
    {
        return (this.id == other.id);
    }
}
