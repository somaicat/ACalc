namespace ACalc {
  public enum TokenType {
    Number,
    Operator,
    Parenthesis,
    Unknown
  }

  public enum OperatorType {
    Add,
    Subtract,
    Divide,
    Multiply,
    Exponent
  }

  static class MathMethods {
    public static int GetPrecedence(this OperatorType type) {
      switch(type) 
      {
        case OperatorType.Exponent: return 0;
        case OperatorType.Multiply: return 1;
        case OperatorType.Divide: return 1;
        case OperatorType.Add: return 2;
        case OperatorType.Subtract: return 2;
      }
      return -1;
    }
  }
  
  public abstract class Token {
    public TokenType Type {get;set;}
  }

  public class NumberToken : Token {
    public uint Number {get;set;}
  }

  public class ParenthesisToken : Token {
    public uint Number {get;set;}
  }

  public class OperatorToken : Token {
    public uint OperatorType {get;set;}
    public NumberToken left{get;set;}
    public NumberToken right {get;set;}
  }

  // Abstract Syntax Tree

  //public enum NodeType {
  //}

  public class Program {
    public static void Main(string[] args) {
//      System.Collections.Generic.List<int> nums = new System.Collections.Generic.List<int> {5,10,70};
//      foreach (var n in nums) {
//        System.Console.WriteLine("{0}", n);
    }
  }
}

