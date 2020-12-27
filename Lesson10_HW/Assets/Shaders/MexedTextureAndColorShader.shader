Shader "Unlit/Mixed texture and color" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _MainColor ("Color", Color) = (1, 1, 1, 1)
      _ColorIntensity ("Color intensity", Range(0,1)) = 0.5
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert finalcolor:mycolor
      struct Input {
          float2 uv_MainTex;
      };
      fixed4 _MainColor;
      float _ColorIntensity;
      void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
      {
          // если _ColorIntensity = 0 то дополняем _MainColor до 1, 
          // при умножении на которую получится тот же что и был цвет
          // если _ColorIntensity = 1 то _MainColor никак не поменяется
          // если _ColorIntensity равно чему-то среднему то _MainColor будет стремиться к белому цвету (1,1,1,1), 
          // чем меньше будет значение _ColorIntensity
          _MainColor += (1 -_MainColor) * (1 - _ColorIntensity);
          color *= _MainColor;
      }
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
           o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }
