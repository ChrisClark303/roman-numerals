namespace RomanNumerals
{
    public class Parser
    {
        public int Parse(string numerals)
        {
            var number = 0;
            switch (numerals.Length)
            {
                case 1:
                    number = ConvertSingleNumeral(numerals);
                break;
                case 2:
                    number = ConvertDoubleNumeral(numerals);
                break;
                default:
                    number = ConvertMultipleNumerals(numerals);
                break;
            }

            return number;
        }

        private int ConvertSingleNumeral(string numerals)
        {
            var numeral = numerals[0];
            return ConvertToNumber(numeral);
        }

        private int ConvertDoubleNumeral(string numerals)
        {
            var numeral1 = ConvertToNumber(numerals.First());
            var numeral2 = ConvertToNumber(numerals.Last());

            if (numeral2 > numeral1)
            {
                return numeral2 - numeral1;
            }

            return numeral1 + numeral2;
        }

        private int ConvertMultipleNumerals(string numerals)
        {
            var numbers = numerals.Select(x => ConvertToNumber(x)).ToArray();

            var operations = new List<Operation>();
            for (var i = 0; i < numbers.Length; i++)
            {
                int number = numbers[i];
                var op = new Operation()
                {
                    Operand = number,
                    Operator = Operator.Add
                };
                if (NextNumberIsHigherValue(numbers, i, number))
                {
                    op.Operator = Operator.Subtract;
                }
                operations.Add(op);
            }

            int total = 0;
            foreach (var op in operations)
            {
                switch (op.Operator)
                {
                    case Operator.Add:
                        total += op.Operand;
                        break;
                    case Operator.Subtract:
                        total -= op.Operand;
                        break;
                }
            }

            return total;
        }

        private static bool NextNumberIsHigherValue(int[] numbers, int i, int number)
        {
            return i < numbers.GetUpperBound(0) && number < numbers[i + 1];
        }

        private int ConvertToNumber(char letter)
        {
            var numeral = Enum.Parse<Numerals>(letter.ToString());
            return (int)numeral;
        }
    }
}