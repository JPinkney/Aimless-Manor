Shader "Custom/s_ripple"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_MainTex("Color (RGB) Alpha (A)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Transparency("Transparency", Range(0.0,0.5)) = 0.25
		_Scale("Scale", float) = 1
		_Speed("Speed", float) = 1
		_Frequency("Frequency", float) = 1
		_Emission("Emission", float) = 1
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
        LOD 200

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

		float _Scale, _Speed, _Frequency;

		void vert(inout appdata_full v) 
		{
			half offsetvert = (v.vertex.z * v.vertex.z) + (v.vertex.x * v.vertex.x);

			half value = _Scale * sin(_Time.w * _Speed + offsetvert * _Frequency);

			v.vertex.y += value;
		}


        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _Emission;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
			o.Emission = c.rgb * tex2D(_MainTex, IN.uv_MainTex).a * _Emission;
        }
        ENDCG
    }
}
