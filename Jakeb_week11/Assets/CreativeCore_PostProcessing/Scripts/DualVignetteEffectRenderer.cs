using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DualVignetteEffectRenderer : ScriptableRendererFeature {
    class CustomRenderPass : ScriptableRenderPass {
        private Material vignetteMaterial;
        private RTHandle tempTexture;
        private DualVignetteEffect dualVignette;

        public void Setup(DualVignetteEffect dualVignette, Material vignetteMaterial) {
            this.dualVignette = dualVignette;
            this.vignetteMaterial = vignetteMaterial;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor) {
            RenderingUtils.ReAllocateIfNeeded(ref tempTexture, cameraTextureDescriptor, name: "_TempTex");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            if (vignetteMaterial == null || dualVignette == null)
                return;

            var cmd = CommandBufferPool.Get("Dual Vignette Effect");

            // Set parameters in the material
            vignetteMaterial.SetColor("_VignetteColor1", dualVignette.vignetteColor1.value);
            vignetteMaterial.SetColor("_VignetteColor2", dualVignette.vignetteColor2.value);
            vignetteMaterial.SetVector("_VignettePosition1", dualVignette.vignettePosition1.value);
            vignetteMaterial.SetVector("_VignettePosition2", dualVignette.vignettePosition2.value);
            vignetteMaterial.SetFloat("_VignetteRadius1", dualVignette.vignetteRadius1.value);
            vignetteMaterial.SetFloat("_VignetteRadius2", dualVignette.vignetteRadius2.value);
            vignetteMaterial.SetFloat("_VignetteSoftness", dualVignette.vignetteSoftness.value);

            // Blit the source to the material
            Blit(cmd, renderingData.cameraData.renderer.cameraColorTargetHandle, tempTexture, vignetteMaterial, 0);
            Blit(cmd, tempTexture, renderingData.cameraData.renderer.cameraColorTargetHandle);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd) {
            if (tempTexture != null) {
                RTHandles.Release(tempTexture);
            }
        }
    }

    CustomRenderPass customPass;
    public Material vignetteMaterial;

    public override void Create() {
        customPass = new CustomRenderPass();
        customPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
        var stack = VolumeManager.instance.stack;
        var dualVignette = stack.GetComponent<DualVignetteEffect>();

        if (dualVignette != null && dualVignette.IsActive()) {
            customPass.Setup(dualVignette, vignetteMaterial);
            renderer.EnqueuePass(customPass);
        }
    }
}