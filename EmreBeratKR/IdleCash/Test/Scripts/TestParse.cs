using UnityEngine;
using EmreBeratKR.IdleCash;

namespace IdleCashSystem.Test
{
    public class TestParse : MonoBehaviour
    {
        public string rawString;
        public IdleCash parsed;

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(rawString))
            {
                parsed = IdleCash.Parse(rawString);
            }
        }
    }
}