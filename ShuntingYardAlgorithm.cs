using System;
using System.Collections.Generic;

namespace ACalc {
  public class ShuntingYardException : Exception {
    public ShuntingYardException() {}
    public ShuntingYardException(string message) : base(message) {}
    public ShuntingYardException(string message, Exception inner) : base(message, inner) {}
  } 

  sealed class ShuntingYardAlgorithm {
    public TokenStream Output {get {if (evaluated) return new TokenStream(output); else return new TokenStream(); }}
    public bool Evaluated {get { return evaluated; }}
    public ShuntingYardAlgorithm() {
      Reset();
    }

    // TODO: Do I need this?
    private static void FlushUntilPredicate(Predicate<Token> pToken) {
      throw new NotImplementedException();
    }

    public void Reset() {
      this.output = new TokenStream();
      this.stack = new Stack<OperatorToken>();
      this.tokenStream = new TokenStream();
    }

    public TokenStream Evaluate(TokenStream eval) {
      Reset();
      this.tokenStream = eval;
      foreach (Token token in tokenStream) {
      //  PrintStatus(token);

        if (token is NumberToken) {
          output.Add(token);
        }
        else if (token is OperatorToken) {
          OperatorToken top = null;
          OperatorToken opToken = token as OperatorToken;
          if (stack.Count > 0) top = stack.Peek();
          switch(opToken.Operator) {
            case OperatorType.OpenParenthesis: stack.Push(opToken); continue;
            case OperatorType.CloseParenthesis:
	     // System.Console.WriteLine("Found close parenthesis, popping until open parenthesis");
              while(stack.Count > 0 && stack.Peek().Operator != OperatorType.OpenParenthesis) 
              {
                output.Add(stack.Pop());
              }
             if (stack.Count > 0) {stack.Pop();}
              continue;
          
            default:
	    
	     while (top != null && stack.Count > 0 && top.Operator != OperatorType.OpenParenthesis && (
	       top.GetPrecedence() > opToken.GetPrecedence()||
	       top.GetPrecedence() == opToken.GetPrecedence() && opToken.GetAssociativity() == OperatorAssociativity.Left )) {
	   //    System.Console.WriteLine("Executing loop, stack has contents of higher precedence, popping until not true");
               output.Add(stack.Pop());
	       if (stack.Count > 0) top = stack.Peek();
	     }
	     stack.Push(opToken);
            continue;
          }
        }
      }
      while (stack.Count > 0) {
         output.Add(stack.Pop());
      }
      
      return output;
    }

  private void PrintStatus(Token token) {
    System.Console.WriteLine("\nEvaluated Token {0}", token);

    System.Console.Write("\nStack: ");
    foreach (OperatorToken to in stack) {
      System.Console.Write("{0} ", to);  
    }
    System.Console.Write("\nOutput: {0}\n",output);
  }

  private TokenStream output = new TokenStream();
  private Stack<OperatorToken> stack = new Stack<OperatorToken>();
  private TokenStream tokenStream = new TokenStream();
  private bool evaluated=true;
  }
}

