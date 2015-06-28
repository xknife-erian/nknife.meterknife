/********************************************************************************************
                                  Class LstSquQuadRegr
             A C#  Class for Least Squares Regression for Quadratic Curve Fitting
                                  Alex Etchells  2010    
 ********************************************************************************************/

using System;
using System.Collections;

namespace MeterKnife.Common
{
    public class LstSquQuadRegr1
    {
        /// <summary>
        ///     用最小二乘法拟合二元多次曲线
        ///     例如y=ax+b
        ///     其中MultiLine将返回a，b两个参数。
        ///     a对应MultiLine[1]
        ///     b对应MultiLine[0]
        /// </summary>
        /// <param name="arrX">已知点的x坐标集合</param>
        /// <param name="arrY">已知点的y坐标集合</param>
        /// <param name="length">已知点的个数</param>
        /// <param name="dimension">方程的最高次数</param>
        public double[] MultiLine(double[] arrX, double[] arrY, int length, int dimension) //二元多次线性方程拟合曲线
        {
            int n = dimension + 1; //dimension次方程需要求 dimension+1个 系数
            var guass = new double[n, n + 1]; //高斯矩阵 例如：y=a0+a1*x+a2*x*x
            for (int i = 0; i < n; i++)
            {
                int j;
                for (j = 0; j < n; j++)
                {
                    guass[i, j] = SumArr(arrX, j + i, length);
                }
                guass[i, j] = SumArr(arrX, i, arrY, 1, length);
            }

            return ComputGauss(guass, n);
        }

        private double SumArr(double[] arr, int n, int length) //求数组的元素的n次方的和
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if (Math.Abs(arr[i]) > 0 || n != 0)
                    s = s + Math.Pow(arr[i], n);
                else
                    s = s + 1;
            }
            return s;
        }

        private double SumArr(double[] arr1, int n1, double[] arr2, int n2, int length)
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if ((Math.Abs(arr1[i]) > 0 || n1 != 0) && (Math.Abs(arr2[i]) > 0 || n2 != 0))
                    s = s + Math.Pow(arr1[i], n1)*Math.Pow(arr2[i], n2);
                else
                    s = s + 1;
            }
            return s;
        }

        private double[] ComputGauss(double[,] guass, int n)
        {
            int i, j;
            int k, m;
            double temp;
            double max;
            double s;
            var x = new double[n];

            for (i = 0; i < n; i++) 
                x[i] = 0.0; //初始化

            for (j = 0; j < n; j++)
            {
                max = 0;
                k = j;
                for (i = j; i < n; i++)
                {
                    if (Math.Abs(guass[i, j]) > max)
                    {
                        max = guass[i, j];
                        k = i;
                    }
                }

                if (k != j)
                {
                    for (m = j; m < n + 1; m++)
                    {
                        temp = guass[j, m];
                        guass[j, m] = guass[k, m];
                        guass[k, m] = temp;
                    }
                }

                if (Math.Abs(max) <= 0)
                {
                    // "此线性方程为奇异线性方程" 
                    return x;
                }

                for (i = j + 1; i < n; i++)
                {
                    s = guass[i, j];
                    for (m = j; m < n + 1; m++)
                    {
                        guass[i, m] = guass[i, m] - guass[j, m]*s/(guass[j, j]);
                    }
                }
            } //结束for (j=0;j<n;j++)


            for (i = n - 1; i >= 0; i--)
            {
                s = 0;
                for (j = i + 1; j < n; j++)
                {
                    s = s + guass[i, j]*x[j];
                }

                x[i] = (guass[i, n] - s)/guass[i, i];
            }

            return x;
        } //返回值是函数的系数
    }

    public class LstSquQuadRegr
    {
        /* instance variables */
        private readonly ArrayList pointArray = new ArrayList();
        private int numOfEntries;
        private double[] pointpair;

        /*constructor */

        public LstSquQuadRegr()
        {
            numOfEntries = 0;
            pointpair = new double[2];
        }

        /*instance methods */


        /// <summary>
        ///     add point pairs
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        public void AddPoints(double x, double y)
        {
            pointpair = new double[2];
            numOfEntries += 1;
            pointpair[0] = x;
            pointpair[1] = y;
            pointArray.Add(pointpair);
        }

        /// <summary>
        ///     returns the a term of the equation ax^2 + bx + c
        /// </summary>
        /// <returns>a term</returns>
        public double aTerm()
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            //notation sjk to mean the sum of x_i^j*y_i^k. 
            double s40 = getSx4(); //sum of x^4
            double s30 = getSx3(); //sum of x^3
            double s20 = getSx2(); //sum of x^2
            double s10 = getSx(); //sum of x
            double s00 = numOfEntries; //sum of x^0 * y^0  ie 1 * number of entries

            double s21 = getSx2y(); //sum of x^2*y
            double s11 = getSxy(); //sum of x*y
            double s01 = getSy(); //sum of y

            //a = Da/D
            return (s21*(s20*s00 - s10*s10) - s11*(s30*s00 - s10*s20) + s01*(s30*s10 - s20*s20))
                   /
                   (s40*(s20*s00 - s10*s10) - s30*(s30*s00 - s10*s20) + s20*(s30*s10 - s20*s20));
        }

        /// <summary>
        ///     returns the b term of the equation ax^2 + bx + c
        /// </summary>
        /// <returns>b term</returns>
        public double bTerm()
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            //notation sjk to mean the sum of x_i^j*y_i^k.  
            double s40 = getSx4(); //sum of x^4
            double s30 = getSx3(); //sum of x^3
            double s20 = getSx2(); //sum of x^2
            double s10 = getSx(); //sum of x
            double s00 = numOfEntries; //sum of x^0 * y^0  ie 1 * number of entries

            double s21 = getSx2y(); //sum of x^2*y
            double s11 = getSxy(); //sum of x*y
            double s01 = getSy(); //sum of y

            //b = Db/D
            return (s40*(s11*s00 - s01*s10) - s30*(s21*s00 - s01*s20) + s20*(s21*s10 - s11*s20))
                   /
                   (s40*(s20*s00 - s10*s10) - s30*(s30*s00 - s10*s20) + s20*(s30*s10 - s20*s20));
        }

        /// <summary>
        ///     returns the c term of the equation ax^2 + bx + c
        /// </summary>
        /// <returns>c term</returns>
        public double cTerm()
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            //notation sjk to mean the sum of x_i^j*y_i^k.  
            double s40 = getSx4(); //sum of x^4
            double s30 = getSx3(); //sum of x^3
            double s20 = getSx2(); //sum of x^2
            double s10 = getSx(); //sum of x
            double s00 = numOfEntries; //sum of x^0 * y^0  ie 1 * number of entries

            double s21 = getSx2y(); //sum of x^2*y
            double s11 = getSxy(); //sum of x*y
            double s01 = getSy(); //sum of y

            //c = Dc/D
            return (s40*(s20*s01 - s10*s11) - s30*(s30*s01 - s10*s21) + s20*(s30*s11 - s20*s21))
                   /
                   (s40*(s20*s00 - s10*s10) - s30*(s30*s00 - s10*s20) + s20*(s30*s10 - s20*s20));
        }

        public double rSquare() // get r-squared
        {
            if (numOfEntries < 3)
            {
                throw new InvalidOperationException("Insufficient pairs of co-ordinates");
            }
            // 1 - (total sum of squares / residual sum of squares)
            return 1 - getSSerr()/getSStot();
        }

        /*helper methods*/

        private double getSx() // get sum of x
        {
            double Sx = 0;
            foreach (double[] ppair in pointArray)
            {
                Sx += ppair[0];
            }
            return Sx;
        }

        private double getSy() // get sum of y
        {
            double Sy = 0;
            foreach (double[] ppair in pointArray)
            {
                Sy += ppair[1];
            }
            return Sy;
        }

        private double getSx2() // get sum of x^2
        {
            double Sx2 = 0;
            foreach (double[] ppair in pointArray)
            {
                Sx2 += Math.Pow(ppair[0], 2); // sum of x^2
            }
            return Sx2;
        }

        private double getSx3() // get sum of x^3
        {
            double Sx3 = 0;
            foreach (double[] ppair in pointArray)
            {
                Sx3 += Math.Pow(ppair[0], 3); // sum of x^3
            }
            return Sx3;
        }

        private double getSx4() // get sum of x^4
        {
            double Sx4 = 0;
            foreach (double[] ppair in pointArray)
            {
                Sx4 += Math.Pow(ppair[0], 4); // sum of x^4
            }
            return Sx4;
        }

        private double getSxy() // get sum of x*y
        {
            double Sxy = 0;
            foreach (double[] ppair in pointArray)
            {
                Sxy += ppair[0]*ppair[1]; // sum of x*y
            }
            return Sxy;
        }

        private double getSx2y() // get sum of x^2*y
        {
            double Sx2y = 0;
            foreach (double[] ppair in pointArray)
            {
                Sx2y += Math.Pow(ppair[0], 2)*ppair[1]; // sum of x^2*y
            }
            return Sx2y;
        }

        private double getYMean() // mean value of y
        {
            double y_tot = 0;
            foreach (double[] ppair in pointArray)
            {
                y_tot += ppair[1];
            }
            return y_tot/numOfEntries;
        }

        private double getSStot() // total sum of squares
        {
            //the sum of the squares of the differences between 
            //the measured y values and the mean y value
            double ss_tot = 0;
            foreach (double[] ppair in pointArray)
            {
                ss_tot += Math.Pow(ppair[1] - getYMean(), 2);
            }
            return ss_tot;
        }

        private double getSSerr() // residual sum of squares
        {
            //the sum of the squares of te difference between 
            //the measured y values and the values of y predicted by the equation
            double ss_err = 0;
            foreach (double[] ppair in pointArray)
            {
                ss_err += Math.Pow(ppair[1] - getPredictedY(ppair[0]), 2);
            }
            return ss_err;
        }

        private double getPredictedY(double x)
        {
            //returns value of y predicted by the equation for a given value of x
            return aTerm()*Math.Pow(x, 2) + bTerm()*x + cTerm();
        }
    }
}