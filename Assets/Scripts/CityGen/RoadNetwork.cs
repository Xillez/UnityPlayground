using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork
{
    float minBlockWidth = 0.01f;
    List<RoadSegment> segments = new List<RoadSegment>();

    public void AddRoadSegment(RoadSegment segment)
    {
        if (!this.segments.Contains(segment))
            this.segments.Add(segment);
    }

    public void SubdivideRoads(int iterations)
    {
        for (int i = iterations; i >= 0; i--)
            for (int seg = 0; seg < this.segments.Count; seg++)
            {
                Random.Range(0.0f, 1.0f);   // Random distance from generation point in percent. Used for subdivision.

            }
    }
}
