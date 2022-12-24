using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestEqualityOperators : MonoBehaviour
    {
        public IdleCash lhs;
        public IdleCash rhs;
        [Space] 
        public bool equals;
        public bool notEquals;
        public bool isSmaller;
        public bool isGreater;
        public bool isSmallerOrEqual;
        public bool isGreaterOrEqual;


        private void OnValidate()
        {
            equals = lhs == rhs;
            notEquals = lhs != rhs;
            isSmaller = lhs < rhs;
            isGreater = lhs > rhs;
            isSmallerOrEqual = lhs <= rhs;
            isGreaterOrEqual = lhs >= rhs;
        }
    }
}