/*
 * 
 * script designed to control game objects
 * 
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject RockPrefab;
    [SerializeField]
    private GameObject CellPrefab;
    [SerializeField]
    private GameObject WoodPrefab;
    [SerializeField]
    private GameObject FogPrefab;
    [SerializeField]
    private Text textLog;
    private float TerrainWidthRial;
    private float TerrainHeightRial;
    private uint rowCount;
    private uint columCount;
    private List<Unit> unitPool = new List<Unit>();
    private List<CellBase> ArrCell = new List<CellBase>();
    private bool newUnit = false;
    private int TipeNewUnit = 0;
    private bool ShopClick = false;
    private Unit newObject;
    private bool bnewObject;
    private int IDUnit = 0;


    void Update()
    {
        if(Input.GetMouseButtonUp(0) && !newUnit) // выбор обекта на поле
        {
            
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 9))//нажато на пустую клетку...
            selectUnit(hit.transform.position);


        }
        if (newUnit && Input.GetMouseButtonUp(0)) //проверка на установку нового обекта
        {
            if (ShopClick)
            {
                ShopClick = false;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 9))// проверка о нажатитт на логическую клетку
                {

                    bool bOk = false;
                    if (TipeNewUnit == 1)
                    {
                        bOk = CreateRock(new Vector2(hit.transform.localScale.x, hit.transform.localScale.z), hit.transform.position);
                    }
                    else if (TipeNewUnit == 2)
                    {
                        bOk = CreateWood(new Vector2(hit.transform.localScale.x, hit.transform.localScale.z), hit.transform.position);
                    }
                    if (bOk)
                    { 
                        newUnit = false;
                        bnewObject = false;
                        newObject.destroy();
                        textLog.text = "";
                    }

                }
            }
        }
        UpdateNewUnit();
        
    }

    public void init()//initialization of values
    {
        GameObject go = GameObject.Find("Terrain");
        TerrainGrid tg = go.GetComponent<TerrainGrid>();
        TerrainWidthRial = tg.GetTerrainWidthRial();
        TerrainHeightRial = tg.GetTerrainHeightRial();
        rowCount = tg.GetrowCount();
        columCount = tg.GetcolumCount();
        CreateFog();
        
    }

    private void CreateFog()// creating fog along the edges of the field
    {
        int SizeZ = (int)TerrainHeightRial / 33;
        GameObject FogObject;
        for (int i = 0; i < SizeZ; i++)
        {
            FogObject = Instantiate(FogPrefab);
            FogObject.transform.position = new Vector3(-2, FogObject.transform.position.y, i * 33);

            FogObject = Instantiate(FogPrefab);
            FogObject.transform.position = new Vector3(102, FogObject.transform.position.y, i * 33);
        }

        int SizeX = (int)TerrainHeightRial / 33;
        for (int i = 0; i < SizeX; i++)
        {
            FogObject = Instantiate(FogPrefab);
            FogObject.transform.Rotate(0f, 90f, 0f);
            FogObject.transform.position = new Vector3(i * 33, FogObject.transform.position.y, -3);

            FogObject = Instantiate(FogPrefab);
            FogObject.transform.Rotate(0f, 90f, 0f);
            FogObject.transform.position = new Vector3(i * 33, FogObject.transform.position.y, 101);
        }
    }

    public void CreateGrid()//creation of functional cells and their arrangement on the playing field
    {
        
        for (int i = 0; i < columCount; i++)// создание и размещение логических клеток
        {
            for (int j = 0; j < rowCount; j++)
            {
                GameObject CellObject = Instantiate(CellPrefab);
                CellBase cell = CellObject.GetComponent<CellBase>();
                cell = CellObject.GetComponent<CellBase>();
                cell.CellSize = new Vector2(TerrainWidthRial / columCount, TerrainHeightRial / rowCount);
                cell.Indes = new Vector2(i, j);
                ArrCell.Add(cell);


            }
        }
        CreatrStsrtUnit();
    }

    private bool CreateRock(Vector2 pIndex) //stone object creation
    {
        int size = ArrCell.Count;
        CellBase cell = ArrCell[0];
        for (int i = 0; i < size; i++)
        {
            cell = ArrCell[i];
            if (cell.Indes.x == pIndex.x && cell.Indes.y == pIndex.y)
            {

                break;
            }
        }
        Vector3 LS = cell.GetLocalScale();
       return CreateRock(new Vector2(LS.x, LS.z), cell.GetPosition());

    }
    private bool CreateRock(Vector2 GridSize, Vector3 Setposition)//stone object creation
    {
        Vector2 vIndex = serchGridIndex(Setposition);
        if (CheckEmptyGrid(vIndex, new Vector2(3, 3)))
        {
            GameObject RockObject = Instantiate(RockPrefab);
            UnitRock rock = RockObject.GetComponent<UnitRock>();
            rock.GridSize = GridSize;
            rock.Setposition(Setposition);
            rock.IDUnit = IDUnit;

            if (vIndex.x > -1)
            {
                FillGrid(IDUnit, vIndex, new Vector2(3, 3));
            }
            unitPool.Add(rock);
        }else
        {
            return false;
        }
        IDUnit++;
        return true;
    }

    private bool CreateWood(Vector2 pIndex)//create object tree
    {
        int size = ArrCell.Count;
        CellBase cell = ArrCell[0];
        for (int i = 0; i < size; i++)
        {
            cell = ArrCell[i];
            if (cell.Indes.x == pIndex.x && cell.Indes.y == pIndex.y)
            {

                break;
            }
        }
        Vector3 LS = cell.GetLocalScale();
        return CreateWood(new Vector2(LS.x, LS.z), cell.GetPosition());

    }

    private bool CreateWood(Vector2 GridSize, Vector3 Setposition)//create object tree
    {
        Vector2 vIndex = serchGridIndex(Setposition);
        if (CheckEmptyGrid(vIndex, new Vector2(2, 2)))
        {
            GameObject WoodObject = Instantiate(WoodPrefab);
            UnitWood wood = WoodObject.GetComponent<UnitWood>();
            wood.GridSize = GridSize;
            wood.Setposition(Setposition);
            wood.IDUnit = IDUnit;
            if (vIndex.x > -1)
            {
                FillGrid(IDUnit, vIndex, new Vector2(2, 2));
            }
            unitPool.Add(wood);
        }
        else
        {
            return false;
        }
        IDUnit++;
        return true;
    }

    private CellBase serchGrid(Vector3 Setposition)// functional cell search
    {
        int size = ArrCell.Count;
        CellBase cell;
        for (int i = 0; i < size; i++)
        {
            cell = ArrCell[i];
            if (cell.transform.position.x == Setposition.x && cell.transform.position.y == Setposition.y && cell.transform.position.z == Setposition.z)
            {

                return cell;
            }
        }

        return null;
    }
    private Vector2 serchGridIndex(Vector3 Setposition)//functional cell search
    {
        CellBase cell = serchGrid(Setposition);
        if (cell != null)
        {
            return cell.Indes;
        }
        return new Vector2(-1, -1);
        
    }

    private void FillGrid(int idUnit, Vector2 pIndex, Vector2 pSixe)//assignment of a new status to all cells on which the object is set
    {
        int size = ArrCell.Count;
        CellBase cell;
        for (int i = 0; i < size; i++)
        {
            cell = ArrCell[i];
            if (cell.Indes.x >= pIndex.x && cell.Indes.x < pIndex.x + pSixe.x && cell.Indes.y >= pIndex.y && cell.Indes.y < pIndex.y + pSixe.y )
            {
                
                cell.Empty = false;
                cell.IDUnit = idUnit;



            }
        }
    }

    public bool CheckEmptyGrid(Vector2 pIndex, Vector2 pSixe)//check if the area is busy where they want to add a new object
    {
        int size = ArrCell.Count;
        CellBase cell;
        for (int i = 0; i < size; i++)
        {
            cell = ArrCell[i];
            if (cell.Indes.x >= pIndex.x && cell.Indes.x < pIndex.x + pSixe.x && cell.Indes.y >= pIndex.y && cell.Indes.y < pIndex.y + pSixe.y)
            {
                
                if(!cell.Empty )
                {
                    return false;
                }

                
            }
        }
        return true;
    }

    private void CreatrStsrtUnit()// creation of decorative objects around the perimeter of the map
    {
       
        for (int i = 0; i < columCount / 3; i++)
        {
            CreateRock(new Vector2((int)Random.Range(0, columCount), (int)Random.Range(0, 2)));
            CreateWood(new Vector2((int)Random.Range(0, columCount), (int)Random.Range(0, 3)));
            CreateWood(new Vector2((int)Random.Range(0, columCount), (int)Random.Range(0, 3)));
        }

        for (int i = 0; i < columCount / 3; i++)
        {
            CreateRock(new Vector2((int)Random.Range(0, columCount), (int)Random.Range(rowCount -2, rowCount)));
            CreateWood(new Vector2((int)Random.Range(0, columCount), (int)Random.Range(rowCount-3, rowCount)));
            CreateWood(new Vector2((int)Random.Range(0, columCount), (int)Random.Range(rowCount-3, rowCount)));
        }

        for (int i = 0; i < columCount / 3; i++)
        {
            CreateRock(new Vector2((int)Random.Range(0, 2), (int)Random.Range(0, rowCount)));
            CreateWood(new Vector2((int)Random.Range(0, 3), (int)Random.Range(0, rowCount)));
            CreateWood(new Vector2((int)Random.Range(0, 3), (int)Random.Range(0, rowCount)));
        }

        for (int i = 0; i < columCount / 3; i++)
        {
            CreateRock(new Vector2((int)Random.Range(columCount - 2, columCount), (int)Random.Range(0 , rowCount)));
            CreateWood(new Vector2((int)Random.Range(columCount- 3, columCount), (int)Random.Range(0, rowCount)));
            CreateWood(new Vector2((int)Random.Range(columCount- 3, columCount), (int)Random.Range(0, rowCount)));
        }
        int count = unitPool.Count;
        for(int i = 0; i < count; i ++)
        {
            unitPool[i].Loot = false;
            unitPool[i].Amountloot = 1;
        }


    }

    public void NewUnit(int TipeUnit)// creating a temporary object when buying it in a store
    {
        TipeNewUnit = TipeUnit;
        newUnit = true;
        ShopClick = true;
        bnewObject = true;
        if(TipeUnit == 2)
        {
            GameObject WoodObject = Instantiate(WoodPrefab);
            newObject  = WoodObject.GetComponent<UnitWood>();
            newObject.Tipe = TipeUnit;
            newObject.Sizs(new Vector3(0.1f, 0.1f, 0.1f));
            textLog.text = "Tree selected for posting";
        }
        else
        {
            GameObject WoodObject = Instantiate(RockPrefab);
            newObject  = WoodObject.GetComponent<UnitRock>();
            textLog.text = "Stone selected for posting";

        }
    }

    private void UpdateNewUnit()//change of position in space of a temporary object
    {
        if(bnewObject)
        {
            //Vector3 v = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 v = cam.transform.position;
            newObject.TP(new Vector3(v.x + 6 , v.y -30, v.z + 3));
            newObject.TR();
        }


    }

    private void selectUnit(Vector3 cellPosition) //receiving information about the selected object on the field
    {
        CellBase cell = serchGrid(cellPosition);
        if(cell.Empty)
        {
            textLog.text = "";

        }
        else
        {
            int count = unitPool.Count;
            for (int i = 0; i < count; i++)
            {
                if (unitPool[i].IDUnit == cell.IDUnit)
                {
                    string stipe = "";
                    if(unitPool[i].Tipe == 1)
                    {
                        stipe = "Tipe - Rock";
                    }
                    else if(unitPool[i].Tipe == 2)
                    {
                        stipe = "Tipe - Wood";
                    }
                    textLog.text = "Object ID  = " + unitPool[i].IDUnit + "\n"
                        + stipe + "\n"
                        + "loot - " + unitPool[i].Loot + "\n"
                        + "Amount loot  =  " + unitPool[i].Amountloot;

                }
            }
            
        }
    }
}
