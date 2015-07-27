using System;
using Urho;

public static class QuaternionExtensions
{
    public static Quaternion FromRotationTo(Vector3 start, Vector3 end)
    {
        Quaternion result = new Quaternion();
        start.Normalize();
        end.Normalize();

        const float epsilon = 0.000001f;
        float d = Vector3.Dot(start, end);
    
        if (d > -1.0f + epsilon)
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
            if (axis.Length < epsilon)
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