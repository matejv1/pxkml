using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace KMLConsoleParse
{
    class Program
    {
        


        static void Main(string[] args)
        {
            XDocument myxml = XDocument.Load(@"Obcine_210.xml");
            var z = myxml.Descendants("coordinates");


            foreach (XElement xe in z)
            {
                string cordinateItem = "";
                string item = xe.Value;
                string[] i = item.Split(' ');
                
                foreach (string s in i)
                {
                    double x1 = 0, x2 = 0, x3 = 0;
                    string[] endItems = s.Split(',');
                    if (endItems.Length < 3)
                    {
                        Console.WriteLine(endItems[0]);
                    }
                    else {
                        //Console.WriteLine(endItems[0] + " - " + endItems[1] + " - " + endItems[2] + " - ");
                        if (endItems != null)
                        {
                            x1 = Convert.ToDouble(endItems[0]);
                            x2 = Convert.ToDouble(endItems[1]);
                            DOM d = new DOM();
                            double[] rArr;
                            rArr = d.gk2GPS(x2, x1, x3);
                            cordinateItem += " " + rArr[1].ToString() + "," + rArr[0].ToString() + ",0";
                            Console.WriteLine(rArr[0].ToString() + " - " + rArr[1].ToString() + " - " + rArr[2].ToString());
                        }
                    } 
                }
                xe.SetValue(cordinateItem);
            }

            myxml.Save(@"Obcine_210.xml");
            Console.ReadLine();

        }
    }
}
