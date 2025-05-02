namespace DS
{
    public class ArbolBST
    {
        ListaDoble listaVehiculos = ListaDoble.Instance;
        ArbolAVL listaRepuestos = ArbolAVL.Instance;
        GrafoNoDirigido relaciones = GrafoNoDirigido.Instance;
        ArbolMerkle listaFacturas = ArbolMerkle.Instance;

        //INSTANCIAR
        private static ArbolBST _instance;
        public static ArbolBST Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ArbolBST();
                }
                return _instance;
            }
        }

        private int idFactura = 0;

        //CARACTERÍSTICAS
        public NodoBST raiz;

        public ArbolBST()
        {
            raiz = null;
        }

        //INSERTAR E INSERTAR RECURSIVAMENTE
        public void agregarServicios(Servicios servicio, string metodoPago)
        {
            if(Buscar(servicio.id) != null)
            {
                Console.WriteLine("El id del servicio ya existe");
                return;
            }

            if(listaVehiculos.BuscarVehiculo(servicio.id_Vehiculo) == null)
            {
                Console.WriteLine("El vehiculo no existe");
                return;
            }

            if(listaRepuestos.Buscar(servicio.id_Repuesto) == null)
            {
                Console.WriteLine("El repuesto no existe");
                return;
            }

            raiz = insertarRecursivamente(raiz, servicio);
            relaciones.AgregarRelacion(servicio.id_Repuesto, servicio.id_Vehiculo);

            idFactura ++;
            double costoServicio = servicio.costo;
            var repuesto = listaRepuestos.Buscar(servicio.id_Repuesto);
            double costoRepuesto = repuesto.repuestos.costo;

            double total = costoRepuesto + costoServicio;
            listaFacturas.agregarFactura(new Facturas(idFactura, servicio.id, total, DateTime.UtcNow, metodoPago));
        }

        private NodoBST insertarRecursivamente(NodoBST nodo, Servicios servicio)
        {
            if(nodo == null)
            {
                return new NodoBST(servicio);
            }
            
            //INSERTAR EL NODO
            if(servicio.id < nodo.servicios.id)
            {
                nodo.izquierda = insertarRecursivamente(nodo.izquierda, servicio);
                //nodo = RotarDerecha(nodo);
            }
            else if(servicio.id > nodo.servicios.id)
            {
                nodo.derecha = insertarRecursivamente(nodo.derecha, servicio);
                //nodo = RotarIzquierda(nodo);
            }
            return nodo;
        }

        public NodoBST Buscar(int id)
        {
            return BuscarRecursivamente(raiz, id);
        }

        private NodoBST BuscarRecursivamente(NodoBST nodo, int id)
        {
            if(nodo == null) return null;
            
            if(id == nodo.servicios.id)
            {
                return nodo;
            }


            if(id < nodo.servicios.id)
            {
                return BuscarRecursivamente(nodo.izquierda, id);
            }
            
            return BuscarRecursivamente(nodo.derecha, id);
        }


        // ROTACIONES PARA ACTUALIZAR EL NODO RAIZ
        private NodoBST RotarDerecha(NodoBST nodo)
        {
            if (nodo == null || nodo.izquierda == null)
                return nodo;

            NodoBST nuevaRaiz = nodo.izquierda;
            nodo.izquierda = nuevaRaiz.derecha;
            nuevaRaiz.derecha = nodo;
            return nuevaRaiz;
        }

        private NodoBST RotarIzquierda(NodoBST nodo)
        {
            if (nodo == null || nodo.derecha == null)
                return nodo;

            NodoBST nuevaRaiz = nodo.derecha;
            nodo.derecha = nuevaRaiz.izquierda;
            nuevaRaiz.izquierda = nodo;
            return nuevaRaiz;
        }

        public void RecorridoPreOrden()
        {
            RecorridoPreOrdenRecursivo(raiz);
        }

        private void RecorridoPreOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                Console.WriteLine($"ID: {nodo.servicios.id}, ID_Repuesto: {nodo.servicios.id_Repuesto}, ID_Vehiculo: {nodo.servicios.id_Vehiculo}, Detalles: {nodo.servicios.detalles}, Costo: {nodo.servicios.costo}");
                RecorridoPreOrdenRecursivo(nodo.izquierda);
                RecorridoPreOrdenRecursivo(nodo.derecha);
            }
        }

        public void RecorridoEnOrden()
        {
            RecorridoEnOrdenRecursivo(raiz);
        }

        private void RecorridoEnOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                RecorridoEnOrdenRecursivo(nodo.izquierda);
                Console.WriteLine($"ID: {nodo.servicios.id}, ID_Repuesto: {nodo.servicios.id_Repuesto}, ID_Vehiculo: {nodo.servicios.id_Vehiculo}, Detalles: {nodo.servicios.detalles}, Costo: {nodo.servicios.costo}");
                RecorridoEnOrdenRecursivo(nodo.derecha);
            }
        }

        public void RecorridoPostOrden()
        {
            RecorridoPostOrdenRecursivo(raiz);
        }

        private void RecorridoPostOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                RecorridoPostOrdenRecursivo(nodo.izquierda);
                RecorridoPostOrdenRecursivo(nodo.derecha);
                Console.WriteLine($"ID: {nodo.servicios.id}, ID_Repuesto: {nodo.servicios.id_Repuesto}, ID_Vehiculo: {nodo.servicios.id_Vehiculo}, Detalles: {nodo.servicios.detalles}, Costo: {nodo.servicios.costo}");
            }
        }


        public string graphvizBST()
        {
            if(raiz == null)
            {
                return "digraph G {\n\tnode[shape=circle];\n\tNULL[label = \"{NULL}\"];\n}\n";
            }

            string graphviz = "";
            graphviz += "digraph AVL{\n";
            graphviz += "\tordering=out;\n";
            graphviz += "\tnode[shape=circle];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Arbol BST\";\n";

            graphviz += graphvizBSTRecursivo(raiz);
            
            graphviz += "\t\t}\n";
            graphviz += "}\n";            
            return graphviz;
        }

        private string graphvizBSTRecursivo(NodoBST nodo)
        {
            string graphviz = "";

            if (nodo != null)
            {
                // Crear la etiqueta del nodo con todos los datos del servicio
                string label = $"ID: {nodo.servicios.id}\nRepuesto: {nodo.servicios.id_Repuesto}\nVehiculo: {nodo.servicios.id_Vehiculo}\nDetalles: {nodo.servicios.detalles}\nCosto: {nodo.servicios.costo}";
                graphviz += $"\t\"{nodo.servicios.id}\" [label = \"{label}\"];\n";

                // Agregar la relación con el hijo izquierdo
                if (nodo.izquierda != null)
                {
                    graphviz += $"\t\"{nodo.servicios.id}\" -> \"{nodo.izquierda.servicios.id}\";\n";
                }

                // Agregar la relación con el hijo derecho
                if (nodo.derecha != null)
                {
                    graphviz += $"\t\"{nodo.servicios.id}\" -> \"{nodo.derecha.servicios.id}\";\n";
                }

                if(nodo.izquierda != null && nodo.derecha != null)
                {
                    graphviz += $"\t{{rank=same; \"{nodo.izquierda.servicios.id}\"; \"{nodo.derecha.servicios.id}\"}};\n";
                }

                // Recorrer los subárboles
                graphviz += graphvizBSTRecursivo(nodo.izquierda);
                graphviz += graphvizBSTRecursivo(nodo.derecha);
            }

            return graphviz;
        }
    }
}