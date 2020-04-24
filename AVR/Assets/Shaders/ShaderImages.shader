Shader "Michal/VIA/ShaderImages"
{
    Properties
    {
        _Color ("ColorBack", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("Background", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_CenterX("CenterX", Range(0,0.99)) = 0.0
		_CenterY("CenterY", Range(0,0.99)) = 0.0
		_PositionX("PositionX", Range(0,1)) = 0.5
		_PositionY("PositionY", Range(0,1)) = 0.5
		_Color2("ColorFront", Color) = (1,1,1,1)
		_Color3("ColorSleeves", Color) = (1,1,1,1)
		_ModelType("ModelType", Int) = 0
		_AnimTex("AnimTex", 2D) = "white" {}
		_IsAnimOn("IsAnimOn", Int) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _AnimTex;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv2_AnimTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _CenterX;
		float _CenterY;
		float _PositionX;
		float _PositionY;
		fixed4 _Color2;
		fixed4 _Color3;
		int _ModelType;
		int _IsAnimOn;

		float square(float2 uv, float2 center, float size, float elements)
		{
			float r1 = distance(uv.x, center.x);
			float r2 = distance(uv.y, center.y);
			float r = max(r1, r2);

			r = step(r, size / elements / 2.);
			return r;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 uv = IN.uv_MainTex / 6.;

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex - float2(floor(_CenterX * 3.) / 3., floor(_CenterY * 3.) / 3.));
			float t = square(uv, float2(_PositionX, _PositionY), 1/6., 3.);
			c *= t;

			fixed4 bg = tex2D(_MainTex2, IN.uv_MainTex2);

			float maskLogo = (c.r + c.g + c.b) / 3. > 0.99 ? 1. : 0.;
			maskLogo += 1. - t;

			float2 uv_bg = IN.uv_MainTex2;
			uv_bg -= float2(0.75, 1.25);

			fixed3 col = _Color * tex2D(_MainTex2, uv_bg);

			if (_ModelType == 1)
			{
				if (uv_bg.x > 0.1)
				{
					col = lerp(c, bg * _Color2.rgb, maskLogo);
				}
			}
			else
			{
				if (uv_bg.x < 0)
				{
					col = lerp(c, bg * _Color2.rgb, maskLogo);
				}
			}

			if (uv_bg.y < -0.95)
			{
				col = _Color3;
			}

			fixed4 AnimColor = tex2D(_AnimTex, IN.uv2_AnimTex);
			fixed4 d = AnimColor;

			if (_IsAnimOn == 1)
			{
				col = lerp(col * AnimColor.rgb + (1 - AnimColor.rgb), col, _SinTime.z * 4.0);
			}

			o.Albedo = col;

			o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
