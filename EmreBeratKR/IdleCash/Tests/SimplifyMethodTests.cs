using NUnit.Framework;

namespace EmreBeratKR.IdleCash.Tests
{
    public class SimplifyMethodTests
    {
        [Test]
        public void Test0()
        {
            // Setup
            var a = new IdleCash
            {
                value = 1_000_000,
                type = IdleCash.FirstType
            };
            
            // Execute
            a.Simplify();
            
            // Assert
            Assert.AreEqual(a, new IdleCash(1, "m"));
        }
        
        [Test]
        public void Test1()
        {
            // Setup
            var a = new IdleCash
            {
                value = -1_730_000,
                type = IdleCash.FirstType
            };
            
            // Execute
            a.Simplify();
            
            // Assert
            Assert.AreEqual(a, new IdleCash(-1.73f, "m"));
        }
    }
}