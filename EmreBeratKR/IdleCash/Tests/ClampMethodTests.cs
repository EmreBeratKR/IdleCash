using NUnit.Framework;

namespace EmreBeratKR.IdleCash.Tests
{
    public class ClampMethodTests
    {
        [Test]
        public void Test0()
        {
            // Setup
            var value = new IdleCash(1.56f, "aa");
            var a = new IdleCash(1.24f, "aa");
            var b = new IdleCash(67, "aa");
            
            // Execute
            var result = IdleCash.Clamp(value, a, b);
            
            // Assert
            Assert.AreEqual(value, result);
        }
        
        [Test]
        public void Test1()
        {
            // Setup
            var value = new IdleCash(1.07f, "aa");
            var a = new IdleCash(1.24f, "aa");
            var b = new IdleCash(67, "aa");
            
            // Execute
            var result = IdleCash.Clamp(value, a, b);
            
            // Assert
            Assert.AreEqual(a, result);
        }
        
        [Test]
        public void Test2()
        {
            // Setup
            var value = new IdleCash(1, "ab");
            var a = new IdleCash(500, "t");
            var b = new IdleCash(67, "aa");
            
            // Execute
            var result = IdleCash.Clamp(value, a, b);
            
            // Assert
            Assert.AreEqual(b, result);
        }
    }
}