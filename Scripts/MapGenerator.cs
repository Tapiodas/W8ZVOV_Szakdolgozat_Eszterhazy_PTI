using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace thesis
{
    public class MapGenerator : MonoBehaviour
    {

        public GameObject mapAlap;
        public GameObject Group;
        
        public int xsize;
        public int ysize;
        public int zsize;
        public List<Vector3> linePoints;
        public int groupsize;


        public float lineWidth;

        private void Awake()
        {
           
        }

      
        private void OnGUI()
        {
            if (GUILayout.Button("Generate map"))
            {

                foreach (Transform child in Group.transform)
                {
                  
                    GameObject.DestroyImmediate(child.gameObject);
                }

                for (int x = 0; x < xsize; x++)
                {
                    for (int y = 0; y < ysize; y++)
                    {
                        for (int z = 0; z < zsize; z++)
                        {
                            linePoints.Add(new Vector3(x, y, z));
                            Debug.Log(x + y + z);
                            GameObject newgameObject;
                            newgameObject = Instantiate(mapAlap, new Vector3(x, y, z), Quaternion.identity);
                            newgameObject.transform.parent = Group.transform;
                            groupsize++;
                        }

                    }
                }
            }
        }


        void Update()
                {
            //newLine = new GameObject();

           

            //if (groupsize<= xsize){
            //    for (int x = 0; x < xsize; x++)
            //    {
            //        for (int y = 0; y < ysize; y++)
            //        {
            //            for (int z = 0; z < zsize; z++)
            //            {
            //                linePoints.Add(new Vector3(x, y, z));
            //                Debug.Log(x + y + z);
            //                GameObject newgameObject = new GameObject();
            //                newgameObject = Instantiate(mapAlap, new Vector3(x, y, z), Quaternion.identity);
            //                newgameObject.transform.parent = Group.transform;
            //                groupsize++;
            //            }

            //        }
            //    }
            //}



            //drawLine = newLine.GetComponent<LineRenderer>();
            //drawLine.material = new Material(Shader.Find("Sprites/Default"));

            //    this.GetComponent<LineRenderer>().startWidth = lineWidth;
            //    this.GetComponent<LineRenderer>().endWidth = lineWidth;

            //    this.GetComponent<LineRenderer>().positionCount = linePoints.Count;


            //    this.GetComponent<LineRenderer>().SetPositions(linePoints.ToArray());


            //linePoints.Clear();











        }

      
        

    }
}