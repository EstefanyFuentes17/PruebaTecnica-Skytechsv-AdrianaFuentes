🚀 Prueba Técnica: Sistema de Gestión de Usuarios - Skytechsv

Este repositorio contiene mi propuesta para la prueba técnica de programador, donde desarrollo un ecosistema de gestión de usuarios integrando tecnologías de escritorio y web.

📝 Descripción del Proyecto

El objetivo principal fue crear una herramienta robusta y fácil de usar para administrar cuentas de usuario. La solución se divide en dos grandes pilares:

Administrador de Escritorio (CRUD): Una aplicación en VB.NET diseñada para gestionar el ciclo de vida completo de un usuario. Incluye búsqueda inteligente, validaciones de seguridad y una interfaz personalizada.

Módulo de Acceso Web: Un componente en C# ASP.NET enfocado en la validación de credenciales, pensado para una futura escalabilidad hacia entornos web.

Toda la lógica pesada de datos no reside en la aplicación, sino en la base de datos mediante Procedimientos Almacenados, garantizando que el sistema sea rápido y seguro.

📂 Estructura del Repositorio

/VB_Desktop_CRUD: Código fuente de la aplicación de escritorio.

/CSharp_Web_Login: Proyecto web de autenticación.

/SQL_Scripts: Scripts de creación de tablas y lógica de base de datos.

🛠️ Pasos para la Puesta en Marcha

1. Preparar la Base de Datos 🗄️
   
Antes de correr las aplicaciones, debemos preparar el terreno en SQL Server:

Abre SQL Server Management Studio.

Carga y ejecuta el script: SQL_Scripts/DB_Skytechsv.sql.

Esto creará automáticamente la base de datos DB_Skytechsv, las tablas y todos los procedimientos necesarios para que el sistema "hable" con los datos.

2. Configurar la Conexión 🔗
Como cada entorno local es diferente, es necesario verificar el nombre de tu servidor:

En el proyecto de VB.NET, busca el archivo Conexion.vb y asegúrate de que el Data Source apunte a tu instancia local de SQL.

En el proyecto Web, realiza el mismo ajuste en el archivo web.config.

3. Ejecutar y Probar 🚀
CRUD Desktop: Abre el .sln en Visual Studio y presiona F5. Puedes crear usuarios, buscarlos en tiempo real, editarlos (incluyendo su contraseña) o darlos de baja cambiando su estado a "Inactivo".

Login Web: Ejecuta el proyecto web para probar la validación de las cuentas creadas.

✨ Características Destacadas

Búsqueda Dinámica: Filtra usuarios por nombre o username mientras escribes.

Seguridad: Uso de procedimientos almacenados para evitar inyecciones SQL y manejo de estados para control de acceso.

👩‍💻 Sobre mí
Actualmente estoy cursando mi quinto año de Ingeniería en Sistemas. Me apasiona resolver problemas mediante código limpio y eficiente. Este proyecto refleja mi capacidad para integrar diferentes lenguajes en una solución cohesiva.

¡Gracias por revisar mi propuesta! Si tienes alguna duda sobre la implementación, no dudes en contactarme.
