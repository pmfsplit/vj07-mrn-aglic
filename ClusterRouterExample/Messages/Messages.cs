using System.Collections.Generic;

namespace Shared
{
    public class Nums
    {
        public IEnumerable<int> Args { get; }
        public Nums(IEnumerable<int> args)
        {
            Args = args;
        }
    }

    public class Job
    {
        public int Num { get; }
        public Job(int num)
        {
            Num = num;
        }
    }
}
