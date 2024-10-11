using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeTest : MonoBehaviour
{
    public Volume volume1;
    DepthOfField dof;
    ColorAdjustments ca;
    ChromaticAberration chr;

    public int mode;
    public float scrollIntensity = 5;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        volume1.profile.TryGet(out dof);
        volume1.profile.TryGet(out ca);
        volume1.profile.TryGet(out chr);
        TryGetComponent<Rigidbody>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            dof.focusDistance.value = Vector3.Distance(Camera.main.transform.position, hit.point);
        }

        if(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0)
        {
            if(mode == 1) Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollIntensity;
            if (mode == 2) ca.postExposure.value += Input.GetAxis("Mouse ScrollWheel") * scrollIntensity;
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            mode = 2;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            mode = 1;
        }



    }
}
