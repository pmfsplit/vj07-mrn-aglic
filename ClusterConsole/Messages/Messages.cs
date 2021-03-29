using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class Msg
    {
        public string Text { get; }

        public Msg(string text)
        {
            Text = text;
        }
    }

    public class SaveToDatabaseAck
    {
        public int Id { get; }
        public SaveToDatabaseAck(int id)
        {
            Id = id;
        }
    }
}
