using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Messages
    {
        public static void sendMessage(Connection con, string[] msgs)
        {
            string msg = "";
            for(int i = 0; i < msgs.Length - 1; i++)
            {
                msg += msgs[i] + ";";
            }
            msg += msgs[msgs.Length - 1];
            con.Buffer = Encoding.UTF8.GetBytes(msg);
            con.Stream.Write(con.Buffer, 0, con.Buffer.Length);
            con.Buffer = new byte[con.bufferSize];
        }

        public static string receiveMessage(Connection con)
        {
            con.Stream.Read(con.Buffer, 0, con.bufferSize);
            string msg = Encoding.UTF8.GetString(con.Buffer);
            if(msg.Contains("\n\r"))
                msg = msg.Remove(msg.IndexOf("\n\r"), msg.Length - msg.IndexOf("\n\r"));
            if(msg.Contains("\0"))
                msg = msg.Remove(msg.IndexOf("\0"), msg.Length - msg.IndexOf("\0"));
            msg.Remove(msg.IndexOf("ENDMESS"), msg.Length - msg.IndexOf("ENDMESS"));
            con.Buffer = new byte[con.bufferSize];
            return msg;
        }

        public static string receiveLongMessage(Connection con)
        {
            string msg = "";
            bool end = false;
            while(!end)
            {
                con.Stream.Read(con.Buffer, 0, con.bufferSize);
                msg += Encoding.UTF8.GetString(con.Buffer);
                if (msg.EndsWith("ENDMESS")) end = true;
            }
            return msg;
        }
    }
}
