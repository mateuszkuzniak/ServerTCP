namespace ServerLibrary
{
    public class Request
    {
        public int Code { get; set; }
        public string Opcode { get; set; }

        public string Args1 { get; set; }
        public string Args2 { get; set; }
        public string Args3 { get; set; }


        public Request(int code, string opcode, string args1 = null,  string args2 = null, string args3 = null) 
        {
            Code = code;
            Opcode = opcode;
            Args1 = args1;
            Args2 = args2;
            Args3 = args3;
        }

        //public Request(int code, string opcode, string args1)
        //{
        //    Code = code;
        //    Opcode = opcode;
        //    Args1 = args1;
        //}

        //public Request(int code, string opcode)
        //{
        //    Code = code;
        //    Opcode = opcode;
        //}

        //public override int GetHashCode()
        //{
        //    return Code;
        //}

        public override bool Equals(object obj)
        {
            return Code == ((Request)obj).Code;
        }

        public override string ToString()
        {
            return Opcode;
        }
    }
}