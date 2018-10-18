using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityGen
{
    // Follow this: http://cybercritics-critic.blogspot.com/2015/08/procedural-city-generation-in-unity3d.html

    private int nextCityId = 0;
        
    // This determins the level of randomization in roadnetworks and how building are placed.
    [Range(0.0f, 1.0f)]
    public float randomizationFactor;
    public int NrBranches;
    public Vector3 genPoint;

    private List<City> cities = new List<City>();
    private int cityLoaded = -1;
    private RoadNetworkGenerator roadGen = new RoadNetworkGenerator();
    private AssetManager assetMgr = new AssetManager();

    public void Start()
    {
        if (NrBranches < 1)
            Debug.Log("Cannot generate city with no branches (Nr Branches > 0)!");
        if (genPoint == null)
            Debug.Log("Can't generate city with no point/position");
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
}
