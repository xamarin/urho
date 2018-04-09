#extension GL_OES_EGL_image_external : require

#include "Uniforms.glsl"
#include "Samplers.glsl"
#include "Transform.glsl"
#include "ScreenPos.glsl"

varying highp vec2 vScreenPos;
uniform float cCameraScale;
uniform samplerExternalOES sTexture;

void VS()
{
	mat4 modelMatrix = iModelMatrix;
	vec3 worldPos = GetWorldPos(modelMatrix);
	gl_Position = GetClipPos(worldPos);
	vScreenPos = GetScreenPosPreDiv(gl_Position);
}

void PS()
{
#if defined(ARCORE_LANDSCAPE_RIGHT)
	vec2 vTexCoord = vec2(vScreenPos.x, 1.0 - vScreenPos.y);
#elif defined(ARCORE_LANDSCAPE_LEFT)
	vec2 vTexCoord = vec2(vScreenPos.x, vScreenPos.y);
#else // PORTRAIT
	vec2 vTexCoord = vec2(1.0 - vScreenPos.y, 1.0 - vScreenPos.x);
#endif
	gl_FragColor = texture2D(sTexture, vTexCoord);
}
