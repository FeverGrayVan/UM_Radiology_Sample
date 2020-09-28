// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CGShader5"
{
    SubShader
    {
        Pass
        {
            /*Cull Off*/

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            uniform float4x4 _CullerSpace;

            struct vout
            {
                float4 pos : SV_POSITION;
                float4 worldpos : TEXCOORD0;
                float4 localpos : TEXCOORD1;
                float4 cullerpos : TEXCOORD2;
            };

            vout vert(float4 vertex : POSITION)
            {
                vout _out;
                _out.pos = UnityObjectToClipPos(vertex);
                _out.localpos = vertex;
                _out.worldpos = mul(unity_ObjectToWorld, vertex);
                _out.cullerpos = mul(_CullerSpace, _out.worldpos);
                return _out;
            }

            float4 frag(vout _in) : COLOR
            {
                /*return float4(_in.cullerpos.x, _in.cullerpos.y, 0, 1);*/

                if (_in.cullerpos.x > -0.5 && _in.cullerpos.x < 0.5 &&
                    _in.cullerpos.y > -0.5 && _in.cullerpos.y < 0.5 &&
                    _in.cullerpos.z > -0.5 && _in.cullerpos.z < 0.5)
                    discard;

                return float4(0, 1, 0, 1);
            }

            ENDCG
        }
    }
}