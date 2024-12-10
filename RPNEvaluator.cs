using System.Collections.Generic;
using System;

namespace ACalc {

  public class RPNEvaluationException : Exception {
    public RPNEvaluationException() {}
    public RPNEvaluationException(string message) : base(message) {}
    public RPNEvaluationException(string message, Exception inner) : base(message, inner) {}
  } 

  sealed class RPNEvaluator {

    public int Evaluate(TokenStream tokenStream) {
      Stack<NumberToken> numStack = new Stack<NumberToken>();
      try {
        foreach (Token token in tokenStream) 
        {
          if (token is NumberToken) {
	    numStack.Push(token as NumberToken);
          }
          else if (token is OperatorToken) 
          {
	    OperatorToken opToken = token as OperatorToken;
  	    NumberToken right = numStack.Pop();
            NumberToken left = numStack.Pop();
            System.Console.Write("Evaluating {0} {1} {2}", left.Number, opToken.Operator, right.Number);
	    numStack.Push(NumberToken.Evaluate(opToken.Operator, left,right));
          }
        }
      }
      catch (InvalidOperationException ex) {
	throw new RPNEvaluationException("RPNEvaluation Failed \"" + ex.Message + "\"");
      }
      catch (TokenEvaluationException ex) {
        throw new RPNEvaluationException("Token Evaluation Failed \"" + ex.Message + "\"");
      }
      return numStack.Peek().Number;  
    }
  }
}

