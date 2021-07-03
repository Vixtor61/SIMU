using System.Collections.Generic;
using System;

namespace polygot
{
    
     public class Matrix: List<List<double>> { 


      }
   public class math
    {
        public void zeroes(Matrix M,int n){
            for(int i=0;i<n;i++){
                List<double> row = new List<double>(new double[n]);
               // List<double> row(n,0.0);
                M.Add(row);
            }
        }

         public void ones(Matrix M,int n){
            for(int i=0;i<n;i++){
                List<double> row = new List<double>(new double[n]);
               // List<double> row(n,0.0);
                M.Add(row);
            }
        }

        public void zeroes(Matrix M,int n,int m){
            for(int i=0;i<n;i++){
               List<double> row = new List<double>(new double[m]);
                M.Add(row);
            }
        }

       public void zeroes(List<double> v,int n){
            for(int i=0;i<n;i++){
                v.Add(0.0);
            }
        }

     public   void copyMatrix(Matrix A, Matrix copy){
            zeroes(copy,A.Count);
            for(int i=0;i<A.Count;i++)
                for(int j=0;j<A[0].Count;j++)
                    copy[i][j] = A[i][j];
        }

     public   double calculateMember(int i,int j,int r,Matrix A,Matrix B){
            double member = 0;
            for(int k=0;k<r;k++)
                member += A[i][k]*B[k][j];
            return member;
        }

     public   Matrix productMatrixMatrix(Matrix A,Matrix B,int n,int r,int m){
            Matrix R = new Matrix();

            zeroes(R,n,m);
            for(int i=0;i<n;i++)
                for(int j=0;j<m;j++)
                    R[i][j] = calculateMember(i,j,r,A,B);

            return R;
        }

   public     void productMatrixVector(Matrix A, List<double>  v, List<double>  R){
            for(int f=0;f<A.Count;f++){
                double cell = 0.0F;
                for(int c=0;c<v.Count;c++){
                    cell += A[f][c]*v[c];
                }
                R[f] += cell;
            }
        }

     public   void productRealMatrix(double real,Matrix M,Matrix R){
            zeroes(R,M.Count);
            for(int i=0;i<M.Count;i++)
                for(int j=0;j<M[0].Count;j++)
                    R[i][j] = real*M[i][j];
        }

           public   void productRealMatrix2(double real,Matrix M){
          //  zeroes(R,M.Count);
            for(int i=0;i<M.Count;i++)
                for(int j=0;j<M[0].Count;j++)
                    M[i][j] = real*M[i][j];
        }

   public     void getMinor(Matrix M,int i, int j){
            //cout << "Calculando menor ("<<i+1<<","<<j+1<<")...\n";
          //  Console.WriteLine(M.Count);   
            M.Remove(M[0+i]);
        
            for(i=0;i<M.Count;i++)
                M[i].Remove(M[i][0+j]);
        }

   public     double determinant(Matrix M,int current){
            
            if(M.Count == 1) return M[0][0];
            else{
                double det=0.0;
                for(int i=0;i<M[0].Count;i++){
                    if (current == 0 )
                    {
                   //     Console.WriteLine($" {i}");
                    }
                    Matrix minor = new Matrix();
                    copyMatrix(M,minor);
                    
                    getMinor(minor,0,i);
                    
                    det +=  (Math.Pow(-1,i))*(M[0][i]*determinant(minor,current++));
                }
                return det;
            }
        }



    

 





   public     void cofactors(Matrix M, Matrix Cof){
            zeroes(Cof,M.Count);
            for(int i=0;i<M.Count;i++){
                for(int j=0;j<M[0].Count;j++){
                    //cout << "Calculando cofactor ("<<i+1<<","<<j+1<<")...\n";
                    Matrix minor = new Matrix();
                    copyMatrix(M,minor);
                    getMinor(minor,i,j);
                    Cof[i][j] = (double)Math.Pow(-1,i+j)*determinant(minor,0);
                }
            }
        }

      public  void transpose(Matrix M, Matrix T){
            zeroes(T,M[0].Count,M.Count);
            for(int i=0;i<M.Count;i++)
                for(int j=0;j<M[0].Count;j++)
                    T[j][i] = M[i][j];
        }

    public    void inverseMatrix(Matrix M, Matrix Minv){
            Console.WriteLine("Iniciando calculo de inversa...\n");
            Matrix Cof =  new Matrix() ;
            Matrix Adj = new Matrix();
  
            double det = determinant(M,0);
  
            if(det == 0){
     
        System.Environment.Exit(1);

            } 
            
            
            Console.WriteLine("Iniciando calculo de cofactores...\n");
            cofactors(M,Cof);
            Console.WriteLine( "Calculo de adjunta...\n");
            transpose(Cof,Adj);
            Console.WriteLine( "Calculo de inversa...\n");
            productRealMatrix(1/det,Adj,Minv);
        }



    
            
    }



    
}

