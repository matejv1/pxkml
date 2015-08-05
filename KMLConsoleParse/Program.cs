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
            XDocument myxml = XDocument.Load(@"OGU20.kml");
            IEnumerable<XNode> de = from el in myxml.DescendantNodes() select el;

            Console.WriteLine(de.Count<XNode>().ToString());

            var z = myxml.Descendants("coordinates");
            foreach (XElement x in z)
            {
                x.Value = x.Value + 100;
            }

            foreach (XElement xe in z)
            {
                string cordinateItem = "";

                string item = xe.Value;
                string[] i = item.Split(',', ' ');

                foreach (string s in i)
                {
                    double tempValue = Convert.ToDouble(s);
                    if (tempValue == 0)
                        cordinateItem += "" + Convert.ToDouble(s).ToString() + "";
                    
                    if (tempValue > 0 && tempValue < 5)
                        cordinateItem += " " + (Convert.ToDouble(s) + 45).ToString() + ",";

                    if (tempValue < -150 && tempValue > - 190)
                        cordinateItem += " " + (Convert.ToDouble(s) + 191).ToString() + ",";
                }
                xe.SetValue(cordinateItem);
            }

            myxml.Save(@"OGU20.kml");

            foreach (XElement xe in z)
            {
                Console.WriteLine(xe.ToString());
            }
            
            Console.ReadLine();

        }
    }
}
