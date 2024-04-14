using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rx = Random.Range(-13, 13);
        float rz = Random.Range(-13, 13);
        float ry = Random.Range(-13, 13);

        this.gameObject.transform.localPosition = new Vector3(rx, ry, rz);
    }

  
}
