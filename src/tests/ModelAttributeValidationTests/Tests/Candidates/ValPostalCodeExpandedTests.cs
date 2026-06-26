#region U S I N G

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelAttributeValidationTests.Models.Candidates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ModelAttributeValidationTests.Tests.Candidates
{
    [TestClass]
    public class ValPostalCodeExpandedTests
    {
        private static (bool isValid, List<ValidationResult> results) Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, ctx, results, true);

            return (isValid, results);
        }

        [TestMethod]
        public void ValPostalCode_Ch_FourDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeChModel { PostalCode = "8001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ch_Letters_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeChModel { PostalCode = "abc" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_At_FourDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeAtModel { PostalCode = "1010" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_At_ThreeDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeAtModel { PostalCode = "101" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Be_FourDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeBeModel { PostalCode = "1000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Be_SixDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeBeModel { PostalCode = "100000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Se_WithSpace_IsValid()
        {
            // Arrange
            var model = new PostalCodeSeModel { PostalCode = "114 51" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Se_WithoutSpace_IsValid()
        {
            // Arrange
            var model = new PostalCodeSeModel { PostalCode = "11451" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Se_FourDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeSeModel { PostalCode = "1145" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Pt_WithHyphen_IsValid()
        {
            // Arrange
            var model = new PostalCodePtModel { PostalCode = "1990-001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Pt_WithoutHyphen_IsInvalid()
        {
            // Arrange
            var model = new PostalCodePtModel { PostalCode = "1990" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Pl_WithHyphen_IsValid()
        {
            // Arrange
            var model = new PostalCodePlModel { PostalCode = "00-950" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Pl_WithoutHyphen_IsInvalid()
        {
            // Arrange
            var model = new PostalCodePlModel { PostalCode = "00950" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Br_WithHyphen_IsValid()
        {
            // Arrange
            var model = new PostalCodeBrModel { PostalCode = "01310-100" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Br_WithoutHyphen_IsValid()
        {
            // Arrange
            var model = new PostalCodeBrModel { PostalCode = "01310100" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Br_FourDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeBrModel { PostalCode = "0131" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_In_WithSpace_IsValid()
        {
            // Arrange
            var model = new PostalCodeInModel { PostalCode = "110 001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_In_WithoutSpace_IsValid()
        {
            // Arrange
            var model = new PostalCodeInModel { PostalCode = "110001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_In_FiveDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeInModel { PostalCode = "11000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ru_SixDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeRuModel { PostalCode = "101000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ru_FiveDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeRuModel { PostalCode = "10100" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Lt_WithPrefix_IsValid()
        {
            // Arrange
            var model = new PostalCodeLtModel { PostalCode = "LT-01100" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Lt_WithoutPrefix_IsValid()
        {
            // Arrange
            var model = new PostalCodeLtModel { PostalCode = "01100" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Lt_AllLetters_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeLtModel { PostalCode = "abcde" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Mc_ValidMonacoCode_IsValid()
        {
            // Arrange
            var model = new PostalCodeMcModel { PostalCode = "98000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Mc_WrongPrefix_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeMcModel { PostalCode = "97000" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Is_ThreeDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeIsModel { PostalCode = "101" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Is_FourDigits_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeIsModel { PostalCode = "1010" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ie_ValidEircodeWithSpace_IsValid()
        {
            // Arrange
            var model = new PostalCodeIeModel { PostalCode = "D02 AF30" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ie_NumericOnly_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeIeModel { PostalCode = "12345" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ar_LetterPlusFourDigits_IsValid()
        {
            // Arrange
            var model = new PostalCodeArModel { PostalCode = "C1425" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ar_FourDigitsOnly_IsValid()
        {
            // Arrange
            var model = new PostalCodeArModel { PostalCode = "1425" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Ar_SpecialCharacters_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeArModel { PostalCode = "!!!" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Nl_FourDigitsSpaceTwoLetters_IsValid()
        {
            // Arrange
            var model = new PostalCodeNlModel { PostalCode = "1234 AB" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Nl_FourDigitsOnly_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeNlModel { PostalCode = "1234" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Md_WithPrefix_IsValid()
        {
            // Arrange
            var model = new PostalCodeMdModel { PostalCode = "MD-2001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Md_DigitsOnly_IsValid()
        {
            // Arrange
            var model = new PostalCodeMdModel { PostalCode = "2001" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValPostalCode_Md_TooShort_IsInvalid()
        {
            // Arrange
            var model = new PostalCodeMdModel { PostalCode = "200" };

            // Act
            var (isValid, _) = Validate(model);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
