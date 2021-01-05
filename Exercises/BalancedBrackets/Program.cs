using System;
using System.Collections.Generic;

namespace BalancedBrackets
{
    class MainClass
    {
        public static void Main()
        {
            TestIsBracketBalanced(1, "{[()]}", true);
            TestIsBracketBalanced(2, "{[(])}", false);
            TestIsBracketBalanced(3, "{{[[(())]]}}", true);
            TestIsBracketBalanced(4, string.Empty, true);
            TestIsBracketBalanced(5, null, true);
            TestIsBracketBalanced(6, "", true);

            Console.WriteLine("Test Complete");
        }

        public static void TestIsBracketBalanced(int testIdentifier, string input, bool expectedResult, bool exceptionExpected = false)
        {
            try
            {
                bool actualResult = IsBracketBalanced(input);

                if (actualResult.Equals(expectedResult))
                    return;
            }
            catch
            {
                if (exceptionExpected)
                    return;
            }

            Console.WriteLine(string.Format("Test Case {0} failed.", testIdentifier));
        }

        public static bool IsBracketBalanced(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            Dictionary<char, char> closeBrackets = new Dictionary<char, char>();
            closeBrackets.Add('}', '{');
            closeBrackets.Add(']', '[');
            closeBrackets.Add(')', '(');

            HashSet<char> openBrackets = new HashSet<char>(closeBrackets.Values);
            Stack<char> holdOpenBrackets = new Stack<char>();

            foreach(char character in input.ToCharArray())
            {
                if (openBrackets.Contains(character))
                {
                    holdOpenBrackets.Push(character);
                    continue;
                }

                if (closeBrackets.ContainsKey(character))
                {
                    if (holdOpenBrackets.Count == 0)
                        return false;

                    char openBracket = holdOpenBrackets.Pop();

                    if (!closeBrackets[character].Equals(openBracket))
                        return false;
                }
            }

            return true;
        }
    }
}
