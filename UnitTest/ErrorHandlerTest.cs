using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using CashMasterPos.Entities;

namespace UnitTest
{
    [TestClass]
    public class ErrorHandlerTest
    {
        private Mock<CashMasterPos.ErrorHandler.IErrorHandler> _mockErrorHandler;
        [TestInitialize]
        public void Mock_Init()
        {
            _mockErrorHandler = new Mock<CashMasterPos.ErrorHandler.IErrorHandler>();
        }
        /// <summary>
        /// Test the scenario where the customer input amount is being compared with the current machine's denomination and is succeeded.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_ValidDenomination_Exist()
        {
            //Arrange
            _mockErrorHandler.Setup(x => x.ValidDenomination(It.IsAny<double>(), It.IsAny<bool>(), It.IsAny<List<double>>())).Returns(() => new MessageResult<bool>
            {
            Data = true,
            Message = "Ok",
            Status = true
        });
            //Act
            var res = _mockErrorHandler.Object.ValidDenomination(It.IsAny<double>(), It.IsAny<bool>(), It.IsAny<List<double>>());
            //Assert

            Assert.IsTrue(res.Data);
            Assert.IsNotNull(res.Message);
            Assert.AreEqual(res.Message, "Ok");
            Assert.IsTrue(res.Status);
        }
        /// <summary>
        /// Test the scenario where the customer input amount of bills is being compared with the current machine's denomination and it fails
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_ValidDenomination_No_Exist()
        {
            //Arrange
            _mockErrorHandler.Setup(x => x.ValidDenomination(It.IsAny<double>(), It.IsAny<bool>(), It.IsAny<List<double>>())).Returns(() => new MessageResult<bool>
            {
            Data = false,
            Message = "Bad",
            Status = false
        });
            //Act
            var res = _mockErrorHandler.Object.ValidDenomination(It.IsAny<double>(), It.IsAny<bool>(), It.IsAny<List<double>>());
            //Assert

            Assert.IsFalse(res.Data);
            Assert.IsNotNull(res.Message);
            Assert.AreEqual(res.Message, "Bad");
            Assert.IsFalse(res.Status);
        }
        /// <summary>
        /// Test the scenario where the customer input is validated to match a numeric value, and succeded.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_ValidNumericInputs_Ok()
        {
            //Arrange
            _mockErrorHandler.Setup(x => x.ValidNumericInputs(It.IsAny<string>())).Returns(() => new MessageResult<string>
            {
            Data = "25",
            Message = "Ok",
            Status = true
        });
            //Act
            var res = _mockErrorHandler.Object.ValidNumericInputs(It.IsAny<string>());
            //Assert
            Assert.IsNotNull(res.Data);
            Assert.AreEqual(res.Data, "25");
            Assert.IsInstanceOfType(res.Data, typeof(string));
            Assert.IsNotNull(res.Message);
            Assert.AreEqual(res.Message, "Ok");
            Assert.IsTrue(res.Status);
        }
        /// <summary>
        /// Test the scenario where the customer input is validated to match a numeric value, and it fails.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_ValidNumericInputs_Bad()
        {
            //Arrange
            _mockErrorHandler.Setup(x => x.ValidNumericInputs(It.IsAny<string>())).Returns(() => new MessageResult<string>
            {
            Data = null,
            Message = "Numeric value is required",
            Status = false
        });
            //Act
            var res = _mockErrorHandler.Object.ValidNumericInputs(It.IsAny<string>());
            //Assert
            Assert.IsNull(res.Data);
            Assert.IsNotNull(res.Message);
            Assert.AreEqual(res.Message, "Numeric value is required");
            Assert.IsFalse(res.Status);
        }

    }
}
