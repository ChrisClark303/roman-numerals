using NUnit.Framework;

namespace RomanNumerals.Tests
{
    public class ParserTests
    {
        [TestCase("I", 1)]
        [TestCase("V", 5)]
        [TestCase("X", 10)]
        [TestCase("L", 50)]
        [TestCase("C", 100)]
        [TestCase("D", 500)]
        [TestCase("M", 1000)]
        public void Parse_SingleNumeral_GivesCorrectNumber(string numeralString, int expected)
        {
            var parser = new Parser();
            var number = parser.Parse(numeralString);

            Assert.AreEqual(expected, number);
        }

        [TestCase("II", 2)]
        [TestCase("VI", 6)]
        [TestCase("XI", 11)]
        [TestCase("LX", 60)]
        [TestCase("CL", 150)]
        [TestCase("DX", 510)]
        [TestCase("MV", 1005)]
        public void Parse_TwoNumerals_FirstLarger_AddsTogether(string numeralString, int expected)
        {
            var parser = new Parser();
            var number = parser.Parse(numeralString);

            Assert.AreEqual(expected, number);
        }

        [TestCase("IV", 4)]
        [TestCase("IX", 9)]
        [TestCase("XL", 40)]
        [TestCase("VC", 95)]
        [TestCase("CD", 400)]
        [TestCase("XD", 490)]
        [TestCase("IM", 999)]
        public void Parse_TwoNumerals_SecondLarger_SubtractsFirstFromSecond(string numeralString, int expected)
        {
            var parser = new Parser();
            var number = parser.Parse(numeralString);

            Assert.AreEqual(expected, number);
        }

        [TestCase("XXX", 30)]
        [TestCase("CCC", 300)]
        [TestCase("MMM", 3000)]
        public void Parse_MultipleNumerals_AllSame_AddsEach(string numeralString, int expected)
        {
            var parser = new Parser();
            var number = parser.Parse(numeralString);

            Assert.AreEqual(expected, number);
        }

        [TestCase("MMVII", 2007)]
        [TestCase("MMVIII", 2008)]
        [TestCase("MMLVIII", 2058)]
        [TestCase("MMCLVIII", 2158)]
        public void Parse_MultipleNumerals_AscendingValue_AddsEach(string numeralString, int expected)
        {
            var parser = new Parser();
            var number = parser.Parse(numeralString);

            Assert.AreEqual(expected, number);
        }

        [TestCase("MMIV", 2004)]
        [TestCase("MMCD", 2400)]
        [TestCase("MMCDLIV", 2454)]
        [TestCase("MCMXCVIII", 1998)]
        public void Parse_MultipleNumerals_MixedValueOrder_AddsAndSubtracts(string numeralString, int expected)
        {
            var parser = new Parser();
            var number = parser.Parse(numeralString);

            Assert.AreEqual(expected, number);
        }
    }
}