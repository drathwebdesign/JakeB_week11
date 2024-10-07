using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NVBlender : MonoBehaviour {
    public Volume volume1;
    public Volume volume2;
    [Range(0f, 1f)] public float blendFactor = 0.5f;

    void Update() {
        volume1.weight = 1.0f - blendFactor;
        volume2.weight = blendFactor;
    }
}