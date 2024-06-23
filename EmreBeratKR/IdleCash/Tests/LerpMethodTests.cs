using NUnit.Framework;

namespace EmreBeratKR.IdleCash.Tests
{
    public class LerpMethodTests
    {
        [Test]
        public void Test0()
        {
            // Setup
            var a = new IdleCash(500);
            var b = new IdleCash(1000);
            var t = 0.0f;
            
            // Execute
            var result = IdleCash.Lerp(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(500), result);
        }
        
        [Test]
        public void Test1()
        {
            // Setup
            var a = new IdleCash(500);
            var b = new IdleCash(1000);
            var t = 0.5f;
            
            // Execute
            var result = IdleCash.Lerp(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(750), result);
        }
        
        [Test]
        public void Test2()
        {
            // Setup
            var a = new IdleCash(500);
            var b = new IdleCash(1000);
            var t = 1f;
            
            // Execute
            var result = IdleCash.Lerp(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(1000), result);
        }
        
        [Test]
        public void Test3()
        {
            // Setup
            var a = new IdleCash(-1000);
            var b = new IdleCash(2000);
            var t = 0f;
            
            // Execute
            var result = IdleCash.Lerp(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(-1000), result);
        }
        
        [Test]
        public void Test4()
        {
            // Setup
            var a = new IdleCash(-1000);
            var b = new IdleCash(2000);
            var t = 0.25f;
            
            // Execute
            var result = IdleCash.Lerp(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(-250), result);
        }
        
        [Test]
        public void Test5()
        {
            // Setup
            var a = new IdleCash(-1000);
            var b = new IdleCash(2000);
            var t = 0.8f;
            
            // Execute
            var result = IdleCash.Lerp(a, b, t);
            
            // Assert
            Assert.AreEqual(new IdleCash(1400), result);
        }
    }
}