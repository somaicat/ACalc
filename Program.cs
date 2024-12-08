using System;
using ACalc;

  public class Program {
    public static void Main(string[] args) {
      var list = Token.Tokenize("1+1*blah");

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

