/*
 * 
 * 
 *the script is responsible for managing the grid on the playing field
 * 
 * 
 * 
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGrid : MonoBehaviour
{
    [SerializeField]
    private Terrain _mTerrain;
    [SerializeField]
    private uint indexTextureGrid = 1;
    private uint indexTextureTerrain = 0;

    //[SerializeField]
    private float lineThickness = 1f;

    [SerializeField]
    private uint rowCount = 10;

    [SerializeField]
    private uint columCount = 10;

    [SerializeField]
    private bool showGrid = false;
    private bool chengShowGrid = false;

    [SerializeField]
    private GameObject CellPrefab;


    private float TerrainWidthRial;
    private float TerrainHeightRial;
    private int LinePixWidth;
    private int LinePixHeight;
    public CellBase[] ArrCell;

    public uint GetrowCount()
    {
        return rowCount;
    }

    public uint GetcolumCount()
    {
        return columCount;
    }


    public float GetTerrainWidthRial()// returns the length of the playing field
    {
        return  TerrainWidthRial;
    }
    public float GetTerrainHeightRial()// returns the width of the playing field
    {
        return TerrainHeightRial;
    }
    void SgowGridOnTerrain(TerrainData pTD) //draws or removes the grid
    {
        float[,,] alphas = pTD.GetAlphamaps(0, 0, pTD.alphamapWidth, pTD.alphamapHeight);
        for(int i = 0; i < pTD.alphamapWidth; i++)
        {
            for(int j = 0; j < pTD.alphamapHeight; j++)
            {
                if(ChekLine(pTD, i, j))
                {
                    for(int l = 0; l < pTD.alphamapLayers; l++)
                    {
                        alphas[i, j, l] = 0f;
                    }
                    if (showGrid)
                    {
                        alphas[i, j, indexTextureGrid] = 1f;
                    }
                    else
                    {
                        alphas[i, j, indexTextureTerrain] = 1f;
                    }
                }
            }
        }
        pTD.SetAlphamaps(0, 0, alphas);
        
    }

    bool ChekLine(TerrainData pTD, int pi, int pj) //check if the pixel falls on the cell line
    {
        

        float fx = Mathf.Abs(pi % (pTD.alphamapWidth / columCount) - (pTD.alphamapWidth / columCount));
        float fy = Mathf.Abs(pj % (pTD.alphamapHeight / rowCount) - (pTD.alphamapHeight / rowCount));

        if (fx < LinePixWidth/2 || fy < LinePixHeight/2)
        {
           
            return true;
        }

        return false;
    }
    void Start() //initialization of values
    {

        TerrainData pTD = _mTerrain.terrainData;
         TerrainWidthRial = pTD.size.x;
         TerrainHeightRial = pTD.size.z;
        LinePixWidth = (int)((lineThickness / TerrainWidthRial) * (float)pTD.alphamapWidth);
         LinePixHeight = (int)(lineThickness / TerrainHeightRial * (float)pTD.alphamapHeight);
       

        GameObject go = GameObject.Find("UnitManager");
        UnitManager um = go.GetComponent<UnitManager>();
        um.init();
        um.CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if(showGrid != chengShowGrid) //check if the user has changed the state of visibility of the grid
        {
            chengShowGrid = showGrid;
            SgowGridOnTerrain(_mTerrain.terrainData);
        }
    }

    public void ChengShowGrid()// mesh visibility change
    {
        showGrid = !showGrid;
    }
}
