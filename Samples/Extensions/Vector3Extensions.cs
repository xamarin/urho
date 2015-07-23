using System;
using Urho;


public static class Vector3Extensions
{
    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
    }
}
