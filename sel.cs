﻿using System.Collections.Generic;
using System;
namespace polygot
{

class sel{
   
void showMatrix(Matrix K){
    
    for(int i=0;i<K[0].Count;i++){
        Console.Write("\t");
        for(int j=0;j<K.Count;j++){
            Console.Write("\t");
        }
        Console.Write("\t");
    }
}

void showKs(List<Matrix> Ks){
    for(int i=0;i<Ks.Count;i++){
        Console.Write( $"K del elemento {i+1}  \n");
        showMatrix(Ks[i]);
        Console.Write("*************************************\n");
    }
}

void showVector(List<float> b){
    Console.Write("\t");
    for(int i=0;i<b.Count;i++){
        Console.Write("\t");
    }
    Console.Write("\t");
}

void showbs(List<List<float>> bs){
    for(int i=0;i<bs.Count;i++){
        Console.Write( $"b del elemento {i+1}  \n");
        showVector(bs[i]);
          Console.Write("*************************************\n");
    }
}

float calculateLocalD(int ind,mesh m){
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

float calculateLocalVolume(int ind,mesh m){
    //Se utiliza la siguiente fórmula:
    //      Dados los 4 puntos vértices del tetrahedro A, B, C, D.
    //      Nos anclamos en A y calculamos los 3 vectores:
    //              V1 = B - A
    //              V2 = C - A
    //              V3 = D - A
    //      Luego el volumen es:
    //              V = (1/6)*det(  [ V1' ; V2' ; V3' ]  )
    
    float V,a,b,c,d,e,f,g,h,i;
    element el = m.getElement(ind);
    node n1 = m.getNode(el.getNode1()-1);
    node n2 = m.getNode(el.getNode2()-1);
    node n3 = m.getNode(el.getNode3()-1);
    node n4 = m.getNode(el.getNode4()-1);

    a = n2.getX()-n1.getX();b = n2.getY()-n1.getY();c = n2.getZ()-n1.getZ();
    d = n3.getX()-n1.getX();e = n3.getY()-n1.getY();f = n3.getZ()-n1.getZ();
    g = n4.getX()-n1.getX();h = n4.getY()-n1.getY();i = n4.getZ()-n1.getZ();
    //Para el determinante se usa la Regla de Sarrus.
    V = (float)(1.0/6.0)*(a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i);

    return V;
}

float ab_ij(float ai, float aj, float a1, float bi, float bj, float b1){
    return (ai - a1)*(bj - b1) - (aj - a1)*(bi - b1);
}

void calculateLocalA(int i,Matrix A,mesh m){
    element e = m.getElement(i);
    node n1 = m.getNode(e.getNode1()-1);
    node n2 = m.getNode(e.getNode2()-1);
    node n3 = m.getNode(e.getNode3()-1);
    node n4 = m.getNode(e.getNode4()-1);

    A[0][0] = ab_ij(n3.getY(),n4.getY(),n1.getY(),n3.getZ(),n4.getZ(),n1.getZ());
    A[0][1] = ab_ij(n4.getY(),n2.getY(),n1.getY(),n4.getZ(),n2.getZ(),n1.getZ());
    A[0][2] = ab_ij(n2.getY(),n3.getY(),n1.getY(),n2.getZ(),n3.getZ(),n1.getZ());
    A[1][0] = ab_ij(n4.getX(),n3.getX(),n1.getX(),n4.getZ(),n3.getZ(),n1.getZ());
    A[1][1] = ab_ij(n2.getX(),n4.getX(),n1.getX(),n2.getZ(),n4.getZ(),n1.getZ());
    A[1][2] = ab_ij(n3.getX(),n2.getX(),n1.getX(),n3.getZ(),n2.getZ(),n1.getZ());
    A[2][0] = ab_ij(n3.getX(),n4.getX(),n1.getX(),n3.getY(),n4.getY(),n1.getY());
    A[2][1] = ab_ij(n4.getX(),n2.getX(),n1.getX(),n4.getY(),n2.getY(),n1.getY());
    A[2][2] = ab_ij(n2.getX(),n3.getX(),n1.getX(),n2.getY(),n3.getY(),n1.getY());
}

void calculateB(Matrix B){
    B[0][0] = -1;
	B[0][1] = 1; 
	B[0][2] = 0; 
	B[0][3] = 0;
    B[1][0] = -1; 
	B[1][1] = 0; 
	B[1][2] = 1; 
	B[1][3] = 0;
    B[2][0] = -1; 
	B[2][1] = 0; 
	B[2][2] = 0;
	B[2][3] = 1;
}

Matrix createLocalK(int element,mesh m){
    // K = (k*Ve/D^2)Bt*At*A*B := K_4x4
    float D,Ve,k = m.getParameter((int)eParameters.THERMAL_CONDUCTIVITY);
    Matrix K,A,B,Bt,At;

    D = calculateLocalD(element,m);
    Ve = calculateLocalVolume(element,m);

    zeroes(A,3);
    zeroes(B,3,4);
    calculateLocalA(element,A,m);
    calculateB(B);
    transpose(A,At);
    transpose(B,Bt);

    productRealMatrix(k*Ve/(D*D),productMatrixMatrix(Bt,productMatrixMatrix(At,productMatrixMatrix(A,B,3,3,4),3,3,4),4,3,4),K);

    return K;
}

float calculateLocalJ(int ind,mesh m){
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

List<float> createLocalb(int element,mesh m){
    List<float> b;

    float Q = m.getParameter((int)eParameters.HEAT_SOURCE),J,b_i;
    J = calculateLocalJ(element,m);

    b_i = Q*J/24.0;
    b.Add(b_i); b.Add(b_i);
    b.Add(b_i); b.Add(b_i);

    return b;
}

void crearSistemasLocales(mesh m,List<Matrix> localKs,List<List<float>> localbs){
    for(int i=0;i<m.getSize(ELEMENTS);i++){
        localKs.Add(createLocalK(i,m));
        localbs.Add(createLocalb(i,m));
    }
}

void assemblyK(element e,Matrix localK,Matrix K){
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

void assemblyb(element e,List<float> localb,List<float> b){
    int index1 = e.getNode1() - 1;
    int index2 = e.getNode2() - 1;
    int index3 = e.getNode3() - 1;
    int index4 = e.getNode4() - 1;

    b[index1] += localb[0];
    b[index2] += localb[1];
    b[index3] += localb[2];
    b[index4] += localb[3];
}

void ensamblaje(mesh m,List<Matrix> localKs,List<List<float>> localbs,Matrix K,List<float> b){
    for(int i=0;i<m.getSize(ELEMENTS);i++){
        element e = m.getElement(i);
        assemblyK(e,localKs[i],K);
        assemblyb(e,localbs[i],b);
    }
}

void applyNeumann(mesh m,List<float> b){
    for(int i=0;i<m.getSize((int)eSizes.NEUMANN);i++){
        condition c = m.getCondition(i,(int)eSizes.NEUMANN);
        b[c.getNode1()-1] += c.getValue();
    }
}

void applyDirichlet(mesh m,Matrix K,List<float> b){
    for(int i=0;i<m.getSize((int)eSizes.DIRICHLET);i++){
        condition c = m.getCondition(i,(int)eSizes.DIRICHLET);
        int index = c.getNode1()-1;

        K.remove(K.begin()+index);
        b.remove(b.begin()+index);

        for(int row=0;row<K.Count;row++){
            float cell = K[row][index];
            
            K[row].Remove(K[row].begin()+index);
            b[row] += -1*c.getValue()*cell;
        }
    }
}

void calculate(Matrix K, List<float> b, List<float> T){
    Console.WriteLine("Iniciando calculo de respuesta...\n");
    Matrix Kinv;
      Console.Write("Calculo de la inversa\n");
    inverseMatrix(K,Kinv);
    Console.Write("Caclulo de la inversa\n");
    productMatrixVector(Kinv,b,T);
}
}
}