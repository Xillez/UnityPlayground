using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class CityGen : MonoBehaviour
{
    #if UNITY_EDITOR
    // Follow this: http://cybercritics-critic.blogspot.com/2015/08/procedural-city-generation-in-unity3d.html

    public List<City> cities = new List<City>();
    public int cityLoaded = -1;
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

        City tempCity = this.cities[this.cityLoaded];
        roadGen.GenRoadNetworkBaseSegments(ref tempCity);
        roadGen.SubdivideRoads(ref tempCity);
        this.cities[this.cityLoaded] = tempCity;
        this.cities[this.cityLoaded].Draw();
        //this.cities[this.cityLoaded].Clear();
    }
    #endif
}
