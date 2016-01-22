Shader "Space/Star" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "" {}
		_Mask ("Mask", 2D) = "" {}
		_ColorStrength ("Color Strength", Float) = 1
		_Direction ("Waves Direction 1 & 2", Vector) = (1.0 ,1.0, -1.0, -1.0)
		_TextureOffsetMultiplier("Texture Offset Multiplier", Float) = 1
	}
	SubShader {
		Tags {"RenderType"="Opaque"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Mask;
		float _TextureOffsetMultiplier;
		

		struct Input {
			float2 uv_MainTex;
			float2 uv_Mask;
		};

		half _ColorStrength;
		fixed4 _Color;
		float4 _Direction;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c1 = tex2D (_MainTex, IN.uv_MainTex + _Time.xx * _Direction.xy)/_TextureOffsetMultiplier;
		    fixed4 c2 = tex2D (_MainTex, IN.uv_MainTex + c1.rg + _Time.xx * _Direction.xy*-1)/_TextureOffsetMultiplier;		
		    fixed4 mask = tex2D (_Mask, IN.uv_Mask + c2.rg + _Time.xx * _Direction.zw);
			fixed4 x = c1+mask;
			o.Emission = (x.r+x.g+x.b)*_Color;
			o.Emission = o.Emission*o.Emission*_ColorStrength;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
