Shader "Unlit/OceanWaves"
{
    Properties
    {
        [MainTexture]_BaseMap("Base Map", 2D) = "white" {}
        [MainColor]_Color("Color", Color) = (1,1,1,1)
        _Smoothness("Smoothness", Range(0, 1)) = 0
        [Normal]_NormalMap("NormalMap", 2D) = "bump" {}
        _NormalStrength("NormalStrength", Range(0, 1)) = 1
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

            Pass
            {
                HLSLPROGRAM

                #pragma vertex vert
                #pragma fragment frag

                #pragma shader_feature USE_VERTEX_DISPLACEMENT
                #pragma shader_feature SINE_WAVE
                #pragma shader_feature NORMALS_IN_PIXEL_SHADER
                #pragma shader_feature CIRCULAR_WAVES
                #pragma shader_feature USE_FBM

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct Attributes
                {
                    float4 positionOS   : POSITION;
                    float2 uv: TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct Varyings
                {
                    float4 positionCS  : SV_POSITION;
                    float2 uv: TEXCOORD0;
                    float3 normal : TEXCOORD1;
                    float3 worldPos : TEXCOORD2;
                };

                struct DirectionalWave 
                {
                    float2 direction;
                    float frequency;
                    float amplitude;
                    float phase;
                };

                StructuredBuffer<DirectionalWave> _Waves;

                #define PI 3.14159265358979323846
                TEXTURE2D(_BaseMap);
                SAMPLER(sampler_BaseMap);

                CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _BaseMap_ST;
                CBUFFER_END
                
                //Sine Wave Equations
                float Sine(float3 v, DirectionalWave w)
                {
                    return w.amplitude * sin(w.frequency * (v.x * w.direction.x + v.z * w.direction.y) + _Time.y * w.phase);
                }

                float3 SineNormal(float3 v, DirectionalWave w)
                {
                    return w.frequency * w.amplitude * cos(w.frequency * (v.x * w.direction.x + v.z * w.direction.y) + _Time.y * w.phase);
                }

                float3 _SunDirection, _SunColor;

                Varyings vert(Attributes IN)
                {
                    Varyings OUT;

                    OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                    OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);

                    return OUT;
                }

                half4 frag(Varyings IN) : SV_Target
                {
                    float4 texel = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);
                    return texel * _Color;
                }
                ENDHLSL
            }
        }
}
