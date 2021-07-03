
using System.IO;
using System.Collections.Generic;
using System;
namespace polygot
{

class Utils{
        public int obtenerDatos(string[] filelines,eLines nlines,int n,eModes mode,item[] item_list,int lineCont){
         string[] constants = {};
            lineCont++;
            if(nlines==eLines.DOUBLELINE) lineCont++;

            for(int i=0;i<n;i++){
               //Console.WriteLine($"Line cont{lineCont}");

                switch(mode){
                case eModes.INT_FLOAT:
                   
                    //Array.ForEach<string>()
                   
                    constants = filelines[lineCont++].Split(" ");
                    int e0; double r0;
                     
                    e0 = int.Parse(constants[0]);
                    r0 = double.Parse(constants[1]);
                    item_list[i].setValues((int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,
                    e0,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,
                    r0);
                    break;
                case eModes.INT_FLOAT_FLOAT_FLOAT:
                
                 constants = filelines[lineCont++].Split(" ");
              //   Console.WriteLine(filelines[lineCont]);
               //  Console.WriteLine(lineCont);
                    int e; double r,rr,rrr;
                 
                
                     e = int.Parse(constants[0]);
                    r = double.Parse(constants[1]);
                    rr = double.Parse(constants[2]);
                    rrr = double.Parse(constants[3]);

                    item_list[i].setValues(e,r,rr,rrr,
                    (int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,
                    (double)eIndicator.NOTHING);
                    break;
                case eModes.INT_INT_INT_INT_INT:
                    constants = filelines[lineCont++].Split(" ");
                    int e1,e2, e3,e4,e5,e6,e7,e8,e9,e10,e11;
                 
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
                    
                    
                    //int e1,e2,e3,e4,e5;
                    //file >> e1 >> e2 >> e3 >> e4 >> e5;
                    item_list[i].setValues(e1,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,
                    e2,e3,e4,e5,e6,e7,e8,e9,e10,e11,
                    (double)eIndicator.NOTHING);
                    break;
                }
            }
            return lineCont;
        }

        public void correctConditions(int n,condition[] list,int[] indices){
  
           // Array.ForEach<int>(indices, Console.WriteLine);
         
            for(int i=0;i<n;i++){

         

                indices[i] = list[i].getNode1();

            }

            for(int i=0;i<n-1;i++){
                int pivot = list[i].getNode1();
                for(int j=i;j<n;j++)
                    //Si la condición actual corresponde a un nodo posterior al nodo eliminado por
                    //aplicar la condición anterior, se debe actualizar su posición.
                    if(list[j].getNode1()>pivot)
                        list[j].setNode1(list[j].getNode1()-1);
            }
        }

/*
        public void addExtension(char *newfilename,char *filename,char *extension){
            int ori_length = strlen(filename);
            int ext_length = strlen(extension);
            int i;
            for(i=0;i<ori_length;i++)
                newfilename[i] = filename[i];
            for(i=0;i<ext_length;i++)
                newfilename[ori_length+i] = extension[i];
            newfilename[ori_length+i] = '\0';
        }
*/
        public void leerMallayCondiciones(mesh m,String filename){
    

            String[] filelines;
            filelines = File.ReadAllLines(filename);
       

            int lineCont = 0;
            double EI,Q;
            List<double> f = new List<double>() ;
            int nnodes,neltos,ndirich,nneu;

 
            string[] constants = filelines[lineCont++].Split(" ");
             EI= double.Parse( constants[0]) ;

             f.Add(-24);
             f.Add(-99);
             f.Add(-12);

            
            //constants = filelines[lineCont++].Split(" ");
          //  f.Add(double.Parse(constants[0]));
           // f.Add(double.Parse(constants[1]));
            //f.Add(double.Parse(constants[2]));



            constants = filelines[lineCont++].Split(" ");
            nnodes = int.Parse(constants[0]);
            neltos = int.Parse(constants[1]);
            ndirich= int.Parse(constants[2]);
            nneu= int.Parse(constants[3]);
            
            

            m.setParameters(EI,f);
            m.setSizes(nnodes,neltos,ndirich,nneu);
            m.createData();


            lineCont++;
            
            lineCont = obtenerDatos(filelines,eLines.SINGLELINE,nnodes,eModes.INT_FLOAT_FLOAT_FLOAT,m.getNodes(),lineCont );
            lineCont++;
            //Console.WriteLine($"contadorr {lineCont} " );
            lineCont = obtenerDatos(filelines,eLines.DOUBLELINE,neltos,eModes.INT_INT_INT_INT_INT,m.getElements(),lineCont);
            lineCont++;
            lineCont = obtenerDatos(filelines,eLines.DOUBLELINE,ndirich,eModes.INT_FLOAT,m.getDirichlet(),lineCont);
            lineCont++;
            lineCont = obtenerDatos(filelines,eLines.DOUBLELINE,nneu,eModes.INT_FLOAT,m.getNeumann(),lineCont);
            
            
          

            //Se corrigen los índices en base a las filas que serán eliminadas
            //luego de aplicar las condiciones de Dirichlet
         
            /*
       
            foreach (node i in m.getNodes())
                {
                    System.Console.WriteLine("{0} {1} {2} {3}", i.getId(), i.getX() ,i.getY(),i.getZ() );
                }


                   Console.WriteLine("elemtes");
                        foreach (element i in m.getElements())
                {
                    System.Console.WriteLine("{0} {1} {2} {3}", i.getNode1(), i.getNode2() ,i.getNode3(),i.getNode4() );
                }
           */
            correctConditions(ndirich,m.getDirichlet(),m.getDirichletIndices());
        }

        public bool findIndex(int v, int s, int[] arr){
            for(int i=0;i<s;i++)
                if(arr[i]==v) return true;
            return false;
        }
/*
        public void writeResults(mesh m,List<double> T,string filename){
            String outputfilename;
            int[] dirich_indices = m.getDirichletIndices();
            condition[] dirich = m.getDirichlet();
            ofstream file;

         //   addExtension(outputfilename,filename,".post.res");
            file.open(outputfilename);

            file << "GiD Post Results File 1.0\n";
            file << "Result \"Temperature\" \"Load Case 1\" 1 Scalar OnNodes\nComponentNames \"T\"\nValues\n";

            int Tpos = 0;
            int Dpos = 0;
            int n = m.getSize((int)eSizes.NODES);
            int nd = m.getSize((int)eSizes.DIRICHLET );
            for(int i=0;i<n;i++){
                if(findIndex(i+1,nd,dirich_indices)){
                    file << i+1 << " " << dirich[Dpos].getValue() << "\n";
                    Dpos++;
                }else{
                    file << i+1 << " " << T.at(Tpos) << "\n";
                    Tpos++;
                }
            }

            file << "End values\n";

            file.close();
        }


*/
}
}
