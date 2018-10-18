using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork
{
    //float minBlockWidth = 0.01f;
    private int nrBranches;
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
                float random = Random.Range(0.4f, 0.6f);   // Random distance from generation point in percent. Used for subdivision.
                Vector3 oldSegmentDirection = this.segments[i].end - this.segments[i].start;
                Mathf.Tan(((360.0f / nrBranches) / 2.0f) * Mathf.Deg2Rad) * Vector3.Magnitude(oldSegmentDirection * random);
                //float randomLength = Random.Range();
                Vector3 newSegmentDirection = Vector3.Cross(oldSegmentDirection, Vector3.up) * Random.Range()
                Vector3 newSegmentStart = (oldSegmentDirection * random) + (newSegmentDirection * Random.Range(0.3f, 0.7f));
                RoadSegment newSegment;
            }
    }
}
