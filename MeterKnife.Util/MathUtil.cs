using System;
using System.Globalization;
using MathNet.Numerics;

namespace MeterKnife.Util
{
    public class MathUtil
    {
        public static string Polynomial1(double[] arrayX, double[] arrayY, out double[] result)
        {
            result = Fit.Polynomial(arrayX, arrayY, 1);
            return Polynomial1(result);
        }

        public static string Polynomial1(double[] v)
        {
            var a = v[1];
            var b = v[0];

            var aa = a >= 0 ? "" : "-";
            var ab = b >= 0 ? "+" : "-";

            var A = GetDoubleFormat(a);
            var B = GetDoubleFormat(b);

            var polynomial = string.Format("Y ={2} {0} t {3} {1}", A, B, aa, ab);
            return polynomial;
        }

        public static string Polynomial2(double[] arrayX, double[] arrayY, out double[] result)
        {
            result = Fit.Polynomial(arrayX, arrayY, 2);
            return Polynomial2(result);
        }

        public static string Polynomial2(double[] v)
        {
            var a = v[2];
            var b = v[1];
            var c = v[0];

            var aa = a >= 0 ? "" : "-";
            var ab = b >= 0 ? "+" : "-";
            var bc = c >= 0 ? "+" : "-";

            var A = GetDoubleFormat(a);
            var B = GetDoubleFormat(b);
            var C = GetDoubleFormat(c);

            var polynomial = string.Format("Y ={3} {0} t^2 {4} {1} t {5} {2}", A, B, C, aa, ab, bc);
            return polynomial;
        }

        public static string GetDoubleFormat(double value)
        {
            return string.Format("({0})", Math.Abs(value).ToString("e4", CultureInfo.InvariantCulture));
//            if (Math.Abs(value) >= 1)
//                return string.Format("({0})", Math.Abs(value).ToString("e4", CultureInfo.InvariantCulture));
//            else 
//                return Math.Abs(value).ToString("0.0000").TrimZero();
        }
    }
}
