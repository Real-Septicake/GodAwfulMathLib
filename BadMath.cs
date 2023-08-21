namespace ReallyBadMath
{
    public class BadMathInt
    {
        protected readonly int data;

        public BadMathInt(int val){
            data = val;
        }

        public static implicit operator int(BadMathInt b) => b.data;
        public static explicit operator BadMathInt(int i) => new(i);

        public static BadMathInt operator +(BadMathInt b1, BadMathInt b2){
            if(b2 == 0) return b1;
            else return ++b1 + --b2;
        }

        public static BadMathInt operator -(BadMathInt b1, BadMathInt b2){
            if(b2 == 0) return b1;
            else return (BadMathInt) (b1 - (int) (--b2));
        }

        public static BadMathInt operator *(BadMathInt b1, BadMathInt b2){
            if(b2 == 0) return (BadMathInt) 0;
            else if (b2 == 1) return b1;
            else return (--b2 * b1) + b1;
        }

        public static BadMathInt operator /(BadMathInt b1, BadMathInt b2){
            if(b2 == 0) throw new DivideByZeroException("Division of " + b1 + " by zero.");
            else if(b1 < b2) return (BadMathInt) 0;
            else if(b1 == b2) return (BadMathInt) 1;
            else return (BadMathInt) (1 + ((b1 - b2) / b2));
        }

        public static BadMathInt operator %(BadMathInt b1, BadMathInt b2){
            if(b1 == 0) return (BadMathInt) 0;
            else if(b1 < b2) return b1;
            else return (b1 - b2) % b2;
        }

        public static BadMathInt operator -(BadMathInt b) => (BadMathInt)(-b.data);

        public static BadMathInt operator ++(BadMathInt b) => (BadMathInt)(b+1);
        public static BadMathInt operator --(BadMathInt b) => (BadMathInt)(b-1);

        public static bool operator ==(BadMathInt b1, BadMathInt b2){
            if(b1 < 0 && b2 < 0) return -b1 == -b2;
            else if(b1 == 0 && b2 != 0 || b2 == 0 && b1 != 0) return false;
            else if(b1 == 0 && b2 == 0) return true;
            else return --b1 == --b2;
        }

        public static bool operator !=(BadMathInt b1, BadMathInt b2){
            if(b1 < 0 && b2 < 0) return -b1 != -b2;
            else if(b1 == 0 && b2 == 0) return false;
            else if(b1 == 0 && b2 != 0 || b2 == 0 && b1 != 0) return true;
            else return --b1 != --b2;
        }

        public static bool operator <(BadMathInt b1, BadMathInt b2){
            if (b1 == b2) return false;
            else if (b1 < 0 && b2 < 0) return !(-b1 < -b2);
            else if (b1 == 0 && b2 > 0) return true;
            else if (b2 == 0 && b1 > 0) return false;
            else return --b1 < --b2;
        }

        public static bool operator >(BadMathInt b1, BadMathInt b2){
            if (b1 == b2) return false;
            else if (b1 < 0 && b2 < 0) return !(-b1 > -b2);
            else if (b1 == 0 && b2 > 0) return false;
            else if (b2 == 0 && b1 > 0) return true;
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

    public static class BadMathFunc{
        public static BadMathInt Pow(BadMathInt b, BadMathInt exp){
            if(exp == 0) return (BadMathInt) 1;
            else if(exp == 1) return b;
            else return Pow(b, --exp) * b;
        }

        public static BadMathInt AbsVal(BadMathInt b){
            return (b > 0)? b : -b;
        }
    }
}