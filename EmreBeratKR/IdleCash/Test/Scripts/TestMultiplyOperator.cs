using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestMultiplyOperator : MonoBehaviour
    {
        [Header("IdleCash * IdleCash")]
        public IdleCash lhs1;
        public IdleCash rhs1;
        public IdleCash result1;
        
        [Header("IdleCash * float")]
        public IdleCash lhs2;
        [Space]
        public float rhs2;
        public IdleCash result2;


        private void OnValidate()
        {
            result1 = lhs1 * rhs1;
            result2 = lhs2 * rhs2;
        }
    }
}