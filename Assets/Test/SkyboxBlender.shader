Shader"Custom/SkyboxBlender" {
    Properties {
        _SkyboxTex1 ("Skybox 1", CUBE) = "" {}
        _SkyboxTex2 ("Skybox 2", CUBE) = "" {}
        _BlendFactor ("Blend Factor", Range(0, 1)) = 0.5
    }
 
    SubShader {
        Tags { "Queue"="Background" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"
 
struct appdata_t
{
    float4 vertex : POSITION;
};
 
struct v2f
{
    float4 vertex : SV_POSITION;
    float3 texcoord : TEXCOORD0;
};
 
samplerCUBE _SkyboxTex1;
samplerCUBE _SkyboxTex2;
float _BlendFactor;
 
v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.texcoord = v.vertex.xyz;
    return o;
}
 
fixed4 frag(v2f i) : SV_Target
{
    fixed4 col1 = texCUBE(_SkyboxTex1, i.texcoord);
    fixed4 col2 = texCUBE(_SkyboxTex2, i.texcoord);
    return lerp(col1, col2, _BlendFactor);
}
            ENDCG
        }
    }
}
