using System;
using UnityEngine;

namespace EmreBeratKR.IdleCash
{
    [Serializable]
    public struct IdleCash : IEquatable<IdleCash>
    {
        private const float ValueTolerance = 0.00001f;
        private const int TypeDifferenceTolerance = 2;
        private const int MaxValue = 1000;
        private const int MinValue = 1;


        public string type;
        public float value;


        public static IdleCash Zero => new IdleCash(0f, FirstType);
        public static IdleCash One => new IdleCash(1f, FirstType);


        public static string FirstType => IdleCashTypeCreator.FirstType;
        
        public static string LastType => IdleCashTypeCreator.LastType;

        
        public int TypeIndex => IdleCashTypeCreator.GetTypeIndex(type);

        public IdleCash Simplified
        {
            get
            {
                var copy = new IdleCash(value, type);
                copy.Simplify();
                return copy;
            }
        }


        public IdleCash(float value)
        {
            this.value = value;
            this.type = FirstType;
        }
        
        public IdleCash(float value, string type)
        {
            if (!IsValidType(type))
            {
                throw new Exception("Invalid Idle Cash Type!");
            }
            
            this.value = value;
            this.type = type;
        }


        public static bool IsValidType(string type)
        {
            return IdleCashTypeCreator.IsValidType(type);
        }

        public static string GetNextType(string type)
        {
            return IdleCashTypeCreator.GetNextType(type);
        }
        
        public static string GetPreviousType(string type)
        {
            return IdleCashTypeCreator.GetPreviousType(type);
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

        public static IdleCash operator *(IdleCash lhs, float rhs)
        {
            lhs.value *= rhs;
            return lhs.Simplified;
        }
        
        public static float operator /(IdleCash lhs, IdleCash rhs)
        {
            SetThemSameType(ref lhs, ref rhs);

            return lhs.value / rhs.value;
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


        private static void SetThemSameType(ref IdleCash first, ref IdleCash second)
        {
            first.Simplify();
            second.Simplify();

            var firstTypeIndex = first.TypeIndex;
            var secondTypeIndex = second.TypeIndex;
            var typeDifference = Mathf.Abs(firstTypeIndex - secondTypeIndex);
            var isTooBigTypeDifference = typeDifference > TypeDifferenceTolerance;
            var isFirstGreater = first > second;

            if (isTooBigTypeDifference)
            {
                if (isFirstGreater)
                {
                    second.type = first.type;
                    second.value = 0f;
                    return;
                }

                first.type = second.type;
                first.value = 0f;
                return;
            }

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
