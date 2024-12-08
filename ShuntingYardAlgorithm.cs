using System.Collections.Generic;

namespace ACalc {

  static class ShuntingYardAlgorithm {
    public static IEnumerable<Token> Evaluate(IEnumerable<Token> tokenStream) {
      List<Token> output = new List<Token>();
      Stack<OperatorToken> stack = new Stack<OperatorToken>();

      foreach (Token token in tokenStream) {

          System.Console.Write("Stack: ");
	  foreach (OperatorToken to in stack) {System.Console.Write("{0} ", to.Operator);}
	  System.Console.Write("\nOutput: ");
	  foreach (Token lo in output) {
	    if (lo is OperatorToken)
	    System.Console.Write("{0} ", ((OperatorToken)lo).Operator);
	    else 
	    System.Console.Write("{0} ", ((NumberToken)lo).Number);
	  }
	  System.Console.Write("\n\n");


        if (token is NumberToken) {
          output.Add(token);
        //  System.Console.WriteLine("Added to output {0}", ((NumberToken)token).Number);
        }
        else if (token is OperatorToken) {
          //System.Console.WriteLine("Working on {0}", ((OperatorToken)token).Operator);
                  OperatorToken top = null;
          OperatorToken opToken = token as OperatorToken;
          OperatorToken tmpOp = null;
          if (stack.Count > 0) top = stack.Peek();
          switch(opToken.Operator) {
            case OperatorType.OpenParenthesis: stack.Push(opToken); continue;
            case OperatorType.CloseParenthesis: 
              while(stack.Count > 0) 
              {
	
		if (stack.Peek().Operator == OperatorType.OpenParenthesis) { stack.Pop(); continue; } 
                output.Add(stack.Pop());
              }
            //  if (stack.Count > 0) {stack.Pop();
	     // System.Console.WriteLine("PopedP {0}",stack.Count);}
//              if (stack.Count > 0 && stack.Peek().Operator == OperatorType.OpenParenthesis) stack.Pop();
// insert check for not finding an open parenthesis 
              continue;
          
            default:
	    
	     while (top != null && stack.Count > 0 && top.GetPrecedence() > opToken.GetPrecedence()) {
               output.Add(stack.Pop());
	       if (stack.Count > 0) top = stack.Peek();
	     }
	     stack.Push(opToken);
	 /*   if (stack.Count == 0) {stack.Push(opToken); continue;}
	    tmpOp=stack.Pop();
              while (stack.Count >= 0 && tmpOp.GetPrecedence() > top.GetPrecedence() || (tmpOp.GetPrecedence() == top.GetPrecedence() && tmpOp.Operator != OperatorType.Exponent)) {
		output.Add(tmpOp);
                tmpOp = stack.Pop();
	      }

	      stack.Push(opToken);*

/*            if (top == null || top.Operator == OperatorType.OpenParenthesis) {
              stack.Push(opToken);
            }
////
            else if (opToken.GetPrecedence() < top.GetPrecedence() || (opToken.GetPrecedence() == top.GetPrecedence() && opToken.Operator == OperatorType.Exponent))
              stack.Push(opToken);
            else if (opToken.GetPrecedence() > top.GetPrecedence() || (opToken.GetPrecedence() == top.GetPrecedence() && opToken.Operator != OperatorType.Exponent)) {
              tmpOp = opToken;
              while (stack.Count >= 0) {
                top = stack.Pop();
                if (opToken.GetPrecedence() > top.GetPrecedence() || (opToken.GetPrecedence() == top.GetPrecedence() && opToken.Operator != OperatorType.Exponent)) {
                  output.Add(top);
                }
              }
              stack.Push(tmpOp);
            }*/
  //            if (stack.Count > 0) tmpOp = stack.Pop();
//              while (stack.Count >= 0 && tmpOp.GetPrecedence() > top.GetPrecedence() || (tmpOp.GetPrecedence() == top.GetPrecedence() && tmpOp.Operator != OperatorType.Exponent));

            continue;
          }
        }
      }
      while (stack.Count > 0) {
         output.Add(stack.Pop());
      }
/*
          if (opToken.Operator == OperatorType.OpenParenthesis) stack.Push(opToken);
System.Console.WriteLine("Operator");
          while(top != null && top.Operator != OperatorType.OpenParenthesis && 
            (top.GetPrecedence() < opToken.GetPrecedence() || (top.GetPrecedence() == opToken.GetPrecedence() && opToken.Operator != OperatorType.Exponent)))
          {
            output.Add(stack.Pop());
            stack.Push(opToken);
          }
*/
      
      return output;
    }
  }
}

