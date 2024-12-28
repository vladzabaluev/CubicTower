Shader "Custom/MaskedObject"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue" = "Geometry"}
        Pass
        {
            Stencil
            {
                Ref 1
                Comp NotEqual
            }
            SetTexture [_MainTex] { combine texture }
        }
    }
}
