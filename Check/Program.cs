using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check
{
    class Program
    {
        
            


        static void Main(string[] args)
        {

            double Gama = -5.34282614688910e-05;


            double x = Math.Sin(Gama);
            
            
            double[] ar = new double[] { 1.0, Math.Sin(Gama) };

            DOM d = new DOM();
            d.gk2GPS(82663.2810000000, 426735.9300000000, 0);
            //426735.9300000000,82663.2810000000



            Console.WriteLine(Gama + " -  " + ar[0] + " - " + ar[1]);
            Console.ReadLine();
        }
    }
}
