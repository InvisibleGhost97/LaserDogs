Shader "Unlit/OverDraw"
{
	SubShader
	{
		Tags
	{
		"Queue" = "Transparent" //the replacement shader will render over top of the skybox
	}

		ZTest Always	//regardless of what is in the depth buffer this will always be drawn
		ZWrite Off
		Blend One One	//an additive blend

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

			half4 _OverDrawColor;	//define a color in the scope of the cg program
				
			fixed4 frag(v2f i) : SV_Target
			{
				return _OverDrawColor;	//return it from the frag function
			}	
			ENDCG
		}
	}
}
