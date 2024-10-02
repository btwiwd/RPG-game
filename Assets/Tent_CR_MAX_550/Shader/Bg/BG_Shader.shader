// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Custom/BG_Shader" {

Properties {
	_Control ("SplatMap (RGBA)", 2D) = "red" {}
	_Splat0 ("Layer 0 (R)", 2D) = "white" {}
	_Splat1 ("Layer 1 (G)", 2D) = "white" {}
	_Splat2 ("Layer 2 (B)", 2D) = "white" {}
	_Splat3 ("Layer 3 (A)", 2D) = "white" {}
	_BaseMap ("BaseMap (RGB)", 2D) = "white" {}
}

// Fragment program
SubShader {
	Tags { "RenderType" = "Opaque" }
	Pass { 
		Tags { "LightMode" = "ForwardBase" }
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma multi_compile_fwdbase

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"

		struct appdata_lightmap {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
			float2 texcoord1 : TEXCOORD1; 
		}; 
		 
		struct v2f_vertex {
			float4 pos : SV_POSITION;
			float4 uv[3] : TEXCOORD0;
			LIGHTING_COORDS(3,4)
		};
		
		uniform sampler2D _Control;
		uniform float4 _Control_ST;
		 
		// uniform float4 unity_LightmapST;
		// uniform sampler2D unity_Lightmap;
		
		uniform sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
		uniform float4 _Splat0_ST,_Splat1_ST,_Splat2_ST,_Splat3_ST;

		v2f_vertex vert (appdata_lightmap v) 
		{
			v2f_vertex o;
			o.pos = UnityObjectToClipPos (v.vertex);
			o.uv[0].xy = TRANSFORM_TEX (v.texcoord.xy, _Control);
			o.uv[0].zw = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
			o.uv[1].xy = TRANSFORM_TEX (v.texcoord.xy, _Splat0);
			o.uv[1].zw = TRANSFORM_TEX (v.texcoord.xy, _Splat1);
			o.uv[2].xy = TRANSFORM_TEX (v.texcoord.xy, _Splat2);
			o.uv[2].zw = TRANSFORM_TEX (v.texcoord.xy, _Splat3);
			TRANSFER_VERTEX_TO_FRAGMENT(o);
			return o;
		}
		
		half4 frag (v2f_vertex i) : COLOR
		{ 
			float attenuation = LIGHT_ATTENUATION(i);
			half4 splat_control = tex2D(_Control, i.uv[0].xy);
			half3 splat_color = splat_control.r * tex2D (_Splat0, i.uv[1].xy).rgb; 
			splat_color += splat_control.g * tex2D (_Splat1, i.uv[1].zw).rgb;
			splat_color += splat_control.b * tex2D (_Splat2, i.uv[2].xy).rgb;
			splat_color += splat_control.a * tex2D (_Splat3, i.uv[2].zw).rgb;
			splat_color *= DecodeLightmap (UNITY_SAMPLE_TEX2D (unity_Lightmap, i.uv[0].zw));
			splat_color *= attenuation;
            return half4(splat_color, 0.0);
		}
		ENDCG
 	} 
}
FallBack "VertexLit"
}