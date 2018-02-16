#include "Uniforms.glsl"
#include "Samplers.glsl"
#include "Transform.glsl"
#include "ScreenPos.glsl"

varying highp vec2 vScreenPos;

uniform float cCameraScale;
uniform float cTx;
uniform float cTy;
uniform float cScaleX;
uniform float cScaleY;
uniform float cScaleXY;
uniform float cScaleYX;
uniform float cYOffset;

void VS()

{
	mat4 modelMatrix = iModelMatrix;
	vec3 worldPos = GetWorldPos(modelMatrix);
	gl_Position = GetClipPos(worldPos);   
	vScreenPos = GetScreenPosPreDiv(gl_Position);
}

void PS()
{
    vec2 center = vec2(0.5, 0.5);

#if defined(ARKIT_LANDSCAPE)
    // TODO: remove this hack 
    float x = vScreenPos.x + cYOffset * cScaleX;
    if (x > 1.0 && cScaleX <= 1.0)
        x -= 1.0;

    vec2 vTexCoordY = vec2(x, 1.0 - vScreenPos.y);
    vec2 vTexCoordUV = vec2(vScreenPos.x, 1.0 - vScreenPos.y);
    vec2 scale = vec2(1.0 / cScaleX, 1.0 / cScaleY);
#else
    float y = vScreenPos.y - cYOffset * cScaleYX;
    if (y < 0.0 && cScaleYX <= 1.0)
        y = 1.0 + y;

    vec2 vTexCoordY = vec2(1.0 - y, 1.0 - vScreenPos.x);
    vec2 vTexCoordUV = vec2(1.0 - vScreenPos.y, 1.0 - vScreenPos.x);
    vec2 scale = vec2(1.0 / cScaleYX, 1.0 / -cScaleXY);
#endif

    vTexCoordY = (vTexCoordY - center) * scale + center;
    vTexCoordUV = (vTexCoordUV - center) * scale + center;

	mat4 ycbcrToRGBTransform = mat4(
		vec4(+1.0000, +1.0000, +1.0000, +0.0000),
		vec4(+0.0000, -0.3441, +1.7720, +0.0000),
		vec4(+1.4020, -0.7141, +0.0000, +0.0000),
		vec4(-0.7010, +0.5291, -0.8860, +1.0000));

	vec4 ycbcr = vec4(texture2D(sDiffMap, vTexCoordY).r,
					  texture2D(sNormalMap, vTexCoordUV).ra, 1.0);
	gl_FragColor = ycbcrToRGBTransform * ycbcr;
}