using Godot;
using Util;

namespace GodotUtil;

public static class UtilTween
{
    public static PropertyTweener TweenShaderUniform(this Tween tween, ShaderMaterial obj, string property, Variant finalVal, double duration) => 
        tween.TweenProperty(obj, "shader_parameter/" + property, finalVal, duration);

    public static void PlayAll(this Tween tween, float bigStep = int.MaxValue - 1)
    {
        tween.CustomStep(bigStep);
        tween.Kill();
    }

}