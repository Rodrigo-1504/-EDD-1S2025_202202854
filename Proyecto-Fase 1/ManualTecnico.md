# Manual Técnico

## Introducción
Este manual técnico tiene como finalidad explicar el código desarrollado para un proyecto en Linux, utilizando **C#**, **.NET** y **Visual Studio Code (VSC)**. Está dirigido a desarrolladores y técnicos que deseen entender, instalar y ejecutar el proyecto.

---

## Especificaciones

### Requisitos del sistema
- **Sistema operativo**: Ubuntu 24.04 (o superior).
- **.NET SDK**: Versión 8.0 o superior.
- **Visual Studio Code**: Última versión estable.
- **Memoria RAM**: Mínimo 2 GB (recomendado 4 GB).
- **Espacio en disco**: Mínimo 500 MB.

---

### INSTALACION DE C# Y DE .NET Y VSC
Para la instalación de .NET (que contiene C#) en Ubuntu, hay que abrir la terminal y escribir los siguiente comandos: 

INSTALAR C#
~~~
sudo apt update && sudo apt upgrade -y
sudo apt install -y wget apt-transport-https software-properties-common
~~~

AGREGAR REPOSITORIO DE MICROSOFT
~~~
wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
~~~

DEPENDENCIAS
~~~
sudo apt install -y dotnet-sdk-8.0
~~~

VERIFICAR INSTALACIÓN
~~~
dotnet --version
~~~

INSTALAR VISUAL STUDIO CODE
~~~
sudo apt update
sudo apt install wget gpg
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor | sudo tee /usr/share/keyrings/packages.microsoft.gpg > /dev/null
echo "deb [arch=amd64 signed-by=/usr/share/keyrings/packages.microsoft.gpg] https://packages.microsoft.com/repos/code stable main" | sudo tee /etc/apt/sources.list.d/vscode.list
sudo apt update
sudo apt install code
~~~


Con esto ya tendriamos la instalación tanto de C#, .NET y de Visual Studio Code.

Ahora proseguiremos a explicar el código, primero para crear un proyecto en C#, debemos escribir en la terminal lo siguiente: 
~~~
dotnet new console -n name
~~~
Y con eso crearemos el proyecto de C#

***
Entonces proseguiremos a explicar el código del Program.cs, que es donde se ejecuta el proyecto (el Program.cs es el equivalente a decir que es el main, entonces es ahí donde se ejecutará el programa)

Program.cs:
![Captura desde 2025-02-28 16-50-11](https://hackmd.io/_uploads/B1hL03yoJl.png)

Métodos:
+ Main(): Es el método que se ejecuta, en este método estamos instanciando la ventana principal y la lista simple enlazada, para guardar al administrador.
+ OnWindowDelte(): Es el método que hace que cuando se cierre la ventana termine o esconda el programa.
***

Luego de eso tenemos 4 carpetas: 
- Estructuras
- generarDot_Png
- Intefaces
- Reportes

A conntinuación estaremos hablando de los archivos que están en Estructuras. Cabe destacar que cada estructura la dividimos en 3:
- Información dentro del Nodo
- Nodo
- Estructura

Por lo que únicamente se explicará una vez la parte de la Información dentro del Nodo y el Nodo para que no sea redundante el Manual

***
Dentro de la carpeta Estructuras tenemos: 
La carpeta Cola que contiene: 
- Servicios.cs
- Nodo.cs
- Cola.cs

Servicios.cs (Información dentro del nodo):
![Captura desde 2025-02-28 17-04-20](https://hackmd.io/_uploads/HkHWZT1j1e.png)

La clase Servicios nos va a servir o su uso es para la información que contendrá el nodo de la Cola. Cada que vez que llamemos a la clase Servicios, tendrá que tener todas los parametros que se le puso en el constructor.

Nodo.cs (Nodo):
![Captura desde 2025-02-28 17-06-49](https://hackmd.io/_uploads/HknqbTkjke.png)

La clase Nodo simulará el comportamiento de un Nodo Cola, que debe contener la información y el apuntador a siguiente.

Cola.cs:
![imagen](https://hackmd.io/_uploads/HJg1GTkiJl.png)

Los métodos importantes de la Cola son:
- agregarServicio(Servicios servicios): este método nos permitirá ingresar los nodos a la Cola, de tal manera que los nodos siempre se ingresen al final de la cola(FIFO).
- eliminarServicio(): elimina el nodo de la cola, desde el primer nodo
- buscarServicios(int id): buscará el nodo con el id que ingresamos
- graphvizCola(): es el método que retornará un string en formato de .dot de los nodos de nuestra Cola

***
Lista Circular:
- Repuestos.cs
- Nodo.cs
- ListaCircular.cs

ListaCircular.cs:
![Captura desde 2025-02-28 17-31-37](https://hackmd.io/_uploads/SyWdw6yjyl.png)

Métodos:
+ agregarRepuesto(Repuestos replace): crea el nodo, luego verífcia que si la cabeza de la lista esta vacia, en caso que lo este, el nodo creado pasa a ser la cabeza de la lista, y su apuntador apunta al siguiente espacio, en caso de que no este vacia, tendría que buscar el nodo final (que apunta a la cabeza) para luego reposicionar el apuntador del nodo final y que apunte al nuevo nodo, y que el apuntador del nodo nuevo apunte a la cabeza.
+ eliminarRepuesto(int id): si el nodo a eliminar es la cabeza, y es el unico nodo, entonces lo que haremos es que es nodo sea nulo; en caso de que no sea el único nodo, buscamos el último nodo, para que en vez de apuntar al nodo cabeza, apunte al siguiente nodo de la cabeza, ya que este será la nueva cabeza; en caso de que el nodo a eliminar no sea la cabeza, solo hay que enlazar el nodo anterior con el nodo siguiente; y luego de eso pasaremos a liberar la memoria
+ buscarRepuestos(int id): recorreremos la lista hasta encontrar el nodo con el id que buscamos
+ graphvizCircular(): nos retorna un string en formato de dot

***
Lista Doble:
- Vehiculos.cs
- Nodo.cs
- ListaDoble.cs

ListaDoble.cs:
![Captura desde 2025-02-28 18-12-36](https://hackmd.io/_uploads/SJHWZRyjyl.png)

Métodos:
- agregarVehiculos(Vehiculos vehicles): crea el nodo, revisa si la cabeza esta vacía, si lo esta, el nodo nuevo es la cabeza de la lista, sí no lo esta, buscará un nodo que su apuntador siguiente sea nulo y que ese apuntador siguiente ya no apuntará a nulo, sino que apuntará al nuevo nodo
- eliminarVehiculo(int id): si el nodo a eliminar es la cabeza, reposicionamos los nodos, el segundo será la cabeza, para que así el primer nodo se elimine; en cambio si el nodo a eliminar no es la cabeza, hay que redirigir los apuntadores, el nodo anterior apuntará al nodo siguiente, y el nodo siguiente apuntará al nodo anterior; y luego de eso liberamos la memoria.
- buscarVehiculo(int id): buscará por la lista el nodo que contenga el id buscado
- graphvizDoble(): retornará un string en formato dot


***
Lista Simple:
- Usuarios.cs
- Nodo.cs
- ListaSimple.cs

ListaSimple.cs:

![Captura desde 2025-02-28 18-34-42](https://hackmd.io/_uploads/S1XN8A1iJg.png)

Métodos:
- AgregarUsuarios(Usuarios user): se crea el nodo, si la cabeza esta vacía, entonces el nodo nuevo pasará a ser la cabeza de la lista; de lo contrario recorrerá la lista hasta encontrar un nodo en donde su apuntador sea nulo, entonces ese apuntador se rediccionará al nuevo nodo
- EliminarUsuario(int id): si el nodo a eliminar es la cabeza, el segundo nodo pasará a ser la cabeza; de lo contrario redireccionaremos el apuntador del nodo anterior al nodo siguiente; y luego de eso liberamos la memoria
- buscarUsuario(int id): recorrerá la lista hasta encontrar el nodo que contenga el id qu buscamos
- graphvizLista(): retornará un string en formato de dot de la lista

***
Matriz
- Nodo.cs (Para las filas y para las columnas)
- NodoInterno.cs (Para las celdas)
- listaEncabezado.cs
- Matriz.cs


Matriz.cs:
![Captura desde 2025-02-28 18-47-21](https://hackmd.io/_uploads/S184F0yoJe.png)

Métodos:
- insertar(posX, posY, string detalles): se crea el nodo interno, se busca los nodos de las filas y columnas, en donde debería posicionarse el nodoInterno, en caso de que no existieran, se crean; y luego se inserta el nodoInterno entre los nodos de las filas y columnas
- graphvizMatriz(): retorna un string en formato dot, el código funciona a medias, crea los nodos, pero no parece una matriz


***
Pila
- Facturas.cs
- Nodo.cs
- Pila.cs

Pila.cs:
![Captura desde 2025-02-28 18-51-32](https://hackmd.io/_uploads/BkBX9Ckjyx.png)

Métodos:
- agregarFactura(Facturas bill): crea el nodo, y el nodo creado pasa a ser el tope de la lista, este método sería el push de una pila
- eliminarFactura(): al igual que el pop, este método elimina el tope de la lista y y retorna el nodo eliminado
- graphvizPila(): retorna un string en formato de dot


***
Ahora que terminamos con la carpeta Estructuras, seguiremos con la carpeta 'generarDot_Png'

Dentro de esta carpeta solo tenemos el archivo:  
Convertidor.cs

Dentro de este archivo tenemos los siguientes métodos: 
- generarArchivoDot(string nombre, string contenido): primero crea una carpeta de reportes si es que no existiese, luego pasa el nombre con el que se guardara el archivo dot, en caso de el nombre del archivo no se guarde con la extensión dot, se le agreará el .dot al final del nombre. Luego por ultimo agregar todo el contenido que le pasamos al archivo dot
- ConvertidorDot_a_Png(string nombre): crear la carpeta Reportes en el caso de que no existiese, luego crear un archivo donde cambia el nombre del archivo dot, y lo cambia a .png; luego ejecuta el comando .dot que lo convierte en imagen.

***
Ahora veremos que es lo que contiene la carpeta Interfaces, en este no se detallará todos los métodos, y se hará un resumen de cada interfaz.

- inicioSesion.cs: este contiene una ventana con 2 'Entry' que son entradas de texto, los cuales se usarán para que el usuario ingrese su correo y contraseña, y en caso de ser correctas se avanzará a la siguiente ventana.
- opcionesAdmin.cs: en esta ventana nos encontraremos con 6 botones, el primer botón nos redirigirá a la ventana de las cargas masivas, el segundo nos redirigirá a la ventana para crear manualmente los usuarios, vehiculos, repuestos y servicios, el tercer botón nos redirigirá a la ventana para actualizar o eliminar usuarios, el cuarto botón nos redirigirá a la ventana de generar Servicios, el quinto botón nos redirigirá a la ventana donde muestran las facturas canceladas, y el ultimó botón es el de Salir que nos redirigirá a la ventana de inicio y nos generará los reportes
- cargaMasiva.cs: esta ventana contiene un ComboBoxText, que nos dará la opción de elegir que carga masiva queremos, si la de usuarios, vehículos o repuestos. También tenemos un botón que abrirá el explorador de Archivos para buscar el archivo Json que queramos abrir y hacer la carga. 
- ingresoManual.cs: esta ventana, tendrá las entradas de texto (Entry) que necesitemos para hacer las cargas individuales, dependiendo que queramos cargar, para eso esta el ComboBoxText, que nos dejará elegir si queremos ingresar Usuarios, Vehículos, Repuestos o Servicios; luego de llenar los campos, podremos ver el botón para guardar cada entidad a su respectiva lista.
- manejoUsuarios.cs: hay un botón que nos mostrará al usuario dependiendo que ID pongamos, con este usuario ya buscado, podemos actualizar datos del usuario o podemos eliminarlo, con sus respectivos botones
- generarServicio: al igual que con la ventana 'ingresoManul.cs', podremos agregar manualmente un servicio
- cancelarFactura: nos mostrará una ventana, que nos mostrará las facturas cancelas cuando presionemos el botón de ver la factura cancelada.


***
Y por último está la carpeta Reportes, que contiene todos los archivos dot y png que nos generá el programa.


