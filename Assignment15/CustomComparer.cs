using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment15
{
    internal class CustomComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if(x == null) return y == null;
            char[] charArr = x.ToCharArray();
            Array.Sort(charArr);
            string s1 = new string(charArr);

            charArr = y.ToCharArray();
            Array.Sort(charArr);
            string s2 = new string(charArr);

            return s1.Equals(s2);
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            string s = obj.OrderBy(c => c).ToString() ?? "";
            return s.GetHashCode();
        }
    }
}
