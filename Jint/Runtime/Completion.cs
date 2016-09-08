using Jint.Native;
using System;

namespace Jint.Runtime
{
    /// <summary>
    /// http://www.ecma-international.org/ecma-262/5.1/#sec-8.9
    /// </summary>
    public class Completion
    {
        public static string Normal = "normal";
        public static string Break = "break";
        public static string Continue = "continue";
        public static string Return = "return";
        public static string Throw = "throw";
		public Exception ExceptionLocation { get; set; }
		public Jint.Parser.Ast.Statement statement;

        public Completion(string type, JsValue? value, string identifier, Exception ex=null, Jint.Parser.Ast.Statement stmt = null)
        {
            Type = type;
            Value = value;
            Identifier = identifier;
			statement = stmt;
			ExceptionLocation = ex;
        }

        public string Type { get; private set; }
        public JsValue? Value { get; private set; }
		public string Identifier { get; private set; }
		public string Statement { get { return statement == null ? "?" : statement.ToString(); } }

        public JsValue GetValueOrDefault()
        {
			JsValue v;
            if( Value.HasValue )
				v = Value.Value;
			else 
				v = Undefined.Instance;
			return v;
        }

        public Jint.Parser.Location Location { get; set; }
	}
}
