## 🛠️ Bloque Técnico: Guía de Implementación (.NET Native AOT)

1. Activación en el archivo de tu proyecto (.csproj)

(Compatible con Minimal APIs y apps de Consola; no funciona con WinForms)
Abrir el archivo .csproj de tu aplicación y añadir la etiqueta <PublishAot>true</PublishAot> dentro del bloque principal:

xml

<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    
    <!-- ¡ESTA ES LA LÍNEA QUE LO ACTIVA! -->
    <PublishAot>true</PublishAot>
  </PropertyGroup>
</Project>

2. Compilar desde la terminal

Abrir la terminal en la carpeta del proyecto y ejecutar el comando de publicación:

bash
dotnet publish -c Release


## ¿Qué ocurre bajo el capó en ese momento?
1. Análisis de código (Trimming): El compilador escanea tu código y borra absolutamente todo lo que no uses de las librerías de Microsoft para reducir el peso final.
2. Compilación Nativa: Invoca herramientas del sistema (como ILCompiler) para traducir tu C# intermedio a binario puro.
3. El resultado: En la carpeta de salida (/bin/Release/net8.0/[sistema]/publish/) no encontrarás archivos .dll, sino un único archivo ejecutable nativo (.exe en Windows o ejecutable sin extensión en Linux).
Restricción: Native AOT no se lleva bien con la reflexión dinámica extrema.
Solución: Usar Source Generators (generadores de código en tiempo de compilación) para Mocks, xUnit, JSON y configuraciones.


3. Configuración del Driver de SQLite

La librería Microsoft.Data.Sqlite utiliza internamente una librería nativa escrita en C (e_sqlite3). Para que funcione con Native AOT, debes asegurar la gestión correcta en el .csproj

xml

<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework> <!-- O net9.0 -->
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <PublishAot>true</PublishAot>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Data.Sqlite" Version="8.*" />
  </ItemGroup>
</Project>


4. Beneficios e Impacto en Rendimiento

• Arranque instantáneo: La aplicación arranca en menos de 10 milisegundos.
• Consumo de memoria mínimo: Al no cargar el motor JIT, la app consume apenas entre 10MB y 20MB de RAM.
• Cero dependencias: El archivo resultante funciona de forma autónoma sin necesidad de instalar .NET runtimes en la máquina de destino.
• Configuración optimizada: El uso de Microsoft.Extensions.Configuration activa Source Generators de forma automática, rellenando tus objetos de configuración en tiempo de compilación a velocidad de vértigo

