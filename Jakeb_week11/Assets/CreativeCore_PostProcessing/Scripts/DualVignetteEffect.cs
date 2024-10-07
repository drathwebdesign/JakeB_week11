using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Custom/Dual Vignette Effect")]
public class DualVignetteEffect : VolumeComponent, IPostProcessComponent {
    public ColorParameter vignetteColor1 = new ColorParameter(Color.black);
    public ColorParameter vignetteColor2 = new ColorParameter(Color.black);
    public Vector2Parameter vignettePosition1 = new Vector2Parameter(new Vector2(0.45f, 0.5f));
    public Vector2Parameter vignettePosition2 = new Vector2Parameter(new Vector2(0.65f, 0.5f));
    public ClampedFloatParameter vignetteRadius1 = new ClampedFloatParameter(0.3f, 0f, 1f);
    public ClampedFloatParameter vignetteRadius2 = new ClampedFloatParameter(0.3f, 0f, 1f);
    public ClampedFloatParameter vignetteSoftness = new ClampedFloatParameter(0.2f, 0f, 1f);

    // This method determines if the effect should be rendered
    public bool IsActive() => vignetteRadius1.value > 0f || vignetteRadius2.value > 0f;

    // This method determines if the effect is compatible with tiling
    public bool IsTileCompatible() => false;
}