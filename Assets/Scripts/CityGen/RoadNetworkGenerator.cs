using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetworkGenerator
{
    Vector3 genPoint = Vector3.zero;
    public float cityRadius;

    //float minBlockWidth = 0.01f;
    private int nrBranches;

    public RoadNetworkGenerator()
    {   
        //
    }

    public void Init(Vector3 genPoint, float radius, int nrBranches)
    {
        this.genPoint = genPoint;
        this.cityRadius = radius;
        this.nrBranches = nrBranches;
    }

    public void GenRoadNetworkBaseSegments(ref RoadNetwork network)
    {
        //int nrBaseSegments = Random.Range(3, 7);
        float intervalRadians = (360.0f / this.nrBranches) * Mathf.Deg2Rad;
        // Generate mainroads
        for (int i = 0; i < this.nrBranches; i++)
        {
            Vector3 end = new Vector3(this.genPoint.x + this.cityRadius * Mathf.Cos((i) * intervalRadians),
                                      this.genPoint.y,
                                      this.genPoint.z + this.cityRadius * Mathf.Sin((i) * intervalRadians));
            network.AddRoadSegment(new RoadSegment(this.genPoint, end));
        }

        // Generate crossroads
        for (int i = 0; i < this.nrBranches; i++)
        {
            Vector3 start = new Vector3(this.genPoint.x + (this.cityRadius / 2.0f) * Mathf.Cos((i + 0.5f) * (intervalRadians)),
                                        this.genPoint.y, 
                                        this.genPoint.z + (this.cityRadius / 2.0f) * Mathf.Sin((i + 0.5f) * intervalRadians));
            Vector3 end = new Vector3(this.genPoint.x + (this.cityRadius / 2.0f) * Mathf.Cos((i + 1.5f) * intervalRadians),
                                      this.genPoint.y, 
                                      this.genPoint.z + (this.cityRadius / 2.0f) * Mathf.Sin((i + 1.5f) * intervalRadians));
            network.AddRoadSegment(new RoadSegment(start, end));
        }
    }

    public void SubdivideRoads(ref RoadNetwork network, int iterations, float minBlockWidth)
    {
        // For every iteration
        for (int i = iterations; i >= 0; i--)
        {
            // Make new perpendicular segments from a random segment
            RoadSegment oldSegment = network.GetSegment(Random.Range(4, network.GetNrSegments() - 1));

            // Random distance (based on selected segment) for calculating new segment. Used for subdivision.
            float baseDist = (oldSegment.start - genPoint).magnitude * Random.Range(0.2f, 0.8f);
            Vector3 newSegmentStart = oldSegment.start + ((genPoint - oldSegment.start) / baseDist);
            Vector3 newSegmentEnd = oldSegment.start + ((oldSegment.end - genPoint) / baseDist);

            // Pick whether start or end should be pushed (40% for both, 20% none).
            float randomValue = Random.Range(0.0f, 1.0f);
            // Pick strech amount.
            float randomStrech = Random.Range(1.2f, 1.8f);
            if (randomValue < 0.4f)
                newSegmentStart += (newSegmentEnd - newSegmentStart) * randomStrech;
            else if (randomValue > 0.4f && randomValue < 0.8f)
                newSegmentEnd += -(newSegmentEnd - newSegmentStart) * randomStrech;

            // Add it in for selection.
            network.AddRoadSegment(new RoadSegment(newSegmentStart, newSegmentEnd));
        }
    }
}
