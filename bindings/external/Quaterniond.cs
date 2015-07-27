#region --- License ---
/*
Copyright (c) 2006 - 2008 The Open Toolkit library.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */
#endregion

using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Urho
{
    /// <summary>
    /// Represents a Quaterniond.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaterniond : IEquatable<Quaterniond>
    {
        #region Fields

        double w;
        double x;
        double y;
        double z;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new Quaterniond from vector and w components (FromAxisAngle)
        /// </summary>
        /// <param name="v">The vector part</param>
        /// <param name="w">The w part</param>
        public Quaterniond(Vector3d v, double w)
        {
            var q = FromAxisAngle(v, w);
            this.w = q.W;
            this.x = q.X;
            this.y = q.Y;
            this.z = q.Z;
        }

        /// <summary>
        /// Construct a new Quaterniond
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        /// <param name="w">The w component</param>
        public Quaterniond(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        // From Euler angles
        public Quaterniond(double x, double y, double z)
        {
            const double M_DEGTORAD_2 = (double)Math.PI / 360.0;
            x *= M_DEGTORAD_2;
            y *= M_DEGTORAD_2;
            z *= M_DEGTORAD_2;
            double sinX = (double)Math.Sin(x);
            double cosX = (double)Math.Cos(x);
            double sinY = (double)Math.Sin(y);
            double cosY = (double)Math.Cos(y);
            double sinZ = (double)Math.Sin(z);
            double cosZ = (double)Math.Cos(z);

            this.x = cosY * sinX * cosZ + sinY * cosX * sinZ;
            this.y = sinY * cosX * cosZ - cosY * sinX * sinZ;
            this.z = cosY * cosX * sinZ - sinY * sinX * cosZ;
            this.w = cosY * cosX * cosZ + sinY * sinX * sinZ;
        }

        public Quaterniond(ref Matrix3 matrix)
        {
            double scale = System.Math.Pow(matrix.Determinant, 1.0d / 3.0d);

            w = (double)(System.Math.Sqrt(System.Math.Max(0, scale + matrix[0, 0] + matrix[1, 1] + matrix[2, 2])) / 2);
            x = (double)(System.Math.Sqrt(System.Math.Max(0, scale + matrix[0, 0] - matrix[1, 1] - matrix[2, 2])) / 2);
            y = (double)(System.Math.Sqrt(System.Math.Max(0, scale - matrix[0, 0] + matrix[1, 1] - matrix[2, 2])) / 2);
            z = (double)(System.Math.Sqrt(System.Math.Max(0, scale - matrix[0, 0] - matrix[1, 1] + matrix[2, 2])) / 2);

            if (matrix[2, 1] - matrix[1, 2] < 0) X = -X;
            if (matrix[0, 2] - matrix[2, 0] < 0) Y = -Y;
            if (matrix[1, 0] - matrix[0, 1] < 0) Z = -Z;
        }

        #endregion

        #region Public Members

        #region Properties

        /*/// <summary>
        /// Gets or sets an OpenTK.Vector3d with the X, Y and Z components of this instance.
        /// </summary>
        [Obsolete("Use Xyz property instead.")]
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public Vector3d XYZ { get { return Xyz; } }

        /// <summary>
        /// Gets or sets an OpenTK.Vector3d with the X, Y and Z components of this instance.
        /// </summary>
        public Vector3d Xyz { get { return xyz; } }*/

        /// <summary>
        /// Gets or sets the X component of this instance.
        /// </summary>
        [XmlIgnore]
        public double X { get { return x; } set { x = value; } }

        /// <summary>
        /// Gets or sets the Y component of this instance.
        /// </summary>
        [XmlIgnore]
        public double Y { get { return y; } set { y = value; } }

        /// <summary>
        /// Gets or sets the Z component of this instance.
        /// </summary>
        [XmlIgnore]
        public double Z { get { return z; } set { z = value; } }

        /// <summary>
        /// Gets or sets the W component of this instance.
        /// </summary>
        public double W { get { return w; } set { w = value; } }

        #endregion

        #region Instance

        #region ToAxisAngle

        /// <summary>
        /// Convert the current Quaterniond to axis angle representation
        /// </summary>
        /// <param name="axis">The resultant axis</param>
        /// <param name="angle">The resultant angle</param>
        public void ToAxisAngle(out Vector3d axis, out double angle)
        {
            Vector4d result = ToAxisAngle();
            axis = result.Xyz;
            angle = result.W;
        }

        /// <summary>
        /// Convert this instance to an axis-angle representation.
        /// </summary>
        /// <returns>A Vector4d that is the axis-angle representation of this Quaterniond.</returns>
        public Vector4d ToAxisAngle()
        {
            Quaterniond q = this;
            if (q.W > 1.0)
                q.Normalize();

            Vector4d result = new Vector4d();

            result.W = 2.0 * (double)System.Math.Acos(q.W); // angle
            double den = (double)System.Math.Sqrt(1.0 - q.W * q.W);
            if (den > 0.0001f)
            {
                result.X = q.X / den;
                result.Y = q.Y / den;
                result.Z = q.Z / den;
            }
            else
            {
                // This occurs when the angle is zero. 
                // Not a problem: just set an arbitrary normalized axis.
                result.Xyz = Vector3d.UnitX;
            }

            return result;
        }

        #endregion

        #region public double Length

        /// <summary>
        /// Gets the length (magnitude) of the Quaterniond.
        /// </summary>
        /// <seealso cref="LengthSquared"/>
        public double Length
        {
            get
            {
                return (double)System.Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
            }
        }

        #endregion

        #region public double LengthSquared

        /// <summary>
        /// Gets the square of the Quaterniond length (magnitude).
        /// </summary>
        public double LengthSquared
        {
            get
            {
                return W * W + X * X + Y * Y + Z * Z;
            }
        }

        #endregion

        #region public void Normalize()

        /// <summary>
        /// Scales the Quaterniond to unit length.
        /// </summary>
        public void Normalize()
        {
            double scale = 1.0 / this.Length;
            X *= scale;
            Y *= scale;
            Z *= scale;
            W *= scale;
        }

        #endregion

        #region public void Conjugate()

        /// <summary>
        /// Convert this Quaterniond to its conjugate
        /// </summary>
        public void Conjugate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }

        #endregion

        #endregion

        #region Static

        #region Fields

        /// <summary>
        /// Defines the identity Quaterniond.
        /// </summary>
        public static Quaterniond Identity = new Quaterniond(0, 0, 0, 1);

        #endregion

        #region Add

        /// <summary>
        /// Add two Quaternionds
        /// </summary>
        /// <param name="left">The first operand</param>
        /// <param name="right">The second operand</param>
        /// <returns>The result of the addition</returns>
        public static Quaterniond Add(Quaterniond left, Quaterniond right)
        {
            return new Quaterniond(
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z,
                left.W + right.W);
        }

        /// <summary>
        /// Add two Quaternionds
        /// </summary>
        /// <param name="left">The first operand</param>
        /// <param name="right">The second operand</param>
        /// <param name="result">The result of the addition</param>
        public static void Add(ref Quaterniond left, ref Quaterniond right, out Quaterniond result)
        {
            result = new Quaterniond(
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z,
                left.W + right.W);
        }

        #endregion

        #region Sub

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>The result of the operation.</returns>
        public static Quaterniond Sub(Quaterniond left, Quaterniond right)
        {
            return new Quaterniond(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z,
                left.W - right.W);
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <param name="result">The result of the operation.</param>
        public static void Sub(ref Quaterniond left, ref Quaterniond right, out Quaterniond result)
        {
            result = new Quaterniond(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z,
                left.W - right.W);
        }

        #endregion

        #region Mult

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <param name="result">A new instance containing the result of the calculation.</param>
        public static void Multiply(ref Quaterniond left, ref Quaterniond right, out Quaterniond result)
        {
            result = new Quaterniond(
                left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y,
                left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z,
                left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X,
                left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z);
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="Quaterniond">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <param name="result">A new instance containing the result of the calculation.</param>
        public static void Multiply(ref Quaterniond Quaterniond, double scale, out Quaterniond result)
        {
            result = new Quaterniond(Quaterniond.X * scale, Quaterniond.Y * scale, Quaterniond.Z * scale, Quaterniond.W * scale);
        }

        #endregion

        #region Conjugate

        /// <summary>
        /// Get the conjugate of the given Quaterniond
        /// </summary>
        /// <param name="q">The Quaterniond</param>
        /// <returns>The conjugate of the given Quaterniond</returns>
        public static Quaterniond Conjugate(Quaterniond q)
        {
            return new Quaterniond(-q.X, -q.Y, -q.Z, q.W);
        }

        /// <summary>
        /// Get the conjugate of the given Quaterniond
        /// </summary>
        /// <param name="q">The Quaterniond</param>
        /// <param name="result">The conjugate of the given Quaterniond</param>
        public static void Conjugate(ref Quaterniond q, out Quaterniond result)
        {
            result = new Quaterniond(-q.X, -q.Y, -q.Z, q.W);
        }

        #endregion

        #region Invert

        /// <summary>
        /// Get the inverse of the given Quaterniond
        /// </summary>
        /// <param name="q">The Quaterniond to invert</param>
        /// <returns>The inverse of the given Quaterniond</returns>
        public static Quaterniond Invert(Quaterniond q)
        {
            Quaterniond result;
            Invert(ref q, out result);
            return result;
        }

        /// <summary>
        /// Get the inverse of the given Quaterniond
        /// </summary>
        /// <param name="q">The Quaterniond to invert</param>
        /// <param name="result">The inverse of the given Quaterniond</param>
        public static void Invert(ref Quaterniond q, out Quaterniond result)
        {
            double lengthSq = q.LengthSquared;
            if (lengthSq != 0.0)
            {
                double i = 1.0 / lengthSq;
                result = new Quaterniond(q.X * -i, q.Y * -i, q.Z * -i, q.W * i);
            }
            else
            {
                result = q;
            }
        }

        #endregion

        #region Normalize

        /// <summary>
        /// Scale the given Quaterniond to unit length
        /// </summary>
        /// <param name="q">The Quaterniond to normalize</param>
        /// <returns>The normalized Quaterniond</returns>
        public static Quaterniond Normalize(Quaterniond q)
        {
            Quaterniond result;
            Normalize(ref q, out result);
            return result;
        }

        /// <summary>
        /// Scale the given Quaterniond to unit length
        /// </summary>
        /// <param name="q">The Quaterniond to normalize</param>
        /// <param name="result">The normalized Quaterniond</param>
        public static void Normalize(ref Quaterniond q, out Quaterniond result)
        {
            double scale = 1.0 / q.Length;
            result = new Quaterniond(q.X * scale, q.Y * scale, q.Z * scale, q.W * scale);
        }

        #endregion

        #region FromAxisAngle

        /// <summary>
        /// Build a Quaterniond from the given axis and angle
        /// </summary>
        /// <param name="axis">The axis to rotate about</param>
        /// <param name="angle">The rotation angle in radians</param>
        /// <returns></returns>
        public static Quaterniond FromAxisAngle(Vector3d axis, double angle)
        {
            if (axis.LengthSquared == 0.0)
                return Identity;

            Quaterniond result = Identity;

            angle *= 0.5f;
            axis.Normalize();
            var vector = axis * (double)System.Math.Sin(angle);
            result.X = vector.X;
            result.Y = vector.Y;
            result.Z = vector.Z;
            result.W = (double)System.Math.Cos(angle);

            return Normalize(result);
        }

        #endregion

        #region Slerp

        /// <summary>
        /// Do Spherical linear interpolation between two Quaternionds 
        /// </summary>
        /// <param name="q1">The first Quaterniond</param>
        /// <param name="q2">The second Quaterniond</param>
        /// <param name="blend">The blend factor</param>
        /// <returns>A smooth blend between the given Quaternionds</returns>
        public static Quaterniond Slerp(Quaterniond q1, Quaterniond q2, double blend)
        {
            // if either input is zero, return the other.
            if (q1.LengthSquared == 0.0)
            {
                if (q2.LengthSquared == 0.0)
                {
                    return Identity;
                }
                return q2;
            }
            else if (q2.LengthSquared == 0.0)
            {
                return q1;
            }


            double cosHalfAngle = q1.W * q2.W + q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z;

            if (cosHalfAngle >= 1.0 || cosHalfAngle <= -1.0)
            {
                // angle = 0.0, so just return one input.
                return q1;
            }
            else if (cosHalfAngle < 0.0)
            {
                q2.X = -q2.X;
                q2.Y = -q2.Y;
                q2.Z = -q2.Z;
                q2.W = -q2.W;
                cosHalfAngle = -cosHalfAngle;
            }

            double blendA;
            double blendB;
            if (cosHalfAngle < 0.99f)
            {
                // do proper slerp for big angles
                double halfAngle = (double)System.Math.Acos(cosHalfAngle);
                double sinHalfAngle = (double)System.Math.Sin(halfAngle);
                double oneOverSinHalfAngle = 1.0 / sinHalfAngle;
                blendA = (double)System.Math.Sin(halfAngle * (1.0 - blend)) * oneOverSinHalfAngle;
                blendB = (double)System.Math.Sin(halfAngle * blend) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0 - blend;
                blendB = blend;
            }

            Quaterniond result = new Quaterniond(blendA * q1.X + blendB * q2.X, blendA * q1.Y + blendB * q2.Y, blendA * q1.Z + blendB * q2.Z, blendA * q1.W + blendB * q2.W);
            if (result.LengthSquared > 0.0)
                return Normalize(result);
            else
                return Identity;
        }

        #endregion

        #endregion

        #region Operators

        /// <summary>
        /// Adds two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaterniond operator +(Quaterniond left, Quaterniond right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            left.W += right.W;
            return left;
        }

        /// <summary>
        /// Subtracts two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaterniond operator -(Quaterniond left, Quaterniond right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            left.W -= right.W;
            return left;
        }

        /// <summary>
        /// Multiplies two instances.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaterniond operator *(Quaterniond left, Quaterniond right)
        {
            Multiply(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="Quaterniond">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>A new instance containing the result of the calculation.</returns>
        public static Quaterniond operator *(Quaterniond Quaterniond, double scale)
        {
            Multiply(ref Quaterniond, scale, out Quaterniond);
            return Quaterniond;
        }

        /// <summary>
        /// Multiplies an instance by a scalar.
        /// </summary>
        /// <param name="Quaterniond">The instance.</param>
        /// <param name="scale">The scalar.</param>
        /// <returns>A new instance containing the result of the calculation.</returns>
        public static Quaterniond operator *(double scale, Quaterniond Quaterniond)
        {
            return new Quaterniond(Quaterniond.X * scale, Quaterniond.Y * scale, Quaterniond.Z * scale, Quaterniond.W * scale);
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left equals right; false otherwise.</returns>
        public static bool operator ==(Quaterniond left, Quaterniond right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The first instance.</param>
        /// <param name="right">The second instance.</param>
        /// <returns>True, if left does not equal right; false otherwise.</returns>
        public static bool operator !=(Quaterniond left, Quaterniond right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Overrides

        #region public override string ToString()

        /// <summary>
        /// Returns a System.String that represents the current Quaterniond.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}, W: {3}", X, Y, Z, W);
        }

        #endregion

        #region public override bool Equals (object o)

        /// <summary>
        /// Compares this object instance to another object for equality. 
        /// </summary>
        /// <param name="other">The other object to be used in the comparison.</param>
        /// <returns>True if both objects are Quaternionds of equal value. Otherwise it returns false.</returns>
        public override bool Equals(object other)
        {
            if (other is Quaterniond == false) return false;
            return this == (Quaterniond)other;
        }

        #endregion

        #region public override int GetHashCode ()

        /// <summary>
        /// Provides the hash code for this object. 
        /// </summary>
        /// <returns>A hash code formed from the bitwise XOR of this objects members.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
        }

        #endregion

        #endregion

        #endregion

        #region IEquatable<Quaterniond> Members

        /// <summary>
        /// Compares this Quaterniond instance to another Quaterniond for equality. 
        /// </summary>
        /// <param name="other">The other Quaterniond to be used in the comparison.</param>
        /// <returns>True if both instances are equal; false otherwise.</returns>
        public bool Equals(Quaterniond other)
        {
            double tolerance = 0.00001;
            return Math.Abs(X - other.X) < tolerance &&
                Math.Abs(Y - other.Y) < tolerance &&
                Math.Abs(Z - other.Z) < tolerance &&
                Math.Abs(W - other.W) < tolerance;
        }

        #endregion
    }
}
