using System.Data;
using System.Text;

namespace ReallyBadMath
{
    public class BadMathInt{
        static protected readonly BadMathInt ONE = new(1);
        static protected readonly BadMathInt ZERO = new(0);

        protected readonly int data;

        public BadMathInt(int val){
            data = val;
        }

        public static explicit operator int(BadMathInt b) => b.data;
        public static explicit operator BadMathInt(int i) => new(i);

        public static BadMathInt operator +(BadMathInt b1, BadMathInt b2){
            if((int)b2 == 0) return b1;
            else return ++b1 + --b2;
        }

        public static BadMathInt operator -(BadMathInt b1, BadMathInt b2){
            if((int)b2 == 0) return b1;
            if((int)b2 == 1) return --b1;
            else return b1 - (--b2);
        }

        public static BadMathInt operator *(BadMathInt b1, BadMathInt b2){
            if((int)b2 == 0) return ZERO;
            else if ((int)b2 == 1) return b1;
            else return (--b2 * b1) + b1;
        }

        public static BadMathInt operator /(BadMathInt b1, BadMathInt b2){
            if((int)b2 == 0) throw new DivideByZeroException("Division of " + b1 + " by zero.");
            else if(b1 < b2) return ZERO;
            else if(b1 == b2) return ONE;
            else return (BadMathInt) ((int)((b1 - b2) / b2) + 1);
        }

        public static BadMathInt operator %(BadMathInt b1, BadMathInt b2){
            if((int)b1 == 0) return ZERO;
            else if(b1 < b2) return b1;
            else return (b1 - b2) % b2;
        }

        public static BadMathInt operator -(BadMathInt b) => (BadMathInt)(-b.data);

        public static BadMathInt operator ++(BadMathInt b) => (BadMathInt)((int)b +1);
        public static BadMathInt operator --(BadMathInt b) => (BadMathInt)((int)b -1);

        public static bool operator ==(BadMathInt b1, BadMathInt b2){
            if((int)b1 < 0 && (int)b2 < 0) return -b1 == -b2;
            else if((int)b1 == 0 && (int)b2 != 0 || (int)b2 == 0 && (int)b1 != 0) return false;
            else if((int)b1 == 0 && (int)b2 == 0) return true;
            else return --b1 == --b2;
        }

        public static bool operator !=(BadMathInt b1, BadMathInt b2){
            if((int)b1 < 0 && (int)b2 < 0) return -b1 != -b2;
            else if((int)b1 == 0 && (int)b2 == 0) return false;
            else if((int)b1 == 0 && (int)b2 != 0 || (int)b2 == 0 && (int)b1 != 0) return true;
            else return --b1 != --b2;
        }

        public static bool operator <(BadMathInt b1, BadMathInt b2){
            if (b1 == b2) return false;
            else if ((int)b1 < 0 && (int)b2 < 0) return !(-b1 < -b2);
            else if ((int)b1 == 0 && (int)b2 > 0) return true;
            else if ((int)b2 == 0 && (int)b1 > 0) return false;
            else return --b1 < --b2;
        }

        public static bool operator >(BadMathInt b1, BadMathInt b2){
            if (b1 == b2) return false;
            else if ((int)b1 < 0 && (int)b2 < 0) return !(-b1 > -b2);
            else if ((int)b1 == 0 && (int)b2 > 0) return false;
            else if ((int)b2 == 0 && (int)b1 > 0) return true;
            else return --b1 > --b2;
        }

        public static bool operator >=(BadMathInt b1, BadMathInt b2){
            return b1 > b2 || b1 == b2;
        }

        public static bool operator <=(BadMathInt b1, BadMathInt b2){
            return b1 < b2 || b1 == b2;
        }

        public override string ToString() => data.ToString();
    }

    public class BadMathFloat{
        protected readonly int data;
        public readonly short decimals;
        protected readonly byte sigZeros;

        public BadMathFloat(double val)
        {
            data = (int)val;
            decimals = Trunc(val);
            sigZeros = SigCount(decimals);
            //sigZeros = SigZeros(val);
        }

        private BadMathFloat(int data, short decimals, byte sigZeros)
        {
            this.data = data;
            this.decimals = decimals;
            this.sigZeros = sigZeros;
        }

        public static explicit operator double(BadMathFloat val)
        {
            double value = val.data;
            value += val.decimals / Math.Pow(10, 4);
            return value;
        }
        public static explicit operator BadMathFloat(double val) => new(val);

        public static byte SigCount(short val)
        {
            if(val > 999)
            {
                return 0;
            }
            if (val > 99)
            {
                return 1;
            }
            if (val > 9)
            {
                return 2;
            }
            if (val > 0)
            {
                return 3;
            }
            return 4;
        }

        private short Trunc(double val)
        {
            val -= (int)val;
            val *= Math.Pow(10, 4);
            return (short)(val + 0.5);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append(data);
            sb.Append('.');
            if (decimals == 0)
            {
                return sb.Append('0').ToString();
            }
            sb.Append('0', sigZeros);
            sb.Append(decimals);
            return sb.ToString().TrimEnd('0');
        }

        public static BadMathFloat operator +(BadMathFloat a, BadMathFloat b)
        {
            int rData = a.data, cData = b.data;
            short rDecimals = a.decimals, cDecimals = b.decimals;
            byte rSigZeros;

            while(cDecimals > 0 || cData > 0)
            {
                if(cData > 0)
                {
                    cData--; rData++;
                }

                if(cDecimals > 0)
                {
                    cDecimals--; rDecimals++;
                }
            }

            rSigZeros = SigCount(rDecimals);

            return new BadMathFloat(rData, rDecimals, rSigZeros);
        }
    }

    public static class BadMathFunc{
        public static BadMathInt Pow(BadMathInt b, BadMathInt exp){
            if((int)exp == 0) return (BadMathInt) 1;
            else if((int)exp == 1) return b;
            else return Pow(b, --exp) * b;
        }

        public static BadMathInt AbsVal(BadMathInt b){
            return ((int)b > 0)? b : -b;
        }
    }
}