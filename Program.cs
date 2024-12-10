using System;
using ACalc;

  public class Program {
    public static void Main(string[] args) {
      ShuntingYardAlgorithm sya = new ShuntingYardAlgorithm();
      RPLEvaluator rplEvaluator = new RPLEvaluator();
      string inexpr;

      Console.Write("Enter expression: ");

      inexpr = Console.ReadLine();

      TokenStream inTokenStream = TokenStream.Tokenize(inexpr); // "3+4*2/(1-5)^2^3"

      Console.WriteLine("Input Token Stream: {0}", inTokenStream);

      var outTokenStream = sya.Evaluate(inTokenStream);

      Console.WriteLine("Output Token Stream: {0}", outTokenStream);
      Console.Write("\n\nEvaluating RPN Token Stream..\n\n");
      Console.WriteLine("Result was \"{0}\" = {1}\n", inexpr, rplEvaluator.Evaluate(outTokenStream));
    }
  }

