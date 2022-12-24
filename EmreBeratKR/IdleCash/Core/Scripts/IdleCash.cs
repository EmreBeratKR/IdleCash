using System;
using UnityEngine;
using EmreBeratKR.IdleCash.Creator;

namespace EmreBeratKR.IdleCash
{
    [Serializable]
    public struct IdleCash : IEquatable<IdleCash>
    {
        private const float ValueTolerance = 0.00001f;
        private const int MaxValue = 1000;
        private const int MinValue = 1;


        public static IdleCash Zero => new IdleCash(0f, FirstType);
        public static IdleCash One => new IdleCash(1f, FirstType);
        public static string FirstType => IdleCashSettingsSO.FirstType;
        public static string LastType => IdleCashSettingsSO.LastType;
        

        public IdleCash Simplified
        {
            get
            {
                var copy = new IdleCash(value, type);
                copy.Simplify();
                return copy;
            }
        }

        public float RealValue => (float) (value * Math.Pow(10, 3 * TypeIndex));
        public int TypeIndex => IdleCashSettingsSO.GetTypeIndex(type);

        
        public string type;
        public float value;
        

        public IdleCash(float value)
        {
            this.value = value;
            this.type = FirstType;
            Simplify();
        }
        
        public IdleCash(float value, string type)
        {
            if (!IsValidType(type))
            {
                throw new Exception("Invalid Idle Cash Type!");
            }
            
            this.value = value;
            this.type = type;
            Simplify();
        }
        

        public static IdleCash Lerp(IdleCash a, IdleCash b, float t)
        {
            var clampedT = Mathf.Clamp01(t);
            return LerpUnclamped(a, b, clampedT);
        }

        public static IdleCash LerpUnclamped(IdleCash a, IdleCash b, float t)
        {
            var difference = b - a;
            return a + difference * t;
        }


        public void Simplify()
        {
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
            SetThemSameType(ref lhs, ref rhs);

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
            SetThemSameType(ref lhs, ref rhs);

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
