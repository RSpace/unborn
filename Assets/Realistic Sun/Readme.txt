The package contains a model of the sun with high-resolution textures and distortion shaders.
To add a glow effect, you must add the standard bloom effect for the camera and activate HDR on main camera.

Model of the sun runs on all platforms(PC and Mobile) with DX9/DX11/OpenGL/OpenGLES and with Forward/Deffered Renderer.
Support all version of Unity since 4.3 and Unity 5.

Bloom settings used in the demo:
intensity 2
threshhold 0.9
blur iterations 4
sample distanse 10



Sun settings:
Color - default albedo color
Albedo(RGB) - main texture of the sun
Mask - mask for animated distortion of main texture 
Color strength - increased the main color(allow set stength of bloom)
waves direction - allow set move speed(distortion animation) of albedo and mask textures.
texture offset multiplier - allow set albedo and mask multiplier