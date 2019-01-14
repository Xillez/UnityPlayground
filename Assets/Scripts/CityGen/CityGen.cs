using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class CityGen : MonoBehaviour
{
    #if UNITY_EDITOR
    // Follow this: http://cybercritics-critic.blogspot.com/2015/08/procedural-city-generation-in-unity3d.html

    /*
        TODO: 
        - Move all city relevant variables to city and add setters there. (nrBranches, nrIterations, minBlockWidth, genPoint, cityRadius)
    */

    // This determins the level of randomization in roadnetworks and how building are placed.
    /*[Range(0.0f, 1.0f)]
    public float randomizationFactor;*/
    public int nrBranches = 4;
    public int nrIterations = 50;
    public float minBlockWidth = 0.1f;
    public Vector3 genPoint;
    public float cityRadius = 10.0f;

    public List<City> cities = new List<City>();
    public int cityLoaded = -1;
    RoadNetwork network = new RoadNetwork();
    private RoadNetworkGenerator roadGen = new RoadNetworkGenerator();
    //private AssetManager assetMgr = new AssetManager();

    public void Start()
    {
        //
    }

    public void Update()
    {
        //
    }

    public void OnFocus()
    {
        //
    }

    public void NewCity(string name)
    {
        // No name written, abort addition.
        if (name == "")
            return;

        // Found city in list.
        bool found = false;
        foreach (City city in this.cities)
        {
            found = found || (city.name == name);
        }

        // City not found. Add it.
        if (!found)
        {
            this.cities.Add(new City(name));
        }
        else    // City was found. State so to user.
        {
            Debug.Log("City already exists: " + name);
        }
    }

    public void LoadCity(int index)
    {
        if (index < 0 || index >= this.cities.Count)
            return;

        this.cityLoaded = index;
    }

    public void DeleteCity(string name)
    {
        //
    }

    public void GenerateCity()
    {
        //
    }

    public void DrawCity()
    {
        Debug.Log("CityGen - DrawCity - Entry!");
        if (this.nrBranches < 1)
            Debug.Log("Cannot generate city with no branches (Nr Branches > 0)!");
        /*if (this.genPoint == null)
            Debug.Log("Can't generate city with no point/position");*/
        if (this.cityRadius < 0.01f)
            Debug.Log("City radius too small!");

        roadGen.Init(this.genPoint, this.cityRadius, this.nrBranches);
        roadGen.GenRoadNetworkBaseSegments(ref this.network);
        roadGen.SubdivideRoads(ref network, this.nrIterations, this.minBlockWidth);
        network.Draw();
        network.Clear();
    }
    #endif
}
