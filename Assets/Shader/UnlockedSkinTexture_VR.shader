Shader "Custom/UnlockedSkinTexture_VR"
{
      Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("MainTex", 2D) = "white" {}
        _Radius("Radius", Range(0.0, 5)) = 0.5
        _Hardness("Hardness", Range(0.01, 0.99999)) = 1.0
        _CenterPoint("Center", Vector) = (0, 0, 0, 0)
        _value1("value1", Float) = 2.0
        _value2("value2", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float3 worldPos    : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float3 viewDirWS   : TEXCOORD3;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

            float4 _Color;
            float3 _CenterPoint;
            float _Radius;
            float _Hardness;
            float _value1;
            float _value2;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                VertexPositionInputs vpi = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionHCS = vpi.positionCS;
                OUT.worldPos = vpi.positionWS;
                OUT.worldNormal = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = GetWorldSpaceNormalizeViewDir(vpi.positionWS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _Color;

                float distFactor = saturate((length(_CenterPoint - IN.worldPos) - _Radius) / (1.0 - _Hardness));
                float fresnelFactor = saturate(1 - pow(saturate(dot(normalize(IN.viewDirWS), normalize(IN.worldNormal))), _value2) * _value1);
                float alpha = saturate(distFactor + fresnelFactor);

                return half4(col.rgb, alpha);
            }
            ENDHLSL
        }
    }
    FallBack "Universal Render Pipeline/Lit"
}
