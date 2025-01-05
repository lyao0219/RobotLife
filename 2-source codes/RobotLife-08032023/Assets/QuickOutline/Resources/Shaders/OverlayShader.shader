Shader "Hidden/OverlayShader"
{
    HLSLINCLUDE

        #include "UnityCG.cginc"

#if Pattern_Diamond
        // - - x - -
        // - x - x -
        // x - c - x
        // - x - x -
        // - - x - -
        static const int samplePattern_Count = 8;
        static const float2 samplePattern[8] =
        {
            float2( 2, 0 ),
            float2(-2, 0 ),
            float2( 1, 1 ),
            float2(-1,-1 ),
            float2( 0, 2 ),
            float2( 0,-2 ),
            float2(-1, 1 ),
            float2( 1,-1 )
        };

#elif Pattern_Rect
        // - - - - -
        // - x x x -
        // - x c x -
        // - x x x -
        // - - - - -
        static const int samplePattern_Count = 8;
        static const float2 samplePattern[8] =
        {
            float2( 1, 0 ),
            float2(-1, 0 ),
            float2( 1, 1 ),
            float2(-1,-1 ),
            float2( 0, 1 ),
            float2( 0,-1 ),
            float2(-1, 1 ),
            float2( 1,-1 )
        };
#endif

        // This provides access to the vertices of the mesh being rendered
        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
        };

        float4 _OutlineColor;
        float4 _FillColor;
        // Range [0, 255]
        float _GroupID;

        UNITY_DECLARE_TEX2D(_OverlayIDTexture);

        bool IsEdgePixel(int2 screenPos, const int sampleCount, const float2 offsets[8] )
        {
            // Get overlay id of the pixel currently being rendered
            float center = _OverlayIDTexture.Load(int3(screenPos.xy, 0)).r;
            // Compare it to all neighbors using the sample offsets
            for(int i = 0; i < sampleCount; i++)
            {
                float neighbor = _OverlayIDTexture.Load(int3(screenPos.xy + offsets[i], 0)).r;
                if(neighbor != center)
                {
                    // This is an edge pixel! use outline color
                    return true;
                }
            }

            return false;         
        }

        // Vertex shader
        v2f Vert(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            return o;
        }

        // Fragment shader (first pass)
        float FragWriteOverlayID(v2f i) : SV_Target
        {
            // Map range [0, 255] to [0.0f, 1.0f]
            return _GroupID / 255.0f;
        }

        // Fragment shader (second pass)
        float4 FragOverlay(UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
        {
            //bool isEdgePixel = IsEdgePixel(screenPos.xy, samplePattern_Count, samplePattern);
            if(IsEdgePixel(screenPos.xy, samplePattern_Count, samplePattern))
                return _OutlineColor;
            else
                return _FillColor;
        }

    ENDHLSL




    SubShader
    {
        Pass // [LEqual P1] Write overlay ID
        {
            ZTest LEqual
            ZWrite Off
            Cull Back
            Blend Off // <-- Turn off alpha blending!

            HLSLPROGRAM
                #pragma target 3.0
                #pragma multi_compile Pattern_Diamond Pattern_Rect
                #pragma vertex Vert
                #pragma fragment FragWriteOverlayID
            ENDHLSL
        }       

        Pass // [LEqual P2] Apply overlay fill and outline
        {
            ZTest LEqual
            ZWrite Off
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
                #pragma target 3.0
                #pragma multi_compile Pattern_Diamond Pattern_Rect
                #pragma vertex Vert
                #pragma fragment FragOverlay
            ENDHLSL
        }

        Pass // [Always P1] Write overlay ID
        {
            Stencil {
                Ref 1
                Comp always
                Pass replace
            }

            ZTest Always
            ZWrite Off
            Cull Back
            Blend Off // <-- Turn off alpha blending!

            HLSLPROGRAM
                #pragma target 3.0
                #pragma multi_compile Pattern_Diamond Pattern_Rect
                #pragma vertex Vert
                #pragma fragment FragWriteOverlayID
            ENDHLSL
        }
 
        Pass // [Always P2] Apply overlay fill and outline
        {
            Stencil {
                Ref 1
                Comp equal
                Pass IncrWrap 
            }

            ZTest Always
            ZWrite Off
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
                #pragma target 3.0
                #pragma multi_compile Pattern_Diamond Pattern_Rect 
                #pragma vertex Vert
                #pragma fragment FragOverlay
            ENDHLSL
        }
    }
}