using System;

namespace Demo.Library
{
    public class Class1
    {
        // ReSharper disable once UnusedMember.Global
        public void MyMethod()
        {
        }

        public void TestMethod(string input)
        {
            if (input == null)
            {
                var substring = input.Substring(5);

                return;
            }

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            input.Substring(10);

            return;
        }
    }
}
