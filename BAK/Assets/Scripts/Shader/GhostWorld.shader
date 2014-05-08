Shader "Hidden/GhostWorld" {
Properties {
	_Transition ("Transition", Range (0, 1)) = 0
	_DeathTimer ("Death Timer", Range (0, 1)) = 0
	_RevivalRange ("Revival Range", Range (0, 1)) = 0
	
	_MainTex ("View", 2D) = "View" {}
	_BlueShift ("Blue Shift", Range (0, 1)) = 0.25
	_WaveCount ("Wave Count", float) = 15
	_WaveSize ("Wave Size", float) = 0.005
	
	_Overlay ("Overlay Effect", 2D) = "Overlay" {}
	_OverlayAlpha ("Overlay Alpha", Range (0, 1)) = 0
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
		
		half _Transition;
		half _DeathTimer;
		half _RevivalRange;

		sampler2D _MainTex;
		half _BlueShift;
		half _WaveCount;
		half _WaveSize;
		
		sampler2D _Overlay;
		half _OverlayAlpha;
		
		v2f_img vert (appdata_base v) {
		    v2f_img o;
		    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		    o.uv = v.texcoord;
		    return o;
		}

		fixed4 frag (v2f_img IN) : COLOR {
			fixed4 original;
			
			if (_Transition > 0) {
				//create a verticle sin wave
				float wave = sin(_Time.z +IN.uv.y * _WaveCount) * _WaveSize/1000;
				
				//sample pixels according to sin wave
				original = tex2D(_MainTex, IN.uv + half2(lerp(0, wave, _Transition), 0));
				
				//gray scale the image
				original.rgb = lerp(original.rgb, dot(original.rgb, float3(0.3, 0.59, 0.11)), _Transition);
				
				//blueshift the image
				original.r = lerp(original.r, original.r * (1 - _BlueShift), _Transition);
				original.g = lerp(original.g, original.g * (1 - _BlueShift), _Transition);
				
				//sample pixels from overlay
				if (_OverlayAlpha > 0) {
					float pulse = lerp(-0.75, 0, _DeathTimer);
					fixed4 overlay = tex2D(_Overlay, IN.uv *(1 + pulse) - 0.5 * pulse);
					_OverlayAlpha = lerp(0, _OverlayAlpha, _Transition);
					original.rgb = lerp(original.rgb, overlay.rgb, overlay.a * _OverlayAlpha);
				}
			} else {
				original = tex2D(_MainTex, IN.uv);
			}
			return original;
		}
		ENDCG

	}
}

Fallback off

}
