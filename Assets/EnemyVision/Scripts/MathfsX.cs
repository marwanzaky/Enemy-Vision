using UnityEngine;

public static class MathfX
{
    public static Vector3 DegressToVector3D(float degress)
    {
        return RadiansToVector3D(degress * Mathf.Deg2Rad);
    }

    public static Vector2 DegressToVector2D(float degress)
    {
        return RadiansToVector2D(degress * Mathf.Deg2Rad);
    }

    public static Vector3 RadiansToVector3D(float radians)
    {
        return new Vector3((float)Mathf.Cos(radians), 0f, (float)Mathf.Sin(radians));
    }

    public static Vector2 RadiansToVector2D(float radians)
    {
        return new Vector2((float)Mathf.Cos(radians), (float)Mathf.Sin(radians));
    }
}
