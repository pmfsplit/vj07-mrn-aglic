using System;

namespace Messages
{
    public class SaveToDatabase
    {
        public int Id { get; }
        public string Content { get; }

        public SaveToDatabase(int id, string content)
        {
            Id = id;
            Content = content;
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