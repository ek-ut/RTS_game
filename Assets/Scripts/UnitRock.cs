using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRock : Unit
{
    // Start is called before the first frame update
    void Start()//assignment of starting parameters of the stone object
    {
        tipe = 1;
        unitSize = new Vector2(3, 3);
        amountloot = Random.Range(5, 20);
        ReSizs();

    }

   
}
