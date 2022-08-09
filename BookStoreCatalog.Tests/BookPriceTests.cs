using System.Reflection;
using NUnit.Framework;

namespace BookStoreCatalog.Tests
{
    [TestFixture]
    public class BookPriceTests : TestBase
    {
        private static readonly object[][] ConstructorData =
        {
            new object[]
            {
                Array.Empty<Type>(),
            },
            new object[]
            {
                new[] { typeof(decimal), typeof(string) },
            },
        };

        private static readonly object[][] BookPriceData =
        {
            new object[] { 0.0m, "EUR" },
            new object[] { 0.0m, "USD" },
            new object[] { 1_234.564m, "EUR" },
            new object[] { 1_234.567m, "USD" },
            new object[] { 123_456_789.234m, "EUR" },
            new object[] { 123_456_789.345m, "USD" },
        };

        private static readonly object[][] ToStringData =
        {
            new object[] { 0.0m, "EUR", "0.00 EUR" },
            new object[] { 0.0m, "USD", "0.00 USD" },
            new object[] { 1_234.564m, "EUR", "1,234.56 EUR" },
            new object[] { 1_234.567m, "USD", "1,234.57 USD" },
            new object[] { 123_456_789.234m, "EUR", "123,456,789.23 EUR" },
            new object[] { 123_456_789.345m, "USD", "123,456,789.35 USD" },
        };

        [SetUp]
        public void SetUp()
        {
            this.ClassType = typeof(BookPrice);
        }

        [Test]
        public void BookPrice_WithoutParamters_ReturnsObject()
        {
            // Act
            BookPrice bookPrice = new ();

            // Assert
            Assert.That(bookPrice.Amount, Is.EqualTo(0.0m));
            Assert.That(bookPrice.Currency, Is.EqualTo("USD"));
        }

        [Test]
        public void BookPrice_AmountIsLessZero_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                try
                {
                    _ = new BookPrice(amount: -1.0m, currency: "USD");
                }
                catch (ArgumentException e)
                {
                    Assert.That(e.ParamName, Is.EqualTo("amount"));
                    throw;
                }
            });
        }

        [Test]
        public void BookPrice_CurrencyIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                try
                {
                    _ = new BookPrice(amount: 1.0m, currency: null);
                }
                catch (ArgumentNullException e)
                {
                    Assert.That(e.ParamName, Is.EqualTo("currency"));
                    throw;
                }
            });
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase("123")]
        [TestCase("AB")]
        [TestCase("ABCD")]
        public void BookPrice_CurrencyIsInvalid_ThrowsArgumentException(string currency)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                try
                {
                    _ = new BookPrice(amount: 1.0m, currency: currency);
                }
                catch (ArgumentException e)
                {
                    Assert.That(e.ParamName, Is.EqualTo("currency"));
                    throw;
                }
            });
        }

        [Test]
        public void SetAmount_ValueIsLessZero_ThrowsArgumentException()
        {
            BookPrice bookPrice = new ();

            Assert.Throws<ArgumentException>(() =>
            {
                try
                {
                    bookPrice.Amount = -1.0m;
                }
                catch (ArgumentException e)
                {
                    Assert.That(e.ParamName, Is.EqualTo("value"));
                    throw;
                }
            });
        }

        [Test]
        public void SetCurrency_ValueIsNull_ThrowsArgumentNullException()
        {
            BookPrice bookPrice = new BookPrice();

            Assert.Throws<ArgumentNullException>(() =>
            {
                try
                {
                    bookPrice.Currency = null;
                }
                catch (ArgumentNullException e)
                {
                    Assert.That(e.ParamName, Is.EqualTo("value"));
                    throw;
                }
            });
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase("123")]
        [TestCase("AB")]
        [TestCase("ABCD")]
        public void SetCurrency_ValueIsInvalid_ThrowsArgumentException(string currency)
        {
            BookPrice bookPrice = new BookPrice();

            Assert.Throws<ArgumentException>(() =>
            {
                try
                {
                    bookPrice.Currency = currency;
                }
                catch (ArgumentException e)
                {
                    Assert.That(e.ParamName, Is.EqualTo("value"));
                    throw;
                }
            });
        }

        [TestCaseSource(nameof(BookPriceData))]
        public void BookPrice_AmountAndCurrencyAreValid_ReturnsAmountAndCurrency(decimal amount, string currency)
        {
            // Arrange
            var bookPrice = new BookPrice(amount: amount, currency: currency);

            // Assert
            Assert.That(bookPrice.Amount, Is.EqualTo(amount));
            Assert.That(bookPrice.Currency, Is.EqualTo(currency));
        }

        [TestCaseSource(nameof(ToStringData))]
        public void ToString_ParametersAreValid_ReturnsString(decimal amount, string currency, string expectedString)
        {
            // Arrange
            var bookPrice = new BookPrice(amount: amount, currency: currency);

            // Act
            string actualString = bookPrice.ToString();

            // Assert
            Assert.That(actualString, Is.EqualTo(expectedString));
        }

        [Test]
        public void IsPublicClass()
        {
            this.AssertThatClassIsPublic(false);
        }

        [Test]
        public void InheritsObject()
        {
            this.AssertThatClassInheritsObject();
        }

        [Test]
        public void HasRequiredMembers()
        {
            Assert.AreEqual(0, this.ClassType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Length);
            Assert.AreEqual(0, this.ClassType.GetFields(BindingFlags.Instance | BindingFlags.Public).Length);
            Assert.AreEqual(2, this.ClassType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Length);

            Assert.AreEqual(0, this.ClassType.GetConstructors(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Length);
            Assert.AreEqual(2, this.ClassType.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Length);
            Assert.AreEqual(0, this.ClassType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Length);

            Assert.AreEqual(0, this.ClassType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Length);
            Assert.AreEqual(2, this.ClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Length);
            Assert.AreEqual(0, this.ClassType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).Length);

            Assert.AreEqual(0, this.ClassType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly).Length);
            Assert.AreEqual(2, this.ClassType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Length);

            Assert.AreEqual(5, this.ClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Length);
            Assert.AreEqual(0, this.ClassType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Length);

            Assert.AreEqual(0, this.ClassType.GetEvents(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length);
        }

        [TestCase("amount", typeof(decimal), false)]
        [TestCase("currency", typeof(string), false)]
        public void HasRequiredField(string fieldName, Type fieldType, bool isInitOnly)
        {
            this.AssertThatClassHasField(fieldName, fieldType, isInitOnly);
        }

        [TestCaseSource(nameof(ConstructorData))]
        public void HasPublicInstanceConstructor(Type[] parameterTypes)
        {
            this.AssertThatClassHasPublicConstructor(parameterTypes);
        }

        [TestCase("Amount", typeof(decimal))]
        [TestCase("Currency", typeof(string))]
        public void HasPublicProperty(string propertyName, Type propertyType)
        {
            this.AssertThatClassHasProperty(propertyName, propertyType, true, true, true, true);
        }

        [TestCase("ToString", false, true, true, typeof(string))]
        [TestCase("ThrowExceptionIfAmountIsNotValid", true, false, false, typeof(void))]
        [TestCase("ThrowExceptionIfCurrencyIsNotValid", true, false, false, typeof(void))]
        public void HasMethod(string methodName, bool isStatic, bool isPublic, bool isVirtual, Type returnType)
        {
            this.AssertThatClassHasMethod(methodName, isStatic, isPublic, isVirtual, returnType);
        }
    }
}
