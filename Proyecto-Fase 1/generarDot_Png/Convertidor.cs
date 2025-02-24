using System;
using System.Diagnostics;
using System.IO;

namespace Dot_Png
{
    public class Convertidor
    {
        public static void generarArchivoDot(string nombre, string contenido)
        {
            try
            {
                string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Reportes");
                if(!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                if(string.IsNullOrEmpty(nombre))
                {
                    Console.WriteLine("Esta vacio, debe contener un nombre");
                    return;
                }

                if(!nombre.EndsWith(".dot"))
                {
                    nombre += ".dot";
                }

                string rutaArchivo = Path.Combine(carpeta, nombre);
                File.WriteAllText(rutaArchivo, contenido);

                Console.WriteLine($"Archivo '{nombre}' generado con éxito en {rutaArchivo}");

            }
            catch(Exception e)
            {
                Console.WriteLine($"Error al generar el archivo: {e.Message}");
            }
        }

        public static void ConvertirDot_a_Png(string nombre)
        {
            try
            {
                string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Reportes");
                string archivoDot = Path.Combine(carpeta, nombre);

                if(string.IsNullOrEmpty(archivoDot))
                {
                    Console.WriteLine("Esta vacio");
                    return;
                }

                if(!File.Exists(archivoDot))
                {
                    Console.WriteLine($"El archivo '{nombre}' no existe en la carpeta actual");
                    return;
                }

                string archivoImagen = Path.ChangeExtension(archivoDot, ".png");
                
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "dot",
                    Arguments = $"-Tpng \"{archivoDot}\" -o \"{archivoImagen}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using(Process proceso = Process.Start(processStartInfo))
                {
                    if(proceso == null)
                    {
                        Console.WriteLine("No se puedo iniciar el proceso para convertir a .dot");
                        return;
                    }

                    proceso.WaitForExit();

                    if(proceso.ExitCode == 0)
                    {
                        Console.WriteLine("Png realizado exitosamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al intentar convertir el .dot a .png");

                        string error = proceso.StandardError.ReadToEnd();
                        Console.WriteLine($"Detalles del error: {error}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}