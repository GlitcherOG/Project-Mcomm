using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server
{
    public static class ConsoleManager
    {

        static Color DefaultColour = Color.White;

        public static void WriteLine(string Input = "", Color? color = null)
        {
            if(color==null)
            {
                color = DefaultColour;
            }

            Console.WriteLine(Input);

        }


    }
}
