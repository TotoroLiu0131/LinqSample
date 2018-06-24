using System.Collections.Generic;

namespace LinqTests
{
    internal class BenCompare : IEqualityComparer<ColorBall>
    {
        public bool Equals(ColorBall x, ColorBall y)
        {
            return x.Prize == y.Prize && x.Color == y.Color;
        }

        public int GetHashCode(ColorBall obj)
        {
            return obj.Color.GetHashCode() & obj.Prize.GetHashCode();
        }
    }
}