using System;
using Jint.Native;
using Jint.Native.Error;
using Jint.Parser.Ast;

namespace Jint.Runtime
{
    public class JavaScriptException : Exception
    {
        private readonly JsValue _errorObject;

		public JavaScriptException( string msg, Exception inner ) : base(msg, inner) { }
        public JavaScriptException(ErrorConstructor errorConstructor) : base("")
        {
            _errorObject = errorConstructor.Construct(Arguments.Empty);
        }

		public JavaScriptException( ErrorConstructor errorConstructor, string message )
			: base(message) {
			_errorObject = errorConstructor.Construct(new JsValue[] { message });
		}
		public JavaScriptException( Statement stmt, string message, JavaScriptException ex )
			: base(message+": "+stmt.ToString(), ex) {
				_errorObject = new JsValue(message + ": " + stmt);
		}

        public JavaScriptException(JsValue error)
            : base(GetErrorMessage(error))
        {
            _errorObject = error;
        }

        private static string GetErrorMessage(JsValue error) 
        {
            if (error.IsObject())
            {
                var oi = error.AsObject();
                var message = oi.Get("message").AsString();
                return message;
            }
            else
                return error.ToString();            
        }

        public JsValue Error { get { return _errorObject; } }

        public override string ToString()
        {
            return _errorObject.ToString();
        }
    }
}
