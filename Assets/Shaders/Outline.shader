//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
///*
//Shader "Outlined/Silhouetted Diffuse" {
//	Properties{
//		_Color("Main Color", Color) = (.5,.5,.5,1)
//		_OutlineColor("Outline Color", Color) = (0,0,0,1)
//		_Outline("Outline width", Range(0.0, 5.00)) = 1.05
//		_MainTex("Base (RGB)", 2D) = "white" { }
//	}
//
//		CGINCLUDE
//#include "UnityCG.cginc"
//
//	struct appdata {
//		float4 vertex : POSITION;
//		float3 normal : NORMAL;
//	};
//
//	struct v2f {
//		float4 pos : POSITION;
//		float4 color : COLOR;
//	};
//
//	uniform float _Outline;
//	uniform float4 _OutlineColor;
//
//	v2f vert(appdata v) {
//		// just make a copy of incoming vertex data but scaled according to normal direction
//		v2f o;
//		o.pos = UnityObjectToClipPos(v.vertex);
//
//		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
//		float2 offset = TransformViewToProjection(norm.xy);
//
//		o.pos.xy += offset * o.pos.z * _Outline;
//		o.color = _OutlineColor;
//		return o;
//	}
//	ENDCG
//
//		SubShader{
//		Tags{ "Queue" = "Transparent" }
//
//		// note that a vertex shader is specified here but its using the one above
//		Pass{
//		Name "OUTLINE"
//		Tags{ "LightMode" = "Always" }
//		Cull Off
//		ZWrite Off
//		ZTest Always
//		ColorMask RGB // alpha not used
//
//					  // you can choose what kind of blending mode you want for the outline
//		Blend SrcAlpha OneMinusSrcAlpha // Normal
//										//Blend One One // Additive
//										//Blend One OneMinusDstColor // Soft Additive
//										//Blend DstColor Zero // Multiplicative
//										//Blend DstColor SrcColor // 2x Multiplicative
//
//		CGPROGRAM
//#pragma vertex vert
//#pragma fragment frag
//
//		half4 frag(v2f i) :COLOR{
//		return i.color;
//	}
//		ENDCG
//	}
//
//		Pass{
//		Name "BASE"
//		ZWrite On
//		ZTest LEqual
//		Blend SrcAlpha OneMinusSrcAlpha
//		Material{
//		Diffuse[_Color]
//		Ambient[_Color]
//	}
//		Lighting On
//		SetTexture[_MainTex]{
//		ConstantColor[_Color]
//		Combine texture * constant
//	}
//		SetTexture[_MainTex]{
//		Combine previous * primary DOUBLE
//	}
//	}
//	}
//
//		SubShader{
//		Tags{ "Queue" = "Transparent" }
//
//		Pass{
//		Name "OUTLINE"
//		Tags{ "LightMode" = "Always" }
//		Cull Front
//		ZWrite Off
//		ZTest Always
//		ColorMask RGB
//
//		// you can choose what kind of blending mode you want for the outline
//		Blend SrcAlpha OneMinusSrcAlpha // Normal
//										//Blend One One // Additive
//										//Blend One OneMinusDstColor // Soft Additive
//										//Blend DstColor Zero // Multiplicative
//										//Blend DstColor SrcColor // 2x Multiplicative
//
//		CGPROGRAM
//#pragma vertex vert
//#pragma exclude_renderers gles xbox360 ps3
//		ENDCG
//		SetTexture[_MainTex]{ combine primary }
//	}
//
//		Pass{
//		Name "BASE"
//		ZWrite On
//		ZTest LEqual
//		Blend SrcAlpha OneMinusSrcAlpha
//		Material{
//		Diffuse[_Color]
//		Ambient[_Color]
//	}
//		Lighting On
//		SetTexture[_MainTex]{
//		ConstantColor[_Color]
//		Combine texture * constant
//	}
//		SetTexture[_MainTex]{
//		Combine previous * primary DOUBLE
//	}
//	}
//	}
//
//		Fallback "Diffuse"
//}
//
//
///*Shader "Shaders/Outline"
//{
//	Properties
//	{
//		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
//		_MainTex("Texture", 2D) = "White" {}
//		_OutlineColor("Outline Color", Color) = (0.0,0.0,0.0,1)
//		_OutlineWidth("Outline Width", Range(1.0,5.0)) = 1.01
//	}
//
//		CGINCLUDE
//#include "UnityCG.cginc"
//
//	struct appdata
//	{
//		float4 vertex : POSITION;
//		float3 normal : NORMAL;
//	};
//
//
//	struct v2f
//	{
//		float4 pos : POSITON;
//		float3 normal : NORMAL;
//	};
//
//
//	float _OutlineWidth;
//	float4 _OutlineColor;
//
//	v2f vert(appdata v)
//	{
//		v.vertex.xyz *= _OutlineWidth;
//
//		v2f o;
//		o.pos = UnityObjectToClipPos(v.vertex);
//		return o;
//	}
//
//
//	ENDCG
//
//
//		SubShader
//	{
//		pass // render the outline
//	{
//		ZWrite off
//
//			CGPROGRAM
//
//		#pragma vertex vert
//		#pragma fragment frag
//
//			half4 frag(v2f i) : COLOR
//		{
//			return _OutlineColor;
//		}
//
//			ENDCG
//	}
//
//	Pass // normal render
//	{
//		ZWrite On
//
//			Material
//		{
//			Diffuse[_Color]
//			Ambient[_Color]
//		}
//
//			Lighting On
//
//			SetTexture[_MainTex]
//		{
//			ConstantColor[_Color]
//		}
//
//			SetTexture[_MainTex]
//		{
//			Combine previous * primary DOUBLE
//		}
//
//	}
//	}
//}﻿
//
//
///*Shader "N3K/Outline"
// {
//  Properties{
//   _Color("Main Color", Color) = (0.5,0.5,0.5,1)
//   _MainTex ("Texture", 2D) = "White" {}
//   _OutlineColor("Outline color", color) = (0,0,0,1)
//   _OutlineWidth("Outline width", Range(1.0,5.0)) = 1.01
// }
//
//
// SubShader{
//  
//  Pass{ //render the outline
//   ZWrite off
//
//   CGPROGRAM
//   #pragma vertex vert
//   #pragma fragment frag
//
//
//   //starthere
//     struct appdata{
//   float4 vertex : POSITION;
//   float3 normal : NORMAL;
//  };
//
//  struct v2f{
//   float4 pos : POSITION;
//   float3 normal : NORMAL;
//  };
//
//  float _OutlineWidth;
//  float4 _OutlineColor;
//
//  v2f vert(appdata v){
//   v.vertex.xyz *= _OutlineWidth;
//
//   v2f o;
//   o.pos = UnityObjectToClipPos(v.vertex);
//   return o;
//  }
//   //endhere
//
//   half4 frag(v2f i) : COLOR
//   {
//    return _OutlineColor;
//   }
//   ENDCG
//  }
//
//
//  Pass
//  {
//   CGPROGRAM
//   #pragma vertex vert
//   #pragma fragment frag
//   // make fog work
//   #pragma multi_compile_fog
//   
//   #include "UnityCG.cginc"
//
//   struct appdata
//   {
//    float4 vertex : POSITION;
//    float2 uv : TEXCOORD0;
//   };
//
//   struct v2f
//   {
//    float2 uv : TEXCOORD0;
//    UNITY_FOG_COORDS(1)
//    float4 vertex : SV_POSITION;
//   };
//
//   sampler2D _MainTex;
//   float4 _MainTex_ST;
//   
//   v2f vert (appdata v)
//   {
//    v2f o;
//    o.vertex = UnityObjectToClipPos(v.vertex);
//    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//    UNITY_TRANSFER_FOG(o,o.vertex);
//    return o;
//   }
//   
//   fixed4 frag (v2f i) : SV_Target
//   {
//    // sample the texture
//    fixed4 col = tex2D(_MainTex, i.uv);
//    // apply fog
//    UNITY_APPLY_FOG(i.fogCoord, col);
//    return col;
//   }
//   ENDCG
//  }
// }
// 
//}﻿*/