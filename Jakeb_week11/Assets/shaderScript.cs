using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<MeshRenderer>().material.SetFloat("_WaveIntensity", 0.5f);

        }
    }
}
