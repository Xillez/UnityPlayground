using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork
{
    private int nrBranches;
    private List<RoadSegment> segments = new List<RoadSegment>();

    public void AddRoadSegment(RoadSegment segment)
    {
        //if (!this.segments.Contains(segment))
        this.segments.Add(segment);
    }

    public void Draw()
    {
        foreach (RoadSegment segment in this.segments)
            Debug.DrawLine(segment.start, segment.end);
    }

    public int GetNrSegments()
    {
        return this.segments.Count;
    }

    public int GetNrBranches()
    {
        return this.segments.Count;
    }

    public RoadSegment GetSegment(int index)
    {
        if (index >= 0 && index < this.segments.Count)
            return this.segments[index];
        return null;
    }

    public void Clear()
    {
        this.segments.Clear();
    }
}
