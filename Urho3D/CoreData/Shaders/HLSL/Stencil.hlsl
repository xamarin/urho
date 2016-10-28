#include "Uniforms.hlsl"
#include "Transform.hlsl"

void VS(float4 iPos : POSITION,
    out float4 oPos : OUTPOSITION)
{
    float4x3 modelMatrix = iModelMatrix;
    float3 worldPos = GetWorldPos(modelMatrix);
    oPos = GetClipPos(worldPos);
}

void PS(out float4 oColor : OUTCOLOR0)
{
    oColor = 1.0;
}
