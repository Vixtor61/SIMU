using System;
namespace polygot
{

enum eIndicator : int {NOTHING};
enum eLines : int {NOLINE,SINGLELINE,DOUBLELINE};
enum eModes :int  {NOMODE,INT_FLOAT,INT_FLOAT_FLOAT_FLOAT,INT_INT_INT_INT_INT};
enum eParameters :int {THERMAL_CONDUCTIVITY,HEAT_SOURCE};
enum eSizes :int {NODES,ELEMENTS,DIRICHLET,NEUMANN};

    class item{
        
		protected    int id;
		protected    float x;
		protected    float y;
		protected    float z;
		protected    int node1;
		protected    int node2;
		protected    int node3;
		protected    int node4;
		protected    float value;

        public void setId(int identifier) {
            id = identifier;
            
        }

        public void setX(float x_coord) {
            x = x_coord;
        }

        public void setY(float y_coord) {
            y = y_coord;
        }

        public void setZ(float z_coord) {
            z = z_coord;
        }

        public void setNode1(int node_1) {
            node1 = node_1;
        }

        public void setNode2(int node_2) {
            node2 = node_2;
        }

        public void setNode3(int node_3) {
            node3 = node_3;
        }

        public void setNode4(int node_4) {
            node4 = node_4;
        }

        public void setValue(float value_to_assign) {
            value = value_to_assign;
        }

        public int getId() {
            return id;
        }

        public float getX() {
            return x;
        }

        public float getY() {
            return y;
        }

        public float getZ() {
            return z;
        }

        public int getNode1() {
            return node1;
        }

        public int getNode2() {
            return node2;
        }

        public int getNode3() {
            return node3;
        }

        public int getNode4() {
            return node4;
        }

        public float getValue() {
            return value;
        }

        public virtual void setValues(int a,float b,float c,float d,int e,int f,int g, int h, float i){
			id=0;
			x =0;
			y=0;
			z=0;
			node1 = 0;
			node2= 0;
			node3= 0;
		}

};


    class node: item{

        public override void setValues(int a,float b,float c,float d,int e,int f,int g, int h, float i){
                id = a;
                x = b;
                y = c;
                z = d;
            }

    };

    class element: item{

        public override void setValues(int a,float b,float c,float d,int e,int f,int g, int h, float i){
                id = a;
                node1 = e;
                node2 = f;
                node3 = g;
                node4 = h;
            }

    };

    class condition: item{


        public override void setValues(int a,float b,float c,float d,int e,int f,int g, int h, float i){
                node1 = e;
                value = i;
            }

    };
    class mesh{
            private float[] parameters = new float[2];

            private int[] sizes = new int[4];
            private node[] node_list;
            private element[] element_list;
            private int[] indices_dirich;
            private condition[] dirichlet_list;
            private condition[] neumann_list;

            public	void setParameters(float k,float Q){
                parameters[(int)eParameters.THERMAL_CONDUCTIVITY]=k;
                parameters[(int)eParameters.HEAT_SOURCE]=Q;
            }
            public	void setSizes(int nnodes,int neltos,int ndirich,int nneu){
                sizes[(int)eSizes.NODES] = nnodes;
                sizes[(int)eSizes.ELEMENTS] = neltos;
                sizes[(int)eSizes.DIRICHLET] = ndirich;
                sizes[(int)eSizes.NEUMANN] = nneu;
            }
            public int getSize(int s){
                return sizes[s];
            }
            public float getParameter(int p){
                return parameters[p];
            }
            public void createData(){
                node_list = new node[sizes[(int)eSizes.NODES]];
                 for (int i = 0; i < node_list.Length; ++i)
                    {
                        node_list[i] = new node();
                    }
                element_list = new element[sizes[(int)eSizes.ELEMENTS]];
                  for (int i = 0; i < element_list.Length; ++i)
                    {
                        element_list[i] = new element();
                    }
                indices_dirich = new int[sizes[(int)eSizes.DIRICHLET]];
               
                dirichlet_list = new condition[sizes[(int)eSizes.DIRICHLET]];
                  for (int i = 0; i < dirichlet_list.Length; ++i)
                    {
                        dirichlet_list[i] = new condition();
                    }
                neumann_list = new condition[sizes[(int)eSizes.NEUMANN]];
              for (int i = 0; i < neumann_list.Length; ++i)
                    {
                        neumann_list[i] = new condition();
                    }
            }
            public node[] getNodes(){
                return node_list;
            }
            public element[] getElements(){
                return element_list;
            }
            public int[] getDirichletIndices(){
                return indices_dirich;
            }
            public condition[] getDirichlet(){
                return dirichlet_list;
            }
            public condition[] getNeumann(){
                return neumann_list;
            }
            public node getNode(int i){
                return node_list[i];
            }
            public element getElement(int i){
                return element_list[i];
            }
            public condition getCondition(int i, int type){
                if(type == (int)eSizes.DIRICHLET) return dirichlet_list[i];
                else return neumann_list[i];
            }
    };

    
}
