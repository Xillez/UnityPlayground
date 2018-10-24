using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork
{
    private int nrBranches;
    private List<RoadSegment> segments = new List<RoadSegment>();

    public void AddRoadSegment(RoadSegment segment)
    {
        if (!this.segments.Contains(segment))
            this.segments.Add(segment);
    }

    public int getNrSegments()
    {
        return this.segments.Count;
    }

    public int getNrBranches()
    {
        return this.segments.Count;
    }

    public RoadSegment getSegment(int index)
    {
        if (index >= 0 && index < this.segments.Count)
            return this.segments[i];
        return null;
    }
}
