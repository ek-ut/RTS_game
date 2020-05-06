
/*
 * 
 * the script is designed to move the camera
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject BootonZoomP;
    [SerializeField]
    private GameObject BootonZoomM;
    Vector3 Point;
    private Camera cam;

    private float MinPoz;
    private float MaxPoz;
    private float MinPozZ;
    private float MaxPozZ;
    private float Delta = 60;
    void Start()
    {
        cam = GetComponent<Camera>();
        DisplacementBoundaries();
        MinPozZ = MinPozZ - 5;
        transform.position = new Vector3( MaxPoz/2, transform.position.y, MinPozZ);
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButton(0)) // moving the camera if the user pulls on the map
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - x , MinPoz , MaxPoz), transform.position.y, Mathf.Clamp(transform.position.z - y, MinPozZ , MaxPozZ));

       


        }
        /*if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            //Debug.Log("Mouv x   " + startPos.x);
            Quaternion poz = transform.rotation;
            Debug.Log(poz);
            
            transform.Rotate(y, 0, 0);




        }*/
        
        if (Input.GetMouseButton(3)) // camera distance
        {
            DisplacementBoundaries();
            Point = new Vector3(Mathf.Clamp(transform.position.x , MinPoz, MaxPoz),  60, Mathf.Clamp(transform.position.z, MinPozZ, MaxPozZ));
            transform.position = Vector3.MoveTowards(transform.position, Point, 20 * Time.deltaTime);

        }
        if (Input.GetMouseButton(4))// camera zoom
        {
            DisplacementBoundaries();
            Point = new Vector3(Mathf.Clamp(transform.position.x, MinPoz, MaxPoz), 30, Mathf.Clamp(transform.position.z, MinPozZ, MaxPozZ));
            transform.position = Vector3.MoveTowards(transform.position, Point, 20 * Time.deltaTime);

        }



    }

    public void Zoom(bool Tipe)// camera zoom through buttons
    {
        
        Vector3 Start = transform.position;
        if (Tipe)
        {
            if (transform.position.y < 60)
            {
                DisplacementBoundaries();
                MinPoz = MinPoz + 5;
                Point = new Vector3(Mathf.Clamp(Start.x, MinPoz, MaxPoz), Start.y + 10, Mathf.Clamp(Start.z, MinPozZ, MaxPozZ));
                transform.position = Point;
                if (!BootonZoomM.active)
                { BootonZoomM.active = true; }
            }else
            {
                { BootonZoomP.active = false; }
            }
        }else
        {
            if (transform.position.y > 30)
            {
                DisplacementBoundaries();
                MinPoz = MinPoz - 5;
               Point = new Vector3(Mathf.Clamp(Start.x, MinPoz, MaxPoz), Start.y - 10, Mathf.Clamp(Start.z, MinPozZ, MaxPozZ));
                transform.position = Point;
                if (!BootonZoomP.active)
                { BootonZoomP.active = true; }
            }
            else
            {
                { BootonZoomM.active = false; }
            }
        }
        
    }
    private void DisplacementBoundaries()//calculation of camera displacement boundaries
    {
        MinPoz = transform.position.y * Mathf.Tan(25F * Mathf.Deg2Rad);
        MaxPoz = 100 - MinPoz;
        MinPozZ = MinPoz - Delta;
        MaxPozZ = MaxPoz - Delta;
    }
}
