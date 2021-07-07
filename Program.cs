using System;
using System.Collections.Generic;

namespace polygot
{
    using Matrix = List<List<double>>;
    class Program
    {
        static void Main(string[] args)
        {
            Utils utils = new Utils();
            sel sel = new sel();
            math math = new math();
            string filename = "test.dat";
            try
            {
                filename = args[0];
            }
            catch (IndexOutOfRangeException e)  // CS0168
            {
                Console.WriteLine(e.Message);
                
                throw new ArgumentOutOfRangeException("Please provide an argument.", e);
            
            }



            List<Matrix> localKs = new List<Matrix>();
            List<List<double>> localbs = new List<List<double>>();
            Matrix K = new Matrix();


            mesh m = new mesh();
            utils.leerMallayCondiciones(m, filename);
            sel.crearSistemasLocales(m, localKs, localbs);

            int nnodes = m.getSize((int)eSizes.NODES);
            List<double> b = new List<double>(new double[3 * nnodes]);
            K = math.MatrixCreate(3 * nnodes, 3 * nnodes);

            sel.ensamblaje(m, localKs, localbs, K, b);

            sel.applyNeumann(m, b);

            for (int i = 0; i < b.Count; i++)
            {
                // Console.WriteLine(b[i]);
            }
            // Array.ForEach<double>(b, Console.WriteLine);
            List<double> T = new List<double>(new double[b.Count]);
            sel.calculate(K, b, T);

            utils.writeResults(m, T, filename);


        }
    }
}
