# 📘 Manual de Arquitectura en .NET: Gestión de Archivos e Inyección de Dependencias

Este manual técnico aborda los fundamentos de la manipulación segura de archivos mediante `System.IO` y su integración con el patrón de Inyección de Dependencias (DI) en arquitecturas modernas de C#.

---

## 📂 Bloque 1: Manipulación de Archivos de Bajo Nivel con `System.IO`

El espacio de nombres `System.IO` provee las herramientas nativas para interactuar con el sistema de archivos del sistema operativo.

### 1. Verificación de Existencia de Rutas
* **`File.Exists(string path)`**: Evalúa la presencia de un archivo físico en el disco. Devuelve un valor booleano (`true`/`false`).
* **`Directory.Exists(string path)`**: Evalúa la presencia de un directorio o carpeta en el disco. Devuelve un valor booleano.

### 2. Gestión e Intersección de Rutas
* **`Path.Combine(string path1, string path2)`**: Combina de forma segura segmentos de rutas. Resuelve de forma automática las discrepancias de caracteres de separación según el sistema operativo anfitrión (`\` para Windows, `/` para entornos basados en Unix/Linux/Mac).
* **`Directory.GetFiles(string path, string searchPattern)`**: Escanea un directorio y devuelve un array de strings con las rutas encontradas. Soporta caracteres comodín (ej. `*.json`, `log_*.txt`) para búsquedas indexadas o por patrones.

### 3. Mecanismos de Creación y Escritura Segura
* **`File.Create(string path)`**: Inicializa un archivo en la ruta especificada. 
  > ⚠️ **Nota Crítica de Seguridad:** Este método devuelve un flujo activo de datos (`FileStream`). Si no se cierra explícitamente, mantendrá el archivo bloqueado a nivel de sistema operativo, provocando excepciones de concurrencia en accesos posteriores.
* **`File.WriteAllText(string path, string contents)`**: Operación atómica de escritura. Si el archivo no existe, lo crea; si existe, sobrescribe por completo su contenido de un solo golpe, liberando el flujo de datos inmediatamente.
* **`File.AppendAllText(string path, string contents)`**: Diseñado específicamente para sistemas de logs o registros históricos. Si el archivo no existe, lo inicializa; si ya existe, posiciona el puntero al final de la cadena de texto y añade el nuevo contenido sin alterar los registros históricos previos.

```csharp
using System.IO;

string rutaArchivo = @"C:\mi-proyecto\app.config";

// Implementación defensiva mediante el patrón de control de flujos
if (!File.Exists(rutaArchivo))
{
    // Garantía de liberación del recurso en disco mediante el bloque utilizando (using)
    using (File.Create(rutaArchivo)) { } 
}
```

---

## 🏗️ Bloque 2: Arquitectura Desacoplada e Inyección de Dependencias (DI)

La transición de un software monolítico y rígidamente acoplado hacia un diseño moderno y testeable se fundamenta en la inversión de control y el aislamiento de dependencias de infraestructura.

### 1. El Antipadrón: Acoplamiento Duro (Mecánica Tradicional)
En el enfoque clásico, las clases de negocio asumen la responsabilidad total del ciclo de vida de sus dependencias: instancian lectores de disco, configuran rutas fijas y realizan llamadas directas mediante la palabra clave `new`.
* ❌ **Deficiencia:** Rigidez absoluta ante cambios, imposibilidad de realizar pruebas unitarias en aislamiento y dependencia directa del hardware del entorno de ejecución.

### 2. El Patrón Moderno: Inyección de Dependencias a través de Constructores
Las clases de lógica de negocio ya no fabrican sus herramientas. En su lugar, declaran sus dependencias de forma abstracta a través de interfaces en su constructor (el cual actúa como la "aduana" de control de la clase).

* **El Contenedor de Servicios (`ServiceCollection`)**: Centraliza la configuración global del sistema de software. Actúa como una fábrica inteligente encargada de resolver las dependencias en orden jerárquico inverso según los planos de abstracción registrados.

### 3. Ciclo de Vida del Dato: Del Disco a la Memoria RAM

┌────────────────────────────────────────────────────────┐
│             EL MAIN (La Frontera / Aduana)             │
│  - Lee el JSON del disco (Una sola vez)                │
│  - Registra las configuraciones en la memoria RAM      │
│  - Configura el contenedor de servicios                │
└───────────────────────────┬────────────────────────────┘
│ (Inyecta las piezas)
▼
┌────────────────────────────────────────────────────────┐
│             MOTOR INTERNO DE LA APLICACIÓN             │
│  - Clases 100% independientes del archivo físico       │
│  - Código limpio, testeable y eficiente                │
└────────────────────────────────────────────────────────┘


1. **Persistencia Estática:** El archivo `appsettings.json` es exclusivamente texto plano en almacenamiento secundario. Es accedido una única vez durante la inicialización del sistema en el método `Main`. El descriptor del archivo en disco se cierra inmediatamente después del parsing.
2. **Mapeo a POO e Intervención del JIT:** Los datos planos estructurados en el JSON se transforman en memoria RAM en instancias de objetos de C# mediante el compilador Just-In-Time (JIT). Esto dota a las configuraciones de comportamiento de objetos (métodos, propiedades fuertemente tipadas y lógica interna).
3. **Optimización mediante Ciclos de Vida Singleton:** Al registrar estas clases de dominio o cadenas de conexión bajo el ciclo de vida `Singleton` en el contenedor de DI, se garantiza que la instancia residirá de forma inmutada en la memoria RAM. El acceso a los parámetros de configuración se reduce de una escala de milisegundos (lectura mecánica de disco) a nanosegundos (puntero directo en memoria RAM).

---

## 🧠 Bloque 3: Las 4 Verdades Absolutas del Diseño de Software

Para consolidar la arquitectura conceptual de nivel senior, se deben asimilar estos cuatro pilares fundamentales:

1. **El JSON es solo texto en el disco:** Su única misión es proveer los datos de configuración iniciales al arrancar la aplicación en el `Main`. Una vez completado el arranque, el disco no se vuelve a consultar.
2. **El JIT le da vida en memoria:** El texto plano se convierte en objetos reales de la Programación Orientada a Objetos (POO). El JSON define los datos; tus clases de dominio aportan la lógica y la inteligencia.
3. **La DI es un juego de constructores:** Las clases internas renuncian al control de instanciación. No ejecutan sentencias `new()` para obtener infraestructura; extienden sus constructores exigiendo recibir los componentes ya preparados desde el exterior.
4. **El Main es el único dueño del `new()` (La Frontera):** El punto de entrada de la aplicación (`Main`) asume la responsabilidad exclusiva de orquestar, instanciar y ensamblar las dependencias del sistema como piezas de un engranaje. Cruzada esta frontera de inicialización, los componentes de software operan bajo total desacoplamiento.

---

## 📋 Apéndice Técnico: Implementación del Flujo Complementario (`System.IO` + DI)

A continuación se ilustra cómo la infraestructura de bajo nivel se encapsula detrás de contratos de abstracción que cumplen con los principios **SOLID**, permitiendo que ambas técnicas actúen de forma complementaria:

```csharp
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace ManualArquitectura.Core
{
    // 1. Contrato de Abstracción (Principio de Inversión de Dependencias)
    public interface IServicioArchivos 
    {
        void RegistrarEvento(string mensaje);
    }

    // 2. Componente de Infraestructura (Manipulación segura con System.IO)
    public class EscritorLogsLocal : IServicioArchivos
    {
        private readonly string _rutaDestino;

        public EscritorLogsLocal(string rutaDestino)
        {
            _rutaDestino = rutaDestino;
        }

        public void RegistrarEvento(string mensaje)
        {
            // El bloque using traduce azúcar sintáctico en una estructura try/finally segura
            using (File.AppendAllText(_rutaDestino, \$"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}\n")) { }
        }
    }

    // 3. El Cuartel General (Orquestación en el punto de entrada)
    class Program
    {
        static void Main(string[] args)
        {
            // Lectura única inicial (Simulación de parsing de appsettings.json)
            string rutaLogConfigurada = Path.Combine(AppContext.BaseDirectory, "sistema.log");

            // Configuración de la Fábrica Inteligente (Contenedor de DI)
            var coleccionServicios = new ServiceCollection();
            
            // Inyección del componente persistido en memoria RAM como Singleton
            coleccionServicios.AddSingleton<IServicioArchivos>(new EscritorLogsLocal(rutaLogConfigurada));
            
            var proveedorServicios = coleccionServicios.BuildServiceProvider();

            // Ejecución segura: El sistema resuelve las jerarquías de objetos de forma automática
            var servicioArchivos = proveedorServicios.GetRequiredService<IServicioArchivos>();
            servicioArchivos.RegistrarEvento("Infraestructura inicializada y acoplada de forma segura mediante DI.");
        }
    }
}
```