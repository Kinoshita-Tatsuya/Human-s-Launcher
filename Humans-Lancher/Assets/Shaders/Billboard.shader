Shader "Unlit/BillboardSh"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "AlphaTest" "RenderType" = "TransparentCutout"
		"IgnoreProjector" = "True" "DisableBatching" = "True" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Cutoff;

            v2f vert (appdata v)
            {
                v2f o;

				// Meshの原点をModelView変換 
				// 0を回転 拡大しようが0平行移動だけ効果を受ける
				float3 origin_mv = UnityObjectToViewPos(float3(0, 0, 0));

				// スケールと回転（平行移動なし）だけModel変換して、View変換はスキップ
				// キャストで平行移動の部分削除 カメラで回転はまだ行われていない
				float3 notTransform_m = mul((float3x3)unity_ObjectToWorld, v.vertex);

				// scaleRotatePosを右手系に変換して、viewPosに加算
				// 本来はView変換で暗黙的にZが反転されているので、
				// View変換をスキップする場合は明示的にZを反転する必要がある
				origin_mv += float3(notTransform_m.xy, -notTransform_m.z);

				// プロジェクション変換
				o.vertex = mul(UNITY_MATRIX_P, float4(origin_mv, 1));

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

				// 0以下の時ドット破棄
				clip(col.a - _Cutoff);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
