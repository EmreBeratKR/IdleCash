using EmreBeratKR.IdleCash;
using UnityEngine;

namespace IdleCashSystem.Test
{
    public class TestTypeIndex : MonoBehaviour
    {
        public IdleCash idleCash;
        [Space]
        public int typeIndex;


        private void OnValidate()
        {
            typeIndex = idleCash.TypeIndex;
        }
    }
}