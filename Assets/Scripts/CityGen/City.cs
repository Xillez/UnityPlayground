using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCity
{
    protected int id;
}

public class City : BaseCity
{
    public City(int id)
    {
        this.id = id;
    }
}
