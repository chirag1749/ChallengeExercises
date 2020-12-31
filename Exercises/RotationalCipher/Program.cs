using System;
using System.Text;

namespace RotationalCipher
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            TestRotationalCipher(1, "Zebra-493?", 3, "Cheud-726?");
            TestRotationalCipher(2, "abcdefghijklmNOPQRSTUVWXYZ0123456789", 39, "nopqrstuvwxyzABCDEFGHIJKLM9012345678");
            TestRotationalCipher(3, string.Empty, 39, "nopqrstuvwxyzABCDEFGHIJKLM9012345678", true);
            TestRotationalCipher(4, "1", 0, string.Empty, true);
            Console.WriteLine("Test Complete");
        }

        private static void TestRotationalCipher(int testIdentifier, String input, int rotationalFactor, string outputExpected, bool exception = false)
        {
            // Write your code he
            try
            {
                string result = RotationalCipher(input, rotationalFactor);
                if (result == outputExpected)
                    return; 
            }
            catch
            {
                if (exception)
                    return;
            }

            Console.WriteLine(string.Format("Test Case: {0} failed.", testIdentifier));
        }

        private static string RotationalCipher(String input, int rotationFactor)
        {
            if (string.IsNullOrEmpty(input) || input.Length == 0 || input.Length > 1000000)
                throw new ArgumentException("Input Length out of range.");
            if(rotationFactor < 1 || rotationFactor > 1000000)
                throw new ArgumentException("Rotation Factor out of range.");

            // Write your code here
            int asciiNumberForLowerCaseA = Convert.ToInt16('a');
            int asciiNumberForLowerCaseZ = Convert.ToInt16('z');
            int asciiNumberForUpperCaseA = Convert.ToInt16('A');
            int asciiNumberForUperCaseZ = Convert.ToInt16('Z');
            int asciiNumberForZero = Convert.ToInt16('0');
            int asciiNumberForNine = Convert.ToInt16('9');

            StringBuilder stringBuilder = new StringBuilder(input.Length);

            foreach(char letter in input.ToCharArray())
            {
                int asciiNumber = Convert.ToInt16(letter);
                int asciiNumberRotate = asciiNumber;

                if (asciiNumberForLowerCaseA <= asciiNumber && asciiNumberForLowerCaseZ >= asciiNumber)
                    asciiNumberRotate = GetAsciiCipher(asciiNumber, asciiNumberForLowerCaseA, asciiNumberForLowerCaseZ, rotationFactor);
                else if (asciiNumberForUpperCaseA <= asciiNumber && asciiNumberForUperCaseZ >= asciiNumber)
                    asciiNumberRotate = GetAsciiCipher(asciiNumber, asciiNumberForUpperCaseA, asciiNumberForUperCaseZ, rotationFactor);
                else if (asciiNumberForZero <= asciiNumber && asciiNumberForNine >= asciiNumber)
                    asciiNumberRotate = GetAsciiCipher(asciiNumber, asciiNumberForZero, asciiNumberForNine, rotationFactor);

                stringBuilder.Append(Convert.ToChar(asciiNumberRotate));
            }

            return stringBuilder.ToString();
        }

        private static int GetAsciiCipher(int inputAscii, int minAscii, int maxAscii, int rotationFactor)
        {
            int newAscii = inputAscii + rotationFactor;

            while(newAscii > maxAscii)
                newAscii = newAscii - (maxAscii - minAscii + 1);
            
            return newAscii;
        }
    }
}
