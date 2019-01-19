using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    // This determins the level of randomization in roadnetworks and how building are placed.
    /*[Range(0.0f, 1.0f)]
    public float randomizationFactor;*/

    public string name { get; set; } = "";
    public Vector3 genPoint { get; set; } = Vector3.zero;
    public float cityRadius { get; set; } = 10.0f;
    public int nrBranches { get; set; } = 4;
    public int nrIterations { get; set; } = 50;
    //public float minBlockWidth { get; set; } = 0.1f;

    public RoadNetwork network = new RoadNetwork();

    public City(string name)
    {
        this.name = name;
    }

    public City(City other)
    {
        this.name = other.name;
        this.genPoint = other.genPoint;
        this.cityRadius = other.cityRadius;
        this.nrBranches = other.nrBranches;
        this.nrIterations = other.nrIterations;
        //this.minBlockWidth = other.minBlockWidth;
    }

    public void Draw()
    {
        if (this.network == null)
            return;

        this.network.Draw();
    }

    public void Clear()
    {
        Reset();
    }

    public void Reset()
    {
        this.name = "";
        this.genPoint = Vector3.zero;
        this.cityRadius = 1.0f;
        this.nrBranches = 4;
        this.nrIterations = 50;
        //this.minBlockWidth = 0.1f;

        this.network.Clear();
    }

    public bool Equals(City other)
    {
        return (this.name == other.name);
    }

    public override string ToString()
    {
        return this.name;
    }
}
