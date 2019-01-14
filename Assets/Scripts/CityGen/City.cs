using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public string name { get; set; } = "";
    protected Vector3 genPoint { get; set; } = Vector3.zero;
    protected float cityRadius { get; set; } = 10.0f;
    protected int nrBranches { get; set; } = 4;
    protected int nrIterations { get; set; } = 50;
    protected float minBlockWidth { get; set; } = 0.1f;

    protected RoadNetwork network = new RoadNetwork();

    public City(string name)
    {
        this.name = name;
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
