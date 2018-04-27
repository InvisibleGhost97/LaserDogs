Shader "Custom/Aurora" {
	Properties{
		_MIX("MIX", 2D) = "white" {}	//who far the vertices are displayed on the mesh
		_VO_Strength("VO_Strength", Float) = 0.1	//makes the vertices of the mesh go in opposite direction one one way and the other the other way
		
		_VO_SPD_U("VO_SPD_U", Float) = 0.1
		_VO_SPD_V("VO_SPD_V", Float) = 0
	
		_Indentations_SPD_U("Indentations_SPD_U", Float) = 0.1
		_Indentations_SPD_V("Indentations_SPD_V", Float) = 0
		_Indentation_Add("Indentation_Add", Float) = 0.5


		_Lines_SPD_U("Lines_SPD_U", Float) = 0.1
		_Lines_SPD_V("Lines_SPD_V", Float) = 0
		_Lines_Strength("Lines_Strength", Float) = 2
		_Lines_Tile_U("Lines_Tile_U", Float) = 4
		_Lines_Tile_V("Lines_Tile_V", Float) = 1
	
		_indentations_Tile_U("indentations_Tile_U", Float) = 3
		_Indentations_Tile_V("Indentations_Tile_V", Float) = 1

		_VO_Tile_U("VO_Tile_U", Float) = 0
		_VO_Tile_V("VO_Tile_V", Float) = 0

		_ColorSlider("ColorSlider", Range(0, 2)) = 1
		_Color_1("Color_1", Color) = (0.0147059,0.7146047,1,1)
		_Color_2("Color_2", Color) = (1,0,0.8068962,1)
			
		_Emissive_Strength("Emissive_Strength", Float) = 1.5

	
	}



		SubShader{
			Tags {
				"IgnoreProjector" = "True"
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			LOD 200
			Pass{
				Name "FORWARD"
				Tags{
					"Lightmode"="ForwardBase"
				}

				Blend One One
				Cull Off
				ZWrite Off
				
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#define UNITY_PASS_FORWARDBASE
				#include "UnityCG.cginc"
				#pragma multi_compile_fwdbase
				#pragma multi_compile_fog
				#pragma only_renderers d3d9 d3d11 glcore gles 
				#pragma target 3.0

				uniform sampler2D _MIX; uniform float4 _MIX_ST;
			
				uniform float _VO_Strength;
				uniform float _VO_SPD_U;
				uniform float _VO_SPD_V;
				uniform float _Indentations_SPD_U;
				uniform float _Indentations_SPD_V;
				uniform float _Indentation_Add;
				uniform float _Lines_SPD_U;
				uniform float _Lines_SPD_V;
				uniform float _Lines_Strength;
				uniform float _Lines_Tile_U;
				uniform float _Lines_Tile_V;
				uniform float _indentations_Tile_U;
				uniform float _Indentations_Tile_V;
				uniform float _VO_Tile_U;
				uniform float _VO_Tile_V;
				uniform float _ColorSlider;
				uniform float4 _Color_1;
				uniform float4 _Color_2;
				uniform float _Emissive_Strength;

				struct VertexInput {
					float4 vertex : POSITION;
					float2 texcoord0 : TEXCOORD0;
				};

				struct VertexOutput {
					float4 pos : SV_POSITION;
					float2 uv0 : TEXCOORD0;
					UNITY_FOG_COORDS(1)
				};

				VertexOutput vert(VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.uv0 = v.texcoord0;
					float4 node_6735 = _Time;
					float2 node_1150 = (((float2(_VO_SPD_U, _VO_SPD_V)*node_6735.g) + o.uv0)*float2(_VO_Tile_U, _VO_Tile_V));
					float4 node_3323 = tex2Dlod(_MIX, float4(TRANSFORM_TEX(node_1150, _MIX), 0.0, 0));
					v.vertex.xyz += float3(float2(0.0, (node_3323.r*_VO_Strength)), 0.0);
					o.pos = UnityObjectToClipPos(v.vertex);
					UNITY_TRANSFER_FOG(o, o.pos);
					return o;
				}

				float4 frag(VertexOutput i, float facing : VFACE) : COLOR{
					float isFrontFace = (facing >= 0 ? 1 : 0);
					float faceSign = (facing >= 0 ? 1 : -1);
					////// Lighting:
					////// Emissive:
					float4 node_6735 = _Time;
					float2 node_7944 = (float2(_Lines_Tile_U,_Lines_Tile_V)*((float2(_Lines_SPD_U,_Lines_SPD_V)*node_6735.g) + i.uv0));
					float4 node_6627 = tex2D(_MIX,TRANSFORM_TEX(node_7944, _MIX));
					float2 node_3595 = (float2(_indentations_Tile_U,_Indentations_Tile_V)*((float2(_Indentations_SPD_U,_Indentations_SPD_V)*node_6735.g) + i.uv0));
					float4 node_9292 = tex2D(_MIX,TRANSFORM_TEX(node_3595, _MIX));
					float4 node_4141 = tex2D(_MIX,TRANSFORM_TEX(i.uv0, _MIX));
					float node_8710 = (((_Lines_Strength*node_6627.a) + (_Indentation_Add + node_9292.b))*node_4141.g);
					float3 emissive = ((lerp(_Color_1.rgb,_Color_2.rgb,(_ColorSlider*node_8710))*node_8710)*_Emissive_Strength);
					float3 finalColor = emissive;
					fixed4 finalRGBA = fixed4(finalColor,1);
					UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
					return finalRGBA;
				}
					ENDCG
		
			}
			Pass{
					Name "ShadowCaster"
					Tags{
					"LightMode" = "ShadowCaster"
				}
					Offset 1, 1
					Cull Off

					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag

					#define UNITY_PASS_SHADOWCASTER

					#include "UnityCG.cginc"
					#include "Lighting.cginc"

					#pragma fragmentoption ARB_precision_hint_fastest
					#pragma multi_compile_shadowcaster
					#pragma multi_compile_fog
					#pragma only_renderers d3d9 d3d11 glcore gles 
					#pragma target 3.0

					uniform sampler2D _MIX; uniform float4 _MIX_ST;
					uniform float _VO_Strength;
					uniform float _VO_SPD_U;
					uniform float _VO_SPD_V;
					uniform float _VO_Tile_U;
					uniform float _VO_Tile_V;

				struct VertexInput {
					float4 vertex : POSITION;
					float2 texcoord0 : TEXCOORD0;
				};
				struct VertexOutput {
					V2F_SHADOW_CASTER;
					float2 uv0 : TEXCOORD1;
				};
				VertexOutput vert(VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.uv0 = v.texcoord0;
					float4 node_6735 = _Time;
					float2 node_1150 = (((float2(_VO_SPD_U,_VO_SPD_V)*node_6735.g) + o.uv0)*float2(_VO_Tile_U,_VO_Tile_V));
					float4 node_3323 = tex2Dlod(_MIX,float4(TRANSFORM_TEX(node_1150, _MIX),0.0,0));
					v.vertex.xyz += float3(float2(0.0,(node_3323.r*_VO_Strength)),0.0);
					o.pos = UnityObjectToClipPos(v.vertex);
					TRANSFER_SHADOW_CASTER(o)
						return o;
				}
				float4 frag(VertexOutput i, float facing : VFACE) : COLOR{
					float isFrontFace = (facing >= 0 ? 1 : 0);
				float faceSign = (facing >= 0 ? 1 : -1);
				SHADOW_CASTER_FRAGMENT(i)
				}
					ENDCG
				}
			}
			FallBack "Diffuse"
			CustomEditor "ShaderForgeMaterialInspector"

}