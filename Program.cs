using System;
using System.Collections.Generic;

namespace polygot
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils utils =  new Utils();
            test s =  new test();
            s.hi();   

            string filename =  "test.dat";
            List<Matrix> localKs =  new List<Matrix>();
            List<List<float>> localbs  =  new List<List<float>>();
            Matrix K = new Matrix();
            List<float> b =  new List<float>();
            List<float> T =  new List<float>();

            mesh m =  new mesh();
            utils.leerMallayCondiciones(m,filename);
            
            Console.WriteLine("Hello World!");
        }
    }
}
