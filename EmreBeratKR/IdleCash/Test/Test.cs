using UnityEngine;
using EmreBeratKR.IdleCash;

public class Test : MonoBehaviour
{
    private void Start()
    {
        IdleCash a = new IdleCash(1, "k");
        IdleCash b = new IdleCash(1, "m");
        float t1 = 0.5f;
        float t2 = 2.34f;
        
        
        // Lerps between "a" to "b" depending on "t1"
        // "t1" is clamped between 0 and 1
        IdleCash.Lerp(a, b, t1);
        
        // Lerps between "a" to "b" depending on "t2"
        IdleCash.LerpUnclamped(a, b, t2);
    }
}