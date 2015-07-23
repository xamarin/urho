using System;
using Urho;

public static class QuaternionExtensions
{
    public const float M_EPSILON = 0.000001f;

    public static Quaternion FromRotationTo(Vector3 start, Vector3 end)
    {
        Quaternion result = new Quaternion();
        start.Normalize();
        end.Normalize();
        
        float d = Vector3.Dot(start, end);
    
        if (d > -1.0f + M_EPSILON)
        {
            Vector3 c = Vector3.Cross(start, end);
            float s = (float) Math.Sqrt((1.0f + d) * 2.0f);
            float invS = 1.0f / s;

            result.X  = c.X * invS;
            result.Y = c.Y * invS;
            result.Z = c.Z * invS;
            result.W = 0.5f * s;
        }
        else
        {
            Vector3 axis = Vector3.Cross(Vector3.UnitX, start);
            if (axis.Length < M_EPSILON)
                axis = Vector3.Cross(Vector3.UnitY, start);

            return FromAngleAxis(180.0f, axis);
        }
        return result;
    }

    public static Quaternion FromAngleAxis(float angle, Vector3 axis)
    {
        axis.Normalize();
        angle *= (float)(Math.PI / 360.0);
        float sinAngle = (float)Math.Sin(angle);
        float cosAngle = (float)Math.Cos(angle);

        return new Quaternion(cosAngle, axis.X * sinAngle, axis.Y * sinAngle, axis.Z * sinAngle);
    }

    //operator * 
    public static Vector3 MutliplyWithVector3(this Quaternion quaternion, Vector3 vector)
    {
        var qVec = new Vector3(quaternion.X, quaternion.Y, quaternion.Z);
        var cross1 = Vector3.Cross(qVec, vector);
        var cross2 = Vector3.Cross(qVec, cross1);

        return vector + 2.0f*cross1*quaternion.W + cross2;
    }
}


public static class Vector3Extensions
{
    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
    }
}

public class Vector4Extensions
{
    /// <summary>
    /// void Define(const Vector3& normal, const Vector3& point)
    /// </summary>
    public static Vector4 FromNormalAndPointVectors(Vector3 normal, Vector3 point)
    {
        //NOTE: realyyyyy not sure about this impl.....

        /*
            normal_ = normal.Normalized();
            absNormal_ = normal_.Abs();
            d_ = -normal_.DotProduct(point);
        */

        normal.Normalize();
        normal = normal.Abs();
        return new Vector4(normal.X, normal.Y, normal.Z, Vector3.Dot(normal, point));
    }
}