using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check
{
    class DOM
    {
        public double fi, la, j, fi0, la0, k, ii, jj, fi01, fi02, la01, la02, fii, lai, ir, fi10, la10, dif, p1;
        ///Konstante za racunanje
        public double wgs84_a = 6378137.0; //m
        public double wgs84_a2 = 40680631590769;
        public double wgs84_b = 6356752.314; //m
        public double wgs84_b2 = 40408299981544.4;
        public double wgs84_e2 = 0.00669438006676466; //e^2
        public double wgs84_e2_ = 0.00673949681993606; //e'^2;
        public double bessel_a = 6377397.155; //m
        public double bessel_a2 = 40671194472602.1; //a^2
        public double bessel_b = 6356078.963; //m
        public double bessel_b2 = 40399739783891.2;
        public double bessel_e2 = 0.00667437217497493; //e^2
        public double bessel_e2_ = 0.00671921874158131; //e'^2
        public double bessel_e4 = 4.45472439300796e-05;
        public double bessel_e6 = 2.97324885358744e-07;
        public double bessel_e8 = 1.98445694176601e-09;
        public double dX = -409.520465;
        public double dY = -72.191827;
        public double dZ = -486.872387;
        public double Alfa = 1.49625622332431e-05;
        public double Beta = 2.65141935723559e-05;
        public double Gama = -5.34282614688910e-05;
        public double dm = -17.919456e-6;

        double E = 4.76916455578838e-12;
        double D = 3.43836164444015e-9;
        double C = 2.64094456224583e-6;
        double B = 0.00252392459157570;
        double A = 1.00503730599692;

        double N, t, t2, t4, L, cosFi, ni2, lambda, X, Y, Z, X1, Y1, Z1, p, O, SinO, Sin3O, CosO, Cos3O, fif, lambdaf, hf;

        public void Init() { 

            

            
        }


        public void gk2GPS(double x, double y, double h)
        {


            double[] M0 = new double[] { 1.0, Math.Sin(Gama), -1 * Math.Sin(Beta) };
            double[] M1 = new double[] { -1 * Math.Sin(Gama), 1, Math.Sin(Alfa) };
            double[] M2 = new double[] { Math.Sin(Beta), -Math.Sin(Alfa), 1 };
            
           
            y = (y - 500000) / 0.9999;
            x = (1 * x + 5000000) / 0.9999; // 1*x !!!!!!!!!!!
            double ab = (1 * bessel_a + 1 * bessel_b);

            fi0 = (2 * x) / ab;
            dif = 1.0;
            p1 = bessel_a * (1 - bessel_e2);
            var n = 25;
            while (Math.Abs(dif) > 0 && n > 0)
            {
                L = p1 * (A * fi0 - B * Math.Sin(2 * fi0) + C * Math.Sin(4 * fi0) - D * Math.Sin(6 * fi0) + E * Math.Sin(8 * fi0));
                dif = (2 * (x - L) / ab);
                fi0 = fi0 + dif;
                n--;
            }
            N = bessel_a / (Math.Sqrt(1 - bessel_e2 * Math.Pow(Math.Sin(fi0), 2)));
            t = Math.Tan(fi0);
            t2 = Math.Pow(t, 2);
            t4 = Math.Pow(t2, 2);
            cosFi = Math.Cos(fi0);
            ni2 = bessel_e2_ * Math.Pow(cosFi, 2);
            lambda = 0.261799387799149 + (y / (N * cosFi)) - (((1 + 2 * t2 + ni2) * Math.Pow(y, 3)) / (6 * Math.Pow(N, 3) * cosFi)) + (((5 + 28 * t2 + 24 * t4) * Math.Pow(y, 5)) / (120 * Math.Pow(N, 5) * cosFi));
            fi = fi0 - ((t * (1 + ni2) * Math.Pow(y, 2)) / (2 * Math.Pow(N, 2))) + (t * (5 + 3 * t2 + 6 * ni2 - 6 * ni2 * t2) * Math.Pow(y, 4)) / (24 * Math.Pow(N, 4)) - (t * (61 + 90 * t2 + 45 * t4) * Math.Pow(y, 6)) / (720 * Math.Pow(N, 6));
            N = bessel_a / (Math.Sqrt(1 - bessel_e2 * Math.Pow(Math.Sin(fi), 2)));
            X = (N + h) * Math.Cos(fi) * Math.Cos(lambda);
            Y = (N + h) * Math.Cos(fi) * Math.Sin(lambda);
            Z = ((bessel_b2 / bessel_a2) * N + h) * Math.Sin(fi);
            X -= dX; Y -= dY; Z -= dZ;
            X /= (1 + dm); Y /= (1 + dm); Z /= (1 + dm);
            X1 = X - M0[1] * Y - M0[2] * Z;
            Y1 = -1 * M1[0] * X + Y - M1[2] * Z;
            Z1 = -1 * M2[0] * X - M2[1] * Y + Z;
            p = Math.Sqrt(Math.Pow(X1, 2) + Math.Pow(Y1, 2));
            O = Math.Atan2(Z1 * wgs84_a, p * wgs84_b);
            SinO = Math.Sin(O);
            Sin3O = Math.Pow(SinO, 3);
            CosO = Math.Cos(O);
            Cos3O = Math.Pow(CosO, 3);
            fif = Math.Atan2(Z1 + wgs84_e2_ * wgs84_b * Sin3O, p - wgs84_e2 * wgs84_a * Cos3O);
            lambdaf = Math.Atan2(Y1, X1);
            N = wgs84_a / Math.Sqrt(1 - wgs84_e2 * Math.Pow(Math.Sin(fif), 2));
            hf = p / Math.Cos(fif) - N;
            fif = (fif * 180) / Math.PI;
            lambdaf = (lambdaf * 180) / Math.PI;
            double[] retVal = new double[] { fif, lambdaf, hf };
            Console.WriteLine(fif + " - " + lambdaf + " - " + hf);
        }


    }
}
