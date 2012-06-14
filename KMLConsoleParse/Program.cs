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
            IEnumerable<XNode> de =
            from el in myxml.DescendantNodes()
            select el;


            Console.WriteLine(de.Count<XNode>().ToString());

            var z = myxml.Descendants("coordinates");
            foreach (XElement x in z)
            {
                x.Value = x.Value + 100;
                
            }


            //IEnumerable<XElement> singleBook = (from b in myxml.Descendants("coordinates")
            //    where ((string)b.Element("title")).Equals("Introducing Microsoft LINQ") select b);


            foreach (XElement xe in z)
            {

                Console.WriteLine("here");
                string cordinateItem = "";

                string item = xe.Value;
                string[] i = item.Split(',', ' ');

                foreach (string s in i)
                {
                    Console.WriteLine("in");
                    //richTextBox1.Text += Convert.ToDouble(s).ToString() + "\n";
                    double tempValue = Convert.ToDouble(s);
                    if (tempValue == 0)
                    {
                        cordinateItem += "" + Convert.ToDouble(s).ToString() + "";
                        //Console.WriteLine(Convert.ToDouble(s).ToString());
                    }
                    if (tempValue > 0 && tempValue < 5) {
                        cordinateItem += " " + (Convert.ToDouble(s) + 45).ToString() + ",";
                    }
                    if (tempValue < -150 && tempValue > - 190) {
                    
                        cordinateItem += " " + (Convert.ToDouble(s) + 191).ToString() + ",";
                    }
                    Console.WriteLine("out");
                }

                xe.SetValue(cordinateItem);


               //Console.WriteLine(i[0].ToString() + i[1].ToString() + i[2].ToString() + i[3].ToString());
               //Console.WriteLine("sample");



            }
            myxml.Save(@"OGU20.kml");



            foreach (XElement xe in z)
            {


               
                Console.WriteLine(xe.ToString());

                //Console.WriteLine(i[0].ToString() + i[1].ToString() + i[2].ToString() + i[3].ToString());
                //Console.WriteLine("sample");



            }
            

            Console.ReadLine();

        }
    }
}
