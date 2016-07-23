using System;
using System.Collections.Generic;
using CalculatorService.Abstract;
using CalculatorService.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace CalculatorService.Test
{
    [TestFixture]
    public class TaxCalculatorTest
    {
        private ICalculator _calculator;
        private Mock<ICalculatorRepository> _calculatorRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            _calculatorRepositoryMock = new Mock<ICalculatorRepository>();
            _calculator = new TaxCalculator(_calculatorRepositoryMock.Object);
        }

        [TestCase]
        public void CtorThrowsExceptionIfNullIsPassed()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => new TaxCalculator(null));    
        }

        [TestCase(5000, ExpectedResult = 500)]
        [TestCase(5800, ExpectedResult = 610.8)]
        [TestCase(9000, ExpectedResult = 1092.1)]
        [TestCase(15000, ExpectedResult = 2539.3)]
        [TestCase(50000, ExpectedResult = 15069.1)]
        public decimal Calculate_ValidEarnings_CorrectTaxAmount(decimal amount)
        {
            Dictionary<double, decimal> taxBands = new Dictionary<double, decimal>();
            taxBands.Add(.10, 5030m);
            taxBands.Add(.14, 8630m);
            taxBands.Add(.23, 14040m);
            taxBands.Add(.30, 21260m);
            taxBands.Add(.33, 40270m);
            taxBands.Add(.45, 40270m);
            _calculatorRepositoryMock.Setup(x => x.GetTaxBands()).Returns(taxBands);

            return _calculator.Calculate(amount);
        }

        [TestCase]
        public void Calculate_TaxBandsIsEmpty_ThrowsApplicationException()
        {
            ExceptionAssert.Throws<ApplicationException>(() => _calculator.Calculate(200));
        }

        [TestCase]
        public void Calculate_AmountIsLessThanZero_ThrowsArgumentException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => _calculator.Calculate(-3000));
        }
    }
}
