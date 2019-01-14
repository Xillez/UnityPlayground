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
        Debug.Log("Generator - Init - Entry!");
        this.genPoint = genPoint;
        this.cityRadius = radius;
        this.nrBranches = nrBranches;
        Debug.Log("Generator - Init - Exit!");
    }

    public void GenRoadNetworkBaseSegments(ref RoadNetwork network)
    {
        Debug.Log("Generator - GenRoadNetworkBaseSegments - Entry!");
        //int nrBaseSegments = Random.Range(3, 7);
        float intervalRadians = (360.0f / this.nrBranches) * Mathf.Deg2Rad;
        Debug.Log("Generator - GenRoadNetworkBaseSegments - Base segments generation!");
        // Generate mainroads
        for (int i = 0; i < this.nrBranches; i++)
        {
            Debug.Log("Generator - GenRoadNetworkBaseSegments - Element generated (" + i + ")!");
            Vector3 end = new Vector3(this.genPoint.x + this.cityRadius * Mathf.Cos((i) * intervalRadians),
                                      this.genPoint.y,
                                      this.genPoint.z + this.cityRadius * Mathf.Sin((i) * intervalRadians));
            network.AddRoadSegment(new RoadSegment(this.genPoint, end, 0));
        }

        Debug.Log("Generator - GenRoadNetworkBaseSegments - Base Cross road generation!");
        // Generate crossroads
        for (int i = 0; i < this.nrBranches; i++)
        {
            Debug.Log("Generator - GenRoadNetworkBaseSegments - Cross road generated (" + i + ")!");
            Vector3 start = new Vector3(this.genPoint.x + (this.cityRadius / 2.0f) * Mathf.Cos((i + 0.5f) * (intervalRadians)),
                                        this.genPoint.y, 
                                        this.genPoint.z + (this.cityRadius / 2.0f) * Mathf.Sin((i + 0.5f) * intervalRadians));
            Vector3 end = new Vector3(this.genPoint.x + (this.cityRadius / 2.0f) * Mathf.Cos((i + 1.5f) * intervalRadians),
                                      this.genPoint.y, 
                                      this.genPoint.z + (this.cityRadius / 2.0f) * Mathf.Sin((i + 1.5f) * intervalRadians));
            network.AddRoadSegment(new RoadSegment(start, end, i));
        }
        Debug.Log("Generator - GenRoadNetworkBaseSegments - Exit!");
    }

    public void SubdivideRoads(ref RoadNetwork network, int iterations, float minBlockWidth)
    {
        Debug.Log("Generator - SubdivideRoads - Entry!");
        // For every iteration
        for (int i = iterations; i >= 0; i--)
        {
            Debug.Log("Generator - SubdivideRoads - Road generated (" + i + ")!");
            // Find parent
            int parentIndex = Random.Range(0, network.GetNrSegments() - 1);

            // Make new perpendicular segments from a random segment
            RoadSegment oldSegment = network.GetSegment(parentIndex);

            // Random distance (based on selected segment) for calculating new segment. Used for subdivision.
            /*
            TODO:
             - Math is broken for subsegments!!!!
            */

            /*float baseDist = (oldSegment.start - genPoint).magnitude * Random.Range(0.25f, 0.75f);
            Vector3 newSegmentStart = oldSegment.start + ((genPoint - oldSegment.start) / baseDist);
            Vector3 newSegmentEnd = oldSegment.start + ((oldSegment.end - genPoint) / baseDist);*/

            Vector3 newSegMidPoint = oldSegment.start + ((oldSegment.end - oldSegment.start) / Random.Range(1.5f, 2.5f));
            Vector3 newSegStart = newSegMidPoint + Vector3.Cross((oldSegment.end - oldSegment.start), ((Random.Range(0.0f, 1.0f) < 0.5f) ? Vector3.up : Vector3.down)).normalized * Random.Range(minBlockWidth, 3.0f);
            Vector3 newSegEnd = newSegMidPoint + (newSegMidPoint - newSegStart) * 2.0f;

            // Pick whether start or end should be pushed (40% for both, 20% none).
            //float randomValue = Random.Range(0.0f, 1.0f);
            // Pick strech amount.
            /*float randomStrech = Random.Range(1.2f, 1.8f);
            if (randomValue < 0.4f)
                newSegmentStart += (newSegmentEnd - newSegmentStart) * randomStrech;
            else if (randomValue > 0.4f && randomValue < 0.8f)
                newSegmentEnd += -(newSegmentEnd - newSegmentStart) * randomStrech;*/

            // Add it in for selection.
            network.AddRoadSegment(new RoadSegment(newSegStart, newSegEnd, parentIndex));

            Debug.Log("Generator - SubdivideRoads - Exit!");
        }
    }
}
