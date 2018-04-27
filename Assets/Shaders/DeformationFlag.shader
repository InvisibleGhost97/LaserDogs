Shader "Unlit/DeformationFlag"
{//working on object2world. when we go through the vertex shader we do not recieve the 3d position of the object in the world we actually recieve it in an object format

	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			//playing around with the geometeries
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); //take the coordinates we have in object space and match it to the screen pixels
															//from the unity coordinate to the actual screen space
															//this vertex is already modified, ready to be rendered on the player screen
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; //multiply current view matrix with the object to world matrix, this also has the original object matrix

				o.vertex.y += sin(worldPos.x + _Time.w); //the mesh is deformed depending on the position in the world

				//o.vertex.y += 1; //puches the mesh up 
				//o.vertex.y -= _SinTime.w; //would create a floating platform

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
