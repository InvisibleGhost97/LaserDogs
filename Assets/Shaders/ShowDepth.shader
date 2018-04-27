Shader "Unlit/ShowDepth"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		//visualize the depth for each pixel in the scene
		Tags { "RenderType"="Opaque" }	//all the opaque materials will have their shaders replace with this subshader
		
		ZWrite On

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float depth : DEPTH;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.depth = -mul(UNITY_MATRIX_MV, v.vertex).z * _ProjectionParams.w;	//vertex position are coming from the objects being rendered and are in the local space of the rendered mesh
				//one over the far plane value of the current camera
				return o;
			}
			
			half4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{
				//further pixels will be white and the farther pixels black
				float invert = 1 - i.depth;
			return fixed4(invert, invert, invert, 1) * _Color;
			}
				ENDCG
		}
	}

		SubShader
			{
				Tags
			{
				"RenderType" = "Transparent"
			}

				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha

				Pass
			{
				CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{
				return _Color;	//color property on any 'glass like material' will show up in the scene
			}
				ENDCG
			}
			}
}
