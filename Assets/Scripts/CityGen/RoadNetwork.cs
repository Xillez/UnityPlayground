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
        Debug.Log("Network - Draw - Entry!");
        foreach (RoadSegment segment in this.segments)
        {
            Debug.Log("Network - Draw - Drawing segments!");
            Debug.DrawLine(segment.start, segment.end);
        }
        Debug.Log("Network - Draw - Exit!");
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
        Debug.Log("Network - Clear - Entry!");
        this.segments.Clear();
        Debug.Log("Network - Clear - Exit!");
    }
}
