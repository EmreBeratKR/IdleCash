using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestRealValue : MonoBehaviour
    {
        public IdleCash idleCashValue;
        [Space]
        public float realValue;


        private void OnValidate()
        {
            realValue = idleCashValue.RealValue;
        }
    }
}