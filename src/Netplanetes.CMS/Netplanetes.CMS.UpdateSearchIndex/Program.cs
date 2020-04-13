using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.UpdateSearchIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IndexOption option = new IndexOption();
                option.InitilalizeWithCommandLine();
                Console.WriteLine("Option:" + option.ToString());

                var processor = new IndexProcessor();
                Task task = processor.ProcessIndexing(option);
                task.Wait();
                if (task.IsFaulted)
                {
                    Console.WriteLine(task.Exception.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
