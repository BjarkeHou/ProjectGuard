Shader "Custom/LineOfSight" {
Properties {
	_MainTex ("View", 2D) = "View" {}
	_LightTex ("Light Map", 2D) = "Light Map" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform sampler2D _LightTex;
		uniform float alpha;
		
		v2f_img vert (appdata_base v) {
		    v2f_img o;
		    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		    o.uv = v.texcoord;
		    return o;
		}

		fixed4 frag (v2f_img i) : COLOR
		{
			fixed4 original = tex2D(_MainTex, i.uv);
			fixed4 input = tex2D(_LightTex, i.uv);
			original.rgb = original.rgb *input.g;
			
			return original;
		}
		ENDCG

	}
}

Fallback off

}