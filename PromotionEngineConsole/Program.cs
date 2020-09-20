using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineConsole
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                new PromotionEngineAppInit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------- Error -----------------");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
