using System.Diagnostics;

namespace LearnApiUsingMiddleware.Demo
{
    public interface IPrinter
    {
        public void Print();
    }

    public class Printer : IPrinter
    {
        public void Print()
        {
            Debug.WriteLine("This is from middleware interfaces");
        }
    }
}