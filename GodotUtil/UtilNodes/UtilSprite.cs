using Godot;

namespace GodotUtil;

public static class UtilSprite
{
    public static Vector2 GetSpriteSize(this Sprite2D sprite)
    {
        return sprite.Texture.GetSize() * sprite.Scale;
    }

}
