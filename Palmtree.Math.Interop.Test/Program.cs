using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palmtree.Math;

namespace Palmtree.Math.Interop.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var xxxx = -BigInteger.One;
            var yyyy = xxxx.ToByteArray();

            if (!BigInteger.Zero.ToUBigInt().IsZero)
                throw new ApplicationException();
            if (!BigInteger.Zero.ToBigInt().IsZero)
                throw new ApplicationException();
            if (!UBigInt.Zero.ToBigInteger().IsZero)
                throw new ApplicationException();
            if (!BigInt.Zero.ToBigInteger().IsZero)
                throw new ApplicationException();

            Test(0x40, 0);
            Test(0x40, 1);
            Test(0x40, 2);

            Test(0x80, 0xff - 8);
            Test(0x80, 0xff - 7);
            Test(0x80, 0xff - 6);

            Test(0x80, 0xffff - 8);
            Test(0x80, 0xffff - 7);
            Test(0x80, 0xffff - 6);

            Test(0x80, 0x0007ffff - 8);
            Test(0x80, 0x0007ffff - 7);
            Test(0x80, 0x0007ffff - 6);

            Console.WriteLine();
        }

        private static void Test(UInt32 x, int bit_count)
        {
            var value_biginteger1 = x * (BigInteger.One << bit_count);
            var value_biginteger2 = -(Int64)x * (BigInteger.One << bit_count);
            var value_ubigint = x * (UBigInt.One << bit_count);
            var value_bigint1 = x * (BigInt.One << bit_count);
            var value_bigint2 = -(Int64)x * (BigInt.One << bit_count);

            if (value_biginteger1.ToUBigInt().ToString("D") != value_bigint1.ToString("D"))
                throw new ApplicationException();
            if (value_biginteger1.ToBigInt().ToString("D") != value_bigint1.ToString("D"))
                throw new ApplicationException();
            if (value_biginteger2.ToBigInt().ToString("D") != value_bigint2.ToString("D"))
                throw new ApplicationException();

            if (value_ubigint.ToBigInteger().ToString("D") != value_ubigint.ToString("D"))
                throw new ApplicationException();
            if (value_bigint1.ToBigInteger().ToString("D") != value_bigint1.ToString("D"))
                throw new ApplicationException();
            if (value_bigint2.ToBigInteger().ToString("D") != value_bigint2.ToString("D"))
                throw new ApplicationException();
        }
    }
}
