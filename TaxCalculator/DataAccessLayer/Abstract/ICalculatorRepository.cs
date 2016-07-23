using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface ICalculatorRepository
    {
        Dictionary<double, decimal> GetTaxBands();
    }
}
