using System.Text;

namespace ToPowSeven
{
    
    public class Base128
    {
        public const int Radix = 128;
        
        public const string Alp =
            "␀␁␂␃␄␅␆␇␈␉␊␋␌␍␎␏␐␑␒␓␔␕␖␗␘␙␚␛␜␝␞␟" +
            "␠" +
            "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~" +
            "␡";
        
        public static char ToChar(int digit) => Alp[digit];
        
        public static int ToDigit(char c)
        {
            int i = Alp.IndexOf(c);
            return i >= 0 ? i : throw new FormatException($"Not founded in alphabet: U+{(int)c:X4}");
        }

        public static bool IsValidChar(char c) => Alp.IndexOf(c) >= 0;

        public static bool IsValidDigit(int digit) => (uint)digit < Radix;
        
        public static List<byte> ToBase128(int[] a)
        {
            List<byte> Base128Data = new List<byte>();

            for (int i = 0; i < a.Length; i++)
            {
                if (IsValidDigit(a.ElementAt(i)))
                {
                    byte b = (byte)a.ElementAt(i);
                    Base128Data.Add(b);
                }
                else
                {
                    throw new ArgumentException($"Base128 limit is 0-127. Number of arrivals: {a[i]}", nameof(a));
                }
            }

            return Base128Data;
        }

        public static string ToString(List<byte> a)
        {
            var Base128String = new StringBuilder(a.Count);

            for (int i = 0; i < a.Count; i++)
            {
                if (IsValidDigit(a.ElementAt(i)))
                {
                    Base128String.Append(Alp[a[i]]);
                }
                else
                {
                    throw new ArgumentOutOfRangeException( nameof(a), $"Base128 limit is 0-127. Number of arrivals: {a[i]}");
                }
            }

            return Base128String.ToString();
        }
    }
}