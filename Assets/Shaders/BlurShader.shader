Shader "Unlit/BlurShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // UnityObjectToClipPos(V.VERTEX); // UnityObjectToClipPos(v.vertex);
				o.uv = v.uv; // TRANSFORM_TEX(v.uv, _MainTex);
							 //UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			sampler2D _MainTex;
			//sampler2D _DisplaceTex;
			float4 _MainTex_TexelSize;	//getting the actual size of a pixel
			//float _Magnitude;

			//adding all the textures
			//sampling the texture 9 times for each pixel adding them all together, returning the resultand dividing by the amount of samples used
			float4 box(sampler2D tex, float2 uv, float4 size) {
				float4 c = tex2D(tex, uv + float2(-size.x, size.y)) + tex2D(tex, uv + float2(0, size.y)) + tex2D(tex, uv + float2(size.x, size.y)) +
					tex2D(tex, uv + float2(-size.x, 0)) + tex2D(tex, uv + float2(0, 0)) + tex2D(tex, uv + float2(size.x, 0)) +
					tex2D(tex, uv + float2(-size.x, -size.y)) + tex2D(tex, uv + float2(0, -size.y)) + tex2D(tex, uv + float2(size.x, -size.y));

				return c / 9; //samples the texture 9 times 

			}

			float4 frag(v2f i) : SV_Target
			{
			//	float2 disp = tex2D(_DisplaceTex, i.uv).xy;	//creating a float2 which is sampled from the displacement map from the regualr uv coordinates 
			//	disp = ((disp * 2) - 1) * _Magnitude;	//pushes the ranges from 0 to 1 to -1 to 1

				float4 col = box(_MainTex, i.uv, _MainTex_TexelSize);
				//col *= float4(i.uv.x, i.uv.y, 0, 1);	//multiplying the texture sample by the uv color
				return col;

			}
		ENDCG
		}
	}
}

