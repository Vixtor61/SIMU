using System.Collections.Generic;
using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace polygot
{
    using Matrix = List<List<double>>;
    class sel
    {
        math math = new math();
        public void showMatrix(Matrix K)
        {
            for (int i = 0; i < K.Count; i++)
            {
                Console.Write("[");

                for (int j = 0; j < K[i].Count; j++)
                {

                    Console.Write("{0:N2}  ", K[i][j]);
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }

        public void showKs(List<Matrix> Ks)
        {
            for (int i = 0; i < Ks.Count; i++)
            {
                Console.Write($"K del elemento {i + 1}  \n");
                showMatrix(Ks[i]);
                Console.Write("*************************************\n");
            }
        }

        public void showVector(List<double> b)
        {
            Console.Write("\t");
            for (int i = 0; i < b.Count; i++)
            {
                Console.Write("{0:N2}  ", b[i]);

            }
            Console.Write("\n");
        }

        public void showbs(List<List<double>> bs)
        {
            for (int i = 0; i < bs.Count; i++)
            {
                Console.Write($"b del elemento {i + 1}  \n");
                showVector(bs[i]);
                Console.Write("*************************************\n");
            }
        }
            // checks if value is too small wich can cause problems doing mathematical operations
        public void checkDelta(ref double n1, ref double delta)
        {
            if ((Math.Abs(n1)) < delta)
            {
                n1 = delta;
            }
        }

        public void calculateLocalU(int i, Matrix U, mesh m)
        {


            element e = m.getElement(i);

            node n1 = m.getNode(e.getNode1() - 1);
            node n2 = m.getNode(e.getNode2() - 1);
            node n3 = m.getNode(e.getNode3() - 1);
            node n4 = m.getNode(e.getNode4() - 1);
            node n5 = m.getNode(e.getNode5() - 1);
            node n6 = m.getNode(e.getNode6() - 1);
            node n7 = m.getNode(e.getNode7() - 1);
            node n8 = m.getNode(e.getNode8() - 1);
            node n9 = m.getNode(e.getNode9() - 1);
            node n10 = m.getNode(e.getNode10() - 1);

            double delta = 0.0000001;

            double x_1 = n1.getX();
            double x_2 = n2.getX();
            double x_8 = n8.getX();

            double subn1n2 = x_2 - x_1;
            checkDelta(ref subn1n2, ref delta); // checks if the substraction between n2_x and n1_x is 0

            // calculate c1 and c2 using n1_x n2_x n8_x


            double c1 = (1.0) / (Math.Pow(subn1n2, 2));//c1
            double c2 = ((4.0 * x_1) + (4.0 * x_2) - (8.0 * x_8)) / (subn1n2);//c2




            checkDelta(ref c1, ref delta);
            checkDelta(ref c2, ref delta);

            double n192c2pow2 = (192.0 * Math.Pow(c2, 2.0));
            double n192c2pow3 = (192.0 * Math.Pow(c2, 3.0));
            double n3840c2pow3 = (3840.0 * Math.Pow(c2, 3.0));



            double n7680c2pow3 = (7680.0 * Math.Pow(c2, 3.0));
            double n768c2pow3 = (7680.0 * Math.Pow(c2, 3.0));
            double n96c2pow3 = (96.0 * Math.Pow(c2, 3.0));
            double n24c2 = 24.0 * c2;

            checkDelta(ref n192c2pow2, ref delta);
            checkDelta(ref n192c2pow3, ref delta);
            checkDelta(ref n3840c2pow3, ref delta);
            checkDelta(ref n7680c2pow3, ref delta);
            checkDelta(ref n768c2pow3, ref delta);
            checkDelta(ref n96c2pow3, ref delta);
            checkDelta(ref n24c2, ref delta);

            double n8c1 = 8.0 * c1;
            double n4c2 = 4.0 * c2;
            double n3c2 = 3.0 * c2;
            double n4c1 = 4.0 * c1;

            double c1pow2 = Math.Pow(c1, 2);
            double c2pow2 = Math.Pow(c2, 2);

            //calculate u matrix components
            double A = (-Math.Pow(n4c1 - c2, 4.0) / n192c2pow2) - (Math.Pow(n4c1 - c2, 3.0) / n24c2) 
                        - (Math.Pow(n4c1 - c2, 5.0) / n3840c2pow3) + ((Math.Pow(n4c1 + n3c2, 5.0) / (n3840c2pow3)));

            double B = -Math.Pow(n4c1 + c2, 4.0) / n192c2pow2 + Math.Pow(n4c1 + c2, 3) / n24c2
                        + Math.Pow(n4c1 + c2, 5.0) / (n3840c2pow3) - Math.Pow(n4c1 - n3c2, 5.0) / n3840c2pow3;
            
            double C = ((4.0 * Math.Pow(c2, 2)) / 15.0);

            
            double D = (Math.Pow(n4c2 - c1, 4) / n192c2pow2) - (Math.Pow(n4c2 - c1, 5.0) / n3840c2pow3)
                        + (Math.Pow(n4c2 + n8c1, 5) / n7680c2pow3) - ((7.0 * Math.Pow(n4c2 - n8c1, 5)) / n7680c2pow3)
                        + Math.Pow(-n8c1, 5) / n768c2pow3 - (c1 * Math.Pow(n4c2 - n8c1, 4)) / n96c2pow3
                        + ((2.0 * c1 - 1.0) * Math.Pow((-n8c1), 4)) / n192c2pow3;

            double E = ((8.0 * c1pow2) / 3.0) + (c2pow2 / 30.0);

            double F = ((2.0 * c1 * c2) / 3.0) - (c2pow2 / 30.0);

            double G = -((16.0 * c1pow2)) / 3.0 - ((4.0 * c1 * c2) / 3.0) - ((2 * c2pow2) / 15.0);

            double H = ((2.0 * c1 * c2) / (3.0)) + (c2pow2 / 30.0);

            double I = -((16.0 * c1pow2) / 3.0) - ((2.0 * c2pow2) / 3.0);

            double J = (2.0 * c2pow2) / 15.0;



            double K = -((4.0 * c1 * c2) / 3.0);



            //    Console.WriteLine($"A {A} B {B} C {C} D {D} E {E} F {F} G {G} H {H} I {I} J {J} K{K} ");//check matrix values
            //    Console.WriteLine();
            //    u matrix
            U[0][0] = A; U[0][1] = E; U[0][4] = -F; U[0][6] = -F; U[0][7] = G; U[0][8] = F; U[0][9] = F;
            U[1][0] = E; U[1][1] = B; U[1][4] = -H; U[1][6] = -H; U[1][7] = I; U[1][8] = H; U[1][9] = H;


            U[4][0] = -F; U[4][1] = -H; U[4][4] = C; U[4][6] = J; U[4][7] = -K; U[4][8] = -C; U[4][9] = -J;

            U[6][0] = -F; U[6][1] = -H; U[6][4] = J; U[6][6] = C; U[6][7] = -K; U[6][8] = -J; U[6][9] = -C;
            U[7][0] = G; U[7][1] = I; U[7][4] = -K; U[7][6] = -K; U[7][7] = D; U[7][8] = K; U[7][9] = K;

            U[8][0] = F; U[8][1] = H; U[8][4] = -C; U[8][6] = -J; U[8][7] = K; U[8][8] = C; U[8][9] = J;
            U[9][0] = F; U[9][1] = H; U[9][4] = -J; U[9][6] = -C; U[9][7] = K; U[9][8] = J; U[9][9] = C;



        }


        public Matrix createLocalK(int element, mesh m, double J)
        {

            double EI = m.getEI();
            Matrix K = new Matrix();
            Matrix U = new Matrix();

            U = math.MatrixCreate(10, 10);
            calculateLocalU(element, U, m);
            K = math.MatrixCreate(30, 30);
            fillLocalK(K, U);

            double constant = EI * J;
            Console.WriteLine(constant);
            math.productRealMatrix2(constant, K);

            return K;

        }

        //puts u inside the final local k matrix
        public void fillLocalK(Matrix K, Matrix U)
        {

            int Usize = U.Count;
            int Usizex2 = 2 * U.Count;

            for (int i = 0; i < Usize; i++)
            {
                for (int j = 0; j < Usize; j++)
                {
                    //Console.WriteLine($"{i} {j}       {i + Usize} {j+Usizex2}       {i}{j+Usizex2}");

                    K[i][j] = U[i][j];
                    K[i + Usize][j + Usize] = U[i][j];
                    K[i + Usizex2][j + Usizex2] = U[i][j];


                }
            }

        }

        public double calculateLocalJ(int ind, mesh m)
        {
            double J, a, b, c, d, e, f, g, h, i;

            element el = m.getElement(ind);

            node n1 = m.getNode(el.getNode1() - 1);
            node n2 = m.getNode(el.getNode2() - 1);
            node n3 = m.getNode(el.getNode3() - 1);
            node n4 = m.getNode(el.getNode4() - 1);

            a = n2.getX() - n1.getX(); b = n3.getX() - n1.getX(); c = n4.getX() - n1.getX();
            d = n2.getY() - n1.getY(); e = n3.getY() - n1.getY(); f = n4.getY() - n1.getY();
            g = n2.getZ() - n1.getZ(); h = n3.getZ() - n1.getZ(); i = n4.getZ() - n1.getZ();

            J = a * e * i + d * h * c + g * b * f - g * e * c - a * h * f - d * b * i;

            return J;
        }

        public List<double> createLocalb(int element, mesh m, double J)
        {
            List<double> b = new List<double>(new double[30]);
            Matrix M = new Matrix();

            M = math.MatrixCreate(30, 30);

            List<double> t = new List<double>();

            // Q 4.5
            M[0][0] = 59.0; M[10][1] = 59.0; M[20][2] = 59.0;
            M[1][0] = -1.0; M[11][1] = -1.0; M[21][2] = -1.0;
            M[2][0] = -1.0; M[12][1] = -1.0; M[22][2] = -1.0;
            M[3][0] = -1.0; M[13][1] = -1.0; M[23][2] = -1.0;
            M[4][0] = 4.0; M[14][1] = 4.0; M[24][2] = 4.0;
            M[5][0] = 4.0; M[15][1] = 4.0; M[25][2] = 4.0;
            M[6][0] = 4.0; M[16][1] = 4.0; M[26][2] = 4.0;
            M[7][0] = 4.0; M[17][1] = 4.0; M[27][2] = 4.0;
            M[8][0] = 4.0; M[18][1] = 4.0; M[28][2] = 4.0;
            M[9][0] = 4.0; M[19][1] = 4.0; M[29][2] = 4.0;

            math.productMatrixVector(M, m.getF(), b);
            double c = J / 120.0;

            for (int i = 0; i < b.Count; i++)
            {
                b[i] = c * b[i];
            }

            return b;
        }

        public void crearSistemasLocales(mesh m, List<Matrix> localKs, List<List<double>> localbs)
        {
            for (int i = 0; i < m.getSize((int)eSizes.ELEMENTS); i++)
            {
                double J = calculateLocalJ(i, m);
                localKs.Add(createLocalK(i, m, J));
                localbs.Add(createLocalb(i, m, J));
            }
        }

        public void assemblyK(element e, Matrix localK, Matrix K)
        {

            //a index
            int nnodes = K.Count / 3;
            int nindex = 10;
            int eid = e.getId();
            int index1 = e.getNode1() - 1;
            int index2 = e.getNode2() - 1;
            int index3 = e.getNode3() - 1;
            int index4 = e.getNode4() - 1;
            int index5 = e.getNode5() - 1;
            int index6 = e.getNode6() - 1;
            int index7 = e.getNode7() - 1;
            int index8 = e.getNode8() - 1;
            int index9 = e.getNode9() - 1;
            int index10 = e.getNode10() - 1;
            int[] indexs = new int[3 * nindex];
            indexs[0] = index1;
            indexs[1] = index2;
            indexs[2] = index3;
            indexs[3] = index4;
            indexs[4] = index5;
            indexs[5] = index6;
            indexs[6] = index7;
            indexs[7] = index8;
            indexs[8] = index9;
            indexs[9] = index10;

            for (int i = 10; i < 20; i++)
            {
                indexs[i] = indexs[i - 10] + nnodes;
            }
            for (int i = 20; i < 30; i++)
            {
                indexs[i] = indexs[i - 20] + 2 * nnodes;
            }


            for (int i = 0; i < 30; i++)
            {
                //   Console.Write($"{indexs[i]} ");
            }
            //  Console.WriteLine();


            for (int i = 0; i < localK.Count; i++)
            {
                for (int j = 0; j < localK.Count; j++)
                {
                    int krow = indexs[i];
                    int kcol = indexs[j];

                    K[krow][kcol] += localK[i][j];

                }

            }




        }

        public void assemblyb(element e, List<double> localb, List<double> b)
        {
            int nnodes = localb.Count / 3;
            int nnodes2 = 2 * nnodes;
            int index1 = e.getNode1() - 1;
            int index2 = e.getNode2() - 1;
            int index3 = e.getNode3() - 1;
            int index4 = e.getNode4() - 1;
            int index5 = e.getNode5() - 1;
            int index6 = e.getNode6() - 1;
            int index7 = e.getNode7() - 1;
            int index8 = e.getNode8() - 1;
            int index9 = e.getNode9() - 1;
            int index10 = e.getNode10() - 1;

            b[index1] += localb[0];
            b[index2] += localb[1];
            b[index3] += localb[2];
            b[index4] += localb[3];
            b[index5] += localb[4];
            b[index6] += localb[5];
            b[index7] += localb[6];
            b[index8] += localb[7];
            b[index9] += localb[8];
            b[index10] += localb[9];

            b[index1 + nnodes] += localb[10];
            b[index2 + nnodes] += localb[11];
            b[index3 + nnodes] += localb[12];
            b[index4 + nnodes] += localb[13];
            b[index5 + nnodes] += localb[14];
            b[index6 + nnodes] += localb[15];
            b[index7 + nnodes] += localb[16];
            b[index8 + nnodes] += localb[17];
            b[index9 + nnodes] += localb[18];
            b[index10 + nnodes] += localb[19];

            b[index1 + nnodes2] += localb[20];
            b[index2 + nnodes2] += localb[21];
            b[index3 + nnodes2] += localb[22];
            b[index4 + nnodes2] += localb[23];
            b[index5 + nnodes2] += localb[24];
            b[index6 + nnodes2] += localb[25];
            b[index7 + nnodes2] += localb[26];
            b[index8 + nnodes2] += localb[27];
            b[index9 + nnodes2] += localb[28];
            b[index10 + nnodes2] += localb[29];


        }

        public void ensamblaje(mesh m, List<Matrix> localKs, List<List<double>> localbs, Matrix K, List<double> b)
        {
            for (int i = 0; i < m.getSize((int)eSizes.ELEMENTS); i++)
            {
                element e = m.getElement(i);
                assemblyK(e, localKs[i], K);
                assemblyb(e, localbs[i], b);
            }
        }

        public void applyNeumann(mesh m, List<double> b)
        {
            for (int i = 0; i < m.getSize((int)eSizes.NEUMANN); i++)
            {
                condition c = m.getCondition(i, (int)eSizes.NEUMANN);
                b[c.getNode1() - 1] += c.getValue();
            }
        }

        public void applyDirichlet(mesh m, Matrix K, List<double> b)
        {

            for (int i = 0; i < m.getSize((int)eSizes.DIRICHLET); i++)
            {
                condition c = m.getCondition(i, (int)eSizes.DIRICHLET);
                int index = c.getNode1() - 1;

                K.Remove(K[0 + index]);
                b.Remove(b[0 + index]);

                for (int row = 0; row < K.Count; row++)
                {
                    double cell = K[row][index];

                    K[row].RemoveAt(index);
                    b[row] += -1 * c.getValue() * cell;
                }
            }
        }

        public void calculate(Matrix K, List<double> b, List<double> T)
        {

            Matrix<double> A = DenseMatrix.OfArray(new double[K.Count, K.Count]);
            for (int i = 0; i < K.Count; i++)
            {
                for (int j = 0; j < K.Count; j++)
                {
                    //Console.Write( K[i][j]);
                    A[i, j] = K[i][j];
                }
            }

            A = A.Inverse();


            Console.WriteLine("Iniciando calculo de respuesta...\n");
            //Matrix Kinv = new Matrix();
            Matrix Kinv = math.MatrixCreate(K.Count, K.Count);

            for (int i = 0; i < K.Count; i++)
            {
                for (int j = 0; j < K.Count; j++)
                {
                    //Console.Write( K[i][j]);
                    Kinv[i][j] = A[i, j];
                }
            }
            //  Matrix Kinv2 = new Matrix();
            Console.Write("Calculo de la inversa\n");


            Console.WriteLine();

            //   Kinv = test.MatrixInverse(K);
            //   math.inverseMatrix(K, Kinv);
            Console.WriteLine(K[0].Count);
            Console.WriteLine(b.Count);

            Console.WriteLine(T.Count);

            math.productMatrixVector(Kinv, b, T);
        }
    }
}
