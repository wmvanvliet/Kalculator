using System;
using System.IO;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace Kerbulator {
	public class JITFunction {
		private string id;
		private Dictionary<string, Object> locals = null;
		private Kerbulator kalc;
		private Queue<Token> tokens;

		private ConstantExpression thisExpression;
		//private ConstantExpression kalcExpression;

		List<string> ins;
		List<string> outs;
		List<string> inDescriptions;
		List<string> outDescriptions;

		private bool inError = false;
		private string errorString = "";

		private Func<Object> compiledFunction = null;

		public JITFunction(string id, string expression, Kerbulator kalc) { 
			this.id = id;

			this.ins = new List<string>();
			this.outs = new List<string>();
			this.inDescriptions = new List<string>();
			this.outDescriptions = new List<string>();

			this.locals = new Dictionary<string, Object>();
			this.thisExpression = Expression.Constant(this);

			this.kalc = kalc;
			//this.kalcExpression = Expression.Constant(kalc);

			try {
				Tokenizer tok = new Tokenizer("unnamed");
				tok.Tokenize(expression);
				tokens = tok.tokens;
			} catch(Exception e) {
				inError = true;
				errorString = e.Message;
			}
		}

		public string Id {
			get { return id; }
			protected set {}
		}

		public List<string> Ins {
			get { return ins; }
			protected set {}
		}

		public List<string> InDescriptions {
			get { return inDescriptions; }
			protected set {}
		}

		public List<string> Outs {
			get { return outs; }
			protected set {}
		}

		public List<string> OutDescriptions {
			get { return outDescriptions; }
			protected set {}
		}

		public bool InError {
			get { return inError; }
			set { inError = value; if(!value) errorString = ""; }
		}

		public string ErrorString {
			get { return errorString; }
			set { errorString = (string)value; Kerbulator.DebugLine(value); inError = true; }
		}

		virtual public List<Object> Execute() {
			return Execute(new List<Object>());
		}

		public static JITFunction FromFile(string filename, Kerbulator kalc) {
			StreamReader file = File.OpenText(filename);
            string contents = file.ReadToEnd();
            file.Close();
			JITFunction f = new JITFunction(Path.GetFileNameWithoutExtension(filename), contents, kalc);
			try {
				f.Parse();
			} catch(Exception e) {
				f.inError = true;
				f.errorString = e.Message;
				//throw e;
			}
			return f;
		}

		public static Dictionary<string, JITFunction> Scan(string dir, Dictionary<string, JITFunction> functions, Kerbulator kalc) {
			foreach(string filename in Directory.GetFiles(dir, "*.math")) {
				JITFunction f = FromFile(filename, kalc);
				if( functions.ContainsKey(f.Id) )
					functions[f.Id] = f;
				else
					functions.Add(f.Id, f);
			}

			return functions;
		}

		public List<Object> Execute(List<Object> arguments) {
			if(compiledFunction == null)
				throw new Exception("Cannot execute function "+ this.id +": function is not compiled yet.");

			// (Re)initialize the local variable dictionary to contain just the input arguments
			this.locals = new Dictionary<string, Object>();
			for(int i=0; i<ins.Count; i++)
				locals.Add(ins[i], arguments[i]);

			// Run the function
			List<Object> result = new List<Object>();
			Object lastVal = compiledFunction();

			// Fetch the output variables from the locals dictionary
			if(outs.Count > 0) {
				foreach(string id in outs) {
					if(!locals.ContainsKey(id))
						throw new Exception("output variable "+ id +" is not defined in the code of function "+ this.id);
					result.Add(locals[id]);
				}
			} else
				// No outputs specified, just return the value yielded by the last statement
				result.Add(lastVal);

			return result;
		}

		// With .NET 4.0, there is a BlockExpression. For now, we must hack our own
		// implementation to execute multiple expressions.
		public Object ExecuteBlock(Object[] statementResults) {
			return statementResults[statementResults.Length-1];
		}

		public void Parse() {
			// Skip leading whitespace
			while(tokens.Peek().type == TokenType.END)
				Consume();

			// Parse in: statements
			while(tokens.Peek().type == TokenType.IN) {
				Consume();
				Token id = Consume(TokenType.IDENTIFIER);

				if(tokens.Peek().type == TokenType.TEXT) {
					inDescriptions.Add( tokens.Dequeue().val );
				}

				Consume(TokenType.END);
				ins.Add(id.val);
				Kerbulator.DebugLine("Found IN statement for "+ id.val);
			}

			// Skip whitespace
			while(tokens.Peek().type == TokenType.END)
				Consume();

			// Parse out: statements
			while(tokens.Peek().type == TokenType.OUT) {
				Consume();
				Token id = Consume(TokenType.IDENTIFIER);

				if(tokens.Peek().type == TokenType.TEXT)
					outDescriptions.Add( tokens.Dequeue().val );
				else
					outDescriptions.Add("");

				Consume(TokenType.END);
				outs.Add(id.val);
				Kerbulator.DebugLine("Found OUT statement for "+ id.val);
			}

			Kerbulator.DebugLine("");

			// Parse all other statements
			List<Expression> statements = new List<Expression>();
			while(tokens.Count > 0) {
				statements.Add( ParseStatement() );
				Consume(TokenType.END);
			}
			
			if(statements.Count == 0)
				throw new Exception("In function "+ this.id +": function does not contain any statemtns (it's empty)");

			// Create expression that will execute all the statements
			Expression functionExpression = Expression.Call(
				thisExpression,
				typeof(JITFunction).GetMethod("ExecuteBlock"),
				Expression.NewArrayInit(typeof(Object), statements)
			);

			compiledFunction = Expression.Lambda<Func<Object>>(functionExpression).Compile();
		}

		public Object SetLocal(string id, Object val) {
			if(locals.ContainsKey(id))
				locals[id] = val;
			else
				locals.Add(id, val);

			return val;
		}

		public Object UnpackList(Object result, List<string> ids) {
			if(result.GetType() != typeof(Object[]))
				throw new Exception("In function "+ this.id +": expression needed to yield "+ ids.Count +" values, but yielded only 1");

			Object[] list = (Object[]) result;

			if(ids.Count != list.Length)
				throw new Exception("In function "+ this.id +": expression needed to yield "+ ids.Count +" values, but yielded only "+ list.Length);	
			
			for(int i=0; i<list.Length; i++)
				SetLocal(ids[i], list[i]);

			return list;
		}

		public Expression ParseStatement() {
			if(tokens.Peek().type == TokenType.END)
				return null;

			List<string> ids = new List<string>();

			while(true) {
				Token t = Consume(TokenType.IDENTIFIER);
				ids.Add(t.val);
				if(tokens.Peek().val == "=")
					break;
				else
					Consume(",");
			}

			Consume("=");
			Expression expr = ParseExpression();

			// If the function has no outputs specified, use the result
			// of the last statement as output
			if(outs.Count == 0) {
				outs = ids;
				outDescriptions = new List<string>(outs.Count);
				for(int i=0; i<outs.Count; i++)
					outDescriptions.Add("");
			}

			if(ids.Count == 1) {
				return Expression.Call(
					thisExpression,
					typeof(JITFunction).GetMethod("SetLocal"),
					Expression.Constant(ids[0]),
					expr
				);
			} else {
				return Expression.Call(
					thisExpression,
					typeof(JITFunction).GetMethod("UnpackList"),
					expr,
					Expression.Constant(ids)
				);
			}
		}

		public Expression ParseExpression() {
			Stack<Expression> expr = new Stack<Expression>();
			Stack<Operator> ops = new Stack<Operator>();

			bool end = false; // If ever set to true, this is the end of the expression

			while(!end && tokens.Count > 0 && tokens.Peek().type != TokenType.END) {
				Token t = tokens.Peek();
				Kerbulator.DebugLine("Token: "+ Enum.GetName(typeof(TokenType), t.type) +": "+ t.val);

				switch(t.type) {
					case TokenType.NUMBER:
						ParseNumber(expr);
						break;
					case TokenType.OPERATOR:
						ParseOperator(expr, ops);
						break;
					case TokenType.BRACE:
						end = ParseBrace(expr, ops);
						break;
					case TokenType.LIST:
						end = ParseList(expr);
						break;
					case TokenType.IDENTIFIER:
						ParseIdentifier(expr, ops);
						break;
					case TokenType.COMMA:
						end = true;
						break;
					default:
						Consume();
						break;
				}

			}

			// Handle remaining ops
			while(ops.Count > 0) {
				Operator op = ops.Pop();
				expr.Push( ExecuteOperator(op, expr, ops) );
			}

			if(expr.Count > 1)
				throw new Exception("Malformed expression");

			return expr.Pop();
		}

		private Token Consume() {
			if(tokens.Count == 0)
				throw new Exception("Unexpected end of expression.");

			return tokens.Dequeue();
		}

		private Token Consume(string val) {
			if(tokens.Count == 0)
				throw new Exception("Expected: "+ val);

			Token t = tokens.Dequeue();
			if(!String.Equals(t.val, val))
				throw new Exception("Expected: "+ val);

			return t;
		}

		private Token Consume(TokenType type) {
			if(tokens.Count == 0)
				throw new Exception("Expected "+ Enum.GetName(typeof(TokenType), type));

			Token t = tokens.Dequeue();
			if(t.type != type)
				throw new Exception("Expected "+ Enum.GetName(typeof(TokenType), type));

			return t;
		}

		private bool PossiblyValidExpression(Stack<Expression>expr, Stack<Operator> ops) {
			if(expr.Count == 0 && ops.Count == 0)
				return false;

			int required = 0;
			int supplied = expr.Count;

			foreach(Operator op in ops) {
				Kerbulator.DebugLine(op.id);
				supplied ++;
				if(op.arity == Arity.BINARY)
					required += 2;
				else
					required += 1;
			}

			Kerbulator.DebugLine("required: "+ required +", supplied: "+ supplied);
			return supplied == required + 1;
		}

		private void ParseNumber(Stack<Expression> expr) {
			Token t = tokens.Dequeue();
			Kerbulator.DebugLine("Pushing "+ t.val);
			expr.Push(
				Expression.Convert(
					Expression.Constant(Double.Parse(t.val, System.Globalization.CultureInfo.InvariantCulture)),
					typeof(Object)
				)
			);
			/*
			expr.Push(
				Expression.Constant(Double.Parse(t.val, System.Globalization.CultureInfo.InvariantCulture))
			);
			*/
		}

		private void ParseOperator(Stack<Expression>expr, Stack<Operator>ops) {
			Token t = tokens.Dequeue();
			Operator op = kalc.Operators[t.val];

			// Handle ambiguous cases of arity
			if(op.arity == Arity.BOTH) {	
				if(PossiblyValidExpression(expr, ops) ) {
					op = new Operator(op.id, op.precidence, Arity.BINARY);
					Kerbulator.DebugLine(op.id +" is binary.");
				} else {
					op = new Operator(op.id, 3, Arity.UNARY);
					Kerbulator.DebugLine(op.id +" is unary.");
				}
			} 

			// Handle operators with higher precidence
			while(ops.Count > 0) {
				Operator prevOp = ops.Peek();

				if(op.arity != Arity.BINARY || prevOp.precidence < op.precidence)
					// Leave for later
					break;
				else
					expr.Push( ExecuteOperator(ops.Pop(), expr, ops) );
			}

			// Push current operator on the stack
			Kerbulator.DebugLine("Pushing "+ op.id);
			ops.Push(op);
		}

		public delegate double UnaryFunction(double a); 
		public delegate double BinaryFunction(double a, double b); 

		public Object ExecuteUnaryFunction(string id, UnaryFunction action, Object a) {
			// Called with a double
			if(a.GetType() == typeof(double))
				return action((double)a);

			// Called with a list
			else if(a.GetType() == typeof(Object[])) {
				Object[] list = (Object[]) a;
				Object[] newList = new Object[list.Length];
				for(int i=0; i<list.Length; i++)
					newList[i] = ExecuteUnaryFunction(id, action, list[i]);
				return newList;

			// Called with something else
			} else
				throw new Exception("In function "+ this.id +": cannot apply "+ id +" to variable of type "+ a.GetType().ToString());
		}

		public Object ExecuteBinaryFunction(string id, BinaryFunction action, Object a, Object b) {
			// Called with two doubles
			if(a.GetType() == typeof(double) && b.GetType() == typeof(double))
				return action((double)a, (double)b);

			// Called with a list and a double
			else if(a.GetType() == typeof(Object[]) && b.GetType() == typeof(double)) {
				Object[] list = (Object[]) a;
				Object[] newList = new Object[list.Length];
				for(int i=0; i<list.Length; i++)
					newList[i] = ExecuteBinaryFunction(id, action, list[i], b);
				return newList;

			// Called with a double and a list
			} else if(a.GetType() == typeof(double) && b.GetType() == typeof(Object[])) {
				Object[] list = (Object[]) b;
				Object[] newList = new Object[list.Length];
				for(int i=0; i<list.Length; i++)
					newList[i] = ExecuteBinaryFunction(id, action, a, list[i]);
				return newList;

			// Called with two lists
			} else if(a.GetType() == typeof(Object[]) && b.GetType() == typeof(Object[])) {
				Object[] listA = (Object[]) a;
				Object[] listB = (Object[]) b;
				if(listA.Length != listB.Length)
					throw new Exception("In function "+ this.id +": cannot apply "+ id +" to lists of different length (got "+ listA.Length +" and "+ listB.Length +")");
				Object[] newList = new Object[listA.Length];
				for(int i=0; i<listA.Length; i++)
					newList[i] = ExecuteBinaryFunction(id, action, listA[i], listB[i]);
				return newList;

			// Called with something else
			} else
				throw new Exception("In function "+ this.id +": cannot apply "+ id +" to variables of type "+ a.GetType().ToString() +" and "+ b.GetType().ToString());
		}

		private Expression CallUnaryLambda(string id, Expression<UnaryFunction> e, Expression a) {
			return Expression.Call(
				thisExpression,
				typeof(JITFunction).GetMethod("ExecuteUnaryFunction"),
				Expression.Constant(id),
				e, a
			);
		}

		private Expression CallBinaryLambda(string id, Expression<BinaryFunction> e, Expression a, Expression b) {
			return Expression.Call(
				thisExpression,
				typeof(JITFunction).GetMethod("ExecuteBinaryFunction"),
				Expression.Constant(id),
				e, a, b
			);
		}

		private Expression CallUnaryMathFunction(string id, string name, Expression a) {
			return Expression.Call(
				thisExpression,
				typeof(JITFunction).GetMethod("ExecuteUnaryFunction"),
				Expression.Constant(id),
				Expression.Constant(
					Delegate.CreateDelegate(
						typeof(UnaryFunction),
						typeof(Math).GetMethod(name, new[] {typeof(double)})
					)
				),
				a
			);
		}

		private Expression CallBinaryMathFunction(string id, string name, Expression a, Expression b) {
			return Expression.Call(
				thisExpression,
				typeof(JITFunction).GetMethod("ExecuteBinaryFunction"),
				Expression.Constant(id),
				Expression.Constant(
					Delegate.CreateDelegate(
						typeof(BinaryFunction),
						typeof(Math).GetMethod(name, new[] {typeof(double), typeof(double)})
					)
				),
				a, b
			);
		}

		private Expression ExecuteOperator(Operator op, Stack<Expression> expr, Stack<Operator> ops) {
			Kerbulator.DebugLine("Executing: "+ op.id);
			if(op.arity == Arity.BINARY && expr.Count < 2)
				throw new Exception("Operator "+ op.id +" expects both a left and a right hand side to operate on.");
			else if(op.arity == Arity.UNARY && expr.Count < 1)
				throw new Exception("Operator "+ op.id +" expects a right hand side to operate on.");
			else if(op.arity == Arity.BOTH)
				throw new Exception("Arity of "+ op.id +" still undefined.");

			Expression a,b;
			Expression opExpression;
			switch(op.id) {
				case "+":
					b = expr.Pop();
					a = expr.Pop();
					opExpression = CallBinaryLambda(op.id, (x,y) => x + y, a, b);
					break;
				case "-":
					b = expr.Pop();
					if(op.arity == Arity.UNARY) 
						opExpression = CallUnaryLambda(op.id, x => -x, b);
					else {
						a = expr.Pop();
						opExpression = CallBinaryLambda(op.id, (x,y)=> x-y, a, b);
					}
					break;
				case "*":
				case "·":
					b = expr.Pop(); a = expr.Pop();
					opExpression = CallBinaryLambda(op.id, (x,y) => x * y, a, b);
					break;
				case "/":
				case "÷":
					b = expr.Pop(); a = expr.Pop();
					opExpression = CallBinaryLambda(op.id, (x,y) => x / y, a, b);
					break;
				case "%":
					b = expr.Pop(); a = expr.Pop();
					opExpression = CallBinaryLambda(op.id, (x,y) => x % y, a, b);
					break;
				case "^":
					b = expr.Pop(); a = expr.Pop();
					opExpression = CallBinaryMathFunction(op.id, "Pow", a, b);
					break;
				case "√":
					b = expr.Pop();
					if(op.arity == Arity.UNARY)
						opExpression = CallUnaryMathFunction(op.id, "Sqrt", b);
					else {
						a = expr.Pop();
						opExpression = CallBinaryMathFunction(op.id, "Pow",
							a,
							Expression.Convert(
								Expression.Divide(
									Expression.Constant(1.0),
									Expression.Convert(b, typeof(double))
								),
								typeof(Object)
							)
						);
					}
					break;
				case "⌊":
					b = expr.Pop();
					opExpression = CallUnaryMathFunction(op.id, "Floor", b);
					break;
				case "⌈":
					b = expr.Pop();
					opExpression = CallUnaryMathFunction(op.id, "Ceiling", b);
					break;
				case "|":
					b = expr.Pop();
					if(b.GetType() == typeof(double))
						opExpression = CallUnaryMathFunction(op.id, "Abs", b);
					else
						opExpression = Expression.Call(
							typeof(VectorMath).GetMethod("Mag"),
							b,
							Expression.Constant("In function "+ this.id)
						);
					break;

				case "buildin-function":
					List<Expression> args = new List<Expression>();
					args.Add(expr.Pop());
					a = expr.Pop();
					opExpression = ParseBuildInFunction(kalc.BuildInFunctions[(string)((ConstantExpression)a).Value], args);
					break;

				case "user-function":
					List<Expression> args2 = new List<Expression>();
					args2.Add(Expression.Convert(expr.Pop(), typeof(double)));
					a = Expression.Convert(expr.Pop(), typeof(double));
					throw new Exception("User functions not implemented yet.");
					// return ParseUserFunction(functions[(string)((ConstantExpression)a).Value], args2);

				default:
					throw new Exception("Unknown operator: "+ op.id);
			}

			return Expression.Convert(opExpression, typeof(Object));
		}

		private bool ParseBrace(Stack<Expression> expr, Stack<Operator> ops) {
			Token t = tokens.Peek();

			// Determine whether it's a left or right brace
			bool isLeft = false;
			switch(t.val) {
				case "(":
				case "{":
				case "⌊":
				case "⌈":
					isLeft = true;
					break;
				case "|":
					isLeft = !PossiblyValidExpression(expr, ops);

					if(isLeft)
						Kerbulator.DebugLine("| is left brace");
					else {
						Kerbulator.DebugLine("| is right brace");
					}
					break;
			} 

			// If it's a left brace, start a sub-expression
			if(isLeft) {
				Consume();

				// Execute sub-expression
				Kerbulator.DebugLine("Starting subexpression");
				Expression subexpr = ParseExpression();
				Kerbulator.DebugLine("End of subexpression");
				expr.Push(subexpr);

				// Consume right brace. Execute operation if any
				switch(t.val) {
					case "(":
						Consume(")");
						break;
					case "{":
						Consume("}");
						break;
					case "⌊":
						Consume("⌋");
						ops.Push(kalc.Operators[t.val]);
						break;
					case "⌈":
						Consume("⌉");
						ops.Push(kalc.Operators[t.val]);
						break;
					case "|":
						Consume("|");
						ops.Push(kalc.Operators[t.val]);
						break;
				}
				return false;
			} else { 
				return true;
			}
		}

		public bool ParseList(Stack<Expression> expr) {
			if(tokens.Peek().val == "]") {
				return true;
			}

			// Consume left brace
			Consume();

			List<Expression> elements = new List<Expression>();
			while(tokens.Peek().val != "]") {
				Kerbulator.DebugLine("Starting subexpression");
				Expression subexpr = ParseExpression();
				Kerbulator.DebugLine("End of subexpression");
				elements.Add(subexpr);
				
				if(tokens.Peek().val != "]")
					Consume(TokenType.COMMA);
			}

			// Consume right brace
			Consume();

			if(elements.Count == 0)
				throw new VarException("Empty lists are not allowed.");

			expr.Push( Expression.NewArrayInit(typeof(Object), elements) );
			return false;
		}

		public Object GetLocal(string id) {
			if(!locals.ContainsKey(id))
				throw new Exception("In function "+ this.id +": variable "+ id +" is not defined.");
			return locals[id];
		}

		public Object GetGlobal(string id) {
			if(!kalc.Globals.ContainsKey(id))
				throw new Exception("In function "+ this.id +": variable "+ id +" is not defined.");
			return kalc.Globals[id];
		}

		public JITFunction GetFunction(string id) {
			throw new Exception("not implemented.");
		}

		private void ParseIdentifier(Stack<Expression> expr, Stack<Operator> ops) {
			Token t = tokens.Dequeue();

			if(kalc.Functions.ContainsKey(t.val)) {
				// User function call
				Console.WriteLine("User function, not implemented yet");

				/*
				JITFunction f = kalc.Functions[t.val];
				if(tokens.Count > 0 && tokens.Peek().val == "(") {
					// Parameter list supplied, execute function now
					List<Expression> args = ParseArgumentList();
					if(args.Count != f.Ins.Count)
						throw new Exception(t.pos + "function "+ f.Id +" takes "+ f.Ins.Count +" arguments, but "+ args.Count +" were supplied");
					// expr.Push( ParseUserFunction(f, args) );
				} else if(f.Ins.Count == 0) {
					// Function takes no arguments, execute now
					// expr.Push( ParseUserFunction(f, new List<Expression>()) );
				} else {
					// Do function call later, when parameters are known
					ops.Push(kalc.Operators["user-function"]);
					expr.Push(Expression.Constant(t.val));
				}
				*/

			} else if(kalc.BuildInFunctions.ContainsKey(t.val)) {
				BuildInFunction f = kalc.BuildInFunctions[t.val];

				if(tokens.Count > 0 && tokens.Peek().val == "(") {
					// Parameter list supplied, execute function now
					List<Expression> args = ParseArgumentList();
					if(args.Count != f.numArgs)
						throw new Exception(t.pos + "function "+ f +" takes "+ f.numArgs +" arguments, but "+ args.Count +" were supplied");
					expr.Push( ParseBuildInFunction(f, args) );
				} else if(f.numArgs == 0) {
					// Function takes no arguments, execute now
					expr.Push( ParseBuildInFunction(f, new List<Expression>()) );
				} else {
					// Do function call later, when parameters are known
					ops.Push(kalc.Operators["buildin-function"]);
					expr.Push(Expression.Constant(t.val));
				}
			} else if(kalc.Globals.ContainsKey(t.val)) {
				expr.Push(Expression.Constant( kalc.Globals[t.val], typeof(Object) ));
			} else {
				// Local identifier
				expr.Push(
					Expression.Call(
						thisExpression,
						typeof(JITFunction).GetMethod("GetLocal"),
						Expression.Constant(t.val)
					)
				);	
			}
		}

		private List<Expression> ParseArgumentList() {
			List<Expression> arguments = new List<Expression>();

			Consume("(");

			while(tokens.Peek().val != ")") {
				Expression subexpr = ParseExpression();
				arguments.Add(subexpr);
				
				if(tokens.Count == 0)
					throw new Exception("In function "+ this.id +": missing ')'");

				if(tokens.Peek().val != ")")
					Consume(TokenType.COMMA);
			}

			Consume(")");

			return arguments;
		}

		private Expression ParseBuildInFunction(BuildInFunction func, List<Expression> arguments) {
			Expression<UnaryFunction> unaryFunction;
			Expression<BinaryFunction> binaryFunction;
			Expression funcExpression;

			switch(func.id) {
				case "abs":
					funcExpression = CallUnaryMathFunction(func.id, "Abs", arguments[0]);
					break;
				case "acos":
					funcExpression = CallUnaryMathFunction(func.id, "Acos", arguments[0]);
					break;
				case "asin":
					funcExpression = CallUnaryMathFunction(func.id, "Asin", arguments[0]);
					break;
				case "atan":
					funcExpression = CallUnaryMathFunction(func.id, "Atan", arguments[0]);
					break;
				case "ceil":
					funcExpression = CallUnaryMathFunction(func.id, "Ceiling", arguments[0]);
					break;
				case "cos":
					funcExpression = CallUnaryMathFunction(func.id, "Cos", arguments[0]);
					break;
				case "exp":
					funcExpression = CallUnaryMathFunction(func.id, "Exp", arguments[0]);
					break;
				case "floor":
					funcExpression = CallUnaryMathFunction(func.id, "Floor", arguments[0]);
					break;
				case "ln":
				case "log":
					funcExpression = CallUnaryMathFunction(func.id, "Log", arguments[0]);
					break;
				case "log10":
					funcExpression = CallUnaryMathFunction(func.id, "Log10", arguments[0]);
					break;
				case "max":
					funcExpression = CallBinaryMathFunction(func.id, "Max", arguments[0], arguments[1]);
					break;
				case "min":
					funcExpression = CallBinaryMathFunction(func.id, "Min", arguments[0], arguments[1]);
					break;
				case "pow":
						funcExpression = CallBinaryMathFunction(func.id, "Pow", arguments[0], arguments[1]);
					break;
				case "round":
					funcExpression = CallBinaryLambda(func.id, (a,b) => Math.Round(a, (int)b), arguments[0], arguments[1]);
					break;
				case "sign":
					funcExpression = CallUnaryLambda(func.id, a => (int)Math.Sign(a), arguments[0]);
					break;
				case "sin":
					funcExpression = CallUnaryMathFunction(func.id, "Sin", arguments[0]);
					break;
				case "sqrt":
					funcExpression = CallUnaryMathFunction(func.id, "Sqrt", arguments[0]);
					break;
				case "tan":
					funcExpression = CallUnaryMathFunction(func.id, "Tan", arguments[0]);
					break;
				case "len":
					funcExpression = Expression.Call(
						typeof(VectorMath).GetMethod("Len"),
						arguments[0],
						Expression.Constant("In function "+ this.id)
					);
					break;
				case "dot":
					funcExpression = Expression.Call(
						typeof(VectorMath).GetMethod("Dot"),
						arguments[0], arguments[1],
						Expression.Constant("In function "+ this.id)
					);
					break;
				case "mag":
					funcExpression = Expression.Call(
						typeof(VectorMath).GetMethod("Mag"),
						arguments[0],
						Expression.Constant("In function "+ this.id)
					);
					break;
				case "norm":
					funcExpression = Expression.Call(
						typeof(VectorMath).GetMethod("Norm"),
						arguments[0],
						Expression.Constant("In function "+ this.id)
					);
					break;
				case "cross":
					funcExpression = Expression.Call(
						typeof(VectorMath).GetMethod("Cross"),
						arguments[0], arguments[1],
						Expression.Constant("In function "+ this.id)
					);
					break;
				default:
					throw new Exception("Unknown build-in function: "+ func.id);
			}

			return Expression.Convert(funcExpression, typeof(Object));
		}

		/*
		private Expression ParseUserFunction(JITFunction func, List<Expression> arguments) {
			return Expression.Call(
				Expression.Constant(func),
				typeof(JITFunction).GetMethod("Execute"),
				arguments
			);
		}
		*/
	}

	public class JITExpression: JITFunction {
		public JITExpression(string expression, Kerbulator kalc)
	   	:base("unnamed", expression, kalc)	{ 
		}

		override public List<Object> Execute() {
			return null;
		}
	}
}