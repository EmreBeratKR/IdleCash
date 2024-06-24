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

        #region (<) operator

        [Test]
        public void LessThanSameType()
        {
            // Setup
            var a = new IdleCash(1500);
            var b = new IdleCash(1600);

            // Execute
            var result = a < b;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LessThanDifferentType()
        {
            // Setup
            var a = new IdleCash(15);
            var b = new IdleCash(2000);

            // Execute
            var result = a < b;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LessThanWithNegativeSameType()
        {
            // Setup
            var a = new IdleCash(-20000);
            var b = new IdleCash(-15000);
            var c = new IdleCash(20000);

            // Execute
            var result1 = a < b;
            var result2 = b < c;
            var result3 = a < c;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
        }

        [Test]
        public void LessThanWithNegativeDifferentType()
        {
            // Setup
            var a = new IdleCash(-15000000);
            var b = new IdleCash(-2000);
            var c = new IdleCash(2);

            // Execute
            var result1 = a < b;
            var result2 = b < c;
            var result3 = a < c;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
        }

        #endregion

        #region (<=) operator

        [Test]
        public void LessThanEqualSameType()
        {
            // Setup
            var a = new IdleCash(1500);
            var b = new IdleCash(1600);
            var c = new IdleCash(1600);

            // Execute
            var result1 = a <= b;
            var result2 = b <= c;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
        }

        [Test]
        public void LessThanEqualDifferentType()
        {
            // Setup
            var a = new IdleCash(15);
            var b = new IdleCash(2000);

            // Execute
            var result = a <= b;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LessThanEqualWithNegativeSameType()
        {
            // Setup
            var a = new IdleCash(-20000);
            var b = new IdleCash(-15000);
            var c = new IdleCash(-15000);
            var d = new IdleCash(20000);

            // Execute
            var result1 = a <= b;
            var result2 = b <= c;
            var result3 = c <= d;
            var result4 = a <= d;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
            Assert.AreEqual(true, result4);
        }

        [Test]
        public void LessThanEqualWithNegativeDifferentType()
        {
            // Setup
            var a = new IdleCash(-15000000);
            var b = new IdleCash(-2000);
            var c = new IdleCash(-2000);
            var d = new IdleCash(2);

            // Execute
            var result1 = a <= b;
            var result2 = b <= c;
            var result3 = c <= d;
            var result4 = a <= d;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
            Assert.AreEqual(true, result4);
        }

        #endregion

        #region (>) operator

        [Test]
        public void GreaterThanSameType()
        {
            // Setup
            var a = new IdleCash(1600);
            var b = new IdleCash(1500);

            // Execute
            var result = a > b;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void GreaterThanDifferentType()
        {
            // Setup
            var a = new IdleCash(2000);
            var b = new IdleCash(15);

            // Execute
            var result = a > b;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void GreaterThanWithNegativeSameType()
        {
            // Setup
            var a = new IdleCash(20000);
            var b = new IdleCash(-15000);
            var c = new IdleCash(-20000);

            // Execute
            var result1 = a > b;
            var result2 = b > c;
            var result3 = a > c;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
        }

        [Test]
        public void GreaterThanWithNegativeDifferentType()
        {
            // Setup
            var a = new IdleCash(2);
            var b = new IdleCash(-2000);
            var c = new IdleCash(-15000000);

            // Execute
            var result1 = a > b;
            var result2 = b > c;
            var result3 = a > c;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
        }

        #endregion

        #region (>=) operator

        [Test]
        public void GreaterThanEqualSameType()
        {
            // Setup
            var a = new IdleCash(1600);
            var b = new IdleCash(1500);
            var c = new IdleCash(1500);

            // Execute
            var result1 = a >= b;
            var result2 = b >= c;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
        }

        [Test]
        public void GreaterThanEqualDifferentType()
        {
            // Setup
            var a = new IdleCash(2000);
            var b = new IdleCash(15);

            // Execute
            var result = a >= b;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void GreaterThanEqualWithNegativeSameType()
        {
            // Setup
            var a = new IdleCash(20000);
            var b = new IdleCash(-15000);
            var c = new IdleCash(-15000);
            var d = new IdleCash(-20000);

            // Execute
            var result1 = a >= b;
            var result2 = b >= c;
            var result3 = c >= d;
            var result4 = a >= d;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
            Assert.AreEqual(true, result4);
        }

        [Test]
        public void GreaterThanEqualWithNegativeDifferentType()
        {
            // Setup
            var a = new IdleCash(2);
            var b = new IdleCash(-2000);
            var c = new IdleCash(-2000);
            var d = new IdleCash(-15000000);

            // Execute
            var result1 = a >= b;
            var result2 = b >= c;
            var result3 = c >= d;
            var result4 = a >= d;

            // Assert
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
            Assert.AreEqual(true, result4);
        }

        #endregion
    }
}
