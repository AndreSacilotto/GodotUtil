using Godot;

namespace GodotUtil;

public static class UtilRaycast
{
    #region Dict Keys
    public static readonly Variant position = nameof(position); // Vector2/3
    public static readonly Variant collider = nameof(collider);
    public static readonly Variant linear_velocity = nameof(linear_velocity); // Vector2/3
    public static readonly Variant collider_id = nameof(collider_id); // long
    public static readonly Variant rid = nameof(rid); // RID
    public static readonly Variant shape = nameof(shape);
    public static readonly Variant normal = nameof(normal);
    #endregion

    // 2D
    public static void Setup(this PhysicsRayQueryParameters2D query, Vector2 from, Vector2 to)
    {
        query.From = from;
        query.To = to;
    }

    // 3D

    public static void Setup(this PhysicsRayQueryParameters3D query, Vector3 from, Vector3 to)
    {
        query.From = from;
        query.To = to;
    }

    public static void MouseRaycast(in PhysicsRayQueryParameters3D query, Camera3D camera, Vector2 mousePos, float rayLength = 1000f)
    {
        query.From = camera.ProjectRayOrigin(mousePos);
        query.To = camera.ProjectRayNormal(mousePos) * rayLength;
    }

}
