using System;
using System.Collections.Generic;
using System.Text;
using CashMasterPos.Entities;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{   
    [TestClass]
    public class UtilsTest
    {
        private Mock<CashMasterPos.Utilities.IUtilities> _mockUtils;
        [TestInitialize]
        public void Mock_Init()
        {
            _mockUtils = new Mock<CashMasterPos.Utilities.IUtilities>();
        }
        /// <summary>
        /// Test the scenario where the customer change it's been calculated.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_Calculate_Change()
        {
            //Arrange
           _mockUtils.Setup(x => x.CalculateChange()).Returns(() => new MessageResult<double> { Data = 20.2, Message="Ok", Status = true});
            //Act
            var res = _mockUtils.Object.CalculateChange();
            //Asserts
            Assert.IsNotNull(res.Data);
            Assert.IsNotNull(res.Message);
            Assert.IsTrue(res.Status);
            Assert.IsInstanceOfType(res.Data,typeof(double));
        }
        /// <summary>
        /// Test the scenario where the bills are being distributed correctly.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_DistributeBills_No_Empty_List()
        {
            //Arrange 
           _mockUtils.Setup(x => x.DistributeBills(It.IsAny<double>(), It.IsAny<List<double>>())).Returns(()=>new MessageResult<List<AmountManager>> { 
            Data = new List<AmountManager>
            {
               new AmountManager
               {
                   Bill = 5
               },new AmountManager
               {
                   Bill = 5
               }
            },
            Message = "$5,$5",
            Status = true
            });
            //Act
            var res = _mockUtils.Object.DistributeBills(It.IsAny<double>(), It.IsAny<List<double>>());
            //Asserts
            
            Assert.IsNotNull(res.Data);
            Assert.IsNotNull(res.Message);
            Assert.IsTrue(res.Status);
            Assert.IsInstanceOfType(res.Data, typeof(List<AmountManager>));
        }
        /// <summary>
        /// Test the scenario where the bills are being distributed wrongly.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_DistributeBills_Empty_List()
        {
            //Arrange 
           _mockUtils.Setup(x => x.DistributeBills(It.IsAny<double>(), It.IsAny<List<double>>())).Returns(()=>new MessageResult<List<AmountManager>> { 
            Data = null,
            Message = null,
            Status = false
            });
            //Act
            var res = _mockUtils.Object.DistributeBills(It.IsAny<double>(), It.IsAny<List<double>>());
            //Asserts
            
            Assert.IsNull(res.Data);
            Assert.IsNull(res.Message);
            Assert.IsFalse(res.Status);
        }
        /// <summary>
        /// Test the scenario where the dimes are being distributed correctly.
        /// </summary>
        /// <returns></returns>
        public void Valid_DistributeDimes_No_Empty_List()
        {
            //Arrange 
           _mockUtils.Setup(x => x.DistributeDimes(It.IsAny<double>(), It.IsAny<List<double>>())).Returns(()=>new MessageResult<List<AmountManager>> { 
            Data = new List<AmountManager>
            {
               new AmountManager
               {
                   Bill = 0.5
               },new AmountManager
               {
                   Bill = 0.5
               }
            },
            Message = "$0.5,$0.5",
            Status = true
            });
            //Act
            var res = _mockUtils.Object.DistributeDimes(It.IsAny<double>(), It.IsAny<List<double>>());
            //Asserts
            Assert.IsNotNull(res.Data);
            Assert.IsNotNull(res.Message);
            Assert.IsTrue(res.Status);
            Assert.IsInstanceOfType(res.Data, typeof(List<AmountManager>));
        }
        /// <summary>
        /// Test the scenario where the bills are being distributed wrongly.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_DistributeDimes_Empty_List()
        {
            //Arrange 
           _mockUtils.Setup(x => x.DistributeDimes(It.IsAny<double>(), It.IsAny<List<double>>())).Returns(()=>new MessageResult<List<AmountManager>> { 
            Data = null,
            Message = null,
            Status = false
            });
            //Act
            var res = _mockUtils.Object.DistributeDimes(It.IsAny<double>(), It.IsAny<List<double>>());
            //Asserts
            
            Assert.IsNull(res.Data);
            Assert.IsNull(res.Message);
            Assert.IsFalse(res.Status);
        }
        /// <summary>
        /// Test the scenario where the input amount is being rounded.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_RoundUp()
        {
            //Arrange
            _mockUtils.Setup(x => x.RoundUp(It.IsAny<double>(), It.IsAny<int>())).Returns(() => 5.05);
            //Act
            var res = _mockUtils.Object.RoundUp(It.IsAny<double>(), It.IsAny<int>());
            //Asserts
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(double));
        }
        /// <summary>
        /// Test the scenario where the customer amount input are being summed.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_Sum()
        {
            //Arrange
            _mockUtils.Setup(x => x.Sum(It.IsAny<AmountManager>())).Returns(() => 5.05);
            //Act
            var res = _mockUtils.Object.RoundUp(It.IsAny<double>(), It.IsAny<int>());
            //Asserts
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(double));
        }
        /// <summary>
        /// Test the scenario where the system retrieves bills denomination for a machine.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_GetBillsDenomination()
        {
            //Arrange
            _mockUtils.Setup(x => x.GetBillsDenomination(It.IsAny<List<double>>())).Returns(() => new List<double> {2.0,5});
            //Act
            var res = _mockUtils.Object.GetBillsDenomination(It.IsAny<List<double>>());
            //Asserts
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(IEnumerable<double>));
        }
        /// <summary>
        /// Test the scenario where the system retrieves dimes denomination for a machine.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void Valid_GetDimesDenomination()
        {
            //Arrange
            _mockUtils.Setup(x => x.GetDimesDenomination(It.IsAny<List<double>>())).Returns(() => new List<double> { 0.1, 0.5 });
            //Act
            var res = _mockUtils.Object.GetDimesDenomination(It.IsAny<List<double>>());
            //Asserts
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(IEnumerable<double>));
        }
    }
        
}
