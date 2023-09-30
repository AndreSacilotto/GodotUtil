using Godot;

namespace GodotUtil;

public static class UtilSprite
{
    public static Vector2 GetSpriteSize(this Sprite2D sprite)
    {
        return sprite.Texture.GetSize() * sprite.Scale;
    }

    public static Vector2 GetSpriteSize(this Sprite3D sprite)
    {
        var scale = sprite.Scale;
        return sprite.Texture.GetSize() * sprite.PixelSize * new Vector2(scale.X, scale.Y);
    }

}
