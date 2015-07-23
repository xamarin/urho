using Urho;


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