Shader "Unlit/NormalOutline"
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _Color ("Color", Color) = (0,1,0,1)
    }
    
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"


            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            v2f vert(appdata_base v)
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = v.texcoord;
                return o;
            }
            
            


            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                

                //return SAMPLE_DEPTH_TEXTURE(_MainTex, i.uv);
                return tex2D(_MainTex, i.uv) * _Color;
            }
            ENDCG
        }
    }
}
