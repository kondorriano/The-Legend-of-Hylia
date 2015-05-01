Shader "Custom/Test" {
	Properties {
		_Cam1 ("Cam1 (RGB)", 2D) = "" {}	
		_Cam2 ("Cam2 (RGB)", 2D) = "" {}	
		_Direction("Direction", Vector) = (0,0,0,0) 
		_Width("Width", Float) = 0 
		_Heigth("Height", Float) = 0 }
	
	// Shader code pasted into all further CGPROGRAM blocks
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};
	
	sampler2D _Cam1;
	sampler2D _Cam2;
	Vector _Direction;
	float _Width;
	float _Heigth;
	
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	fixed4 frag(v2f i) : SV_Target 
	{	
		float2 dir = normalize(_Direction.xy);
		half2 aux = i.uv;
		aux.y = 1-aux.y;
		
		float2 p = (aux * _ScreenParams.xy) - (0.5 * _ScreenParams.xy);
		float signo = dot(dir,p);
		
		//float angle = dot(dir,normalize(p));
		//float2x2 rotMat = float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
		//float2 r = mul(rotMat,p);
		//r = float2(r.x/_ScreenParams.x, r.y/_ScreenParams.y);
		//float rdist = abs(r.y);
		//return fixed4(rdist,0 ,0,1); 
		
		if(signo < 0 ) return tex2D(_Cam1, aux);
		else return tex2D(_Cam2, aux);
	}

	ENDCG 
	
Subshader {
 Pass {
	  ZTest Always Cull Off ZWrite Off
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
  }
}

Fallback off
	
} // shader
