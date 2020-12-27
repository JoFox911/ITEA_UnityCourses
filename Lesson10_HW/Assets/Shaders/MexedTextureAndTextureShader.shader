Shader "Unlit/Mixed texture and texture" {
    Properties {
      _PrimaryTex ("Primary texture", 2D) = "white" {}
      _SecondaryTex ("Secondary texture", 2D) = "white" {}
      _SecondaryTexIntensity ("Second texture intensity", Range(0,1)) = 0.5
    }
    SubShader {
        Pass {
		    SetTexture[_PrimaryTex]
		    SetTexture[_SecondaryTex] { 
			    ConstantColor (0,0,0, [_SecondaryTexIntensity]) 
			    Combine texture Lerp(constant) previous
		    }		
	    }
    } 
    Fallback "Diffuse"
  }
