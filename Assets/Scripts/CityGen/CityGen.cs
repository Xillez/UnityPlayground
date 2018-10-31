using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityGen
{
    // Follow this: http://cybercritics-critic.blogspot.com/2015/08/procedural-city-generation-in-unity3d.html

    private int nextCityId = 0;
        
    // This determins the level of randomization in roadnetworks and how building are placed.
    /*[Range(0.0f, 1.0f)]
    public float randomizationFactor;*/
    public int nrBranches = 6;
    public float minBlockWidth;
    public Vector3 genPoint;
    public float cityRadius = 10.0f;

    private List<City> cities = new List<City>();
    //private int cityLoaded = -1;
    RoadNetwork network = new RoadNetwork();
    private RoadNetworkGenerator roadGen = new RoadNetworkGenerator();
    //private AssetManager assetMgr = new AssetManager();

    public void Start()
    {
        Debug.Log("[" + this.GetType().Name + "]: Start() - entry");
        if (this.nrBranches < 1)
            Debug.Log("Cannot generate city with no branches (Nr Branches > 0)!");
        /*if (this.genPoint == null)
            Debug.Log("Can't generate city with no point/position");*/
        if (this.cityRadius < 0.01f)
            Debug.Log("City radius too small!");

        Debug.Log("[" + this.GetType().Name + "]: Start() - roadGen.Init()");
        roadGen.Init(this.genPoint, this.cityRadius, this.nrBranches);
        Debug.Log("[" + this.GetType().Name + "]: Start() - roadGen.GenRoadNetwork()");
        roadGen.GenRoadNetworkBaseSegments(ref this.network);       
    }

    public void Update()
    {
        //
    }

    public void OnFocus()
    {
        //
    }

    public void NewCity()
    {
        this.cities.Add(new City(this.nextCityId++));
    }

    public void DeleteCity(int id)
    {
        //
    }

    public void GenerateCity()
    {
        //
    }

    public void DrawCity()
    {
        Debug.Log("[" + this.GetType().Name + "]: DrawCity() - network.Draw()");
        network.Draw();
    }
}
