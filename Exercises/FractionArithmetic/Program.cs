using System;

namespace FractionArithmetic
{
    class MainClass
    {
        public static void Main()
        {
            Fraction<int> fractionInput = new Fraction<int>(2, 3);
            Fraction<int> fractionInputTwo = new Fraction<int>(3, 2);
            ITwoFractionCalculator<int> calculator = new FractionAdditionCalculator();
            Fraction<int> fractionOutput = calculator.Calculate(fractionInput, fractionInputTwo);

            if(fractionOutput.GetNumerator() != 13 || fractionOutput.GetDenominator() != 6)
                Console.WriteLine("Test Failed.");

            Console.WriteLine("Test Complete.");
        }
    }

    public enum OperationType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class Fraction<T>
    {
        T Numerator;
        T Denominator;

        public Fraction(T numerator, T denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public T GetNumerator()
        {
            return Numerator;
        }

        public T GetDenominator()
        {
            return Denominator;
        }
    }

    public interface ITwoFractionCalculator<T>
    {
        Fraction<T> Calculate(Fraction<T> inputOne, Fraction<T> inputTwo);
    }

    public abstract class TwoFractionCalculator: ITwoFractionCalculator<int>
    {
        OperationType OperationType;

        public TwoFractionCalculator(OperationType operationType)
        {
            OperationType = operationType;
        }

        public virtual Fraction<int> Calculate(Fraction<int> inputOne, Fraction<int> inputTwo)
        {
            if (inputOne.GetDenominator() == 0 || inputTwo.GetDenominator() == 0)
                throw new DivideByZeroException();

            switch (OperationType)
            {
                case OperationType.Subtraction:
                case OperationType.Addition:

                    int numeratorOneCalculated = inputOne.GetNumerator() * inputTwo.GetDenominator();
                    int numeratorTwoCalculated = inputTwo.GetNumerator() * inputOne.GetDenominator();
                    int denominatorCalculated = inputOne.GetDenominator() * inputTwo.GetDenominator();
                    return Calculate(numeratorOneCalculated, numeratorTwoCalculated, denominatorCalculated, denominatorCalculated);

                case OperationType.Multiplication:
                case OperationType.Division:
                    return Calculate(inputOne.GetNumerator(), inputTwo.GetNumerator(), inputOne.GetDenominator(), inputTwo.GetDenominator());
                default:
                    throw new NotImplementedException();
            }
        }

        public abstract Fraction<int> Calculate(
            int numeratorOneCalculated,
            int numeratorTwoCalculated,
            int denominatorOneCalculated,
            int denominatorTwoCalculated);
    }

    public class FractionAdditionCalculator: TwoFractionCalculator
    {
        public FractionAdditionCalculator() : base(OperationType.Addition) { }

        public override Fraction<int> Calculate(int numeratorOneCalculated, int numeratorTwoCalculated, int denominatorOneCalculated, int denominatorTwoCalculated)
        {
            return new Fraction<int>(numeratorOneCalculated + numeratorTwoCalculated, denominatorTwoCalculated);
        }
    }

    public class FractionSubtractionCalculator : TwoFractionCalculator
    {
        public FractionSubtractionCalculator() : base(OperationType.Subtraction) { }

        public override Fraction<int> Calculate(int numeratorOneCalculated, int numeratorTwoCalculated, int denominatorOneCalculated, int denominatorTwoCalculated)
        {
            return new Fraction<int>(numeratorOneCalculated - numeratorTwoCalculated, denominatorTwoCalculated);
        }
    }

    public class FractionMultiplicationCalculator : TwoFractionCalculator
    {
        public FractionMultiplicationCalculator() : base(OperationType.Multiplication) { }

        public override Fraction<int> Calculate(int numeratorOneCalculated, int numeratorTwoCalculated, int denominatorOneCalculated, int denominatorTwoCalculated)
        {
            return new Fraction<int>(numeratorOneCalculated * numeratorTwoCalculated, denominatorOneCalculated * denominatorTwoCalculated);
        }
    }

    public class FractionDivisionCalculator : TwoFractionCalculator
    {
        public FractionDivisionCalculator() : base(OperationType.Division) { }

        public override Fraction<int> Calculate(int numeratorOneCalculated, int numeratorTwoCalculated, int denominatorOneCalculated, int denominatorTwoCalculated)
        {
            return new Fraction<int>(numeratorOneCalculated * denominatorTwoCalculated, denominatorOneCalculated * numeratorTwoCalculated);
        }
    }
}
