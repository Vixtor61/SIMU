using System.Collections.Generic;
namespace polygot
{

     public class Matrix: List<List<float>> {  }
    class math
    {
        void zeroes(Matrix M,int n){
            for(int i=0;i<n;i++){
                List<float> row = new List<float>(new float[n]);
               // List<float> row(n,0.0);
                M.Add(row);
            }
        }

        void zeroes(Matrix M,int n,int m){
            for(int i=0;i<n;i++){
               List<float> row = new List<float>(new float[m]);
                M.Add(row);
            }
        }

        void zeroes(List<float> v,int n){
            for(int i=0;i<n;i++){
                v.Add(0.0);
            }
        }

        void copyMatrix(Matrix A, Matrix copy){
            zeroes(copy,A.Count);
            for(int i=0;i<A.Count;i++)
                for(int j=0;j<A[0].Count;j++)
                    copy[i][j] = A[i][j];
        }

        float calculateMember(int i,int j,int r,Matrix A,Matrix B){
            float member = 0;
            for(int k=0;k<r;k++)
                member += A[i][k]*B[k][j];
            return member;
        }

        Matrix productMatrixMatrix(Matrix A,Matrix B,int n,int r,int m){
            Matrix R = new Matrix();

            zeroes(R,n,m);
            for(int i=0;i<n;i++)
                for(int j=0;j<m;j++)
                    R[i][j] = calculateMember(i,j,r,A,B);

            return R;
        }

        void productMatrixVector(Matrix A, List<float>  v, List<float>  R){
            for(int f=0;f<A.Count;f++){
                float cell = 0.0;
                for(int c=0;c<v.Count;c++){
                    cell += A[f][c]*v[c];
                }
                R[f] += cell;
            }
        }

        void productRealMatrix(float real,Matrix M,Matrix R){
            zeroes(R,M.Count);
            for(int i=0;i<M.Count;i++)
                for(int j=0;j<M[0].Count;j++)
                    R[i][j] = real*M[i][j];
        }

        void getMinor(Matrix M,int i, int j){
            //cout << "Calculando menor ("<<i+1<<","<<j+1<<")...\n";
            M.erase(M.begin()+i);
            for(int i=0;i<M.Count;i++)
                M[i].erase(M[i].begin()+j);
        }

        float determinant(Matrix M){
            if(M.Count == 1) return M[0][0];
            else{
                float det=0.0;
                for(int i=0;i<M[0].Count;i++){
                    Matrix minor;
                    copyMatrix(M,minor);
                    getMinor(minor,0,i);
                    det += pow(-1,i)*M[0][i]*determinant(minor);
                }
                return det;
            }
        }

        void cofactors(Matrix M, Matrix Cof){
            zeroes(Cof,M.Count);
            for(int i=0;i<M.Count;i++){
                for(int j=0;j<M[0].Count;j++){
                    //cout << "Calculando cofactor ("<<i+1<<","<<j+1<<")...\n";
                    Matrix minor;
                    copyMatrix(M,minor);
                    getMinor(minor,i,j);
                    Cof[i][j] = pow(-1,i+j)*determinant(minor);
                }
            }
        }

        void transpose(Matrix M, Matrix T){
            zeroes(T,M[0].Count,M.Count);
            for(int i=0;i<M.Count;i++)
                for(int j=0;j<M[0].Count;j++)
                    T[j][i] = M[i][j];
        }

        void inverseMatrix(Matrix M, Matrix Minv){
            cout << "Iniciando calculo de inversa...\n";
            Matrix Cof, Adj;
            cout << "Calculo de determinante...\n";
            float det = determinant(M);
            if(det == 0) exit(EXIT_FAILURE);
            cout << "Iniciando calculo de cofactores...\n";
            cofactors(M,Cof);
            cout << "Calculo de adjunta...\n";
            transpose(Cof,Adj);
            cout << "Calculo de inversa...\n";
            productRealMatrix(1/det,Adj,Minv);
        }
            
    }
}

