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
        int nrBaseSegments = Random.Range(3, 7);
        float randomDistance = Random.Range(0.3f, 1.0f);
        for (int i = 0; i < nrBaseSegments; i++)
        {
            RoadSegment road = new RoadSegment(nrBranches++, Vector3.zero, Vector3.zero);
        }
    }

    public void SubdivideRoads(ref RoadNetwork network, int iterations)
    {
        for (int i = iterations; i >= 0; i--)
            for (int seg = 0; seg < network.getNrSegments(); seg++)
            {
                float random = Random.Range(0.4f, 0.6f);   // Random distance from generation point in percent. Used for subdivision.
                RoadSegment segment = network.getSegment(i);
                Vector3 oldSegmentDirection = segment.end - segment.start;

                /*Mathf.Tan(((360.0f / nrBranches) / 2.0f) * Mathf.Deg2Rad) * Vector3.Magnitude(oldSegmentDirection * random);
                //float randomLength = Random.Range();
                Vector3 newSegmentDirection = Vector3.Cross(oldSegmentDirection, Vector3.up) * Random.Range()
                Vector3 newSegmentStart = (oldSegmentDirection * random) + (newSegmentDirection * Random.Range(0.3f, 0.7f));
                RoadSegment newSegment;*/
            }
    }
}
