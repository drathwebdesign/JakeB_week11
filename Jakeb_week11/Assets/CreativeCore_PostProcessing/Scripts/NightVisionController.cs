using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVisionToggle : MonoBehaviour {
    public Camera mainCamera; 
    public Camera overlayCamera; 
    public GameObject nightVisionVolume; 
    public GameObject vignetteOverlayVolume; 

    private bool nightVisionActive = false;

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.N)) {
            
            nightVisionActive = !nightVisionActive;

            
            nightVisionVolume.SetActive(nightVisionActive);
            vignetteOverlayVolume.SetActive(nightVisionActive);

            
            overlayCamera.enabled = nightVisionActive;
        }
    }
}