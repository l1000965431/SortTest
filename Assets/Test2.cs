using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (g != null)
        {
            g.transform.RotateAround(g.transform.position, Vector3.forward, 300 * Time.deltaTime);
        }
    }
}
