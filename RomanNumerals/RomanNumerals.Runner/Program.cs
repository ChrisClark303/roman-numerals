// See https://aka.ms/new-console-template for more information

var parser = new RomanNumerals.Parser();

start:

Console.WriteLine("Enter roman numerals: ");

var numerals = Console.ReadLine();
var number = parser.Parse(numerals!);
Console.WriteLine($"{numerals} = {number}");

goto start;

