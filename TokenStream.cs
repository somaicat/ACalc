using System;
using System.Collections.Generic;

namespace ACalc {
 public sealed class TokenStream : List<Token> {
    public TokenStream() : base() {}
    public TokenStream(TokenStream stream) : base(stream) {}

    public static TokenStream Tokenize(string str) {
    TokenStream tokenList = new TokenStream();
    OperatorType type;
    int number=0;
    bool numInProgress=false;
    bool numNegate = false;
    bool wasCloseParenthesis=false;
    foreach (char c in str) {
      if (TokenMethods.CharTokenType(c) == TokenType.Invalid && c != ' ') throw new TokenizationException("Encountered invalid character, not number operator or space");

      if ((type = TokenMethods.GetOperator(c)) != OperatorType.Invalid) // It's a valid operator token
      { 
        if (type == OperatorType.Subtract && !numInProgress && !wasCloseParenthesis) { // number is negative
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
	if (type == OperatorType.CloseParenthesis) wasCloseParenthesis=true;
	else wasCloseParenthesis=false;
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
 //  Console.WriteLine("{0} {1}", tokenList,tokenList.Count);

   return tokenList;
    }

    public override string ToString() 
    {
      string result = "";
      foreach (Token token in this) {
        result += token;
      }
      return result;
    }

  }

}
