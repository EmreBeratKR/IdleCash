using UnityEngine;
using EmreBeratKR.IdleCash;

public class Test : MonoBehaviour
{
    private void Start()
    {
        IdleCash idleCashLhs = new IdleCash(975.1f, "b");
        IdleCash idleCashRhs = new IdleCash(13.78f, "t");
        float floatRhs = 101.48f;

        IdleCash add = idleCashLhs + idleCashRhs;
        IdleCash substract = idleCashLhs - idleCashRhs;
        
        IdleCash multiply_IdleCash_IdleCash = idleCashLhs * idleCashRhs;
        IdleCash multiply_IdleCash_float = idleCashLhs * floatRhs;
        
        IdleCash divide_IdleCash_IdleCash = idleCashLhs / idleCashRhs;
        IdleCash divide_IdleCash_float = idleCashLhs / floatRhs;

        bool equals = idleCashLhs == idleCashRhs;
        bool notEquals = idleCashLhs != idleCashRhs;
        
        bool isSmaller = idleCashLhs < idleCashRhs;
        bool isGreater = idleCashLhs > idleCashRhs;
        
        bool isSmallerOrEqual = idleCashLhs <= idleCashRhs;
        bool isGreaterOrEqual = idleCashLhs >= idleCashRhs;
    }
}