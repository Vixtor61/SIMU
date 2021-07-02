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

public float calculateLocalVolume(int ind,mesh m){
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

public float ab_ij(float ai, float aj, float a1, float bi, float bj, float b1){
    return (ai - a1)*(bj - b1) - (aj - a1)*(bi - b1);
}

public void calculateLocalA(int i,Matrix A,mesh m){
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

public void calculateB(Matrix B){
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

public Matrix createLocalK(int element,mesh m){
    // K = (k*Ve/D^2)Bt*At*A*B := K_4x4
    float D,Ve,k = m.getParameter((int)eParameters.THERMAL_CONDUCTIVITY);
    Matrix K = new Matrix();
    Matrix A= new Matrix();
    Matrix B= new Matrix();
    Matrix Bt= new Matrix();
    Matrix At= new Matrix();

    D = calculateLocalD(element,m);
    Ve = calculateLocalVolume(element,m);

    math.zeroes(A,3);
    math.zeroes(B,3,4);
    calculateLocalA(element,A,m);
    calculateB(B);
   math.transpose(A,At);
    math.transpose(B,Bt);
     showMatrix(A);
    math.productRealMatrix(k*Ve/(D*D),math.productMatrixMatrix(Bt,math.productMatrixMatrix(At,math.productMatrixMatrix(A,B,3,3,4),3,3,4),4,3,4),K);
  //  showMatrix(K);
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

    float Q = m.getParameter((int)eParameters.HEAT_SOURCE),J,b_i;
    J = calculateLocalJ(element,m);

    b_i = (float)Q*J/24.0F;
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
  //  Console.WriteLine($"{index1} {index2} {index3} {index4}" );

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
    Console.WriteLine($"\n SIZE:{m.getSize((int)eSizes.DIRICHLET)}");
      
    for(int i=0;i<m.getSize((int)eSizes.DIRICHLET);i++){
        Console.WriteLine(m.getDirichlet()[i].getValue());


    }
    
   // Console.WriteLine($"\n SIZE:{m.getSize((int)eSizes.DIRICHLET)}");
    for(int i=0;i<m.getSize((int)eSizes.DIRICHLET);i++){
         Console.WriteLine($"\n   D {(int)eSizes.DIRICHLET}");
        condition c = m.getCondition(i,(int)eSizes.DIRICHLET);
        int index = c.getNode1()-1;

        //K.remove(K.begin()+index);
        //b.remove(b.begin()+index);
     //   Console.WriteLine("hi");
    //    showMatrix(K);
        K.Remove(K[0+index]);
        b.Remove(b[0+index]);


        Console.WriteLine($"\n INDEX {i}  NODE {index}  Count {K.Count}");
    //     Console.WriteLine(K.Count);
        
       // showMatrix(K);
        for(int row=0;row<K.Count;row++){
            float cell = K[row][index];
         //   Console.WriteLine(cell);
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
