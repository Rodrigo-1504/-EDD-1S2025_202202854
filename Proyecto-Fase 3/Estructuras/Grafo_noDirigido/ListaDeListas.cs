using System.Text;

namespace DS
{
    //CLASE QUE UNE LOS NODOS, OSEA QUE HACE LA LISTA DE LISTAS
    public class ListaDeListas
    {
        public NodoPrincipal Cabecera { get; set; }
        public NodoPrincipal Cola { get; set; }

        public void Insertar(int indice, int valor)
        {
            NodoPrincipal nodoExistente = BuscarNodo(indice);
            if(nodoExistente != null)
            {
                if(!ExistenteEnSubNodos(nodoExistente.Lista, valor))
                {
                    nodoExistente.agregarSubNodo(valor);
                }
            }
            else
            {
                NodoPrincipal newNodo = new NodoPrincipal();
                newNodo.Indice = indice;
                newNodo.agregarSubNodo(valor);

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
                    NodoPrincipal aux = Cabecera;
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

        private NodoPrincipal BuscarNodo(int indice)
        {
            NodoPrincipal aux = Cabecera;
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

        private bool ExistenteEnSubNodos(SubNodo inicio, int valor)
        {
            SubNodo aux = inicio;
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
            NodoPrincipal aux = Cabecera;
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

        public void AgregarRelacion(int idRepuesto, int idVehiculo)
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
            //Console.WriteLine($"Relacion creada: Vehiculo {idVehiculo} <-> {idRepuesto}");
        }

        private bool ExisteVehiculo(int id)
        {
            return listaVehiculos.BuscarVehiculo(id) != null;
        }

        private bool ExisteRepuesto(int id)
        {
            return listaRepuestos.Buscar(id) != null;
        }

        public void ImprimirGrafoNoDirigido()
        {
            Console.WriteLine("\n=== Relaciones en el grafo ===");
            
            NodoPrincipal aux = relaciones.Cabecera;
            while(aux != null)
            {
                string tipo = ExisteVehiculo(aux.Indice) ? "Vehículo" : "Repuesto";
                Console.Write($"{tipo} {aux.Indice} está relacionado con los Repuestos: ");
                
                SubNodo subAux = aux.Lista;
                while(subAux != null)
                {
                    Console.Write($"{subAux.valor} ");
                    subAux = subAux.siguiente;
                }
                Console.WriteLine();
                aux = aux.siguiente;
            }
        }

        public string GenerarDOT()
        {
            string graphviz = "";

            graphviz += "graph G {\n";
            graphviz += "\tnode[style=filled, fillcolor=white];\n";

            // Forma para vehículos
            graphviz += "\tnode [shape=ellipse, fillcolor=lightblue];\n";

            NodoPrincipal nodoActual = relaciones.Cabecera;
            while (nodoActual != null)
            {
                graphviz += $"\tV{nodoActual.Indice};\n";
                nodoActual = nodoActual.siguiente;
            }

            // Forma para repuestos
            graphviz += "\tnode [shape=box, fillcolor=lightyellow];\n";

            nodoActual = relaciones.Cabecera;
            while (nodoActual != null)
            {
                SubNodo subNodoActual = nodoActual.Lista;
                while (subNodoActual != null)
                {
                    graphviz += $"\tR{subNodoActual.valor};\n";
                    subNodoActual = subNodoActual.siguiente;
                }
                nodoActual = nodoActual.siguiente;
            }

            // Relaciones
            nodoActual = relaciones.Cabecera;
            while (nodoActual != null)
            {
                SubNodo subNodoActual = nodoActual.Lista;
                while (subNodoActual != null)
                {
                    graphviz += $"\tV{nodoActual.Indice} -- R{subNodoActual.valor};\n";
                    subNodoActual = subNodoActual.siguiente;
                }
                nodoActual = nodoActual.siguiente;
            }

            graphviz += "}";
            return graphviz;
        }
    }
}