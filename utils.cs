using System;
using System.IO;
using System.Collections.Generic;
namespace polygot
{

class Utils{
        public void obtenerDatos(string[] filelines,eLines nlines,int n,eModes mode,item[] item_list,int lineCont){
        //    string line;
         //   file >> line;
         string[] constants;
            if(nlines==eLines.DOUBLELINE) lineCont++;

            for(int i=0;i<n;i++){
                switch(mode){
                case eModes.INT_FLOAT:
                    constants = filelines[lineCont++].Split(" ");
                    int e0; float r0;
                    e0 = int.Parse(constants[0]);
                    r0 = float.Parse(constants[1]);
                    item_list[i].setValues((int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,e0,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,r0);
                    break;
                case eModes.INT_FLOAT_FLOAT_FLOAT:
                 constants = filelines[lineCont++].Split(" ");
                    int e; float r,rr,rrr;
                     e = int.Parse(constants[0]);
                    r = float.Parse(constants[1]);
                    rr = float.Parse(constants[2]);
                    rrr = float.Parse(constants[3]);
                    
                 //   int e; float r,rr,rrr;
                   // file >> e >> r >> rr >> rrr;
                    item_list[i].setValues(e,r,rr,rrr,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING);
                    break;
                case eModes.INT_INT_INT_INT_INT:
                    constants = filelines[lineCont++].Split(" ");
                    int e1,e2, e3,e4,e5;
                    e1 = int.Parse(constants[0]);
                    e2 = int.Parse(constants[1]);
                    e3 = int.Parse(constants[2]);
                    e4 = int.Parse(constants[3]);
                    e5 = int.Parse(constants[3]);
                    
                    
                    //int e1,e2,e3,e4,e5;
                    //file >> e1 >> e2 >> e3 >> e4 >> e5;
                    item_list[i].setValues(e1,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,(int)eIndicator.NOTHING,e2,e3,e4,e5,(int)eIndicator.NOTHING);
                    break;
                }
            }
        }

        public void correctConditions(int n,condition[] list,int[] indices){
            for(int i=0;i<n;i++)
                indices[i] = list[i].getNode1();

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
          //  String inputfilename;

            String[] filelines;
            filelines = File.ReadAllLines(filename);
            //ifstream file;

            int lineCont = 0;
            float k,Q;
            int nnodes,neltos,ndirich,nneu;

            //addExtension(inputfilename,filename,".dat");
           // file.open(inputfilename);
            string[] constants = filelines[lineCont++].Split(" ");
          //   k >> Q;
            k = float.Parse( constants[0]) ;
            Q = float.Parse( constants[1] );


            //cout << "k y Q: "<<k<<" y "<<Q<<"\n";
           // file >> nnodes >> neltos >> ndirich >> nneu;

            constants = filelines[lineCont++].Split(" ");
            nnodes = int.Parse(constants[0]);
            neltos = int.Parse(constants[1]);
            ndirich= int.Parse(constants[2]);
            nneu= int.Parse(constants[3]);
            //cout << "sizes: "<<nnodes<<" y "<<neltos<<" y "<<ndirich<<" y "<<nneu<<"\n";

            m.setParameters(k,Q);
            m.setSizes(nnodes,neltos,ndirich,nneu);
            m.createData();

            obtenerDatos(filelines,eLines.SINGLELINE,nnodes,eModes.INT_FLOAT_FLOAT_FLOAT,m.getNodes(),lineCont );
            obtenerDatos(filelines,eLines.DOUBLELINE,neltos,eModes.INT_INT_INT_INT_INT,m.getElements(),lineCont);
            obtenerDatos(filelines,eLines.DOUBLELINE,ndirich,eModes.INT_FLOAT,m.getDirichlet(),lineCont);
            obtenerDatos(filelines,eLines.DOUBLELINE,nneu,eModes.INT_FLOAT,m.getNeumann(),lineCont);
            
            
          //  file.close();

            //Se corrigen los índices en base a las filas que serán eliminadas
            //luego de aplicar las condiciones de Dirichlet
            correctConditions(ndirich,m.getDirichlet(),m.getDirichletIndices());
        }

        public bool findIndex(int v, int s, int[] arr){
            for(int i=0;i<s;i++)
                if(arr[i]==v) return true;
            return false;
        }
/*
        public void writeResults(mesh m,List<float> T,string filename){
            String outputfilename;
            int *dirich_indices = m.getDirichletIndices();
            condition *dirich = m.getDirichlet();
            ofstream file;

         //   addExtension(outputfilename,filename,".post.res");
            file.open(outputfilename);

            file << "GiD Post Results File 1.0\n";
            file << "Result \"Temperature\" \"Load Case 1\" 1 Scalar OnNodes\nComponentNames \"T\"\nValues\n";

            int Tpos = 0;
            int Dpos = 0;
            int n = m.getSize(NODES);
            int nd = m.getSize(DIRICHLET);
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
