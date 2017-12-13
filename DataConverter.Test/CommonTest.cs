using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataConverter.Helpers;
using NUnit.Framework;

namespace DataConverter.Test
{
    [TestFixture]
    public class CommonTest
    {
        [Test]
        public void ChangeExt()
        {
            string file = @"D:\Temp\1.txt";
            file = Path.ChangeExtension(file, "doc");
            Assert.True(Path.GetExtension(file) == ".doc");

            file = Path.ChangeExtension(file, string.Empty);
            Assert.True(Path.GetExtension(file) == "");
        }

        [Test]
        public void TestRightPad()
        {
            string s = StringHelper.StrLineRightPad(12, true, 1.79769E+308, 1.79769E+308, 1.79769E+308);
            Console.WriteLine(s);
        }

    }
}
