using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestPlusOperator : MonoBehaviour
    {
        public IdleCash lhs;
        public IdleCash rhs;
        public IdleCash result;


        private void OnValidate()
        {
            result = lhs + rhs;
        }
    }
}