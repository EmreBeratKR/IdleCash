using System;
using UnityEngine;
using EmreBeratKR.IdleCash;

namespace IdleCashSystem.Test
{
    public class IdleCashTest : MonoBehaviour
    {
        [Header("String Value Test")]
        [SerializeField] private IdleCash idleCashToPrint;
        [SerializeField] private string idleCashAsString;

        [Header("Simplification Test")]
        [SerializeField] private IdleCash idleCashToSimplify;
        [SerializeField] private bool simplify;
        
        [Header("Bool Operator Test")] 
        [SerializeField] private IdleCashBoolOperatorTest boolOperator;
        [SerializeField] private IdleCash lhsIdleCashBool;
        [SerializeField] private IdleCash rhsIdleCashBool;
        [SerializeField] private bool boolOperatorResult;

        [Header("Numeric Operator Test")]
        [SerializeField] private IdleCashNumericOperatorTest numericOperator;
        [SerializeField] private IdleCash lhsIdleCashNumeric;
        [SerializeField] private IdleCash rhsIdleCashNumeric;
        [SerializeField] private float rhsFloatNumeric;
        [SerializeField] private IdleCash numericOperatorIdleCashResult;

        [Header("Lerp Test")]
        [SerializeField] private IdleCash a;
        [SerializeField] private IdleCash b;
        [SerializeField] private float t;
        [SerializeField] private bool isClamped;
        [SerializeField] private IdleCash lerpResult;
        

        private void OnValidate()
        {
            StringValueTest();
            SimplificationTest();
            BoolOperatorTest();
            NumericOperatorTest();
            LerpTest();
        }


        private void StringValueTest()
        {
            idleCashAsString = idleCashToPrint.ToString();
        }

        private void SimplificationTest()
        {
            if (!simplify) return;

            simplify = false;
            
            idleCashToSimplify.Simplify();
        }
        
        private void BoolOperatorTest()
        {
            boolOperatorResult = boolOperator switch
            {
                IdleCashBoolOperatorTest.Equal => lhsIdleCashBool == rhsIdleCashBool,
                IdleCashBoolOperatorTest.NotEqual => lhsIdleCashBool != rhsIdleCashBool,
                IdleCashBoolOperatorTest.Smaller => lhsIdleCashBool < rhsIdleCashBool,
                IdleCashBoolOperatorTest.Greater => lhsIdleCashBool > rhsIdleCashBool,
                IdleCashBoolOperatorTest.SmallerOrEqual => lhsIdleCashBool <= rhsIdleCashBool,
                IdleCashBoolOperatorTest.GreaterOrEqual => lhsIdleCashBool >= rhsIdleCashBool,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void NumericOperatorTest()
        {
            numericOperatorIdleCashResult = numericOperator switch
            {
                IdleCashNumericOperatorTest.Add => lhsIdleCashNumeric + rhsIdleCashNumeric,
                IdleCashNumericOperatorTest.Subtract => lhsIdleCashNumeric - rhsIdleCashNumeric,
                IdleCashNumericOperatorTest.Multiply => lhsIdleCashNumeric * rhsFloatNumeric,
                IdleCashNumericOperatorTest.Divide => lhsIdleCashNumeric / rhsIdleCashNumeric,
                IdleCashNumericOperatorTest.DivideByFloat => lhsIdleCashNumeric / rhsFloatNumeric,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void LerpTest()
        {
            lerpResult = isClamped switch
            {
                true => IdleCash.Lerp(a, b, t),
                false => IdleCash.LerpUnclamped(a, b, t)
            };
        }
    }

    public enum IdleCashBoolOperatorTest
    {
        Equal,
        NotEqual,
        Smaller,
        Greater,
        SmallerOrEqual,
        GreaterOrEqual
    }

    public enum IdleCashNumericOperatorTest
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        DivideByFloat
    }
}