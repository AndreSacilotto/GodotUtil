using Godot;

namespace Util.Vector
{
    public static class VectorMath
    {
        public static Vector2 RadianToVector2(float radian) => 
            new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        public static Vector2 DegreeToVector2(float degree) => 
            RadianToVector2(Mathf.Deg2Rad(degree));

        public static float TauAtan2(Vector2 vector) => TauAtan2(vector.y, vector.x);
        public static float TauAtan2(float y, float x) => Mathf.Atan2(y, x) + Mathf.Pi;


    }
}
