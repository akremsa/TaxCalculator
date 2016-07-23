using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorService.Abstract;
using DataAccessLayer.Abstract;

namespace CalculatorService.Concrete
{
    public class TaxCalculator: ICalculator
    {
        private Dictionary<double, decimal> _taxBands;

        private readonly ICalculatorRepository _calculatorRepository;

        public TaxCalculator(ICalculatorRepository calculatorRepository)
        {
            if (calculatorRepository == null)
            {
                throw new ArgumentNullException("calculatorRepository");
            }

            _calculatorRepository = calculatorRepository;
        }

        private void SetTaxBands()
        {
            _taxBands = _calculatorRepository.GetTaxBands();

            if (_taxBands == null || !_taxBands.Any())
            {
                throw new ApplicationException("Tax bands collection is null or empty. Can not calculate tax.");
            }
        }

        public decimal Calculate(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount is less than zero");
            }

            SetTaxBands();

            decimal tax = 0;

            decimal lowerThreshold = 0;

            foreach (var item in _taxBands)
            {
                if (amount <= 0)
                {
                    return tax;
                }

                decimal rangeSum = (item.Value - lowerThreshold);

                if (amount > rangeSum && rangeSum > 0)
                {
                    tax += rangeSum * (decimal)item.Key;
                    amount -= rangeSum;
                    lowerThreshold = item.Value;
                }
                else
                {
                    tax += amount * (decimal)item.Key;
                    amount -= item.Value;
                }
            }

            return tax;
        }
    }
}
