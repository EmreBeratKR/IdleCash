using System;
using UnityEngine;
using EmreBeratKR.IdleCash.Creator;
using EmreBeratKR.IdleCash.Exceptions;
using System.Text.RegularExpressions;

namespace EmreBeratKR.IdleCash
{
    /// <summary>
    ///     <para>Representation of Idle Game Currencies.</para>
    ///     <a href="https://github.com/EmreBeratKR/IdleCash/blob/main/README.md">Documentation</a>
    /// </summary>
    /// <example>7.15k, 15.99t, 945.3ac</example>
    [Serializable]
    public struct IdleCash : IEquatable<IdleCash>
    {
        private const float ValueTolerance = 0.00001f;
        private const int MaxValue = 1000;
        private const int MinValue = 1;


        /// <summary>
        ///     <para>Shorthand for writing IdleCash(0).</para>
        /// </summary>
        public static IdleCash Zero => new IdleCash(0, FirstType);
        
        /// <summary>
        ///     <para>Shorthand for writing IdleCash(1).</para>
        /// </summary>
        public static IdleCash One => new IdleCash(1, FirstType);
        
        /// <summary>
        ///     <para>The first type of IdleCash</para>
        /// </summary>
        public static string FirstType => IdleCashSettingsSO.FirstType;
        
        /// <summary>
        ///     <para>The last type of IdleCash</para>
        /// </summary>
        public static string LastType => IdleCashSettingsSO.LastType;
        

        /// <summary>
        ///     <para>The simplified copy which its value field is between 1 and 1000.</para>
        /// </summary>
        /// <returns>A simplified copy.</returns>
        /// <seealso cref="Simplify"/>
        public IdleCash Simplified
        {
            get
            {
                var copy = new IdleCash(value, type);
                copy.Simplify();
                return copy;
            }
        }

        /// <summary>
        ///     <para>The real value which it represents.</para>
        /// </summary>
        /// <returns>value * 10^(3 * TypeIndex).</returns>
        /// <seealso cref="value"/>
        public float RealValue => (float) (value * Math.Pow(10, 3 * TypeIndex));
        
        /// <summary>
        ///     <para>The index of its current type.</para>
        /// </summary>
        /// <seealso cref="type"/>
        public int TypeIndex => IdleCashSettingsSO.GetTypeIndex(type);

        
        /// <summary>
        ///     <para>The string representation of its 10's power factor.</para>
        /// </summary>
        /// <seealso cref="TypeIndex"/>
        public string type;
        
        /// <summary>
        ///     <para>The float part.</para>
        /// </summary>
        /// <seealso cref="RealValue"/>
        public float value;
        

        public IdleCash(float value)
        {
            this.value = value;
            this.type = FirstType;
            Simplify();
        }
        
        public IdleCash(float value, string type)
        {
            if (type == null)
            {
                type = FirstType;
            }
            
            if (!IsValidType(type))
            {
                Debug.LogWarning(new IdleCashInvalidTypeException(type));
            }
            
            this.value = value;
            this.type = type;
            Simplify();
        }
        

        /// <summary>
        ///   <para>Linearly interpolates between two IdleCash values.</para>
        /// </summary>
        /// <param name="a">Start value, returned when t = 0.</param>
        /// <param name="b">End value, returned when t = 1.</param>
        /// <param name="t">
        ///     Value used to interpolate between a and b.
        ///     It's clamped between 0 and 1.
        /// </param>
        /// <returns>
        ///   <para>Interpolated IdleCash value.</para>
        /// </returns>
        public static IdleCash Lerp(IdleCash a, IdleCash b, float t)
        {
            var clampedT = Mathf.Clamp01(t);
            return LerpUnclamped(a, b, clampedT);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two IdleCash values.</para>
        /// </summary>
        /// <param name="a">Start value, returned when t = 0.</param>
        /// <param name="b">End value, returned when t = 1.</param>
        /// <param name="t">
        ///     Value used to interpolate between a and b.
        ///     It's not clamped.
        /// </param>
        /// <returns>
        ///   <para>Interpolated IdleCash value.</para>
        /// </returns>
        public static IdleCash LerpUnclamped(IdleCash a, IdleCash b, float t)
        {
            var difference = b - a;
            return a + difference * t;
        }
        
        /// <summary>
        ///   <para>Clamps between two IdleCash values. Like Mathf.Clamp()</para>
        /// </summary>
        /// <param name="value">
        ///     The value to be clamped between a and b.
        /// </param>
        /// <param name="a">Min value.</param>
        /// <param name="b">Max value.</param>
        /// <returns>
        ///   <para>Clamped IdleCash value.</para>
        /// </returns>
        /// <seealso cref="UnityEngine.Mathf.Clamp"/>
        public static IdleCash Clamp(IdleCash value, IdleCash a, IdleCash b)
        {
            if (value > b) return b;

            if (value < a) return a;

            return value;
        }


        /// <summary>
        ///     <para>
        ///         Simplifies itself to keep its value field between 1 and 1000.
        ///     </para>
        /// </summary>
        /// <seealso cref="Simplified"/>
        public void Simplify()
        {
            var isNegative = value < 0;
            value = Mathf.Abs(value);

            while (true)
            {
                if (value >= MaxValue)
                {
                    if (!TryConvertNextType()) break;
                }
                
                else if (value < MinValue)
                {
                    if (!TryConvertPreviousType()) break;
                }

                else
                {
                    break;
                }
            }

            value = (isNegative ? -1f : 1f) * value;
        }
        
        
        public bool Equals(IdleCash other)
        {
            Simplify();
            other.Simplify();
            
            return type == other.type && Mathf.Abs(value - other.value) < ValueTolerance;
        }

        public override bool Equals(object obj)
        {
            return obj is IdleCash other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((type != null ? type.GetHashCode() : 0) * 397) ^ value.GetHashCode();
            }
        }

        public override string ToString()
        {
            Simplify();
            
            return $"{value:0.00}{type}";
        }

        /// <summary>
        /// Parse any string to IdleCash
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <param name="result"><see cref="IdleCash"/> variable</param>
        /// <returns>true if success, false if fails (invalid format)</returns>
        public static bool TryParse(string s, out IdleCash result)
        {
            result = Parse(s);
            return result != Zero && s != "0";
        }

        /// <summary>
        /// Parse string to IdleCash
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <returns><see cref="IdleCash"/> variable if success, returns <see cref="IdleCash.Zero"/> if fails.</returns>
        public static IdleCash Parse(string s)
        {
            bool isValid = IsValidFormat(s, out float valueS, out string typeS);

            if (isValid)
            {
                return new IdleCash(valueS, typeS);
            }
            else
            {
                return Zero;
            }
        }

        private static bool IsValidFormat(string s, out float parsedValue, out string parsedType)
        {
            parsedValue = 0;
            parsedType = string.Empty;

            string pattern = @"^(-)?(0|[1-9]\d*)?(\.\d+)?(?<=\d)(\D+)?$";
            Match regex = Regex.Match(s, pattern);

            if (regex.Success)
            {
                string temp = string.Empty;
                for (int i = 1; i < regex.Groups.Count; i++)
                {
                    // if the last group is NaN, set as parsedType
                    if (i == regex.Groups.Count - 1 && !float.TryParse(regex.Groups[i].Value, out float num))
                    {
                        parsedType = regex.Groups[^1].Value;
                        break;
                    }

                    temp += regex.Groups[i];
                }
                parsedValue = float.Parse(temp);
            }

            return regex.Success;
        }

        public static IdleCash operator +(IdleCash lhs, IdleCash rhs)
        {
            SetThemSameType(ref lhs, ref rhs);

            lhs.value += rhs.value;
            return lhs.Simplified;
        }
        
        public static IdleCash operator -(IdleCash lhs, IdleCash rhs)
        {
            SetThemSameType(ref lhs, ref rhs);

            lhs.value -= rhs.value;
            return lhs.Simplified;
        }

        public static IdleCash operator *(IdleCash lhs, IdleCash rhs)
        {
            var rhsTypeIndex = rhs.TypeIndex;

            for (int i = 0; i < rhsTypeIndex; i++)
            {
                lhs.value *= MaxValue;
                lhs.Simplify();
            }
            
            lhs.value *= rhs.value;
            return lhs.Simplified;
        }
        
        public static IdleCash operator *(IdleCash lhs, float rhs)
        {
            lhs.value *= rhs;
            return lhs.Simplified;
        }
        
        public static IdleCash operator /(IdleCash lhs, IdleCash rhs)
        {
            var rhsTypeIndex = rhs.TypeIndex;

            for (int i = 0; i < rhsTypeIndex; i++)
            {
                lhs.value /= MaxValue;
                lhs.Simplify();
            }
            
            lhs.value /= rhs.value;
            return lhs.Simplified;
        }
        
        public static IdleCash operator /(IdleCash lhs, float rhs)
        {
            lhs.value /= rhs;
            return lhs.Simplified;
        }
        
        public static bool operator ==(IdleCash lhs, IdleCash rhs)
        {
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(IdleCash lhs, IdleCash rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator <(IdleCash lhs, IdleCash rhs)
        {
            lhs.Simplify();
            rhs.Simplify();

            var lhsTypeIndex = lhs.TypeIndex;
            var rhsTypeIndex = rhs.TypeIndex;

            if (lhsTypeIndex == rhsTypeIndex)
            {
                return lhs.value < rhs.value;
            }

            return lhsTypeIndex < rhsTypeIndex;
        }
        
        public static bool operator >(IdleCash lhs, IdleCash rhs)
        {
            lhs.Simplify();
            rhs.Simplify();

            var lhsTypeIndex = lhs.TypeIndex;
            var rhsTypeIndex = rhs.TypeIndex;

            if (lhsTypeIndex == rhsTypeIndex)
            {
                return lhs.value > rhs.value;
            }

            return lhsTypeIndex > rhsTypeIndex;
        }
        
        public static bool operator <=(IdleCash lhs, IdleCash rhs)
        {
            lhs.Simplify();
            rhs.Simplify();

            var lhsTypeIndex = lhs.TypeIndex;
            var rhsTypeIndex = rhs.TypeIndex;

            if (lhsTypeIndex == rhsTypeIndex)
            {
                return lhs.value <= rhs.value;
            }

            return lhsTypeIndex <= rhsTypeIndex;
        }
        
        public static bool operator >=(IdleCash lhs, IdleCash rhs)
        {
            lhs.Simplify();
            rhs.Simplify();

            var lhsTypeIndex = lhs.TypeIndex;
            var rhsTypeIndex = rhs.TypeIndex;

            if (lhsTypeIndex == rhsTypeIndex)
            {
                return lhs.value >= rhs.value;
            }

            return lhsTypeIndex >= rhsTypeIndex;
        }

        
        private static bool IsValidType(string type)
        {
            return IdleCashSettingsSO.IsValidType(type);
        }

        private static string GetNextType(string type)
        {
            return IdleCashSettingsSO.GetNextType(type);
        }
        
        private static string GetPreviousType(string type)
        {
            return IdleCashSettingsSO.GetPreviousType(type);
        }

        private static void SetThemSameType(ref IdleCash first, ref IdleCash second)
        {
            first.Simplify();
            second.Simplify();

            var firstTypeIndex = first.TypeIndex;
            var secondTypeIndex = second.TypeIndex;
            var typeDifference = Mathf.Abs(firstTypeIndex - secondTypeIndex);
            var isFirstGreater = first > second;

            if (isFirstGreater)
            {
                first.type = second.type;
                first.value *= Mathf.Pow(MaxValue, typeDifference);
                return;
            }
            
            second.type = first.type;
            second.value *= Mathf.Pow(MaxValue, typeDifference);
        }
        
        
        private bool TryConvertNextType()
        {
            var nextType = GetNextType(type);

            if (nextType == null) return false;
            
            value /= MaxValue;
            type = nextType;

            return true;
        }

        private bool TryConvertPreviousType()
        {
            var previousType = GetPreviousType(type);

            if (previousType == null) return false;
            
            value *= MaxValue;
            type = previousType;

            return true;
        }
    }
}
