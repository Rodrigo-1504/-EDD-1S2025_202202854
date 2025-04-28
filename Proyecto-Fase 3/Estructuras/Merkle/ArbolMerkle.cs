namespace DS
{
    public class ArbolMerkle
    {
        private static ArbolMerkle? _instance;
        public static ArbolMerkle Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ArbolMerkle();
                }
                return _instance;
            }
        }

        private List<NodoMerkle>? Hojas;
        private NodoMerkle? raiz;

        public ArbolMerkle()
        {
            Hojas = new List<NodoMerkle>();
            raiz = null;
        }

        public void agregarFactura(Facturas factura)
        {
            NodoMerkle newFactura = new NodoMerkle(factura);

            foreach( var hoja in Hojas )
            {
                if(hoja.facturas.id == factura.id)
                {
                    Console.WriteLine("El id de la factura ya existe");
                    return;
                }
            }

            Hojas.Add(newFactura);
            BuildTree();
        }

        private void BuildTree()
        {
            if(Hojas.Count == 0)
            {
                raiz = null;
                return;
            }

            List<NodoMerkle> nivelActual = new List<NodoMerkle>(Hojas);
            while(nivelActual.Count > 1)
            {
                List<NodoMerkle> siguienteNivel = new List<NodoMerkle>();
                
                for(int i = 0; i < nivelActual.Count; i+= 2)
                {
                    NodoMerkle izquierdo = nivelActual[i];
                    NodoMerkle? derecha = (i + 1 < nivelActual.Count) ? nivelActual[i+1] : izquierdo;
                    NodoMerkle padre = new NodoMerkle(izquierdo, derecha);
                    siguienteNivel.Add(padre);
                }

                nivelActual = siguienteNivel;
            }

            raiz = nivelActual[0];
        }

        public void ImprimirFacturas()
        {
            if (Hojas == null || Hojas.Count == 0)
            {
                Console.WriteLine("No hay facturas en el árbol");
                return;
            }

            Console.WriteLine("Facturas en el árbol:");
            foreach (var hoja in Hojas)
            {
                if (hoja.facturas != null)
                {
                    Console.WriteLine($"ID: {hoja.facturas.id}, Total: {hoja.facturas.total}, Hash: {hoja.Hash}");
                }
            }
        }

        public string graphvizMerkle()
        {
            
            string graphviz = "";
            graphviz += "digraph Merkle{\n";
            graphviz += "\tnode [shape=record];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Arbol de Merkle\";\n";

            if(raiz == null)
            {
                graphviz += "\t\tempty [label=\"Arbol vacio\"];";
            }
            else
            {
                Dictionary<string, int> nodeIds = new Dictionary<string, int>();
                int idCounter = 0;
                graphviz += graphvizMerkleRecursivo(raiz, nodeIds, ref idCounter);
            }

            graphviz += "\t\t}\n";
            graphviz += "}\n";
            return graphviz;
        }

        private string graphvizMerkleRecursivo(NodoMerkle nodo, Dictionary<string, int> nodeIDS, ref int idCounter)
        {
            if(nodo == null) return "";
            
            string graph = "";

            if(!nodeIDS.ContainsKey(nodo.Hash))
            {
                nodeIDS[nodo.Hash] = idCounter ++;
            }

            int nodeId = nodeIDS[nodo.Hash];
            string label;

            if(nodo.facturas != null)
            {
                label = $"\t\t\"Factura {nodo.facturas.id}\\nTotal: {nodo.facturas.total}\\nMetodoPago: {nodo.facturas.metodoPago}\\nHash: {nodo.Hash.Substring(0, 8)}...\"";
            }
            else
            {
                label = $"\t\t\"Hash: {nodo.Hash.Substring(0, 8)}...\"";                
            }

            graph += $"\t\tnode{nodeId} [label={label}];\n";

            if(nodo.izquierda != null)
            {
                if(!nodeIDS.ContainsKey(nodo.izquierda.Hash))
                {
                    nodeIDS[nodo.izquierda.Hash] = idCounter++;
                }
                int leftId = nodeIDS[nodo.izquierda.Hash];
                graph += $"\t\tnode{nodeId} -> node{leftId}";
                graph += graphvizMerkleRecursivo(nodo.izquierda, nodeIDS, ref idCounter);

            }

            if(nodo.derecha != null)
            {
                if(!nodeIDS.ContainsKey(nodo.derecha.Hash))
                {
                    nodeIDS[nodo.derecha.Hash] = idCounter++;
                }
                int righId = nodeIDS[nodo.derecha.Hash];
                graph += $"\t\tnode{nodeId} -> node{righId}";
                graph += graphvizMerkleRecursivo(nodo.derecha, nodeIDS, ref idCounter);
            }

            return graph;
        }
    }
}