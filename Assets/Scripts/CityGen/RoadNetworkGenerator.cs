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

    public void SubdivideRoads(ref RoadNetwork network, int iterations)
    {
        // For every iteration
        for (int i = iterations; i >= 0; i--)
        {
            // Make a temporary list to avoid infinite loop
            List<RoadSegment> temp = new List<RoadSegment>();
            // Make new perpendicular segments from a random segment
            /*for (int seg = 0; seg < network.GetNrSegments(); seg++)
            {*/
                int seg = Random.Range(0, network.GetNrSegments() - 1);
            Debug.Log(seg);
                // Find a random distance to generate
                float randomDist = Random.Range(0.3f, 0.7f);   // Random distance from segment start point in percent. Used for subdivision.
                RoadSegment oldSegment = network.GetSegment(seg);
                Vector3 newSegmentStart = oldSegment.start + Vector3.Cross((oldSegment.Direction()), Vector3.up) * Random.Range(0.4f, 0.9f); // + (segment.Direction() * randomDist);
                Vector3 newSegmentEnd = (-newSegmentStart) * Random.Range(1.5f, 2.0f);
                temp.Add(new RoadSegment(newSegmentStart, newSegmentEnd));


                /*Mathf.Tan(((360.0f / nrBranches) / 2.0f) * Mathf.Deg2Rad) * Vector3.Magnitude(oldSegmentDirection * random);
                //float randomLength = Random.Range();
                Vector3 newSegmentDirection = Vector3.Cross(oldSegmentDirection, Vector3.up) * Random.Range()
                Vector3 newSegmentStart = (oldSegmentDirection * random) + (newSegmentDirection * Random.Range(0.3f, 0.7f));
                RoadSegment newSegment;*/
            //}

            // Add all cross roads after to avoid infinite loop
            foreach (RoadSegment segment in temp)
                network.AddRoadSegment(segment);
        }
    }
}
