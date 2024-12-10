using System.Collections.Generic;
namespace ACalc {

  sealed class RPLEvaluator {

    public int Evaluate(TokenStream tokenStream) {
      Stack<NumberToken> numStack = new Stack<NumberToken>();
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

	  numStack.Push(NumberToken.Evaluate(opToken.Operator, left,right));

        }
      }
      return numStack.Peek().Number;  
    }
  }
}

