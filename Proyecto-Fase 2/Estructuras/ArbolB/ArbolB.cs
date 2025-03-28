using System;
using System.Collections.Generic;
using System.Text;
using Structures;

namespace Structures
{
    public class ArbolB
    {

        private static ArbolB _instance;
        public static ArbolB Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ArbolB();
                }
                return _instance;
            }
        }

        private NodoB raiz;

        public ArbolB()
        {
            raiz = new NodoB();
            raiz.hoja = true;
        }

        // Método público para insertar una factura
        public void Insertar(Facturas factura)
        {
            if (factura == null)
            {
                Console.WriteLine("Error: Factura no puede ser nula");
                return;
            }

            // Verificar si ya existe
            if (Buscar(factura.id) != null)
            {
                Console.WriteLine($"Error: Factura con ID {factura.id} ya existe");
                return;
            }

            // Si la raíz está llena, dividirla
            if (raiz.Lleno())
            {
                NodoB nuevaRaiz = new NodoB();
                nuevaRaiz.hoja = false;
                nuevaRaiz.hijos.Add(raiz);
                DividirHijo(nuevaRaiz, 0);
                raiz = nuevaRaiz;
            }
            InsertarNoLleno(raiz, factura);
        }

        private void InsertarNoLleno(NodoB nodo, Facturas factura)
        {
            int i = nodo.claves.Count - 1;

            if (nodo.hoja)
            {
                // Insertar en orden en nodo hoja
                while (i >= 0 && factura.id < nodo.claves[i].id)
                {
                    i--;
                }
                nodo.claves.Insert(i + 1, factura);
            }
            else
            {
                // Encontrar hijo adecuado
                while (i >= 0 && factura.id < nodo.claves[i].id)
                {
                    i--;
                }
                i++;

                // Verificar si el hijo está lleno
                if (nodo.hijos[i].Lleno())
                {
                    DividirHijo(nodo, i);
                    // Después de dividir, decidir en qué hijo insertar
                    if (factura.id > nodo.claves[i].id)
                    {
                        i++;
                    }
                }
                InsertarNoLleno(nodo.hijos[i], factura);
            }
        }

        private void DividirHijo(NodoB padre, int indiceHijo)
        {
            NodoB hijo = padre.hijos[indiceHijo];
            NodoB nuevoHijo = new NodoB();
            nuevoHijo.hoja = hijo.hoja;

            Facturas facturaMedia = hijo.claves[NodoB.min_Claves];

            for(int i = NodoB.min_Claves + 1; i < NodoB.max_Claves; i++)
            {
                nuevoHijo.claves.Add(hijo.claves[i]);
            }

            if(!hijo.hoja)
            {
                for(int i = (NodoB.orden / 2); i < NodoB.orden; i++)
                {
                    nuevoHijo.hijos.Add(hijo.hijos[i]);
                }
                hijo.hijos.RemoveRange((NodoB.orden / 2), hijo.hijos.Count - (NodoB.orden / 2));
            }

            hijo.claves.RemoveRange(NodoB.min_Claves, hijo.claves.Count - NodoB.min_Claves);

            padre.hijos.Insert(indiceHijo + 1, nuevoHijo);

            int j = 0;
            while(j < padre.claves.Count && padre.claves[j].id < facturaMedia.id)
            {
                j++;
            }
            padre.claves.Insert(j, facturaMedia);
        }

        public Facturas Buscar(int id)
        {
            return BuscarRecursivo(raiz, id);
        }

        private Facturas BuscarRecursivo(NodoB nodo, int id)
        {
            if (nodo == null) return null;

            int i = 0;
            while (i < nodo.claves.Count && id > nodo.claves[i].id)
            {
                i++;
            }

            if (i < nodo.claves.Count && id == nodo.claves[i].id)
            {
                return nodo.claves[i];
            }

            if (nodo.hoja)
            {
                return null;
            }

            return BuscarRecursivo(nodo.hijos[i], id);
        }

        public Facturas Buscar2(int id)
        {
            return BuscarRecursivo2(raiz, id);
        }

        private Facturas BuscarRecursivo2(NodoB nodo, int id)
        {
            if (nodo == null) return null;

            int i = 0;
            while (i < nodo.claves.Count && id > nodo.claves[i].id_Servicio)
            {
                i++;
            }

            if (i < nodo.claves.Count && id == nodo.claves[i].id_Servicio)
            {
                return nodo.claves[i];
            }

            if (nodo.hoja)
            {
                return null;
            }

            return BuscarRecursivo2(nodo.hijos[i], id);
        }

        // Elimina una factura por ID
        public void Eliminar(int id)
        {
            EliminarRecursivo(raiz, id);

            // Si la raíz quedó vacía pero tiene hijos
            if (raiz.claves.Count == 0 && !raiz.hoja)
            {
                raiz = raiz.hijos[0];
            }
        }

        private void EliminarRecursivo(NodoB nodo, int id)
        {
            int indice = EncontrarIndice(nodo, id);

            // Caso 1: La clave está en este nodo
            if (indice < nodo.claves.Count && nodo.claves[indice].id == id)
            {
                if (nodo.hoja)
                {
                    EliminarDeHoja(nodo, indice);
                }
                else
                {
                    EliminarDeNoHoja(nodo, indice);
                }
            }
            else
            {
                if (nodo.hoja)
                {
                    Console.WriteLine($"La factura con ID {id} no existe");
                    return;
                }

                bool ultimoHijo = (indice == nodo.claves.Count);

                if (nodo.hijos[indice].claves.Count <= NodoB.min_Claves)
                {
                    LlenarHijo(nodo, indice);
                }

                if (ultimoHijo && indice > nodo.claves.Count)
                {
                    EliminarRecursivo(nodo.hijos[indice - 1], id);
                }
                else
                {
                    EliminarRecursivo(nodo.hijos[indice], id);
                }
            }
        }

        // Métodos auxiliares para eliminación
        private int EncontrarIndice(NodoB nodo, int id)
        {
            int indice = 0;
            while (indice < nodo.claves.Count && nodo.claves[indice].id < id)
            {
                indice++;
            }
            return indice;
        }

        private void EliminarDeHoja(NodoB nodo, int indice)
        {
            nodo.claves.RemoveAt(indice);
        }

        private void EliminarDeNoHoja(NodoB nodo, int indice)
        {
            Facturas clave = nodo.claves[indice];

            if (nodo.hijos[indice].claves.Count > NodoB.min_Claves)
            {
                Facturas predecesor = ObtenerPredecesor(nodo, indice);
                nodo.claves[indice] = predecesor;
                EliminarRecursivo(nodo.hijos[indice], predecesor.id);
            }
            else if (nodo.hijos[indice + 1].claves.Count > NodoB.min_Claves)
            {
                Facturas sucesor = ObtenerSucesor(nodo, indice);
                nodo.claves[indice] = sucesor;
                EliminarRecursivo(nodo.hijos[indice + 1], sucesor.id);
            }
            else
            {
                Fusionar(nodo, indice);
                EliminarRecursivo(nodo.hijos[indice], clave.id);
            }
        }

        private Facturas ObtenerPredecesor(NodoB nodo, int indice)
        {
            NodoB actual = nodo.hijos[indice];
            while (!actual.hoja)
            {
                actual = actual.hijos[actual.hijos.Count - 1];
            }
            return actual.claves[actual.claves.Count - 1];
        }

        private Facturas ObtenerSucesor(NodoB nodo, int indice)
        {
            NodoB actual = nodo.hijos[indice + 1];
            while (!actual.hoja)
            {
                actual = actual.hijos[0];
            }
            return actual.claves[0];
        }

        private void LlenarHijo(NodoB nodo, int indice)
        {
            if (indice > 0 && nodo.hijos[indice - 1].claves.Count > NodoB.min_Claves)
            {
                TomarDeAnterior(nodo, indice);
            }
            else if (indice < nodo.claves.Count && nodo.hijos[indice + 1].claves.Count > NodoB.min_Claves)
            {
                TomarDeSiguiente(nodo, indice);
            }
            else
            {
                if (indice < nodo.claves.Count)
                {
                    Fusionar(nodo, indice);
                }
                else
                {
                    Fusionar(nodo, indice - 1);
                }
            }
        }

        private void TomarDeAnterior(NodoB nodo, int indice)
        {
            NodoB hijo = nodo.hijos[indice];
            NodoB hermano = nodo.hijos[indice - 1];

            hijo.claves.Insert(0, nodo.claves[indice - 1]);

            if (!hijo.hoja)
            {
                hijo.hijos.Insert(0, hermano.hijos[hermano.hijos.Count - 1]);
                hermano.hijos.RemoveAt(hermano.hijos.Count - 1);
            }

            nodo.claves[indice - 1] = hermano.claves[hermano.claves.Count - 1];
            hermano.claves.RemoveAt(hermano.claves.Count - 1);
        }

        private void TomarDeSiguiente(NodoB nodo, int indice)
        {
            NodoB hijo = nodo.hijos[indice];
            NodoB hermano = nodo.hijos[indice + 1];

            hijo.claves.Add(nodo.claves[indice]);

            if (!hijo.hoja)
            {
                hijo.hijos.Add(hermano.hijos[0]);
                hermano.hijos.RemoveAt(0);
            }

            nodo.claves[indice] = hermano.claves[0];
            hermano.claves.RemoveAt(0);
        }

        private void Fusionar(NodoB nodo, int indice)
        {
            NodoB hijo = nodo.hijos[indice];
            NodoB hermano = nodo.hijos[indice + 1];

            hijo.claves.Add(nodo.claves[indice]);

            for (int i = 0; i < hermano.claves.Count; i++)
            {
                hijo.claves.Add(hermano.claves[i]);
            }

            if (!hijo.hoja)
            {
                for (int i = 0; i < hermano.hijos.Count; i++)
                {
                    hijo.hijos.Add(hermano.hijos[i]);
                }
            }

            nodo.claves.RemoveAt(indice);
            nodo.hijos.RemoveAt(indice + 1);
        }

        // Genera reporte en formato DOT para Graphviz
        public string graphvizB()
        {
            if (raiz == null || raiz.claves.Count == 0)
            {
                return "digraph G {\n\tnode[shape=record];\n\tNULL[label = \"{Árbol B vacío}\"];\n}\n";
            }

            StringBuilder graphviz = new StringBuilder();
            graphviz.AppendLine("digraph BTree {");
            graphviz.AppendLine("\tnode[shape=record];");
            graphviz.AppendLine("\tgraph[pencolor=transparent];");
            graphviz.AppendLine("\tsubgraph cluster_0{");
            graphviz.AppendLine("\t\tlabel = \"Árbol B (Orden 5)\";");
            
            // Usamos un contador para identificar nodos únicos
            int nodeCounter = 0;
            graphviz.Append(graphvizBRecursivo(raiz, ref nodeCounter));
            
            graphviz.AppendLine("\t}");
            graphviz.AppendLine("}");
            
            return graphviz.ToString();
        }

        private string graphvizBRecursivo(NodoB nodo, ref int nodeCounter)
        {
            StringBuilder graphviz = new StringBuilder();
            
            if (nodo != null && nodo.claves.Count > 0)
            {
                // Generar un ID único para el nodo
                int currentNode = nodeCounter++;
                string nodeName = $"node{currentNode}";
                
                // Construir la etiqueta del nodo con todas las facturas
                StringBuilder label = new StringBuilder();
                label.Append("{");
                
                for (int i = 0; i < nodo.claves.Count; i++)
                {
                    if (i > 0) label.Append("|");
                    label.Append($"ID: {nodo.claves[i].id}\\nServ: {nodo.claves[i].id_Servicio}\\nTotal: {nodo.claves[i].total.ToString("0.00")}");
                }
                
                label.Append("}");
                
                graphviz.AppendLine($"\t\"{nodeName}\" [label = \"{label}\"];");
                
                // Procesar hijos
                if (!nodo.hoja)
                {
                    for (int i = 0; i < nodo.hijos.Count; i++)
                    {
                        if (nodo.hijos[i] != null && nodo.hijos[i].claves.Count > 0)
                        {
                            int childNode = nodeCounter;
                            graphviz.Append(graphvizBRecursivo(nodo.hijos[i], ref nodeCounter));
                            graphviz.AppendLine($"\t\"{nodeName}\" -> \"node{childNode}\";");
                        }
                    }
                }
            }
            
            return graphviz.ToString();
        }

        // Obtiene todas las facturas en orden
        public List<Facturas> ObtenerFacturasOrdenadas()
        {
            List<Facturas> facturas = new List<Facturas>();
            RecorrerInOrden(raiz, facturas);
            return facturas;
        }

        private void RecorrerInOrden(NodoB nodo, List<Facturas> facturas)
        {
            if (nodo == null) return;

            int i;
            for (i = 0; i < nodo.claves.Count; i++)
            {
                if (!nodo.hoja)
                {
                    RecorrerInOrden(nodo.hijos[i], facturas);
                }
                facturas.Add(nodo.claves[i]);
            }

            if (!nodo.hoja)
            {
                RecorrerInOrden(nodo.hijos[i], facturas);
            }
        }

        public void ImprimirPreOrden()
        {
            ImprimirPreOrdenRecursivo(raiz);
        }

        private void ImprimirPreOrdenRecursivo(NodoB nodo)
        {
            if (nodo != null && nodo.claves.Count > 0)
            {
                // Primero imprimir las claves del nodo actual
                for (int i = 0; i < nodo.claves.Count; i++)
                {
                    Console.WriteLine($"ID: {nodo.claves[i].id}, Servicio: {nodo.claves[i].id_Servicio}, Total: {nodo.claves[i].total}");
                }

                // Luego recorrer los hijos (si no es hoja)
                if (!nodo.hoja)
                {
                    for (int i = 0; i < nodo.hijos.Count; i++)
                    {
                        ImprimirPreOrdenRecursivo(nodo.hijos[i]);
                    }
                }
            }
        }

        public void ImprimirEnOrden()
        {
            ImprimirEnOrdenRecursivo(raiz);
        }

        private void ImprimirEnOrdenRecursivo(NodoB nodo)
        {
            if (nodo != null && nodo.claves.Count > 0)
            {
                // Si no es hoja, recorrer el primer hijo
                if (!nodo.hoja)
                {
                    ImprimirEnOrdenRecursivo(nodo.hijos[0]);
                }

                for (int i = 0; i < nodo.claves.Count; i++)
                {
                    // Imprimir la clave actual
                    Console.WriteLine($"ID: {nodo.claves[i].id}, Servicio: {nodo.claves[i].id_Servicio}, Total: {nodo.claves[i].total}");

                    // Si no es hoja, recorrer el siguiente hijo
                    if (!nodo.hoja && i + 1 < nodo.hijos.Count)
                    {
                        ImprimirEnOrdenRecursivo(nodo.hijos[i + 1]);
                    }
                }
            }
        }

        public void ImprimirPostOrden()
        {
            ImprimirPostOrdenRecursivo(raiz);
        }

        private void ImprimirPostOrdenRecursivo(NodoB nodo)
        {
            if (nodo != null && nodo.claves.Count > 0)
            {
                // Primero recorrer todos los hijos (si no es hoja)
                if (!nodo.hoja)
                {
                    for (int i = 0; i < nodo.hijos.Count; i++)
                    {
                        ImprimirPostOrdenRecursivo(nodo.hijos[i]);
                    }
                }

                // Luego imprimir las claves del nodo actual
                for (int i = 0; i < nodo.claves.Count; i++)
                {
                    Console.WriteLine($"ID: {nodo.claves[i].id}, Servicio: {nodo.claves[i].id_Servicio}, Total: {nodo.claves[i].total}");
                }
            }
        }

        public void VerificarIntegridadCompleta()
        {
            Console.WriteLine("\n--- VERIFICACIÓN COMPLETA DEL ÁRBOL B ---");
            Console.WriteLine($"Nodos totales: {ContarNodos()}");
            Console.WriteLine($"Facturas totales: {ContarFacturas()}");
            Console.WriteLine($"Altura del árbol: {CalcularAltura()}");
            Console.WriteLine($"Raíz - Claves: {raiz.claves.Count}, Hijos: {raiz.hijos.Count}, Es hoja: {raiz.hoja}");
            
            if (!raiz.hoja)
            {
                for (int i = 0; i < raiz.hijos.Count; i++)
                {
                    Console.WriteLine($"  Hijo {i} - Claves: {raiz.hijos[i].claves.Count}, Es hoja: {raiz.hijos[i].hoja}");
                }
            }
            
            Console.WriteLine("--- FIN DE VERIFICACIÓN ---\n");
        }

        public void ImprimirEstructura()
        {
            Console.WriteLine("\n--- ESTRUCTURA DEL ÁRBOL B ---");
            ImprimirNodo(raiz, 0);
            Console.WriteLine("--- FIN DE ESTRUCTURA ---\n");
        }

        private void ImprimirNodo(NodoB nodo, int nivel)
        {
            if (nodo == null) return;

            string indent = new string(' ', nivel * 4);
            Console.WriteLine($"{indent}Nivel {nivel} - Claves: {nodo.claves.Count}, Hoja: {nodo.hoja}");
            
            foreach (var clave in nodo.claves)
            {
                Console.WriteLine($"{indent}  ID: {clave.id}, Serv: {clave.id_Servicio}, Total: {clave.total}");
            }

            if (!nodo.hoja)
            {
                for (int i = 0; i < nodo.hijos.Count; i++)
                {
                    Console.WriteLine($"{indent}  Hijo {i}:");
                    ImprimirNodo(nodo.hijos[i], nivel + 1);
                }
            }
        }

        private int ContarNodos()
        {
            return ContarNodosRec(raiz);
        }

        private int ContarNodosRec(NodoB nodo)
        {
            if (nodo == null) return 0;
            
            int count = 1;
            if (!nodo.hoja)
            {
                foreach (var hijo in nodo.hijos)
                {
                    count += ContarNodosRec(hijo);
                }
            }
            return count;
        }

        private int ContarFacturas()
        {
            return ContarFacturasRec(raiz);
        }

        private int ContarFacturasRec(NodoB nodo)
        {
            if (nodo == null) return 0;
            
            int count = nodo.claves.Count;
            if (!nodo.hoja)
            {
                foreach (var hijo in nodo.hijos)
                {
                    count += ContarFacturasRec(hijo);
                }
            }
            return count;
        }

        private int CalcularAltura()
        {
            return CalcularAlturaRec(raiz);
        }

        private int CalcularAlturaRec(NodoB nodo)
        {
            if (nodo == null) return 0;
            if (nodo.hoja) return 1;
            
            return 1 + CalcularAlturaRec(nodo.hijos[0]);
        }
    }
}