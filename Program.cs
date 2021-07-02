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
            Console.WriteLine("LocalKs");

            sel.showVector(localbs[2]);

            
            math.zeroes(K,m.getSize((int)eSizes.NODES));
            math.zeroes(b,m.getSize((int)eSizes.NODES));
            sel.ensamblaje(m,localKs,localbs,K,b);
            sel.showMatrix(K);

            Console.WriteLine(K[0].Count);
            Console.WriteLine(b.Count);
            sel.applyNeumann(m,b);

            sel.applyDirichlet(m,K,b);
            sel.showMatrix(K);
            math.zeroes(T,b.Count);
            sel.calculate(K,b,T);
    //showMatrix(K); showVector(b);
   // cout << "******************************\n";
    //cout << K.size() << " - "<<K.at(0).size()<<"\n";
    //cout << b.size() <<"\n";
/*
    
    //showMatrix(K); showVector(b);
    cout << "******************************\n";
    //cout << K.size() << " - "<<K.at(0).size()<<"\n";
    //cout << b.size() <<"\n";

    applyDirichlet(m,K,b);
    //showMatrix(K); showVector(b);
    cout << "******************************\n";
    //cout << K.size() << " - "<<K.at(0).size()<<"\n";
    //cout << b.size() <<"\n";

    zeroes(T,b.size());
    calculate(K,b,T);
*/

            Console.WriteLine("Hello World!");
        }
    }
}
