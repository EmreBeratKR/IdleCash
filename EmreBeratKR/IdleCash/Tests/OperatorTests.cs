using NUnit.Framework;

namespace EmreBeratKR.IdleCash.Tests
{
    public class OperatorTests
    {
        #region (++) operator
        
        [Test]
        public void IncrementRhs()
        {
            // Setup
            var a = new IdleCash(999);
            
            // Execute
            var notIncremented = a++;

            // Assert
            Assert.AreEqual(a, new IdleCash(1000));
            Assert.AreEqual(notIncremented, new IdleCash(999));
        }
        
        [Test]
        public void IncrementLhs()
        {
            // Setup
            var a = new IdleCash(999);
            
            // Execute
            var incremented = ++a;

            // Assert
            Assert.AreEqual(a, new IdleCash(1000));
            Assert.AreEqual(incremented, a);
        }
        
        #endregion

        #region (+) operator

        [Test]
        public void AddWithFloatRhs()
        {
            // Setup
            var a = new IdleCash(900);
            var b = 105;
            
            // Execute
            var result = a + b;
            
            // Assert
            Assert.AreEqual(new IdleCash(1005), result);
        }
        
        [Test]
        public void AddWithFloatLhs()
        {
            // Setup
            var a = 105;
            var b = new IdleCash(900);
            
            // Execute
            var result = a + b;
            
            // Assert
            Assert.AreEqual(new IdleCash(1005), result);
        }
        
        [Test]
        public void AddWithSelf()
        {
            // Setup
            var a = new IdleCash(105);
            var b = new IdleCash(900);
            
            // Execute
            var result = a + b;
            
            // Assert
            Assert.AreEqual(new IdleCash(1005), result);
        }

        #endregion
        
        #region (--) operator

        [Test]
        public void DecrementRhs()
        {
            // Setup
            var a = new IdleCash(1_000_000);
            
            // Execute
            var notDecremented = a--;
            
            // Assert
            Assert.AreEqual(a, new IdleCash(999_999));
            Assert.AreEqual(notDecremented, new IdleCash(1_000_000));
        }
        
        [Test]
        public void DecrementLhs()
        {
            // Setup
            var a = new IdleCash(1_000_000);
            
            // Execute
            var decremented = --a;
            
            // Assert
            Assert.AreEqual(a, new IdleCash(999_999));
            Assert.AreEqual(decremented, a);
        }

        #endregion

        #region (-) operator
        
        [Test]
        public void Negate()
        {
            // Setup
            var a = new IdleCash(1000);
            
            // Execute
            var negate = -a;
            
            // Assert
            Assert.AreEqual(new IdleCash(-1000), negate);
        }
        
        [Test]
        public void SubtractWithFloatRhs()
        {
            // Setup
            var a = new IdleCash(1000);
            var b = 105f;
            
            // Execute
            var result = a - b;
            
            // Assert
            Assert.AreEqual(new IdleCash(895), result);
        }
        
        [Test]
        public void SubtractWithFloatLhs()
        {
            // Setup
            var a = 105f;
            var b = new IdleCash(1000);
            
            // Execute
            var result = a - b;
            
            // Assert
            Assert.AreEqual(new IdleCash(-895), result);
        }
        
        [Test]
        public void SubtractWithSelf()
        {
            // Setup
            var a = new IdleCash(105);
            var b = new IdleCash(1000);
            
            // Execute
            var result = a - b;
            
            // Assert
            Assert.AreEqual(new IdleCash(-895), result);
        }

        #endregion

        #region (*) operator

        [Test]
        public void MultiplyWithFloatRhs()
        {
            // Setup
            var a = new IdleCash(500);
            var b = 2f;
            
            // Execute
            var result = a * b;
            
            // Assert
            Assert.AreEqual(new IdleCash(1000), result);
        }
        
        [Test]
        public void MultiplyWithFloatLhs()
        {
            // Setup
            var a = 2f;
            var b = new IdleCash(500);
            
            // Execute
            var result = a * b;
            
            // Assert
            Assert.AreEqual(new IdleCash(1000), result);
        }
        
        [Test]
        public void MultiplyWithSelf()
        {
            // Setup
            var a = new IdleCash(500);
            var b = new IdleCash(400);
            
            // Execute
            var result = a * b;
            
            // Assert
            Assert.AreEqual(new IdleCash(200000), result);
        }

        #endregion

        #region (/) operator
        
        [Test]
        public void DivideWithFloatRhs()
        {
            // Setup
            var a = new IdleCash(1500);
            var b = 2f;
            
            // Execute
            var result = a / b;
            
            // Assert
            Assert.AreEqual(new IdleCash(750), result);
        }
        
        [Test]
        public void DivideWithFloatLhs()
        {
            // Setup
            var a = 1500f;
            var b = new IdleCash(2);
            
            // Execute
            var result = a / b;
            
            // Assert
            Assert.AreEqual(new IdleCash(750), result);
        }
        
        [Test]
        public void DivideWithSelf()
        {
            // Setup
            var a = new IdleCash(3200);
            var b = new IdleCash(400);
            
            // Execute
            var result = a / b;
            
            // Assert
            Assert.AreEqual(new IdleCash(8), result);
        }

        #endregion
    }
}
