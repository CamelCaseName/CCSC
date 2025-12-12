using Silk.NET.Core.Native;
using Silk.NET.Direct2D;
using Silk.NET.DXGI;
using Silk.NET.Maths;
using System.Numerics;
using Rectangle = System.Drawing.Rectangle;

namespace CCSC.Direct2D
{
    public unsafe static class D2DExtension
    {
        public static D3Dcolorvalue ToD3D(this Color color)
        {
            D3Dcolorvalue val = new(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
            return val;
        }

        public static Box2D<float> ToBox<T>(this Rectangle rectangle) where T : struct, IComparable, IComparable<float>, IConvertible, IEquatable<float>, IFormattable, IParsable<float>, ISpanFormattable, ISpanParsable<float>, IUtf8SpanFormattable, IUtf8SpanParsable<float>, IAdditionOperators<float, float, float>, IAdditiveIdentity<float, float>, IBinaryFloatingPointIeee754<float>, IBinaryNumber<float>, IBitwiseOperators<float, float, float>, IComparisonOperators<float, float, bool>, IEqualityOperators<float, float, bool>, IDecrementOperators<float>, IDivisionOperators<float, float, float>, IIncrementOperators<float>, IModulusOperators<float, float, float>, IMultiplicativeIdentity<float, float>, IMultiplyOperators<float, float, float>, INumber<float>, INumberBase<float>, ISubtractionOperators<float, float, float>, IUnaryNegationOperators<float, float>, IUnaryPlusOperators<float, float>, IExponentialFunctions<float>, IFloatingPointConstants<float>, IFloatingPoint<float>, ISignedNumber<float>, IFloatingPointIeee754<float>, IHyperbolicFunctions<float>, ILogarithmicFunctions<float>, IPowerFunctions<float>, IRootFunctions<float>, ITrigonometricFunctions<float>, IMinMaxValue<float>
        {
            return new(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public static Box2D<float> ToBox<T>(this RectangleF rectangle) where T : struct, IComparable, IComparable<float>, IConvertible, IEquatable<float>, IFormattable, IParsable<float>, ISpanFormattable, ISpanParsable<float>, IUtf8SpanFormattable, IUtf8SpanParsable<float>, IAdditionOperators<float, float, float>, IAdditiveIdentity<float, float>, IBinaryFloatingPointIeee754<float>, IBinaryNumber<float>, IBitwiseOperators<float, float, float>, IComparisonOperators<float, float, bool>, IEqualityOperators<float, float, bool>, IDecrementOperators<float>, IDivisionOperators<float, float, float>, IIncrementOperators<float>, IModulusOperators<float, float, float>, IMultiplicativeIdentity<float, float>, IMultiplyOperators<float, float, float>, INumber<float>, INumberBase<float>, ISubtractionOperators<float, float, float>, IUnaryNegationOperators<float, float>, IUnaryPlusOperators<float, float>, IExponentialFunctions<float>, IFloatingPointConstants<float>, IFloatingPoint<float>, ISignedNumber<float>, IFloatingPointIeee754<float>, IHyperbolicFunctions<float>, ILogarithmicFunctions<float>, IPowerFunctions<float>, IRootFunctions<float>, ITrigonometricFunctions<float>, IMinMaxValue<float>
        {
            return new(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public static Box2D<int> ToBox(this Rectangle rectangle)
        {
            return new(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public static Box2D<int> ToBox(this RectangleF rectangle)
        {
            return new((int)rectangle.Left, (int)rectangle.Top, (int)rectangle.Right, (int)rectangle.Bottom);
        }

        public static ID2D1Brush* AsBrush(this ComPtr<ID2D1SolidColorBrush> br)
        {
            return (ID2D1Brush*)br.Handle;
        }

        public static ID2D1Brush* AsBrush(this ComPtr<ID2D1LinearGradientBrush> br)
        {
            return (ID2D1Brush*)br.Handle;
        }

        public static Ellipse ToEllipse(this Rectangle rect)
        {
            return new()
            {
                Point = new(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2),
                RadiusX = rect.Width / 2,
                RadiusY = rect.Height / 2
            };
        }

        public static Ellipse ToEllipse(this RectangleF rect)
        {
            return new()
            {
                Point = new(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2),
                RadiusX = rect.Width / 2,
                RadiusY = rect.Height / 2
            };
        }

        public static RoundedRect ToRoundedRect(this RectangleF rect, float radius)
        {
            return new()
            {
                Rect = rect.ToBox<float>(),
                RadiusX = radius,
                RadiusY = radius
            };
        }

        public static RoundedRect ToRoundedRect(this Rectangle rect, float radius)
        {
            return new()
            {
                Rect = rect.ToBox<float>(),
                RadiusX = radius,
                RadiusY = radius
            };
        }

        public static Matrix3X2<float> ToMatrix3X2(this Matrix3x2 matrix)
        {
            return new()
            {
                M11 = matrix.M11,
                M21 = matrix.M21,
                M31 = matrix.M31,
                M12 = matrix.M12,
                M22 = matrix.M22,
                M32 = matrix.M32,
            };
        }

        public static Vector2D<float> ToVec2D(this PointF point)
        {
            return new(point.X, point.Y);
        }
    }
}
