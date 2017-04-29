#include "Uniforms.hlsl"
#include "Samplers.hlsl"
#include "Transform.hlsl"
#include "ScreenPos.hlsl"

// parameters for PS
// defined in materials (see Materials/Earth.xml)
#ifdef COMPILEPS
uniform float cCloudsFactor;
uniform float4 cSpecColor;
uniform float2 cCloudsOffset;
#endif

void PS(
	float4 iTexCoord : TEXCOORD0,
	float4 iTangent : TEXCOORD3,
	float3 iNormal : TEXCOORD1,
	float4 iWorldPos : TEXCOORD2,
	float4 iScreenPos : TEXCOORD5,
	out float4 oColor : OUTCOLOR0)
{
	float4 earthDiff = Sample2D(DiffMap, iTexCoord.xy);
	float4 clouds = Sample2D(EnvMap, iTexCoord.xy + cCloudsOffset);
	float4 night = Sample2D(EmissiveMap, iTexCoord.xy);
	float3 spec = Sample2D(SpecMap, iTexCoord.xy);
	float3x3 tbn = float3x3(iTangent.xyz, float3(iTexCoord.zw, iTangent.w), iNormal);
	float3 normal = normalize(mul(DecodeNormal(Sample2D(NormalMap, iTexCoord.xy)), tbn));

	// Earth texture
	float3 finalColor = earthDiff.rgb;
	// Specular map
	finalColor += cSpecColor.rgb * spec.rgb * cLightColor.a;
	// Normal map & directional light (we use only this kind of light in this shader, no Ambient, Spot, etc)
	finalColor *= saturate(dot(normal, cLightDirPS));
	// Clouds:
	finalColor += clouds.rgb * (dot(iNormal, cLightDirPS) + 0.5);
	// Nigth lamps (only for the dark side)
	finalColor += night.rgb * (1 - dot(iNormal, cLightDirPS));
	// Return final color
	oColor = float4(finalColor, 1.0);
}


// Default LitSolid Vertex Shader impl:
void VS(float4 iPos : POSITION,
		float3 iNormal : NORMAL,
		float2 iTexCoord : TEXCOORD0,
		float4 iTangent : TANGENT,
	out float4 oTexCoord : TEXCOORD0,
	out float4 oTangent : TEXCOORD3,
	out float3 oNormal : TEXCOORD1,
	out float4 oWorldPos : TEXCOORD2,
	out float4 oScreenPos : TEXCOORD5,
	out float4 oPos : OUTPOSITION)
{
	float3 worldPos = GetWorldPos(iModelMatrix);
	oPos = GetClipPos(worldPos);
	oNormal = GetWorldNormal(iModelMatrix);
	oWorldPos = float4(worldPos, GetDepth(oPos));
	float3 tangent = GetWorldTangent(iModelMatrix);
	float3 bitangent = cross(tangent, oNormal) * iTangent.w;
	oTexCoord = float4(GetTexCoord(iTexCoord), bitangent.xy);
	oTangent = float4(tangent, bitangent.z);
}