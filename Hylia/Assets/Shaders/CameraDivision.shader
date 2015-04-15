Shader "Camera/Division" {
	Properties {
		_mValue ("mValue", Float) = 0
		_nValue ("nValue", Float) = 0

	}
   SubShader {
      Pass {
         Cull Off // turn off triangle culling, alternatives are:
         // Cull Back (or nothing): cull only back faces 
         // Cull Front : cull only front faces
 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
         
         float _mValue;
         float _nValue;
 
         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
         	float auxY = _mValue*input.pos.x + _nValue;
         	float auxX = (input.pos.y - _nValue)/_mValue;
         	
         	if(input.pos.x < auxX && input.pos.y < auxY) return float4(0.0, 0.0, 1.0, 1.0);
         	else if(input.pos.x > auxX && input.pos.y > auxY) discard;
         	else discard;
         	
            return float4(0.0, 1.0, 0.0, 1.0); // green
         }
     
 
         ENDCG  
      }
   }
}