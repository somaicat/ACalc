using System.Collections.Generic;
namespace ACalc {

  class RPLEvaluator {

    public int Evaluate(TokenStream tokenStream) {
    int result = 0;

    int index = 0;
  //  tokenStream;
  //  Stack<Token> tokenStack = new Stack<Token>(tokenStream);
    Stack<NumberToken> numStack = new Stack<NumberToken>();
   // while(index < tokenStack.Length) {
    foreach (Token token in tokenStream) 
    {
      System.Console.WriteLine("Processing {0}", token);
      if (token is NumberToken) {
	System.Console.WriteLine("Pushing {0}", (token as NumberToken).Number);
	numStack.Push(token as NumberToken);
      }
      else if (token is OperatorToken) 
      {
	OperatorToken opToken = token as OperatorToken;
	NumberToken right = numStack.Pop();
        NumberToken left = numStack.Pop();

	numStack.Push(NumberToken.Evaluate(opToken.Operator, left,right));

//	int num=0;
//	if (opToken.Operator == OperatorType.Exponent) {
 //         right = numStack.Pop();
//	  left = numStack.Pop();
//	}
//	else {
       //   right = numStack.Pop();
//	  left = numStack.Pop();
//	}

	
      }
//    System.Console.WriteLine("{0}", tokenStack.Pop());
    }
   /* while (index < tokenStream.Count);
       Token token = tokenStream[index];    
       if (token is OperatorToken) {
         OperatorToken opToken = token as OperatorToken;
	 NumberToken left = tokenStream[index
       }
    }*/
  return numStack.Peek().Number;  
  }
}
}

