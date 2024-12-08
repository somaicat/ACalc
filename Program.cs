using System;
using ACalc;

  public class Program {
    public static void Main(string[] args) {
      var list = Token.Tokenize("3+4*2/(1-5)^2^3");
//      var list = Token.Tokenize("30+44*-200/(10-5)^2^3");

      foreach (var token in list) {
        if (token is NumberToken)
        Console.WriteLine("{0}: {1}",token, ((NumberToken) token).Number);
        else if (token is OperatorToken)
        Console.WriteLine("{0}: {1}",token,((OperatorToken) token).Operator);
        else
        Console.WriteLine("{0} (unknown token subclass?)");
      }
//      System.Collections.Generic.List<int> nums = new System.Collections.Generic.List<int> {5,10,70};
        list = ShuntingYardAlgorithm.Evaluate(list);
      foreach (var token in list) {
        if (token is NumberToken)
        Console.WriteLine("{0}: {1}",token, ((NumberToken) token).Number);
        else if (token is OperatorToken)
        Console.WriteLine("{0}: {1}",token,((OperatorToken) token).Operator);
        else
        Console.WriteLine("{0} (unknown token subclass?)");
      }
//      System.Collections.Generic.List<int> nums = new System.Collections.Generic.List<int> {5,10,70};
//      foreach (var n in nums) {
//        System.Console.WriteLine("{0}", n);
    }
  }

