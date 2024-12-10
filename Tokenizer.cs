using System; 
using System.Collections.Generic;

namespace ACalc {

  public enum TokenType {
    Number,
    Operator,
    Invalid
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

    public static int GetNumber(char c) { // Yes I know .net provides methods much like this
      switch (c)
      {
        case '0' : return 0;
        case '1' : return 1;
        case '2' : return 2;
        case '3' : return 3;
        case '4' : return 4;
        case '5' : return 5;
        case '6' : return 6;
        case '7' : return 7;
        case '8' : return 8;
        case '9' : return 9;
        default : return -1;
      }
    }

    public static TokenType CharTokenType(char c) {
      if (GetOperator(c) != OperatorType.Invalid)
        return TokenType.Operator;
      if (GetNumber(c) != -1)
        return TokenType.Number;
      return TokenType.Invalid;
    }

    public static int GetPrecedence(this OperatorType type) {
      switch(type) 
      {
        case OperatorType.OpenParenthesis: return 3;
        case OperatorType.CloseParenthesis: return 3;
        case OperatorType.Exponent: return 2;
        case OperatorType.Multiply: return 1;
        case OperatorType.Divide: return 1;
        case OperatorType.Add: return 0;
        case OperatorType.Subtract: return 0;
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
    private uint AddNumber(uint existing, uint num) {
      return existing * 10 + num;
    }
  }
  
  public class NumberToken : Token {
    public int Number {get;set;}=0;
    public NumberToken() :base () {}
    public NumberToken(int num) : base() 
    {
      this.Type = TokenType.Number;
      this.Number = num; 
    }
    public static NumberToken Evaluate(OperatorType type, NumberToken left, NumberToken right) {
      NumberToken result = new NumberToken();
      System.Console.WriteLine("Evaluating {0} {1} {2}", left.Number, type, right.Number);
      switch (type) {
        case OperatorType.Exponent: result.Number = left.Number ^ right.Number; break;
        case OperatorType.Multiply: result.Number = left.Number * right.Number; break;
        case OperatorType.Divide: result.Number = left.Number / right.Number; break;
        case OperatorType.Add: result.Number = left.Number + right.Number; break;
        case OperatorType.Subtract: result.Number = left.Number - right.Number; break;
        default: throw new TokenizationException("Invalid Evaluation, bad OperatorType"); // TODO: Improve this
      }
      return result;
    }
  }

  public class OperatorToken : Token {
    public OperatorType Operator {get;set;}
    public OperatorToken(OperatorType type) : base() 
    {
      this.Operator = type; 
      this.Type = TokenType.Operator;
    }
    public int GetPrecedence() {
      return this.Operator.GetPrecedence();
    }
  }

  public class TokenStream : List<Token> {
    public TokenStream() : base() {}
    public TokenStream(TokenStream stream) : base(stream) {}

    public static TokenStream Tokenize(string str) {
    TokenStream tokenList = new TokenStream();
    OperatorType type;
    int number=0;
    bool numInProgress=false;
    bool numNegate = false;
    Console.WriteLine("Running prototype tokenizer on \"{0}\"", str);

    foreach (char c in str) {
      if (TokenMethods.CharTokenType(c) == TokenType.Invalid && c != ' ') throw new TokenizationException("Encountered invalid character, not number operator or space");

      if ((type = TokenMethods.GetOperator(c)) != OperatorType.Invalid) // It's a valid operator token
      { 
        if (type == OperatorType.Subtract && !numInProgress) { // number is negative
          numNegate = !numNegate;
          continue;
        }
        if (numInProgress) { // Unfinished number token
          if (numNegate) number = -number;
          tokenList.Add(new NumberToken(number));
          number = 0;
          numInProgress=false;
          numNegate=false;
        }
        tokenList.Add(new OperatorToken(type));
        continue;
      }
        
      int num = (int) TokenMethods.GetNumber(c);
      if (num != -1) {
      number = number * 10 + num;
      numInProgress = true;
      }
    }

   if (numInProgress) { // We have an outstanding number token
     if (numNegate) number = -number;
       tokenList.Add(new NumberToken(number));
     }
   Console.WriteLine("{0} {1}", tokenList,tokenList.Count);

   return tokenList;
//    throw new TokenizationException("Method not implemented yet");
    }

/*    public override string ToString() 
    {
      if (this is NumberToken != null)
      return string.Format("TKN: {0} {1}", this,(this as NumberToken).Number);
      else if (this is OperatorToken != null)
      return string.Format("TKN: {0} {1}", this,(this as OperatorToken).Operator);
      
      return "";
    }
*/
  }
}
