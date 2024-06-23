using NUnit.Framework;

namespace EmreBeratKR.IdleCash.Tests
{
    public class LerpUnclampedMethodTests
    {
        [Test]
        public void Test0()
        {
            // Setup
            var a = new IdleCash(100);
            var b = new IdleCash(1100);
            var t = 5f;
            
            // Execute
            var result = IdleCash.LerpUnclamped(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(5100), result);
        }
        
        [Test]
        public void Test1()
        {
            // Setup
            var a = new IdleCash(100);
            var b = new IdleCash(1100);
            var t = -3f;
            
            // Execute
            var result = IdleCash.LerpUnclamped(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(-2900), result);
        }
    }
}