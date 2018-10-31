using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork
{
    private int nrBranches;
    private List<RoadSegment> segments = new List<RoadSegment>();

    public void AddRoadSegment(RoadSegment segment)
    {
        Debug.Log("[" + this.GetType().Name + "]: AddRoadSegment() - entry");
        if (!this.segments.Contains(segment))
            this.segments.Add(segment);
    }

    public void Draw()
    {
        Debug.Log("[" + this.GetType().Name + "]: Draw() - entry");
        /*for (int i = 0; i < this.segments.Count; i++)
            Debug.DrawLine(this.segments[i].start, this.segments[i].end);*/
    }

    public int GetNrSegments()
    {
        Debug.Log("[" + this.GetType().Name + "]: GetNrSegments() - entry");
        return this.segments.Count;
    }

    public int GetNrBranches()
    {
        Debug.Log("[" + this.GetType().Name + "]: GetNrBranches() - entry");
        return this.segments.Count;
    }

    public RoadSegment GetSegment(int index)
    {
        Debug.Log("[" + this.GetType().Name + "]: GetSegment() - entry");
        if (index >= 0 && index < this.segments.Count)
            return this.segments[index];
        return null;
    }
}
