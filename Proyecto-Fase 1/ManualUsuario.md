# Manual de Usuario

## Introducción
Este manual está diseñado para guiar a los usuarios en el uso de la **primera fase del proyecto de Estructura de Datos del año 2025**. Aquí encontrarás instrucciones detalladas para instalar, configurar y utilizar el programa.

---

## Requisitos del sistema
- **Sistema operativo**: Windows 10, macOS 10.15 o superior, Ubuntu 20.04.
- **Memoria RAM**: Mínimo 4 GB.

---

## Instrucciones paso a paso

### 1. Compilar y ejecutar el programa
1. Abre una terminal en la carpeta del proyecto.
2. Ejecuta el siguiente comando para compilar el programa:
~~~
dotnet build
~~~
3. Luego escribir en la consola:
~~~ 
dotnet run 
~~~

### 2. Inicio Sesioón

Le aparecerá la siguiente ventana:
![Captura desde 2025-02-28 21-06-52](https://hackmd.io/_uploads/Bk8xclljJg.png)


1. En la entrada de texto debajo de correo ingresamos: *root@gmail.com* , y en la entrada de texto debajo de contraseña ingresamos: *root123* y luego clic a Login para ingresar como Admin

### 3. Opciones del Admin
Nos aparecerá esta ventana: 
![Captura desde 2025-02-28 21-08-49](https://hackmd.io/_uploads/SklDqglsJg.png)

La recomendación para el usuario es que primero ingresé a las Cargas Masivas, por lo cual ingresaremos a esa ventana.

### 4. Cargas Masivas

1. Nos aparecerá una ventana así: 
![Captura desde 2025-02-28 21-10-53](https://hackmd.io/_uploads/r1LJjgeikl.png)
Elegiremos que tipo de carga Masiva haremos, lo recomendable es primero hacer la de usuarios y luego la de vehículos y luego la de repuesto. Luego de elegir que tipo de carga Masiva deseamos hacer le damos clic al botón *Cargar* y se nos abrirá el explorador de Archivos, seleccionamos el archivo Json que contiene los datos del tipo de entidad que elegimos. Luego  de hacer las cargas masivas de todo, podemos regresar a la ventana anterior.

### 5. Ingreso Individual de las entidades a sus listas

1. Luego de regresar a la ventana, le damos clic a 'Ingreso Individual' y podemos elegir que entidad deseamos agregar a su respectiva lista, debemos llenar los campos y los guardamos.

![Captura desde 2025-02-28 21-29-30](https://hackmd.io/_uploads/SJG4ybli1x.png)
En caso que el ID ya exista, no se agregará a su respectiva lista

Luego de ingresar las entidades que deseamos individualmente podemos regresar a la ventana anterior

### 6. Manejo de Usuarios

1. Luego de regresar a la ventana, le damos clic a 'Manejo de Usuarios' y podemos buscar a un Usuario con el ID, entonces colocamos un ID existente y buscamos al Usuario. Para poder actualizar hay que llenar los campos de entrada (aunque se le vaya a dejar con algunos datos similares se les debe colocar, porque sino al momento de la actualización esos campos aparecerán vacios) y luego le damos clic al botón de *Actualizar*.
2. Si deseamos borrarlos le hacemos clic al botón de *Eliminar* y el usuario será eliminado

![Captura desde 2025-02-28 22-22-41](https://hackmd.io/_uploads/SyHy3-giJx.png)


### 7. Generar los servicios

1. Luego de regresar a la ventana anterior, le damos clic al botón de *Generar Servicios* y al igual que con la ventana de *Ingreso Individual* podemos ingresar servicios de la misma manera, llenamos los campos y los guardamos

![Captura desde 2025-02-28 21-59-44](https://hackmd.io/_uploads/rkMBLZejyl.png)

Y luego podemos regresar a la ventana anterior

### 8. Cancelación de Facturas

1. Luego de regresar a la ventana anterior, le damos clic al botón *cancelar Facturas* y para mostrar las Facturas canceladas debemos dar clic en el botón de *Mostrar Factura cancelada* y eliminará la Factura de la Pila

![Captura desde 2025-02-28 22-05-24](https://hackmd.io/_uploads/rkt5PWljyg.png)

Luego de cancelar las Facturas ya pagadas, podemos regresar a la ventana anterior

### 9. Reportes

1. Luego de regresar a la ventana anterior, le damos clic al botón de *Salir* que nos generará los Reportes de todas las estructuras

Y podemos cerrar la ventana luego de salir. Y para terminar el programa, nos vamos a la terminal y usamos la combinación de letras *Ctrl + C*


---

## Consejos y recomendaciones

- Orden de cargas: Realiza las cargas masivas en el siguiente orden: usuarios, vehículos, repuestos.

- Formato de archivos JSON: Asegúrate de que los archivos JSON estén correctamente formateados antes de cargarlos.

- Copias de seguridad: Realiza copias de seguridad de los datos importantes antes de realizar cambios masivos.



## Solución de Problemas
- El programa no inicia: Verifica que .NET esté correctamente instalado y que cumpla con los requisitos del sistema.

- Archivo JSON no se carga: Revisa que el archivo JSON esté correctamente formateado y que no contenga errores.

- Error al iniciar sesión: Asegúrate de ingresar las credenciales correctas (root@gmail.com y root123).