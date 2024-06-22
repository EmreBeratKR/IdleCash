using System;
using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestClamp : MonoBehaviour
    {
        public IdleCash value;
        public IdleCash a;
        public IdleCash b;
        public IdleCash result;


        private void OnValidate()
        {
            result = IdleCash.Clamp(value, a, b);
        }
    }
}