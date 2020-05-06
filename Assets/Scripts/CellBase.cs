/*
 * 
 * the script contains the base class of the logical cell.
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    private Vector2 indes;
    private Vector2 cellSize;
    private bool empty = true;
    private int idUnit = -1;

    public Vector2 Indes // assignment of a cell index and its placement on the playing field
    {
        get
        {
           return indes;
        }
        set
        {
            indes = value;
            Vector3 newPos = new Vector3();
            newPos.x = indes.y * cellSize.y + 0.5f * cellSize.y;
            newPos.z = indes.x * cellSize.x  + 0.5f * cellSize.x ;
            newPos.y = 0f;
            transform.position = newPos;
            transform.localScale = new Vector3(cellSize.y, transform.localScale.y, cellSize.x);
        }
    }


    public Vector2 CellSize //cell size
    {
        get 
        { 
            return cellSize; 
        }
        set
        {
            cellSize = value ;
        }
    }


    public bool Empty// the presence of objects on the cell
    {
        get
        {
            return empty;
        }
        set
        {
            empty = value;
        }
    }
    public int IDUnit//identifier of the object that stands on this cell
    {
        get
        {
            return idUnit;
        }
        set
        {
            idUnit = value;
        }
    }

    public Vector3 GetPosition()// cage return
    {
        return transform.position;
    }
    public Vector3 GetLocalScale()//cell size return
    {
        return transform.localScale;
    }
}
