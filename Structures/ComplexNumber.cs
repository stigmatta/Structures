using System;
using System.Text.RegularExpressions;

interface ICalculator
{
    void Add(object v1, object v2);
    void Extract(object v1, object v2);
    void Multiply(object v1, object v2);
    void Divide(object v1, object v2);
}

namespace ComplexClasses
{
    public struct ComplexNumber
    {
        private static readonly string regPattern = @"^([-+]?\d+)([-+]\d*)i$";
        private string expression;

        public ComplexNumber(string value)
        {
            expression = string.Empty;
            Expression = value;
        }

        public string Expression
        {
            get { return expression; }
            set
            {
                value = value.Replace(" ", "");

                if (!Regex.IsMatch(value, regPattern))
                {
                    throw new ArgumentException("Number is not complex");
                }

                expression = value;
            }
        }

        public (double Real, double Imaginary) GetParts()
        {
            var match = Regex.Match(expression, regPattern);
            if (match.Success)
            {
                double real = double.Parse(match.Groups[1].Value);
                double imaginary = double.Parse(match.Groups[2].Value);
                return (real, imaginary);
            }
            throw new InvalidOperationException("Invalid complex number format.");
        }

        public override string ToString()
        {
            return expression;
        }
    }

    public struct ComplexCalculator : ICalculator
    {
        public void Add(object v1, object v2)
        {
            if (!(v1 is ComplexNumber c1) || !(v2 is ComplexNumber c2))
            {
                throw new Exception("Values are not complex numbers");
            }

            var (real1, imag1) = c1.GetParts();
            var (real2, imag2) = c2.GetParts();

            double realResult = real1 + real2;
            double imaginaryResult = imag1 + imag2;

            Console.WriteLine($"Addition Result: {realResult:F2}{(imaginaryResult >= 0 ? "+" : "")}{imaginaryResult:F2}i");
        }

        public void Extract(object v1, object v2)
        {
            if (!(v1 is ComplexNumber c1) || !(v2 is ComplexNumber c2))
            {
                throw new Exception("Values are not complex numbers");
            }

            var (real1, imag1) = c1.GetParts();
            var (real2, imag2) = c2.GetParts();

            double realResult = real1 - real2;
            double imaginaryResult = imag1 - imag2;

            Console.WriteLine($"Subtraction Result: {realResult:F2}{(imaginaryResult >= 0 ? "+" : "")}{imaginaryResult:F2}i");
        }

        public void Multiply(object v1, object v2)
        {
            if (!(v1 is ComplexNumber c1) || !(v2 is ComplexNumber c2))
            {
                throw new Exception("Values are not complex numbers");
            }

            var (real1, imag1) = c1.GetParts();
            var (real2, imag2) = c2.GetParts();

            double realResult = (real1 * real2) - (imag1 * imag2);
            double imaginaryResult = (real1 * imag2) + (imag1 * real2);

            Console.WriteLine($"Multiplication Result: {realResult:F2}{(imaginaryResult >= 0 ? "+" : "")}{imaginaryResult:F2}i");
        }

        public void Divide(object v1, object v2)
        {
            if (!(v1 is ComplexNumber c1) || !(v2 is ComplexNumber c2))
            {
                throw new Exception("Values are not complex numbers");
            }

            var (real1, imag1) = c1.GetParts();
            var (real2, imag2) = c2.GetParts();

            if (real2 == 0 && imag2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            double denominator = (real2 * real2) + (imag2 * imag2);
            double realResult = (real1 * real2 + imag1 * imag2) / denominator;
            double imaginaryResult = (imag1 * real2 - real1 * imag2) / denominator;

            Console.WriteLine($"Division Result: {realResult:F2}{(imaginaryResult >= 0 ? "+" : "")}{imaginaryResult:F2}i");
        }
    }
}
