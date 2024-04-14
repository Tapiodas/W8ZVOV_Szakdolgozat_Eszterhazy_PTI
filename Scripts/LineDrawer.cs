using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class LineDrawer : MonoBehaviour
{ 
    public List<GameObject> linePointsGameobject;
    public List<Vector3> linePoints;
    public GameObject Wallparent;

    [SerializeField]
    float timer;
    public float timerdelay;

    public GameObject Wall;
 
    GameObject newLine;
    LineRenderer drawLine;
    float ms;
   // public float lineWidth;
    public Vector3 delayedPosition;

    public bool stopCoroutine;

    float tavolsagSokszorosito = 2.0f;

    // public GameObject startPoint;
    // public GameObject endPoint;
    [SerializeField]
    Vector3 p0;
    [SerializeField]
    Vector3 p3;
    [SerializeField]
    Vector3 p1;
    [SerializeField]
    Vector3 p2;


    [SerializeField]
    public List<Vector3> besierPontok;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject Wallparent1 = new GameObject();
        //Wallparent1.name = "WallparentLinaDrawerben";
        //Wallparent = Wallparent1;
        // Wallparent.transform.SetParent(this.gameObject.transform);
        // Wallparent.transform.localPosition = new Vector3(0, 0, 0);
        besierPontok =new List<Vector3>();

           timer = timerdelay;
       
        // float ms = GetComponent<AI>().moveSpeed;
        //newLine = new GameObject();
        //drawLine = GetComponent<LineRenderer>();

        //drawLine.startWidth = lineWidth;
        //drawLine.endWidth = lineWidth;

        //drawLine.positionCount = linePointsGameobject.Count;
        p3 = this.transform.position - this.transform.forward * tavolsagSokszorosito;
     
        stopCoroutine = false;
    }

    // Update is called once per frame
    private void wallmaker(Vector3 position)
    {
        GameObject wall;
       if (Wallparent != null)
        {
            wall = Instantiate(Wall, position, Quaternion.identity, Wallparent.transform);
            wall.transform.forward = this.transform.forward;
            linePointsGameobject.Add(wall);

        }

       

            //if (!stopCoroutine)
            //{

            //}
         
       
        
       

    }
    void FixedUpdate()
    {


        //Debug.Log(stopCoroutine+" Fal generálás");

        if (!stopCoroutine) {

        timer -= Time.deltaTime;



     
        if (besierPontok.Count.Equals(4))
        {
            timer = timerdelay;
           


            //StartCoroutine( GenerateBezierCurve(p0, p3));

            GenerateBezierCurve(besierPontok);
                // delayedPosition = transform.TransformDirection(new Vector3(0f, 0f, -2f)) + transform.position;

                //  GameObject wall = Instantiate(Wall, delayedPosition, Quaternion.identity);
                // wall.transform.forward = transform.forward;
                //linePointsGameobject.Add(wall);


            }
            else
            {
                if (timer <= 0) { besierPontok.Add(this.transform.position - this.transform.forward * tavolsagSokszorosito); timer = timerdelay; }
            }


            //foreach (var item in linePointsGameobject)
            //{
            //    linePoints.Add(item.transform.position);
            //}

            //drawLine.SetPositions(linePoints.ToArray());

            //linePoints.Clear();

            // timer -= Time.deltaTime;
        }
    }
    private void GenerateBezierCurve(List <Vector3>besierPontok)
    {


        // Két irányítópont helye (itt csak egy egyenes szakaszt hozunk létre)
        //Vector3 p1 = p0 + Vector3.forward * 2f;
        //Vector3 p2 = p3 + Vector3.back * 2f;

        //Vector3 p1 = p0 + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        //Vector3 p2 = p3 + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));

        //Vector3 p1 = p0 + Vector3.forward * 2f;
        //Vector3 p2 = p3 + Vector3.back * 2f;





        //P = P0 + t(P1 – P0)  ,  0 < t < 1

        //    B(t) = P0 + t(P1 – P0) = (1 - t) P0 + tP1 , 0 < t < 1


        // Bézier-görbe pontjainak meghatározása

        if (!stopCoroutine) {

        for (float t = 0; t <= 1; t += 0.1f)
        {
            
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

                //B(t) = (1-t)3P0 + 3(1-t)2tP1 + 3(1-t)t2P2 + t3P3 , 0 < t < 1

                try
                {
                    if (besierPontok.Count.Equals(4))
                    {

                        Vector3 position = uuu * besierPontok[0] + 3 * uu * t * besierPontok[1] + 3 * u * tt * besierPontok[2] + ttt * besierPontok[3];

                        // Instantiate objektumok a görbe mentén

                        wallmaker(position);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
           
         
            };

            p3 = besierPontok[3];

    
            besierPontok.Clear();
           // Debug.Log("clear    "+ besierPontok.Count);
            besierPontok.Add(p3);
        }
    }

}
