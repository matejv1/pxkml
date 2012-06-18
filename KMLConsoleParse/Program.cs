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
            IEnumerable<XNode> de =
                    from el in myxml.DescendantNodes()
                    select el;


            Console.WriteLine(de.Count<XNode>().ToString());

            var z = myxml.Descendants("coordinates");
            foreach (XElement x in z)
            {
                x.Value = x.Value + 100;
                Console.WriteLine("here");
            }


            //IEnumerable<XElement> singleBook = (from b in myxml.Descendants("coordinates")
            //    where ((string)b.Element("title")).Equals("Introducing Microsoft LINQ") select b);

            /*
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
                    Console.WriteLine("out - " + cordinateItem + " - " + tempValue);
                }

                xe.SetValue(cordinateItem);


               //Console.WriteLine(i[0].ToString() + i[1].ToString() + i[2].ToString() + i[3].ToString());
               //Console.WriteLine("sample");



            }
            myxml.Save(@"OGU20_Original.kml");
            



            string[] splitAgain = s.Split(',');
            double xVal = Convert.ToDouble(splitAgain[0]);
            double yVal = Convert.ToDouble(splitAgain[0]);

            Console.WriteLine(xVal + " - " + yVal + "\n");
            */











            int m = 0;
            foreach (XElement xe in z)
            {

                Console.WriteLine("here");
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
