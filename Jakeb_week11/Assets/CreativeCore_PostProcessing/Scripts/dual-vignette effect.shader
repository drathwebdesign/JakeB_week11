Shader "Hidden/Custom/DualVignette"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VignetteColor1 ("Vignette Color 1", Color) = (0, 0, 0, 1)
        _VignetteColor2 ("Vignette Color 2", Color) = (0, 0, 0, 1)
        _VignettePosition1 ("Vignette Position 1", Vector) = (0.45, 0.5, 0, 0)
        _VignettePosition2 ("Vignette Position 2", Vector) = (0.65, 0.5, 0, 0)
        _VignetteRadius1 ("Vignette Radius 1", Float) = 0.3
        _VignetteRadius2 ("Vignette Radius 2", Float) = 0.3
        _VignetteSoftness ("Vignette Softness", Float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _VignetteColor1;
            float4 _VignetteColor2;
            float2 _VignettePosition1;
            float2 _VignettePosition2;
            float _VignetteRadius1;
            float _VignetteRadius2;
            float _VignetteSoftness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv);

                // Calculate distance from the center for both vignettes
                float dist1 = distance(uv, _VignettePosition1);
                float dist2 = distance(uv, _VignettePosition2);

                // Apply the first vignette, ensuring correct transition with smoothstep
                float vignette1 = smoothstep(_VignetteRadius1, _VignetteRadius1 - _VignetteSoftness, dist1);
                col.rgb = lerp(col.rgb, _VignetteColor1.rgb, vignette1);

                // Apply the second vignette, blending it with the first
                float vignette2 = smoothstep(_VignetteRadius2, _VignetteRadius2 - _VignetteSoftness, dist2);
                col.rgb = lerp(col.rgb, _VignetteColor2.rgb, vignette2);

                // Return the final color with the blended vignettes
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}