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

    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Color oldFaintColor = Gizmos.color;
        oldFaintColor.a = 0.25f;
        Color selectedFaintColor = Color.green;
        selectedFaintColor.a = 0.25f;

        if (this.cities.Count > 0)
            for (int i = 0; i < this.cities.Count; ++i)
            {
                Gizmos.color = ((this.cityLoaded == i) ? selectedFaintColor : oldFaintColor);
                Gizmos.DrawSphere(this.cities[i].genPoint, this.cities[i].cityRadius);
            }

        Gizmos.color = oldColor;
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
            found = found || (city.name == name);

        // City not found. Add it.
        if (!found)
            this.cities.Add(new City(name));
        else    // City was found. State so to user.
            Debug.Log(Global.LOG_INFO + "City already exists: " + name);
    }

    public void LoadCity(int index)
    {
        if (index < 0 || index >= this.cities.Count)
            return;

        this.cityLoaded = index;
    }

    public void DeleteCity(int index)
    {
        if (!this.ValidateCityGenState())
            return;

        this.cities[index].Clear();
        this.cities.RemoveAt(index);
        if (index <= this.cityLoaded && this.cityLoaded != -1)
            this.cityLoaded--;
    }

    public void GenerateCityBase()
    {
        if (!this.ValidateCityGenState())
            return;

        City tempCity = this.cities[this.cityLoaded];
        roadGen.GenRoadNetworkBaseSegments(ref tempCity);
        this.cities[this.cityLoaded] = tempCity;
        tempCity.Draw();

    }

    public void SubdivideCity()
    {
        if (!this.ValidateCityGenState())
            return;

        City tempCity = this.cities[this.cityLoaded];
        roadGen.SubdivideRoads(ref tempCity);
        this.cities[this.cityLoaded] = tempCity;
    }

    public void DrawCity()
    {
        Debug.Log("CityGen - DrawCity - Entry!");
        if (!this.ValidateCityGenState())
            return;

        this.cities[this.cityLoaded].Draw();
    }

    public void ClearCity()
    {
        if (!this.ValidateCityGenState())
            return;
        
        this.cities[this.cityLoaded].Clear();
    }

    public bool ValidateCityGenState(bool verbose = true)
    {
        if (this.cities == null)
        {
            if (verbose)
                Debug.Log("I have no list to save cities, this should not appear, try restarting CityGen!");
            return false;
        }

        if (this.cities.Count <= 0)
        {
            if (verbose)
                Debug.Log(Global.LOG_INFO + "I'm missing cities here! Make some new cities first!");
            return false;
        }
        
        if (this.cityLoaded < 0)
        {
            if (verbose)
                Debug.Log(Global.LOG_INFO + "I don't know what city you're talking about! Please select one on the top-left!");
            return false;
        }

        if (this.cities[this.cityLoaded] == null)
        {
            if (verbose)
                Debug.Log(Global.LOG_INFO + "Something went wrong with the city! It apparently doesn't exist.");
            return false;
        }

        // Nothing went wrong!
        return true;
    }
    #endif
}
