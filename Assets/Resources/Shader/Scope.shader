Shader "Custom/ScopeWithDarkEdgeTransparentCenter"
{
    Properties
    {
        _MainTex ("Scope Image (RGBA)", 2D) = "white" { }
        _Darkness ("Edge Darkness", Range(0, 1)) = 1.0 // �ܺ� ��ο� ����
        _ScopeCenter ("Scope Center", Vector) = (0.5, 0.5, 0, 0) // �������� �߽� (0~1, 0~1)
        _ScopeRadius ("Scope Radius", Float) = 0.2 // �������� ũ��
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }

        Pass
        {
            // ���� ���� �߰�
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 pos : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Darkness;
            float4 _ScopeCenter; // �������� �߽�
            float _ScopeRadius; // ������ ������

            // ���ؽ� ���̴�
            v2f vert(appdata_t v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            // �����׸�Ʈ ���̴�
            half4 frag(v2f i) : SV_Target {
                // �ؽ�ó���� ���� ��������
                half4 col = tex2D(_MainTex, i.texcoord);

                // ������ �ٱ����� ��Ӱ� ó�� (������ �����ϸ鼭 ��ο� ���� ����)
                float2 scopeCenter = _ScopeCenter.xy; // �������� �߽�
                float dist = distance(i.texcoord, scopeCenter); // ���� �ȼ��� ������ �߽ɰ��� �Ÿ�

                if (dist > _ScopeRadius) {
                    // �ٱ��� ������ ������ ������ ��ο� ������ ����
                    col.rgb *= _Darkness; // ������ ��Ӱ� ���� (0~1 ���� ������ ����)
                    col.a = _Darkness; // �ܺ��� ���� ����
                }
                else {
                    // �� ������ ������ �����ϰ� ����
                    col.a = 0.0;
                }

                return col;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
