using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recongnize_Text_Console.Controllers;

namespace Recongnize_Text_Console
{
    class Program
    {
        static void Main()
        {
            #region Start

            var controller = new Controller();
            controller.Start();
            
            #endregion


        }
    }
}
