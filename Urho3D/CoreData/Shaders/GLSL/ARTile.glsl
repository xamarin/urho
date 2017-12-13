#include "Uniforms.glsl"
#include "Samplers.glsl"
#include "Transform.glsl"
#include "ScreenPos.glsl"
#include "Fog.glsl"

varying vec2 vTexCoord;
varying vec4 vWorldPos;
varying vec3 vNormal;
varying vec4 vEyeVec;

uniform vec4 cMeshColor;
uniform float cMeshScale;
uniform float cMeshAlpha;

void VS()
{
    mat4 modelMatrix = iModelMatrix;
    vNormal = GetWorldNormal(iModelMatrix);
    vec3 worldPos = GetWorldPos(modelMatrix);
    gl_Position = GetClipPos(worldPos);
    vTexCoord = GetTexCoord(iTexCoord);
    vWorldPos = vec4(worldPos, GetDepth(gl_Position));


    //Calculate vTexCoord from normal
    vec3 vNormal = abs(GetWorldNormal(modelMatrix));
    if (vNormal.x > vNormal.y)
        if (vNormal.x > vNormal.z)
            vTexCoord = vec2(worldPos.y,worldPos.z);
        else
            vTexCoord = vec2(worldPos.x,worldPos.y);
    else
        if (vNormal.y > vNormal.z)
            vTexCoord = vec2(worldPos.x,worldPos.z);
        else
            vTexCoord = vec2(worldPos.x,worldPos.y);
}

void PS()
{
    gl_FragColor = vec4(cMeshColor.rgb, texture2D(sDiffMap, vTexCoord * cMeshScale).a * cMeshColor.a * cMeshAlpha);
}