using System.Collections.Generic;
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

public void showVector(List<float> b){
    Console.Write("\t");
    for(int i=0;i<b.Count;i++){
        Console.Write($"{b[i]}\t" );
    }
    Console.Write("\t");
}

public void showbs(List<List<float>> bs){
    for(int i=0;i<bs.Count;i++){
        Console.Write( $"b del elemento {i+1}  \n");
        showVector(bs[i]);
          Console.Write("*************************************\n");
    }
}

public float calculateLocalD(int ind,mesh m){
    float D,a,b,c,d,e,f,g,h,i;

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

    float c1 = 1/ Math.Pow(n2.getX()  - n1.getX(), 2 );
    float c2 =  1/ (n2.getX()  - n1.getX() );
    c2 = c2* ( 4 * n1.getX()   + 4 * n2.getX() - 8 * n8.getX());

    //A
    float A = -(1/(192* Math.Pow(c2,2))) * Math.Pow(4*c1 - c2 ,4);
    A =  A -  (1/(24*c2) ) * Math.Pow(4*c1 - c2 ,3);
    A =  A -  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1  - c2,5);
    A =  A +  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1 + 3* c2 ,5);

    //B
    float B = -(1/(192* Math.Pow(c2,2))) * Math.Pow(4*c1 + c2 ,4);
    B =  B +  (1/(24*c2) ) * Math.Pow(4*c1 + c2 ,3);
    B =  B +  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1  + c2,5);
    B =  B -  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c1 - 3* c2 ,5);

    //C
    float C =  (4 / 15) *  Math.Pow (c2,2);

    //D
    float D = (1/(192* Math.Pow(c2,2))) * Math.Pow(4*c2 - c1 ,4);
    D =  D -  (1/(3840* Math.Pow(c2,3))) * Math.Pow(4*c2 - c1,5);
    D =  D +  (1/(7680* Math.Pow(c2,3))) * Math.Pow(4*c2  + 8* c1,5);
    D =  D -  (1/(7680* Math.Pow(c2,3))) * Math.Pow(4*c2  - 8* c1,5);
    D =  D + (1/(768* Math.Pow(c2,3))) * Math.Pow(-8*c1  ,5);
    D =  D -  (c1/(96* Math.Pow(c2,3))) * Math.Pow(4*c2 - 8* c1 ,4);
    D =  D +  ((2*c1 - 1)/(192* Math.Pow(c2,3))) * Math.Pow(-8*c1,4);

    float E = (8/3)*Math.Pow(c1,2) + (1/30) * Math.Pow(c2,2);

    float F = (2/3)*(c1*c2) + (1/30) * Math.Pow(c2,2);

    float G = -(16/3)*Math.Pow(c1,2) - (4/3) * (c1*c2) - (2/15)* Math.Pow(c2,2);

    float H = (2/3) * (c1*c2) + (1/30)* Math.Pow(c2,2);

    float I = -(16/3)*Math.Pow(c1,2)  - (2/3)* Math.Pow(c2,2);

    float J= -(2/15)*Math.Pow(c2,2);

    float K = -(4/3) * (c1*c2);

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
    float D,Ve,EI = m.getEI();
    Matrix K = new Matrix();
    Matrix A= new Matrix();
    Matrix B= new Matrix();
    Matrix U= new Matrix();
    Matrix At= new Matrix();


    

    D = calculateLocalD(element,m);
    Ve = 3;

    //math.zeroes(A,3);
    math.zeroes(U,10,10);
    calculateLocalU(U);
   // calculateLocalA(element,A,m);
   // calculateB(B);
  // math.transpose(A,At);
   // math.transpose(B,Bt);
    // showMatrix(A);
   // math.productRealMatrix(EI*Ve/(D*D),math.productMatrixMatrix(Bt,math.productMatrixMatrix(At,math.productMatrixMatrix(A,B,3,3,4),3,3,4),4,3,4),K);
 
    return K;
}

public float calculateLocalJ(int ind,mesh m){
    float J,a,b,c,d,e,f,g,h,i;

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

public List<float> createLocalb(int element,mesh m){
    List<float> b = new List<float>();
    List<float> f = new List<float>();
    f = m.getF();
    float J,b_i;
    J = calculateLocalJ(element,m);
// Q 4.5
    b_i = (float)4.5*J/24.0F;
    b.Add(b_i); b.Add(b_i);
    b.Add(b_i); b.Add(b_i);

    return b;
}

public void crearSistemasLocales(mesh m,List<Matrix> localKs,List<List<float>> localbs){
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
 

    K[index1][index1] += localK[0][0];
    K[index1][index2] += localK[0][1];
    K[index1][index3] += localK[0][2];
    K[index1][index4] += localK[0][3];
    K[index2][index1] += localK[1][0];
    K[index2][index2] += localK[1][1];
    K[index2][index3] += localK[1][2];
    K[index2][index4] += localK[1][3];
    K[index3][index1] += localK[2][0];
    K[index3][index2] += localK[2][1];
    K[index3][index3] += localK[2][2];
    K[index3][index4] += localK[2][3];
    K[index4][index1] += localK[3][0];
    K[index4][index2] += localK[3][1];
    K[index4][index3] += localK[3][2];
    K[index4][index4] += localK[3][3];
}

public void assemblyb(element e,List<float> localb,List<float> b){
    int index1 = e.getNode1() - 1;
    int index2 = e.getNode2() - 1;
    int index3 = e.getNode3() - 1;
    int index4 = e.getNode4() - 1;

    b[index1] += localb[0];
    b[index2] += localb[1];
    b[index3] += localb[2];
    b[index4] += localb[3];
    
}

public void ensamblaje(mesh m,List<Matrix> localKs,List<List<float>> localbs,Matrix K,List<float> b){
    for(int i=0;i<m.getSize((int)eSizes.ELEMENTS);i++){
        element e = m.getElement(i);
        assemblyK(e,localKs[i],K);
        assemblyb(e,localbs[i],b);
    }

   
}

public void applyNeumann(mesh m,List<float> b){
    for(int i=0;i<m.getSize((int)eSizes.NEUMANN);i++){
        condition c = m.getCondition(i,(int)eSizes.NEUMANN);
        b[c.getNode1()-1] += c.getValue();
    }
}

public void applyDirichlet(mesh m,Matrix K,List<float> b){

    for(int i=0;i<m.getSize((int)eSizes.DIRICHLET);i++){
  
        condition c = m.getCondition(i,(int)eSizes.DIRICHLET);
        int index = c.getNode1()-1;


        K.Remove(K[0+index]);
        b.Remove(b[0+index]);

   
        for(int row=0;row<K.Count;row++){
            float cell = K[row][index];
        
            K[row].RemoveAt(index);
            b[row] += -1*c.getValue()*cell;
        }
    }
}

public void calculate(Matrix K, List<float> b, List<float> T){
    Console.WriteLine("Iniciando calculo de respuesta...\n");
    Matrix Kinv = new Matrix();
      Console.Write("Calculo de la inversa\n");
    math.inverseMatrix(K,Kinv);
    Console.Write("Caclulo de la inversa\n");
    math.productMatrixVector(Kinv,b,T);
}
}
}
