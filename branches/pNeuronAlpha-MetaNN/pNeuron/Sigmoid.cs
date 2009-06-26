using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron.Core
{
    /// <summary>
    /// Math utility methods.
    /// </summary>
    public static class SigmoidUtils
    {
        public static double DerivativeSigmoid(double value)
        {
            return value * (1.0F - value);
        }

        public static double Sigmoid(double value)
        {
            return (1 / (1 + Math.Exp(-value)));
        }

        public static double UnSigmoid(double value)
        {
            return -Math.Log((1.0d / value - 1.0d), Math.E);

        }

        /// <summary>
        /// Low precision, high performance magic.
        /// See http://martin.ankerl.com/2007/10/04/optimized-pow-approximation-for-java-and-c-c/
        /// and http://citeseer.ist.psu.edu/lazzaro99jpeg.html
        /// and http://citeseer.ist.psu.edu/schraudolph98fast.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Pow(double a, double b)
        {
            int x = (int)(BitConverter.DoubleToInt64Bits(a) >> 32);
            int y = (int)(b * (x - 1072632447) + 1072632447);
            return BitConverter.Int64BitsToDouble(((long)y) << 32);
        }

        /// <summary>
        /// More magic.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double Exp(double val)
        {
            long tmp = (long)(1512775 * val + (1072693248 - 60801));
            return BitConverter.Int64BitsToDouble(tmp << 32);
        }

    }
}
