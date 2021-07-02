using System;
using System.Collections.Generic;

namespace polygot
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils utils =  new Utils();
            sel sel = new sel();
            math math = new math();
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
            sel.crearSistemasLocales(m,localKs,localbs);
            //Console.WriteLine(localKs[]);
            /*
            Console.WriteLine("LocalKs");
            sel.showKs(localKs);
            sel.showbs(localbs);
            */
         //   sel.showVector(localbs[2]);

            
            math.zeroes(K,m.getSize((int)eSizes.NODES));
            math.zeroes(b,m.getSize((int)eSizes.NODES));
            sel.ensamblaje(m,localKs,localbs,K,b);
    

            sel.applyNeumann(m,b);

            sel.applyDirichlet(m,K,b);
     
          //  sel.showVector(b);


            sel.showMatrix(K);
            math.zeroes(T,b.Count);
            sel.calculate(K,b,T);

            sel.showVector(T);
  
 

            Console.WriteLine("Hello World!");
        }
    }
}
