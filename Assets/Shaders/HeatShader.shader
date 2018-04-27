// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/HeatShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0,0.1)) = 1	//will add a range slider
	}
	SubShader
	{
		//Tags { "RenderType"="Opaque" }
		//LOD 100

		//no culling or depth
		Cull Off ZWrite off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				//UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			
			//float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // UnityObjectToClipPos(V.VERTEX); // UnityObjectToClipPos(v.vertex);
				o.uv = v.uv; // TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _DisplaceTex;
			float _Magnitude;

			float4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				
				
				
				//float4 col = tex2D(_MainTex, i.uv);
				//col *= float4(i.uv.x, i.uv.y, 0, 1);	//multiplying the texture sample by the uv color
				//return col;

				float2 disp = tex2D(_DisplaceTex, i.uv).xy;	//creating a float2 which is sampled from the displacement map from the regualr uv coordinates 
				disp = ((disp * 2) - 1) * _Magnitude;	//pushes the ranges from 0 to 1 to -1 to 1

				float4 col = tex2D(_MainTex, i.uv + disp);
				//col *= float4(i.uv.x, i.uv.y, 0, 1);	//multiplying the texture sample by the uv color
				return col;

			}
			ENDCG
		}
	}
}
