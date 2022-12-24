using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestLerp : MonoBehaviour
    {
        public IdleCash a;
        public IdleCash b;
        [Range(-10, 10)]
        public float t;
        public bool clamp;
        public IdleCash result;


        private void OnValidate()
        {
            result = clamp ? IdleCash.Lerp(a, b, t) : IdleCash.LerpUnclamped(a, b, t);
        }
    }
}