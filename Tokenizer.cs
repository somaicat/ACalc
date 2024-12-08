using System; 
using System.Collections.Generic;

namespace ACalc {
  public enum TokenType {
    Number,
    Operator,
    Unknown
  }

  public enum OperatorType {
    Add,
    Subtract,
    Divide,
    Multiply,
    Exponent,
    OpenParenthesis,
    CloseParenthesis,
    Invalid
  }

  static class TokenMethods {
    public static OperatorType GetOperator(char c) {
      switch (c) 
      {
        case '+' : return OperatorType.Add;
        case '-' : return OperatorType.Subtract;
        case '/' : return OperatorType.Divide;
        case '*' : return OperatorType.Multiply;
        case '^' : return OperatorType.Exponent;
        case '(' : return OperatorType.OpenParenthesis;
        case ')' : return OperatorType.CloseParenthesis;
        default : return OperatorType.Invalid;
      }
    }

    public static int GetPrecedence(this OperatorType type) {
      switch(type) 
      {
        case OperatorType.OpenParenthesis: return 0;
        case OperatorType.CloseParenthesis: return 0;
        case OperatorType.Exponent: return 1;
        case OperatorType.Multiply: return 2;
        case OperatorType.Divide: return 2;
        case OperatorType.Add: return 3;
        case OperatorType.Subtract: return 3;
      }
      return -1;
    }
  }

  public class TokenizationException : Exception {
    public TokenizationException() {}
    public TokenizationException(string message) : base(message) {}
    public TokenizationException(string message, Exception inner) : base(message, inner) {}
  } 
  
  public abstract class Token {
    public TokenType Type {get;set;}
    public static IEnumerable<Token> Tokenize(string str) {
      List<Token> tokenList = new List<Token>();
      OperatorType type;

      foreach (char c in str) {
        if ((type = TokenMethods.GetOperator(c)) != OperatorType.Invalid) // It's a valid operator token
        { 
          OperatorToken token = new OperatorToken(type);
          tokenList.Add(token);
          continue;
        }
        
//        int num = 
     }
    throw new TokenizationException("Method not implemented yet");
    }
  }

  public class NumberToken : Token {
    public int Number {get;set;}
    public NumberToken(int num) : base() 
    {
      this.Type = TokenType.Number;
      this.Number = num; 
    }
  }

  public class OperatorToken : Token {
    public OperatorType Operator {get;set;}
    public OperatorToken(OperatorType type) : base() 
    {
      this.Operator = type; 
      this.Type = TokenType.Operator;
    }
  }

  

}
