﻿using System.Collections.Generic;
using System;
namespace polygot
{

class sel{
   math math = new math();
 public void showMatrix(Matrix K){
    
    for(int i=0;i<K[0].Count;i++){
        Console.Write("\t");
        for(int j=0;j<K.Count;j++){
            Console.Write(K[i][j] + "\t");
        }
        Console.Write("\n");
    }
}

public void showKs(List<Matrix> Ks){
    for(int i=0;i<Ks.Count;i++){
        Console.Write( $"K del elemento {i+1}  \n");
        showMatrix(Ks[i]);
        Console.Write("*************************************\n");
    }
}

public void showVector(List<double> b){
    Console.Write("\t");
    for(int i=0;i<b.Count;i++){
        Console.Write($"{b[i]}\t" );
    }
    Console.Write("\t");
}

public void showbs(List<List<double>> bs){
    for(int i=0;i<bs.Count;i++){
        Console.Write( $"b del elemento {i+1}  \n");
        showVector(bs[i]);
          Console.Write("*************************************\n");
    }
}

public double calculateLocalD(int ind,mesh m){
    double D,a,b,c,d,e,f,g,h,i;

    element el = m.getElement(ind);

    node n1 = m.getNode(el.getNode1()-1);
    node n2 = m.getNode(el.getNode2()-1);
    node n3 = m.getNode(el.getNode3()-1);
    node n4 = m.getNode(el.getNode4()-1);

    a=n2.getX()-n1.getX();b=n2.getY()-n1.getY();c=n2.getZ()-n1.getZ();
    d=n3.getX()-n1.getX();e=n3.getY()-n1.getY();f=n3.getZ()-n1.getZ();
    g=n4.getX()-n1.getX();h=n4.getY()-n1.getY();i=n4.getZ()-n1.getZ();
    //Se calcula el determinante de esta matriz utilizando
    //la Regla de Sarrus.
    D = a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i;

    return D;
}



public void calculateLocalU(int i,Matrix U,mesh m){
    
  
    element e = m.getElement(i);
    //calculate c
     node n1 = m.getNode(e.getNode1()-1);
    node n2 = m.getNode(e.getNode2()-1);
    node n3 = m.getNode(e.getNode3()-1);
    node n4 = m.getNode(e.getNode4()-1);
     node n5 = m.getNode(e.getNode5()-1);
    node n6= m.getNode(e.getNode6()-1);
    node n7 = m.getNode(e.getNode7()-1);
    node n8 = m.getNode(e.getNode8()-1);
     node n9 = m.getNode(e.getNode9()-1);
    node n10 = m.getNode(e.getNode10()-1);

    double c1 =	 1/ Math.Pow(n2.getX()  - n1.getX(), 2 );
    double c2 = 	 1/ (n2.getX()  - n1.getX() );
    c2 = 		 c2* ( 4 * n1.getX()   + 4 * n2.getX() - 8 * n8.getX());

    //A
    double A = 	 -(1/(192* Math.Pow(c2,2))) * Math.Pow(4*c1 - c2 ,4);
    A =  		 A -  (1/(24*c2) ) * Math.Pow(4*c1 - c2 ,3);
    A =  		 A -  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1  - c2,5);
    A =  		 A +  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1 + 3* c2 ,5);

    double B = 				-(1/(192* Math.Pow(c2,2))) * Math.Pow(4*c1 + c2 ,4);
    B =  					B +  (1/(24*c2) ) * Math.Pow(4*c1 + c2 ,3);
    B =  					B +  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1  + c2,5);
    B =  					B -  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1 - 3* c2 ,5);

    //C
    double C = (double) (4 / 15) *  Math.Pow (c2,2);

    //D
    double D = 			(1/(192* Math.Pow(c2,2))) * Math.Pow(4*c2 - c1 ,4);
    D = 				D -  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c2 - c1,5);
    D = 				D +  (1/(7680* Math.Pow(c2,3))) * Math.Pow(4*c2  + 8* c1,5);
    D = 				D -  (1/(7680* Math.Pow(c2,3))) * Math.Pow(4*c2  - 8* c1,5);
    D = 			 	D + (1/(768* Math.Pow(c2,3))) * Math.Pow(-8*c1  ,5);
    D = 				D -  (c1/(96* Math.Pow(c2,3))) * Math.Pow(4*c2 - 8* c1 ,4);
    D = 			 	D +  ((2*c1 - 1)/(192* Math.Pow(c2,3))) * Math.Pow(-8*c1,4);

    double E =  (8/3)*Math.Pow(c1,2) + (1/30) * Math.Pow(c2,2);

    double F =  (2/3)*(c1*c2) + (1/30) * Math.Pow(c2,2);

    double G =  -(16/3)*Math.Pow(c1,2) - (4/3) * (c1*c2) - (2/15)* Math.Pow(c2,2);

    double H =  (2/3) * (c1*c2) + (1/30)* Math.Pow(c2,2);

    double I =  -(16/3)*Math.Pow(c1,2)  - (2/3)* Math.Pow(c2,2);

    double J=  -(2/15)*Math.Pow(c2,2);

    double K =  -(4/3) * (c1*c2);

    U[0][0] = A;U[0][1] = E;U[0][4] = -F;U[0][6] = -F;U[0][7] = G;U[0][8] = F;U[0][9] = F;
    U[1][0] = E;U[1][1] = B;U[1][4] = -H;U[1][6] = -H;U[1][7] = I;U[1][8] = H;U[1][9] = H;


    U[4][0] = -F;U[4][1] = -H;U[4][4] = C;U[4][6] = J;U[4][7] = -K;U[4][8] = -C;U[4][9] = -J;

    U[6][0] = -F;U[6][1] = -H;U[6][4] = J;U[6][6] = C;U[6][7] = -K;U[6][8] = -J;U[6][9] = -C;
    U[7][0] = G;U[7][1] = I;U[7][4] = -K;U[7][6] = -K;U[7][7] = D;U[7][8] = K;U[7][9] = K;

    U[8][0] = F;U[8][1] = H;U[8][4] = -C;U[8][6] = -J;U[8][7] = K;U[8][8] = C;U[8][9] = J;
    U[9][0] = F;U[9][1] = H;U[9][4] = -J;U[9][6] = -C;U[9][7] = K;U[9][8] = J;U[9][9] = C;




   
 
}


public Matrix createLocalK(int element,mesh m){
    // K = (k*Ve/D^2)Bt*At*A*B := K_4x4
    double D,Ve,EI = m.getEI();
    Matrix K = new Matrix();
    Matrix U= new Matrix();



    

  
    

    //math.zeroes(A,3);
    math.zeroes(U,10,10);
    calculateLocalU(element,U,m);
    fillLocalK(K,U);


   // calculateLocalA(element,A,m);
   // calculateB(B);
  // math.transpose(A,At);
   // math.transpose(B,Bt);
    // showMatrix(A);
   // math.productRealMatrix(EI*Ve/(D*D),math.productMatrixMatrix(Bt,math.productMatrixMatrix(At,math.productMatrixMatrix(A,B,3,3,4),3,3,4),4,3,4),K);
 
    return K;
}

public void fillLocalK(Matrix K,Matrix U){
    math.zeroes(K,30,30);
    int Usize = U.Count;
    int Usizex2 = 2* U.Count;
    for (int i = 0; i < Usize; i++)
    {
        for (int j = 0; j < Usize; j++)
        {
            K[i][j] = U[i][j];
            K[i + Usize][j+Usize] = U[i][j];
            K[i + Usizex2][j+ Usizex2] = U[i][j];
            
        }
    }
} 

public double calculateLocalJ(int ind,mesh m){
    double J,a,b,c,d,e,f,g,h,i;

    element el = m.getElement(ind);

    node n1 = m.getNode(el.getNode1()-1);
    node n2 = m.getNode(el.getNode2()-1);
    node n3 = m.getNode(el.getNode3()-1);
    node n4 = m.getNode(el.getNode4()-1);

    a=n2.getX()-n1.getX();b=n3.getX()-n1.getX();c=n4.getX()-n1.getX();
    d=n2.getY()-n1.getY();e=n3.getY()-n1.getY();f=n4.getY()-n1.getY();
    g=n2.getZ()-n1.getZ();h=n3.getZ()-n1.getZ();i=n4.getZ()-n1.getZ();
    //Se calcula el determinante de esta matriz utilizando
    //la Regla de Sarrus.
    J = a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i;

    return J;
}

public List<double> createLocalb(int element,mesh m){
    List<double> b = new List<double>();
    List<double> f = new List<double>();
    f = m.getF();
    double J,b_i;
    J = calculateLocalJ(element,m);
// Q 4.5
    b_i = (double)4.5*J/24.0F;
    b.Add(b_i); b.Add(b_i);
    b.Add(b_i); b.Add(b_i);

    return b;
}

public void crearSistemasLocales(mesh m,List<Matrix> localKs,List<List<double>> localbs){
    for(int i=0;i<m.getSize(  (int)eSizes.ELEMENTS);i++){
        localKs.Add(createLocalK(i,m));
        localbs.Add(createLocalb(i,m));
    }
}

public void assemblyK(element e,Matrix localK,Matrix K){
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
 

    K[index1][index1] += localK[0][0];
    K[index1][index2] += localK[0][1];
    K[index1][index3] += localK[0][2];
    K[index1][index4] += localK[0][3];
	K[index1][index5] += localK[0][4];
    K[index1][index6] += localK[0][5];
    K[index1][index7] += localK[0][6];
    K[index1][index8] += localK[0][7];
	K[index1][index9] += localK[0][8];
    K[index1][index10] += localK[0][9];

    K[index2][index1] += localK[1][0];
    K[index2][index2] += localK[1][1];
    K[index2][index3] += localK[1][2];
    K[index2][index4] += localK[1][3];
	K[index2][index5] += localK[1][4];
    K[index2][index6] += localK[1][5];
    K[index2][index7] += localK[1][6];
    K[index2][index8] += localK[1][7];
	K[index2][index9] += localK[1][8];
    K[index2][index10] += localK[1][9];


    K[index3][index1] += localK[2][0];
    K[index3][index2] += localK[2][1];
    K[index3][index3] += localK[2][2];
    K[index3][index4] += localK[2][3];
	K[index3][index5] += localK[2][4];
    K[index3][index6] += localK[2][5];
    K[index3][index7] += localK[2][6];
    K[index3][index8] += localK[2][7];
	K[index3][index9] += localK[2][8];
    K[index3][index10] += localK[2][9];

    K[index4][index1] += localK[3][0];
    K[index4][index2] += localK[3][1];
    K[index4][index3] += localK[3][2];
    K[index4][index4] += localK[3][3];
	K[index4][index5] += localK[3][4];
    K[index4][index6] += localK[3][5];
    K[index4][index7] += localK[3][6];
    K[index4][index8] += localK[3][7];
	K[index4][index9] += localK[3][8];
    K[index4][index10] += localK[3][9];


    K[index5][index1] += localK[4][0];
    K[index5][index2] += localK[4][1];
    K[index5][index3] += localK[4][2];
    K[index5][index4] += localK[4][3];
	K[index5][index5] += localK[4][4];
    K[index5][index6] += localK[4][5];
    K[index5][index7] += localK[4][6];
    K[index5][index8] += localK[4][7];
	K[index5][index9] += localK[4][8];
    K[index5][index10] += localK[4][9];


    K[index6][index1] += localK[5][0];
    K[index6][index2] += localK[5][1];
    K[index6][index3] += localK[5][2];
    K[index6][index4] += localK[5][3];
	K[index6][index5] += localK[5][4];
    K[index6][index6] += localK[5][5];
    K[index6][index7] += localK[5][6];
    K[index6][index8] += localK[5][7];
	K[index6][index9] += localK[5][8];
    K[index6][index10] += localK[5][9];


    K[index7][index1] += localK[6][0];
    K[index7][index2] += localK[6][1];
    K[index7][index3] += localK[6][2];
    K[index7][index4] += localK[6][3];
	K[index7][index5] += localK[6][4];
    K[index7][index6] += localK[6][5];
    K[index7][index7] += localK[6][6];
    K[index7][index8] += localK[6][7];
	K[index7][index9] += localK[6][8];
    K[index7][index10] += localK[6][9];

    K[index8][index1] += localK[7][0];
    K[index8][index2] += localK[7][1];
    K[index8][index3] += localK[7][2];
    K[index8][index4] += localK[7][3];
	K[index8][index5] += localK[7][4];
    K[index8][index6] += localK[7][5];
    K[index8][index7] += localK[7][6];
    K[index8][index8] += localK[7][7];
	K[index8][index9] += localK[7][8];
    K[index8][index10] += localK[7][9];

    K[index9][index1] += localK[8][0];
    K[index9][index2] += localK[8][1];
    K[index9][index3] += localK[8][2];
    K[index9][index4] += localK[8][3];
	K[index9][index5] += localK[8][4];
    K[index9][index6] += localK[8][5];
    K[index9][index7] += localK[8][6];
    K[index9][index8] += localK[8][7];
	K[index9][index9] += localK[8][8];
    K[index9][index10] += localK[8][9];

    K[index10][index1] += localK[9][0];
    K[index10][index2] += localK[9][1];
    K[index10][index3] += localK[9][2];
    K[index10][index4] += localK[9][3];
	K[index10][index5] += localK[9][4];
    K[index10][index6] += localK[9][5];
    K[index10][index7] += localK[9][6];
    K[index10][index8] += localK[9][7];
	K[index10][index9] += localK[9][8];
    K[index10][index10] += localK[9][9];


}

public void assemblyb(element e,List<double> localb,List<double> b){
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
    

}

public void ensamblaje(mesh m,List<Matrix> localKs,List<List<double>> localbs,Matrix K,List<double> b){
    for(int i=0;i<m.getSize((int)eSizes.ELEMENTS);i++){
        element e = m.getElement(i);
        assemblyK(e,localKs[i],K);
        assemblyb(e,localbs[i],b);
    }

   
}

public void applyNeumann(mesh m,List<double> b){
    for(int i=0;i<m.getSize((int)eSizes.NEUMANN);i++){
        condition c = m.getCondition(i,(int)eSizes.NEUMANN);
        b[c.getNode1()-1] += c.getValue();
    }
}

public void applyDirichlet(mesh m,Matrix K,List<double> b){

    for(int i=0;i<m.getSize((int)eSizes.DIRICHLET);i++){
  
        condition c = m.getCondition(i,(int)eSizes.DIRICHLET);
        int index = c.getNode1()-1;


        K.Remove(K[0+index]);
        b.Remove(b[0+index]);

   
        for(int row=0;row<K.Count;row++){
            double cell = K[row][index];
        
            K[row].RemoveAt(index);
            b[row] += -1*c.getValue()*cell;
        }
    }
}

public void calculate(Matrix K, List<double> b, List<double> T){
    Console.WriteLine("Iniciando calculo de respuesta...\n");
    Matrix Kinv = new Matrix();
      Console.Write("Calculo de la inversa\n");
    math.inverseMatrix(K,Kinv);
    Console.Write("Caclulo de la inversa\n");
    math.productMatrixVector(Kinv,b,T);
}
}
}
