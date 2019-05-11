/*
 * The MIT License
 *
 * Copyright 2019 Palmtree Software.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Numerics;

namespace Palmtree.Math
{
    public static class BigIntegerExtensions
    {
        public static UBigInt ToUBigInt(this BigInteger x)
        {
            bool negative;
            var x_array = ToBigInt(x, out negative);
            if (negative)
                throw new OverflowException();
            var value = UBigInt.FromByteArray(x_array);
            return (value);
        }

        public static BigInt ToBigInt(this BigInteger x)
        {
            bool negative;
            var x_array = ToBigInt(x, out negative);
            var value = BigInt.FromByteArray(x_array);
            return (negative ? value - 1 : value);
        }

        public static BigInteger ToBigInteger(this UBigInt x)
        {
            return (ToBigInteger(x.ToByteArray()));
        }

        public static BigInteger ToBigInteger(this BigInt x)
        {
            return (ToBigInteger(x.ToByteArray()));
        }

        private static byte[] ToBigInt(BigInteger x, out bool negative)
        {
            var x_array = x.ToByteArray();
            if (x_array.Length == 1 && x_array[0] == 0)
            {
                negative = false;
                return (new byte[] { 0x00, 0x00 });
            }
            if (x_array[x_array.Length - 1] >= 0x80)
            {
                negative = true;
                for (var index = 0; index < x_array.Length; ++index)
                    x_array[index] = (byte)~x_array[index];
            }
            else
                negative = false;
            if (x_array.Length < 0xff)
            {
                var buffer = new byte[x_array.Length + 1 + 1];
                buffer[0] = negative ? (byte)0x03 : (byte)0x01;
                buffer[1] = (byte)x_array.Length;
                Array.Copy(x_array, 0, buffer, buffer.Length - x_array.Length, x_array.Length);
                return (buffer);
            }
            else if (x_array.Length < 0xffff)
            {
                var buffer = new byte[x_array.Length + 1 + 2];
                buffer[0] = negative ? (byte)0x07 : (byte)0x05;
                buffer[1] = (byte)x_array.Length;
                buffer[2] = (byte)(x_array.Length >> 8);
                Array.Copy(x_array, 0, buffer, buffer.Length - x_array.Length, x_array.Length);
                return (buffer);
            }
            else
            {
                var buffer = new byte[x_array.Length + 1 + 4];
                buffer[0] = negative ? (byte)0x0b : (byte)0x09;
                buffer[1] = (byte)x_array.Length;
                buffer[2] = (byte)(x_array.Length >> 8);
                buffer[3] = (byte)(x_array.Length >> 16);
                buffer[4] = (byte)(x_array.Length >> 24);
                Array.Copy(x_array, 0, buffer, buffer.Length - x_array.Length, x_array.Length);
                return (buffer);
            }
        }

        private static BigInteger ToBigInteger(byte[] x_array)
        {
            var sign_bit = x_array[0] & 0x03;
            var length_bit = x_array[0] & 0x0c;
            int sign;
            switch (sign_bit)
            {
                case 0x00:
                    sign = 0;
                    break;
                case 0x01:
                    sign = 1;
                    break;
                case 0x03:
                    sign = -1;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            if (sign == 0)
                return (BigInteger.Zero);
            byte[] buffer;
            switch (length_bit)
            {
                case 0x00:
                    buffer = new byte[x_array.Length - 1 - 1 + 1];
                    Array.Copy(x_array, x_array.Length - buffer.Length + 1, buffer, 0, buffer.Length - 1);
                    break;
                case 0x04:
                    buffer = new byte[x_array.Length - 1 - 2 + 1];
                    Array.Copy(x_array, x_array.Length - buffer.Length + 1, buffer, 0, buffer.Length - 1);
                    break;
                case 0x08:
                    buffer = new byte[x_array.Length - 1 - 4 + 1];
                    Array.Copy(x_array, x_array.Length - buffer.Length + 1, buffer, 0, buffer.Length - 1);
                    break;
                case 0x0c:
                    throw new OverflowException();
                default:
                    throw new InvalidOperationException();
            }
            buffer[buffer.Length - 1] = 0;
            if (sign >= 0)
                return (new BigInteger(buffer));
            else
            {
                for (var index = 0; index < buffer.Length; ++index)
                    buffer[index] = (byte)~buffer[index];
                return (new BigInteger(buffer) + 1);
            }
        }
    }

}

/*
 * END OF FILE
 */