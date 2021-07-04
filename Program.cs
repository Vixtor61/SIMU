using System;
using System.Collections.Generic;

namespace polygot
{
    using Matrix = List<List<double>>;
    class Program
    {
        static void Main(string[] args)
        {
            Utils utils =  new Utils();
            sel sel = new sel();
            math math = new math();
            test s =  new test();
          

            string filename =  "test.dat";
            List<Matrix> localKs =  new List<Matrix>();
            List<List<double>> localbs  =  new List<List<double>>();
            Matrix K = new Matrix();
            List<double> b =  new List<double>();
            List<double> T =  new List<double>();

            mesh m =  new mesh();
            utils.leerMallayCondiciones(m,filename);
            sel.crearSistemasLocales(m,localKs,localbs);
           //sel.showKs(localKs);
            
            //Console.WriteLine(localKs[]);
            
          
            
            math.zeroes(K,m.getSize((int)eSizes.NODES));
            math.zeroes(b,m.getSize((int)eSizes.NODES));
            sel.ensamblaje(m,localKs,localbs,K,b);
           //   Console.WriteLine(localKs[0][29][29]);

            sel.applyNeumann(m,b);

           sel.applyDirichlet(m,K,b);
     
          //  sel.showVector(b);


     
           math.zeroes(T,b.Count);
           sel.calculate(K,b,T);

         //   sel.showVector(T);
           // Console.WriteLine(T.Count);
 

//            Console.WriteLine("Hello World!");
        }
    }
}
