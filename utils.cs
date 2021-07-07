
using System.Collections.Generic;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.IO;
namespace polygot
{

    using Matrix = List<List<double>>;
    class Utils
    {
        //Reads diritchlet data and sets the indexes for it to work with 3 components
        public int obtenerDatosDiritchlet(string[] filelines, eLines nlines, int n, eModes mode, item[] item_list, int lineCont, int dirichx, int dirichy, int dirichz, int nnodes)
        {
            Console.WriteLine();
            int nnodes2 = 2 * nnodes;
            string[] constants = { };
            lineCont++;
            if (nlines == eLines.DOUBLELINE) lineCont++;

            for (int i = 0; i < dirichx; i++)
            {
                constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int e0; double r0;

                e0 = int.Parse(constants[0]);
                Console.WriteLine(e0);
                r0 = double.Parse(constants[1]);
                item_list[i].setValues((int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                e0, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                r0);

            }
            int a = dirichx;
            int b = dirichy + dirichx;
            Console.WriteLine();
            for (int i = a; i < b; i++)
            {
                constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int e0; double r0;
                e0 = int.Parse(constants[0]) + nnodes;
                r0 = double.Parse(constants[1]);
                Console.WriteLine(e0);
                item_list[i].setValues((int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                e0, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                r0);

            }
            a = b;
            b = b + dirichz;
            Console.WriteLine();
            for (int i = a; i < b; i++)
            {
                constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int e0; double r0;

                e0 = int.Parse(constants[0]) + nnodes2;
                Console.WriteLine(e0);
                r0 = double.Parse(constants[1]);
                item_list[i].setValues((int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                e0, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                r0);

            }

            return lineCont;
        }

        public int obtenerDatos(string[] filelines, eLines nlines, int n, eModes mode, item[] item_list, int lineCont)
        {
            string[] constants = { };
            lineCont++;
            if (nlines == eLines.DOUBLELINE) lineCont++;

            for (int i = 0; i < n; i++)
            {
                switch (mode)
                {
                    case eModes.INT_FLOAT:

                        constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        int e0; double r0;

                        e0 = int.Parse(constants[0]);
                        r0 = double.Parse(constants[1]);
                        item_list[i].setValues((int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                        e0, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                        r0);
                        break;
                    case eModes.INT_FLOAT_FLOAT_FLOAT:

                        constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        int e; double r, rr, rrr;

                        e = int.Parse(constants[0]);
                        r = double.Parse(constants[1]);
                        rr = double.Parse(constants[2]);
                        rrr = double.Parse(constants[3]);

                        item_list[i].setValues(e, r, rr, rrr,
                        (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                        (double)eIndicator.NOTHING);
                        break;
                    case eModes.INT_INT_INT_INT_INT:
                        constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        int e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11;

                        //sets the id and the 10 nodes of the element
                        e1 = int.Parse(constants[0]);
                        e2 = int.Parse(constants[1]);
                        e3 = int.Parse(constants[2]);
                        e4 = int.Parse(constants[3]);
                        e5 = int.Parse(constants[4]);
                        e6 = int.Parse(constants[5]);
                        e7 = int.Parse(constants[6]);
                        e8 = int.Parse(constants[7]);
                        e9 = int.Parse(constants[8]);
                        e10 = int.Parse(constants[9]);
                        e11 = int.Parse(constants[10]);

                        item_list[i].setValues(e1, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING, (int)eIndicator.NOTHING,
                        e2, e3, e4, e5, e6, e7, e8, e9, e10, e11,
                        (double)eIndicator.NOTHING);
                        break;
                }
            }
            return lineCont;
        }

        public void correctConditions(int n, condition[] list, int[] indices)
        {
            for (int i = 0; i < n; i++)
            {
                indices[i] = list[i].getNode1();

            }
            for (int i = 0; i < n - 1; i++)
            {
                int pivot = list[i].getNode1();
                for (int j = i; j < n; j++)
                    if (list[j].getNode1() > pivot)
                        list[j].setNode1(list[j].getNode1() - 1);
            }
        }


        public void leerMallayCondiciones(mesh m, String filename)
        {
            String[] filelines;
            filelines = File.ReadAllLines(filename);
            int lineCont = 0;//keeps track of the current line inside the file
            double EI, Q;
            List<double> f = new List<double>();
            int nnodes, neltos, ndirich, nneu;

            string[] constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);// splits values on the line
            Array.ForEach<string>(constants, Console.WriteLine);
            EI = double.Parse(constants[0]);

            f.Add(double.Parse(constants[1]));//f_x
            f.Add(double.Parse(constants[2]));//f_y
            f.Add(double.Parse(constants[3]));//f_z


            constants = filelines[lineCont++].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            nnodes = int.Parse(constants[0]);
            neltos = int.Parse(constants[1]);

            int dirichx = int.Parse(constants[2]);//w_x
            int dirichy = int.Parse(constants[3]);//w_y
            int dirichz = int.Parse(constants[4]);//w_z
            ndirich = dirichx + dirichy + dirichz;


            nneu = int.Parse(constants[5]);



            m.setParameters(EI, f);
            m.setSizes(nnodes, neltos, ndirich, nneu);
            m.createData();


            lineCont++;

            lineCont = obtenerDatos(filelines, eLines.SINGLELINE, nnodes, eModes.INT_FLOAT_FLOAT_FLOAT, m.getNodes(), lineCont);
            lineCont++;
            lineCont = obtenerDatos(filelines, eLines.DOUBLELINE, neltos, eModes.INT_INT_INT_INT_INT, m.getElements(), lineCont);
            lineCont++;
            lineCont = obtenerDatosDiritchlet(filelines, eLines.DOUBLELINE, ndirich, eModes.INT_FLOAT, m.getDirichlet(), lineCont, dirichx, dirichy, dirichz, nnodes);
            lineCont++;
            lineCont = obtenerDatos(filelines, eLines.DOUBLELINE, nneu, eModes.INT_FLOAT, m.getNeumann(), lineCont);


            correctConditions(ndirich, m.getDirichlet(), m.getDirichletIndices());
        }

        public bool findIndex(int v, int s, int[] arr)
        {
            for (int i = 0; i < s; i++)
                if (arr[i] == v) return true;
            return false;
        }

        public async Task writeResults(mesh m, List<double> T, string filename)
        {
            String outputfilename;
            outputfilename = filename + ".post.res";
            using StreamWriter file = new(outputfilename);

            int[] dirich_indices = m.getDirichletIndices();
            condition[] dirich = m.getDirichlet();
            int Tpos = 0;
            int Dpos = 0;
            int n = 3 * (m.getSize((int)eSizes.NODES));
            int nd = m.getSize((int)eSizes.DIRICHLET);

            await file.WriteLineAsync("Fourth line");
            await file.WriteLineAsync("GiD Post Results File 1.0\n");
            await file.WriteLineAsync("Result \"Temperature\" \"Load Case 1\" 1 Scalar OnNodes\nComponentNames \"T\"\nValues\n");
            
            for (int i = 0; i < n; i++)
            {
                if (findIndex(i + 1, nd, dirich_indices))
                {
                    await file.WriteLineAsync($"{i + 1} {dirich[Dpos].getValue()}");
                    Dpos++;
                }
                else
                {
                    await file.WriteLineAsync($"{i + 1} {T[Tpos]}");
                    Tpos++;
                }
            }
            await file.WriteLineAsync("End values");
        }



    }
}
