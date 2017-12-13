using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataConverter.Helpers
{
    public enum StringPadDirection
    {
        Left,
        Right
    }

    public static class StringHelper
    {
        public static readonly char[] Whitespaces = {' ', '\t'};
        public static readonly string DecSep = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

        public static readonly string Comma = ",";
        public static readonly string Point = ".";

        public static readonly char[] DefaultWasteChars = {' ', '\t', '-', '_', ',', '.', ';'};

        #region Format

        public static string AsFormat(this string template, params object[] args)
        {
            return string.Format(template, args);
        }

        #endregion

        #region Tokenize

        public static string[] GetTokens(this string sLine, char[] separator)
        {
            return sLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetTokens(this string sLine)
        {
            return GetTokens(sLine, Whitespaces);
        }

        public static string[] GetTokens(this string sLine, bool convertDecSep)
        {
            return GetTokens(sLine, Whitespaces, convertDecSep);
        }

        public static string[] GetTokens(this string sLine, char[] separator, bool convertDecSep)
        {
            if (convertDecSep)
            {
                sLine = DecSepToSys(sLine);
            }
            return GetTokens(sLine, separator);
        }

        #endregion

        #region Convert

        public static string DecSepToSys(this string s, string sep)
        {
            return s.Replace(sep, DecSep);
        }

        public static string DecSepToSys(this string s)
        {
            s = s.Replace(Point, DecSep);
            s = s.Replace(Comma, DecSep);
            return s;
        }

        public static string SysSepToDec(this string s)
        {
            return SysSepToDec(s, Point);
        }

        public static string SysSepToDec(this string s, string sep)
        {
            return s.Replace(DecSep, sep);
        }

        public static double StrToDouble(this string s)
        {
            return double.Parse(DecSepToSys(s));
        }

        public static bool StrToBoolean(this string s)
        {
            return bool.Parse(s);
        }

        public static int StrToInt(this string s)
        {
            return int.Parse(s);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string DoubleToStr(this double value)
        {
            return SysSepToDec(value.ToString());
        }

        public static string DoubleToStr(this double value, string format)
        {
            return SysSepToDec(value.ToString(format));
        }

        public static string DoubleToStr(this double value, StringPadDirection direction, int pad)
        {
            string s = SysSepToDec(value.ToString());
            switch (direction)
            {
                case StringPadDirection.Left:
                    return PadLeft(s, pad);
                case StringPadDirection.Right:
                    return PadRight(s, pad);
                default:
                    return s;
            }
        }

        #endregion

        #region Padding

        public static string StrLineRightPad(int pad, params string[] str)
        {
            var sbOut = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (pad > str[i].Length)
                    sbOut.Append(str[i].PadRight(pad));
                else
                    sbOut.Append(str[i] + "  ");
            }
            return sbOut.ToString();
        }

        public static string StrLineRightPad(int pad, bool changeSysToDec, params double[] array)
        {
            string[] str = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                str[i] = array[i].ToString();
                if (changeSysToDec)
                    str[i] = SysSepToDec(str[i]);
            }
            return StrLineRightPad(pad, str);
        }

        public static string StrLineRightPad(int pad, bool changeSysToDec, bool trimEnd, params double[] array)
        {
            string s = StrLineRightPad(pad, changeSysToDec, array);
            if (trimEnd) s = s.TrimEnd(null);
            return s;
        }

        public static string PadRight(this string s, int pad)
        {
            if (s.Length < pad)
                return s.PadRight(pad);
            else
                return s + " ";
        }

        public static string PadLeft(this string s, int pad)
        {
            if (s.Length < pad)
                return s.PadLeft(pad);
            else
                return " " + s;
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Грубое сравнение строк.
        /// Не учитываются ведущие и завершающие пробелы, а так же заданные символы.
        /// Буква Ё(ё) считается равной букве Е(е).
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <param name="otherStr">Строка для сравнения</param>
        /// <param name="wasteChars">Символы, которые не будут учитываться</param>
        /// <returns>Булевый результат сравнения</returns>
        public static bool RoughCompareTo(this string s, string otherStr, char[] wasteChars = null)
        {
            if (wasteChars == null) wasteChars = DefaultWasteChars;

            s = RemoveWasteChars(s.Trim(), wasteChars);
            s = ReplaceCharsForRoughCompareTo(s);

            otherStr = RemoveWasteChars(otherStr.Trim(), wasteChars);
            otherStr = ReplaceCharsForRoughCompareTo(otherStr);

            return s.Equals(otherStr, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Заменяет жестко заданные строки для грубого сравнения
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <returns>Результирующая строка</returns>
        private static string ReplaceCharsForRoughCompareTo(string s)
        {
            var sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == 'ё') sb[i] = 'е';
                if (sb[i] == 'Ё') sb[i] = 'Е';
            }
            return sb.ToString();
        }

        /// <summary>
        /// Убирает заданные символы из строки
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <param name="wasteChars">Массив символов, которые надо убрать из строки</param>
        /// <returns>Результирующая строка</returns>
        private static string RemoveWasteChars(string s, char[] wasteChars)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                var si = s[i];

                bool isFound = false;
                for (int j = 0; j < wasteChars.Length; j++)
                {
                    if (si == wasteChars[j])
                    {
                        isFound = true;
                        break;
                    }
                }
                if (isFound) continue;

                sb.Append(si);
            }

            return sb.ToString();
        }

        #endregion

        #region Unique names

        public static IList<string> GenerateUniqueNames(IList<string> names)
        {
            var resultNames = new List<string>(names);

            for (int i = 0; i < resultNames.Count - 1; i++)
            {
                for (int j = i + 1; j < resultNames.Count; j++)
                {
                    if (resultNames[i] == resultNames[j])
                    {
                        resultNames[j] = GenerateUniqueName(resultNames, resultNames[j]);
                    }
                }
            }

            return resultNames;
        }

        public static string GenerateUniqueName(IEnumerable<string> names, string candidate)
        {
            var set = new HashSet<string>(names);
            if (!set.Contains(candidate)) return candidate;

            var result = string.Empty;
            var tuple = DivideNameByDigit(candidate);
            var s1 = tuple.Item1;
            var s2 = tuple.Item2;

            if (s2.Length == 0) s2 = "0";
            var n2 = int.Parse(s2);

            var i = 1;
            while (i < 50) // ставим ограничение на 50 проверок, чтобы не зациклиться
            {
                result = s1 + (n2 + i);
                if (!set.Contains(result)) break;
                i++;
            }

            return result;
        }

        internal static Tuple<string, string> DivideNameByDigit(string name)
        {
            if (name == null) return null;

            var k = 0;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(name, i))
                {
                    k = i;
                    break;
                }
            }

            var s1 = string.Empty;
            var s2 = string.Empty;
            if (k > 0 && k < name.Length - 1)
            {
                s1 = name.Substring(0, k + 1);
                s2 = name.Substring(k + 1);
            }
            else if (k == 0)
            {
                s2 = name;
            }
            else
            {
                s1 = name;
            }

            var tuple = new Tuple<string, string>(s1, s2);
            return tuple;
        }

        #endregion

        public static string ToStr(this object obj)
        {
            return obj == null ? "null" : obj.ToString();
        }

        public static bool Contains(this string source, string toCheck, StringComparison comparison)
        {
            return source.IndexOf(toCheck, comparison) >= 0;
        }
    }
}