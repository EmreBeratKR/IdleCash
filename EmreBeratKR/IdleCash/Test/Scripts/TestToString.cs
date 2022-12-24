using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestToString : MonoBehaviour
    {
        public IdleCash idleCash;
        public string toString;


        private void OnValidate()
        {
            toString = idleCash.ToString();
        }
    }
}