using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataConverter.Helpers
{
    public class EnumTypeConverter : EnumConverter
    {
        #region Fields

        private const string EnumSeparator = ", ";
        private static readonly string[] EnumSeperatorArray = new string[] { EnumSeparator, };

        #endregion Fields

        #region Constructor(s)

        public EnumTypeConverter(Type type) : base(type) { }

        #endregion Constructor(s)

        #region Methods

        private static string GetMemberText(MemberInfo field, object value, CultureInfo culture)
        {
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (attribute != null && !String.IsNullOrEmpty(attribute.Description))
            {
                return attribute.Description;
            }//if
            return Convert.ToString(value, culture);
        }

        private string GetValueText(object value, CultureInfo culture)
        {
            var name = Enum.GetName(EnumType, value);
            if (String.IsNullOrEmpty(name))
            {
                const string messageFormat = "Name for value \"{0}\" does not found in enum \"{1}\".";
                var message = String.Format(messageFormat, value, EnumType);
                throw new ArgumentException(message, "value");
            }//if

            var field = EnumType.GetField(name, BindingFlags.Static | BindingFlags.Public);
            if (field == null)
            {
                const string messageFormat = "Field \"{0}\" for value \"{1}\" does not found in enum \"{2}\".";
                var message = String.Format(messageFormat, name, value, EnumType);
                throw new InvalidOperationException(message);
            }//if

            Debug.Assert(Equals(field.GetValue(null), value), String.Format("Equals(field.GetValue(null) = \"{0}\", value = \"{1}\")", field.GetValue(null), value));
            return GetMemberText(field, value, culture);
        }

        private string GetObjectText(object value, CultureInfo culture)
        {
            var temp = Enum.ToObject(EnumType, value);
            return GetValueText(temp, culture);
        }

        private string GetFlagsString(object value, CultureInfo culture)
        {
            // See Enum::InternalFlagsFormat(Type, object) for algorithm details at
            // http://www.koders.com/csharp/fid1915D89166617B48892C5574838EA09656710B25.aspx?s=InternalFlagsFormat#L223
            var initial = Convert.ToUInt64(value);
            var values = Enum.GetValues(EnumType)
                          .Cast<object>()
                          .Select(item => Convert.ToUInt64(item, culture))
                          .ToArray();
            if (initial == 0)
            {
                if (values.Length > 0 && values[0] == 0)
                {
                    return GetObjectText(values[0], culture);
                }
                else
                {
                    return Convert.ToString(0, culture);
                }//if
            }//if

            var rest = initial;
            var builder = new StringBuilder();
            var empty = true; // builder is empty

            for (var index = values.Length - 1; index >= 0 && values[index] != 0; index--)
            {
                var current = values[index];
                if ((rest & current) == current)
                {
                    rest -= current;
                    if (!empty)
                    {
                        builder.Append(EnumSeparator);
                    }
                    else
                    {
                        empty = false;
                    }//if

                    var text = GetObjectText(current, culture);
                    builder.Append(text);
                }//if
            }//for

            if (rest != 0)
            {
                return Convert.ToString(initial, culture);
            }//if

            return builder.ToString();
        }

        private static IDictionary<string, object> GetValuesDictionary(Type type, CultureInfo culture)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }//if

            const BindingFlags flags = BindingFlags.Static | BindingFlags.Public;
            var items = from field in type.GetFields(flags)
                        let value = field.GetValue(null)
                        let text = GetMemberText(field, value, culture)
                        select new
                        {
                            Value = value,
                            Text = text,
                        };
            var comparer = GetCultureAwareComparer(culture);
            return items.ToDictionary(item => item.Text, item => item.Value, comparer);
        }

        private static StringComparer GetCultureAwareComparer(CultureInfo culture)
        {
            if (culture != null && !Equals(culture, CultureInfo.CurrentCulture))
            {
                return new CultureAwareComparer(culture);
            }//if

            return StringComparer.CurrentCulture;
        }

        #endregion Methods

        #region class CultureAwareComparer

        [Serializable]
        private sealed class CultureAwareComparer : StringComparer
        {
            #region Fields

            private readonly CultureInfo _culture;

            #endregion Fields

            #region Constructor(s)

            public CultureAwareComparer(CultureInfo culture)
            {
                if (culture == null)
                {
                    throw new ArgumentNullException("culture");
                }//if

                this._culture = culture;
            }

            #endregion Constructor(s)

            #region Properties

            private CultureInfo Culture
            {
                [DebuggerStepThrough]
                get { return _culture; }
            }

            #endregion Properties

            #region Overrides

            public override int Compare(string x, string y)
            {
                return Culture.CompareInfo.Compare(x, y);
            }

            public override bool Equals(string x, string y)
            {
                return Compare(x, y) == 0;
            }

            public override int GetHashCode(string obj)
            {
                var key = Culture.CompareInfo.GetSortKey(obj);
                if (key == null)
                {
                    var messageFormat = "CompareInfo::GetSortKey({0}) returns \"null\" reference.";
                    var message = String.Format(messageFormat, obj ?? "<null>");
                    throw new InvalidOperationException(message);
                }//if
                return key.GetHashCode();
            }

            #endregion Overrides
        }

        #endregion class CultureAwareComparer

        #region Overrides

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (Enum.IsDefined(EnumType, value))
                {
                    return GetValueText(value, culture);
                }
                else if (EnumType.IsDefined(typeof(FlagsAttribute), false))
                {
                    return GetFlagsString(value, culture);
                }//if
            }//if
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var text = value as string;
            if (text != null)
            {
                var dictionary = GetValuesDictionary(EnumType, culture);
                Func<string, object> parse = str =>
                {
                    object found;
                    if (!dictionary.TryGetValue(str, out found))
                    {
                        const string messageFormat = "Can not parse value \"{0}\" for enum {1}.";
                        var message = String.Format(messageFormat, str, EnumType);
                        throw new ArgumentException(message, "value");
                    }//if
                    return found;
                };

                if (text.IndexOf(EnumSeparator) == -1)
                {
                    return parse(text);
                }
                else if (EnumType.IsDefined(typeof(FlagsAttribute), false))
                {
                    var items = text.Split(EnumSeperatorArray, StringSplitOptions.None);
                    var result = 0L;
                    foreach (var item in items)
                    {
                        var found = parse(item);
                        result |= Convert.ToInt64(found);
                    }//for
                    return Enum.ToObject(EnumType, result);
                }//if
            }//if

            return base.ConvertFrom(context, culture, value);
        }

        #endregion Overrides
    }
}
