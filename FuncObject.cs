using System;

namespace AbusingDotNet 
{
    public static class FuncTools {
        public static FuncObject<TArg, TRes> F<TArg, TRes>(Func<TArg, TRes> func) {
            return new FuncObject<TArg, TRes>(func);
        }
    }

    public class FuncObject<TArg, TRes> {

        private Func<TArg, TRes> _innerFunc;

        public FuncObject(Func<TArg, TRes> innerFunc) {
            _innerFunc = innerFunc;
        }

        public TRes Invoke(TArg arg) {
            return _innerFunc(arg);
        }
        

        /// <summary>
        ///  Function composition
        /// </summary>
        public static FuncObject<TArg, TRes> operator + (FuncObject<TArg, TRes> a, 
                                                         FuncObject<TRes, TRes> b) {
            return new FuncObject<TArg, TRes>(
                (TArg arg) => b.Invoke(a.Invoke(arg))
            );
        }

        /// <summary>
        ///  Pipe-forward operator
        /// </summary>
        public static TRes operator | (TArg arg, FuncObject<TArg, TRes> f) {
            return f.Invoke(arg);
        }
    }
}