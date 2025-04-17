using System.Text;

namespace DS
{
    public class ListaDeListas
    {
        public NodoGrafo Cabecera { get; set; }
        public NodoGrafo Cola { get; set; }

        public void Insertar(int indice, int valor)
        {
            NodoGrafo nodoExistente = BuscarNodo(indice);
            if(nodoExistente != null)
            {
                if(!ExistenteEnSubNodos(nodoExistente.Lista, valor))
                {
                    nodoExistente.agregar(valor);
                }
            }
            else
            {
                NodoGrafo newNodo = new NodoGrafo();
                newNodo.Indice = indice;
                newNodo.agregar(valor);

                if(Cabecera == null)
                {
                    Cabecera = newNodo;
                    Cola = newNodo;
                }
                else if(indice < Cabecera.Indice)
                {
                    newNodo.siguiente = Cabecera;
                    Cabecera.anterior = newNodo;
                    Cabecera = newNodo;
                }
                else
                {
                    NodoGrafo aux = Cabecera;
                    while(aux.siguiente != null && indice > aux.siguiente.Indice)
                    {
                        aux = aux.siguiente;
                    }

                    newNodo.siguiente = aux.siguiente;
                    newNodo.anterior = aux;

                    if(aux.siguiente != null)
                    {
                        aux.siguiente.anterior = newNodo;
                    }
                    else
                    {
                        Cola = newNodo;
                    }
                    aux.siguiente = newNodo;
                }
            }
        }

        private NodoGrafo BuscarNodo(int indice)
        {
            NodoGrafo aux = Cabecera;
            while(aux != null)
            {
                if(aux.Indice == indice)
                {
                    return aux;
                }
                aux = aux.siguiente;
            }
            return null;
        }

        private bool ExistenteEnSubNodos(Subnodo inicio, int valor)
        {
            Subnodo aux = inicio;
            while(aux != null)
            {
                if(aux.valor == valor)
                {
                    return true;
                }
                aux = aux.siguiente;
            }
            return false;
        }

        public void ImprimirLista()
        {
            NodoGrafo aux = Cabecera;
            while(aux != null)
            {
                Console.WriteLine($"Nodo {aux.Indice}: ");
                aux.Imprimir();
                aux = aux.siguiente;
            }
        }
    }

    public class GrafoNoDirigido
    {
        private ListaDoble listaVehiculos = ListaDoble.Instance;
        private ArbolAVL listaRepuestos = ArbolAVL.Instance;
        private ListaDeListas relaciones = new ListaDeListas();

        private static GrafoNoDirigido? _instance;
        public static GrafoNoDirigido Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GrafoNoDirigido();
                }
                return _instance;
            }
        }

        public void AgregarRelacion(int idVehiculo, int idRepuesto)
        {
            if(!ExisteVehiculo(idVehiculo))
            {
                Console.WriteLine($"El vehiculo con ID {idVehiculo} no existe");
                return;
            }

            if(!ExisteRepuesto(idRepuesto))
            {
                Console.WriteLine($"El repuesto con ID {idRepuesto} no existe");
                return;
            }

            relaciones.Insertar(idVehiculo, idRepuesto);

            Console.WriteLine($"Relacion creada: Vehiculo {idVehiculo} <-> {idRepuesto}");
        }

        private bool ExisteVehiculo(int id)
        {
            return listaVehiculos.BuscarVehiculo(id) != null;
        }

        private bool ExisteRepuesto(int id)
        {
            return listaRepuestos.Buscar(id) != null;
        }

        public void MostrarRelaciones()
        {
            Console.WriteLine("\nRelaciones en el grafo no dirigido");

            NodoGrafo aux = relaciones.Cabecera;
            while(aux != null)
            {
                if(ExisteVehiculo(aux.Indice))
                {
                    Console.WriteLine($"Vehiculo {aux.Indice} esta relacionado con repuestos: ");
                }
                else
                {
                    Console.WriteLine($"Repuesto {aux.Indice} esta relacionado con vehiculos: ");
                }

                Subnodo subAux = aux.Lista;
                while(subAux != null)
                {
                    Console.Write($"{subAux.valor}");
                    subAux = subAux.siguiente;
                }
                Console.WriteLine();
                aux = aux.siguiente;
            }
        }

        public string GraphvizGrafo()
        {
            if(relaciones.Cabecera == null)
            {
                return "graph G {\n\tnode[shape=circle];\n\t\"Grafo Vacío\" [label=\"Grafo Vacío\"];\n}\n";
            }

            StringBuilder graphviz = new StringBuilder();
            graphviz.AppendLine("graph GrafoVehiculosRepuestos {");
            graphviz.AppendLine("\tlabel=\"Grafo No Dirigido: Vehiculos - Repuestos\";");
            graphviz.AppendLine("\tlabel=\"Rodrigo Tahuite - 202202854\";");
            graphviz.AppendLine("\tnode [shape=box, style=filled];");
            graphviz.AppendLine("\trankdir=LR;");
            graphviz.AppendLine("\tfontsize=20;");
            graphviz.AppendLine();

            // Agregar nodos de vehículos (cajas azules)
            NodoDoble auxVehiculos = listaVehiculos.cabeza;
            while(auxVehiculos != null)
            {
                string label = $"Vehículo\\nID: V{auxVehiculos.vehiculo.id}\\nMarca: {auxVehiculos.vehiculo.marca}";
                graphviz.AppendLine($"\t\"V{auxVehiculos.vehiculo.id}\" [label=\"{label}\", shape=box, color=lightblue];");
                auxVehiculos = auxVehiculos.siguiente;
            }

            // Agregar nodos de repuestos (elipses verdes)
            if (listaRepuestos.raiz != null)
            {
                Stack<NodoAVL> pila = new Stack<NodoAVL>();
                NodoAVL actual = listaRepuestos.raiz;

                while (actual != null || pila.Count > 0)
                {
                    while (actual != null)
                    {
                        pila.Push(actual);
                        actual = actual.izquierda;
                    }

                    actual = pila.Pop();
                    
                    string label = $"Repuesto\\nID: R{actual.repuestos.id}\\nNombre: {actual.repuestos.repuesto}";
                    if (!string.IsNullOrEmpty(actual.repuestos.detalles))
                    {
                        label += $"\\nDetalles: {actual.repuestos.detalles}";
                    }
                    if (actual.repuestos.costo > 0)
                    {
                        label += $"\\nCosto: {actual.repuestos.costo}";
                    }
                    
                    graphviz.AppendLine($"\t\"R{actual.repuestos.id}\" [label=\"{label}\", shape=ellipse, color=lightgreen];");

                    actual = actual.derecha;
                }
            }

            // Agregar relaciones (conexiones no dirigidas)
            HashSet<string> relacionesAgregadas = new HashSet<string>();
            
            NodoGrafo aux = relaciones.Cabecera;
            while(aux != null)
            {
                // Solo procesamos nodos que sean vehículos (asumimos que los índices corresponden a vehículos)
                if(ExisteVehiculo(aux.Indice))
                {
                    Subnodo subAux = aux.Lista;
                    while(subAux != null)
                    {
                        string from = $"V{aux.Indice}";
                        string to = $"R{subAux.valor}";
                        
                        string claveRelacion = $"{from}-{to}"; // No necesitamos ordenar porque siempre será V-R
                        
                        if(!relacionesAgregadas.Contains(claveRelacion))
                        {
                            graphviz.AppendLine($"\t\"{from}\" -- \"{to}\";");
                            relacionesAgregadas.Add(claveRelacion);
                        }
                        subAux = subAux.siguiente;
                    }
                }
                aux = aux.siguiente;
            }

            graphviz.AppendLine("}");
            return graphviz.ToString();
        }
    }
}