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

        //First iteration: simply convert the numeral to its specified value
        private int ConvertSingleNumeral(string numerals)
        {
            var numeral = numerals[0];
            return ConvertToNumber(numeral);
        }

        //Second iteration: if there are only two numerals, then it's either an addition or 
        //subtraction, depending on whether it's in ascending or descending order of value. 
        //If the value is the same, it's an addition.
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

        //Third iteration: calculates numerals of any number of digits, checking ahead to see if the next numeral needs to be added or subtracted 
        //In order to avoid having to calculate parts of the string ahead of others, subtractions are performed one digit to the left, eg:
        //19 = 10 - 1 + 10 (X - I + X), rather than 10 + (10 - 1) / X + (X - I)
        //Otherwise the operation would be to resolve IX to 9 prior to adding it to the first X, and I wasn't keen on having to model 
        //the order of precedence.
        private int ConvertMultipleNumerals(string numerals)
        {
            var numbers = numerals.Select(x => ConvertToNumber(x)).ToArray();

            var operations = new List<Operation>();
            for (var i = 0; i < numbers.Length; i++)
            {
                int number = numbers[i];
                var nextIsHigher = NextNumberIsHigherValue(numbers, i, number);
                var op = new Operation()
                {
                    Operand = number,
                    Operator = nextIsHigher ? Operator.Subtract : Operator.Add
                };

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
            try
            {
                var numeral = Enum.Parse<Numerals>(letter.ToString());
                return (int)numeral;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Error parsing values as a roman numeral", letter.ToString());
            }
        }
    }
}