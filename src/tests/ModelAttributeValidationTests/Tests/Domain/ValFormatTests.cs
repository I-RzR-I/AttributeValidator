#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Domain
{
    [TestClass]
    public class ValFormatTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void IpAddress_ValidIPv4_IsValid()
        {
            // Arrange
            var model = new IpAddressModel { IpAddress = "192.168.0.1" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IpAddress_ValidIPv6Loopback_IsValid()
        {
            // Arrange
            var model = new IpAddressModel { IpAddress = "::1" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IpAddress_InvalidOctet_IsInvalid()
        {
            // Arrange
            var model = new IpAddressModel { IpAddress = "999.1.1.1" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IpAddress_NonIpString_IsInvalid()
        {
            // Arrange
            var model = new IpAddressModel { IpAddress = "notanip" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IpAddress_Null_IsInvalid()
        {
            // Arrange
            var model = new IpAddressModel { IpAddress = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IpAddress_ValidIPv6Full_IsValid()
        {
            // Arrange
            var model = new IpAddressModel { IpAddress = "2001:db8::ff00:42:8329" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void HexColor_SixDigit_IsValid()
        {
            // Arrange
            var model = new HexColorModel { Color = "#FF0000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void HexColor_ThreeDigit_IsValid()
        {
            // Arrange
            var model = new HexColorModel { Color = "#f00" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void HexColor_NamedColor_IsInvalid()
        {
            // Arrange
            var model = new HexColorModel { Color = "red" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void HexColor_InvalidHexDigit_IsInvalid()
        {
            // Arrange
            var model = new HexColorModel { Color = "#GG0000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void HexColor_Null_IsInvalid()
        {
            // Arrange
            var model = new HexColorModel { Color = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void HexColor_EightDigitWithAlpha_IsValid()
        {
            // Arrange
            var model = new HexColorModel { Color = "#FF0000FF" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void HexColor_FiveDigits_IsInvalid()
        {
            // Arrange
            var model = new HexColorModel { Color = "#FF000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Base64_ValidEncodedString_IsValid()
        {
            // Arrange
            var model = new Base64Model { Payload = "SGVsbG8=" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Base64_InvalidCharacters_IsInvalid()
        {
            // Arrange
            var model = new Base64Model { Payload = "Hello!!" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Base64_Null_IsInvalid()
        {
            // Arrange
            var model = new Base64Model { Payload = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Base64_WrongPaddingLength_IsInvalid()
        {
            // Arrange
            var model = new Base64Model { Payload = "SGVsbG8" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert — length 7 is not a multiple of 4.
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Base64_ValidLongString_IsValid()
        {
            // Arrange
            var model = new Base64Model { Payload = "SGVsbG8gV29ybGQ=" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void PhoneE164_ValidUsNumber_IsValid()
        {
            // Arrange
            var model = new PhoneE164Model { PhoneNumber = "+14155552671" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void PhoneE164_LocalFormat_IsInvalid()
        {
            // Arrange
            var model = new PhoneE164Model { PhoneNumber = "(415)555" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void PhoneE164_StartsWithPlusZero_IsInvalid()
        {
            // Arrange
            var model = new PhoneE164Model { PhoneNumber = "+0123" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void PhoneE164_Null_IsInvalid()
        {
            // Arrange
            var model = new PhoneE164Model { PhoneNumber = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void PhoneE164_TooLong_IsInvalid()
        {
            // Arrange
            var model = new PhoneE164Model { PhoneNumber = "+1234567890123456" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void PhoneE164_ValidUkNumber_IsValid()
        {
            // Arrange
            var model = new PhoneE164Model { PhoneNumber = "+44207946000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Iban_KnownValidIban_IsValid()
        {
            // Arrange
            var model = new IbanModel { Iban = "GB82WEST12345698765432" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Iban_KnownValidIbanWithSpaces_IsValid()
        {
            // Arrange
            var model = new IbanModel { Iban = "GB82 WEST 1234 5698 7654 32" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Iban_BadCheckDigits_IsInvalid()
        {
            // Arrange
            var model = new IbanModel { Iban = "GB82WEST12345698765433" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Iban_RandomString_IsInvalid()
        {
            // Arrange
            var model = new IbanModel { Iban = "notaniban" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Iban_Null_IsInvalid()
        {
            // Arrange
            var model = new IbanModel { Iban = null };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Iban_InvalidIban_DefaultErrorMessageContainsIban()
        {
            // Arrange
            var model = new IbanModel { Iban = "notaniban" };

            // Act
            var (_, results) = Validate(model);

            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "IBAN");
        }

        [TestMethod]
        public void Iban_InvalidIban_DefaultErrorMessageContainsMemberName()
        {
            // Arrange
            var model = new IbanModel { Iban = "notaniban" };

            // Act
            var (_, results) = Validate(model);

            // Assert
            Assert.IsTrue(results.Count > 0);
            StringAssert.Contains(results[0].ErrorMessage, "Iban");
        }

        [TestMethod]
        public void Iban_EmptyString_IsInvalid()
        {
            // Arrange
            var model = new IbanModel { Iban = "" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Iban_KnownValidDeIban_IsValid()
        {
            // Arrange
            var model = new IbanModel { Iban = "DE89370400440532013000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
