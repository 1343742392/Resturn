// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Ghost"
{
    Properties
    {
        _DistortStrength("DistortStrength", Range(0,1)) = 0.2
        _NoiseTex("NoiseTexture", 2D) = "white" {}
            _Amp("Amp", Float) = 0.3
                        _Speed("Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        GrabPass
        {
            "_BackgroundTexture"//变量名字
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
                        sampler2D _GrabTempTex;
            float4 _GrabTempTex_ST;
            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float _DistortStrength;
            float _Speed;
            float _Amp;
            #include "UnityCG.cginc"

            float random(float2 st, float n) {
                st = floor(st * n);
                return frac(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

            struct v2f
            {
                float4 grabPos : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
               
                o.uv = TRANSFORM_TEX(v.texcoord, _NoiseTex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                return o;
            }

            sampler2D _BackgroundTexture;

            half4 frag(v2f i) : SV_Target
            {
                float4 offset = tex2D(_NoiseTex, i.uv - _Time.xy * 0.05f) * _DistortStrength;
                ////////////////////////////////////////获取到的贴图          贴图坐标
                                half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos + offset);
                                return  bgcolor;
                            }
                            ENDCG
                        }
    }
}