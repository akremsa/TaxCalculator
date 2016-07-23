using System.Collections.Generic;
using DataAccessLayer.Abstract;

namespace DataAccessLayer.Concrete
{
    public class CalculatorRepository: ICalculatorRepository
    {
        public Dictionary<double, decimal> GetTaxBands()
        {
            //let`s suppose here we read values from database and construct a dictionary

            Dictionary<double, decimal> taxBands = new Dictionary<double, decimal>();
            taxBands.Add(.10, 5030m);
            taxBands.Add(.14, 8630m);
            taxBands.Add(.23, 14040m);
            taxBands.Add(.30, 21260m);
            taxBands.Add(.33, 40270m);
            taxBands.Add(.45, 40270m);

            return taxBands;
        }
    }
}
