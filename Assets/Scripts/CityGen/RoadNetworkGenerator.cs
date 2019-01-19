using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetworkGenerator
{
    public RoadNetworkGenerator()
    {   
        //
    }

    public void GenRoadNetworkBaseSegments(ref City city)
    {
        if (!ValidateCity(city))
            return;

        Debug.Log("Generator - GenRoadNetworkBaseSegments - Entry!");
        float intervalRadians = (360.0f / city.nrBranches) * Mathf.Deg2Rad;
        Debug.Log("Generator - GenRoadNetworkBaseSegments - Base segments generation!");
        // Generate mainroads
        for (int i = 0; i < city.nrBranches; i++)
        {
            Debug.Log("Generator - GenRoadNetworkBaseSegments - Element generated (" + i + ")!");
            Vector3 end = new Vector3(city.genPoint.x + city.cityRadius * Mathf.Cos((i) * intervalRadians),
                                      city.genPoint.y,
                                      city.genPoint.z + city.cityRadius * Mathf.Sin((i) * intervalRadians));
            city.network.AddRoadSegment(new RoadSegment(city.genPoint, end, 0));
        }

        Debug.Log("Generator - GenRoadNetworkBaseSegments - Base Cross road generation!");
        // Generate crossroads
        for (int i = 0; i < city.nrBranches; i++)
        {
            Debug.Log("Generator - GenRoadNetworkBaseSegments - Cross road generated (" + i + ")!");
            Vector3 start = new Vector3(city.genPoint.x + (city.cityRadius / 2.0f) * Mathf.Cos((i + 0.5f) * (intervalRadians)),
                                        city.genPoint.y, 
                                        city.genPoint.z + (city.cityRadius / 2.0f) * Mathf.Sin((i + 0.5f) * intervalRadians));
            Vector3 end = new Vector3(city.genPoint.x + (city.cityRadius / 2.0f) * Mathf.Cos((i + 1.5f) * intervalRadians),
                                      city.genPoint.y, 
                                      city.genPoint.z + (city.cityRadius / 2.0f) * Mathf.Sin((i + 1.5f) * intervalRadians));
            city.network.AddRoadSegment(new RoadSegment(start, end, i));
        }
        Debug.Log("Generator - GenRoadNetworkBaseSegments - Exit!");
    }

    public void SubdivideRoads(ref City city)
    {
        if (!ValidateCity(city))
            return;

        Debug.Log("Generator - SubdivideRoads - Entry!");
        // For every iteration
        for (int i = city.nrIterations; i >= 0; i--)
        {
            Debug.Log("Generator - SubdivideRoads - Road generated (" + i + ")!");
            // Find parent
            int parentIndex = Random.Range(0, city.network.GetNrSegments() - 1);

            // Make new perpendicular segments from a random segment
            RoadSegment oldSegment = city.network.GetSegment(parentIndex);

            // Random distance (based on selected segment) for calculating new segment. Used for subdivision.
            /*
            TODO:
             - Math is broken for subsegments!!!!
            */

            /*float baseDist = (oldSegment.start - genPoint).magnitude * Random.Range(0.25f, 0.75f);
            Vector3 newSegmentStart = oldSegment.start + ((genPoint - oldSegment.start) / baseDist);
            Vector3 newSegmentEnd = oldSegment.start + ((oldSegment.end - genPoint) / baseDist);*/

            Vector3 newSegMidPoint = oldSegment.start + ((oldSegment.end - oldSegment.start) / Random.Range(1.5f, 2.5f));                                                                     //  v--------------Should be minBlockWidth!
            Vector3 newSegStart = newSegMidPoint + Vector3.Cross((oldSegment.end - oldSegment.start), ((Random.Range(0.0f, 1.0f) < 0.5f) ? Vector3.up : Vector3.down)).normalized * Random.Range(1.0f, 3.0f);
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
            city.network.AddRoadSegment(new RoadSegment(newSegStart, newSegEnd, parentIndex));

            Debug.Log("Generator - SubdivideRoads - Exit!");
        }
    }

    public bool ValidateCity(City city)
    {
        if (city.nrBranches < 1)
        {
            Debug.Log("Cannot generate city with no branches (Nr Branches > 0)!");
            return false;
        }

        /*if (this.genPoint == null)
            Debug.Log("Can't generate city with no point/position");*/
        if (city.cityRadius < 0.01f)
        {
            Debug.Log("City radius too small!");
            return false;
        }

        return true;
    }
}
