using System;

namespace ServerLibrary
{
    internal class Response
    {
        public int Code { get; set; }
        public string Args { get; set; }
        public Action Action;
        public Action<string> Action1;
        public Action<string, string> Action2;
        public Func<string,string> GenerateResponse;

        public Response(int code,  Action<string, string> action, string args, Func<string,string> generateResponse)
        {
            Code = code;
            GenerateResponse = generateResponse;
            Args = args;
            Action2 = action;
        }

        public Response(int code, Action<string> action, Func<string,string> generateResponse)
        {
            Code = code;
            Action1 = action;
            GenerateResponse = generateResponse;
        }

        public Response(int code, Action action, Func<string,string> generateResponse)
        {
            Code = code;
            Action = action;
            GenerateResponse = generateResponse;
        }

        public override int GetHashCode()
        {
            return Code;
        }

        public override bool Equals(object obj)
        {
            return Code == ((Request)obj).Code;
        }

        public override string ToString()
        {
            return GenerateResponse(Args);
        }

    }
}