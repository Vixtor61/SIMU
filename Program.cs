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

            List<Matrix> localKs =  new List<Matrix>();
            List<List<double>> localbs  =  new List<List<double>>();
            Matrix K = new Matrix();
   

            mesh m =  new mesh();
            utils.leerMallayCondiciones(m,filename);
            sel.crearSistemasLocales(m,localKs,localbs);


                  
          
          int nnodes = m.getSize((int)eSizes.NODES);
           List<double> b =  new List<double>(new double[3*nnodes]);
          K =math.MatrixCreate(3*nnodes,3*nnodes);
          
          sel.ensamblaje(m,localKs,localbs,K,b);
          sel.showMatrix(K);

          
           sel.applyNeumann(m,b);
           sel.applyDirichlet(m,K,b);
    
            List<double> T =  new List<double>(new double[b.Count]);
           sel.calculate(K,b,T);

           utils.writeResults(m,T,filename);


        }
    }
}
