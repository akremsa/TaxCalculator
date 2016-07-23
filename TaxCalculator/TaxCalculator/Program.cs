using System;
using CalculatorService.Abstract;
using CalculatorService.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.Practices.Unity;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = new UnityContainer();

                RegisterDependencies(container);

                var a = container.Resolve<ICalculator>().Calculate(5000);

                Console.WriteLine(a);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<ICalculator, TaxCalculator>();
            container.RegisterType<ICalculatorRepository, CalculatorRepository>();
        }
    }
}
