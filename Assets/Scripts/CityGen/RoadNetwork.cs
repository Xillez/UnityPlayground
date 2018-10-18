using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork
{
    List<RoadSegment> segments = new List<RoadSegment>();

    public void AddRoadSegment(RoadSegment segment)
    {
        if (!this.segments.Contains(segment))
            this.segments.Add(segment);
    }

    public void SubdivideRoads(int depth)
    {
        int currentDepth = depth;
        Vector3 lineStart;
        Vector3 lineDirection;

        for (int i = 0; i < this.segments.Count; i++)
        {
            Random.Range(0.0f, 1.0f);

        }
    }
}
