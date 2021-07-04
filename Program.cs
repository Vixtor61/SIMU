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
          
            
            string filename =  args[0] ?? "test.dat" ;

          
        //  string filename = "cube.dat";
            List<Matrix> localKs =  new List<Matrix>();
            List<List<double>> localbs  =  new List<List<double>>();
            Matrix K = new Matrix();
     
            List<double> T =  new List<double>();

            mesh m =  new mesh();
            utils.leerMallayCondiciones(m,filename);
            sel.crearSistemasLocales(m,localKs,localbs);


      //   sel.showKs(localKs);
                  
          
          int nnodes = m.getSize((int)eSizes.NODES);
           List<double> b =  new List<double>(new double[3*nnodes]);
          K =math.MatrixCreate(3*nnodes,3*nnodes);
          
          sel.ensamblaje(m,localKs,localbs,K,b);
         // sel.showMatrix(K);
          
           sel.applyNeumann(m,b);
           sel.applyDirichlet(m,K,b);
    
     
           math.zeroes(T,b.Count);
           sel.calculate(K,b,T);

         //   sel.showVector(T);
           // Console.WriteLine(T.Count);
 

//            Console.WriteLine("Hello World!");
        }
    }
}
