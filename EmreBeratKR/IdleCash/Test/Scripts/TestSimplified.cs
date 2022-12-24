using UnityEngine;
using EmreBeratKR.IdleCash;

namespace IdleCashSystem.Test
{
    public class TestSimplified : MonoBehaviour
    {
        public IdleCash normal;
        public IdleCash simplified;


        private void OnValidate()
        {
            simplified = normal.Simplified;
        }
    }
}