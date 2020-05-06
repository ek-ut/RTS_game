using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWood : Unit
{
    // Start is called before the first frame update
    void Start() //assignment of start parameters of the object tree
    {
        tipe = 2;
        unitSize = new Vector2(2, 2);
        amountloot = Random.Range(10, 40);
        ReSizs();
    }

    
}
