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
        for (int i = 0; i < this.nrBranches; i++)
        {
            Vector3 start = new Vector3(this.genPoint.x + this.cityRadius * Mathf.Cos(i * intervalRadians),
                                        this.genPoint.y, 
                                        this.genPoint.z + this.cityRadius * Mathf.Sin(i * intervalRadians));
            Vector3 end = new Vector3(this.genPoint.x + this.cityRadius * Mathf.Cos((i + 1) * intervalRadians),
                                      this.genPoint.y, 
                                      this.genPoint.z + this.cityRadius * Mathf.Sin((i + 1) * intervalRadians));
            network.AddRoadSegment(new RoadSegment(i, start, end));

                                    /*new Vector3(Mathf.Cos(i) * radius + pos.x, 
                                                pos.y, 
                                                Mathf.Sin(i) * radius + pos.z), 
                                    new Vector3(Mathf.Cos(i + 0.015f) * radius + pos.x, 
                                                pos.y, 
                                                Mathf.Sin(i + 0.015f) * radius + pos.z)*/
        }
    }

    public void SubdivideRoads(ref RoadNetwork network, int iterations)
    {
        /*for (int i = iterations; i >= 0; i--)
            for (int seg = 0; seg < network.GetNrSegments(); seg++)
            {
                float random = Random.Range(0.4f, 0.6f);   // Random distance from generation point in percent. Used for subdivision.
                RoadSegment segment = network.GetSegment(i);
                Vector3 newSegmentStart = Vector3.Cross((segment.Direction() * Random.Range(0.5f, 1.0f)), Vector3.up) + (segment.Direction() * random);
                Vector3 newSegmentEnd = (-newSegmentStart) * Random.Range(0.5f, 1.0f);


                /*Mathf.Tan(((360.0f / nrBranches) / 2.0f) * Mathf.Deg2Rad) * Vector3.Magnitude(oldSegmentDirection * random);
                //float randomLength = Random.Range();
                Vector3 newSegmentDirection = Vector3.Cross(oldSegmentDirection, Vector3.up) * Random.Range()
                Vector3 newSegmentStart = (oldSegmentDirection * random) + (newSegmentDirection * Random.Range(0.3f, 0.7f));
                RoadSegment newSegment;*
            }*/
    }
}
