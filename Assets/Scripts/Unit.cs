
/*
 * base class for visual objects on stage
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int ID;
    protected Vector2 unitSize;
    protected Vector2 gridSize;
    protected Vector3 position;
    protected bool loot = true;
    protected int amountloot;// = Random.Range(5,20);
    protected int tipe;
    protected float RotationY = 0;


    

   

    public int IDUnit
    {
        get
        {
            return ID;
        }
        set
        {
            ID = value;
        }
    }

    public Vector2 UnitSize
    {
        get
        {
            return unitSize;
        }
        set
        {
            unitSize = value;
        }
    }
    public bool Loot
    {
        get
        {
            return loot;
        }
        set
        {
            loot = value;
        }
    }
    public int Amountloot
    {
        get
        {
            return amountloot;
        }
        set
        {
            amountloot = value;
        }
    }
    public int Tipe
    {
        get
        {
            return tipe;
        }
        set
        {
            tipe = value;
        }
    }
    public Vector2 GridSize
    {
        get
        {
            return gridSize;
        }
        set
        {
            gridSize = value;
        }
    }
    public void Setposition(Vector3 vPos)
    {
       
        position = vPos;
        
    }

    public void ReSizs()//calculation of the size of the object depending on the size of the cell on which the object was placed
    {
        Vector3 newPos = new Vector3();
       
        newPos.y = 0f;
        if (tipe == 1)
        {
            newPos.x = position.x + gridSize.y;
            newPos.z = position.z + gridSize.x;
            float x = (gridSize.y * 512 * unitSize.y) / 50;
            float y = (gridSize.x * 512 * unitSize.x) / 50;
            if(x==0 || y==0)
            {
                x = 25;
                y = 25;
            }
            transform.localScale = new Vector3(2 * x, 2 * y, 2 * y);
        }else if(tipe == 2)
        {
            newPos.x = position.x + 0.5f * gridSize.y;
            newPos.z = position.z + 0.5f * gridSize.x;
        }
        transform.position = newPos;
    }

    public void Sizs(Vector3 newPos) // assignment of a new size and rotation of 90 degrees
    {
        transform.localScale = newPos;
        if (tipe == 2)
        { 
            transform.Rotate(90, 0, 0);
        }
    }
    public void TP(Vector3 pos)//setting an object to a position
    {
        transform.position = pos;
    }

    public void TR()// object rotation by 1 degree
    {
        if (tipe == 2)
        {
            transform.Rotate(0f, 1f, 0f); 

        }

        else
        {
            transform.Rotate(0f, 0f, 1f);
        }
        
    }
    public void destroy()//destruction of an object
    {

        Destroy(this.gameObject);

    }
}
