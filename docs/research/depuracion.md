# Arquitectura Limpia y Estrategia de Pruebas en .NET AOT

Este documento resume la transición conceptual desde el desarrollo fullstack tradicional (pruebas manuales) hacia la automatización moderna de pruebas unitarias utilizando **Arquitectura Limpia**, **SOLID** y compilación nativa **.NET AOT**.

---

## 🧩 El Equipo de Pruebas Moderno

1. **Test Unitario:** Es el concepto. Es la acción de probar una función pequeña y aislada del código. Lo que antes se validaba introduciendo datos manualmente en un formulario web para verificar fallos, ahora se define mediante código ejecutable.
2. **xUnit:** Es la herramienta (el *framework*). Es el motor encargado de escanear el proyecto, ejecutar cientos de pruebas en milisegundos de forma automática y reportar visualmente el estado: **Luz Verde** (éxito) o **Luz Roja** (fallo).
3. **Mock / Fake:** Es el ayudante (el simulacro). Es un objeto sustituto que implementa una interfaz para "engañar" a la lógica de negocio. Permite simular interacciones complejas (como respuestas de bases de datos o servicios externos) sin levantar infraestructura real.

---

## 🔄 La Evolución del Flujo de Trabajo

La transición mental para un desarrollador Fullstack senior es directa, ya que los objetivos de calidad siguen siendo los mismos:

| Antes (Validación Manual) | Ahora (Automatizado con xUnit + Mocks) |
| :--- | :--- |
| Abrir el navegador o Postman, rellenar los campos del formulario y hacer clic en "Enviar". | Escribir un método en xUnit que inyecta los datos de entrada directamente a la función de la lógica. |
| Inspeccionar la pantalla buscando el mensaje de "Éxito" o revisar la consola del servidor por si hay excepciones. | Usar aserciones automáticas mediante comandos `Assert` (ej. `Assert.True(resultado)`). xUnit valida el output por ti. |
| Para probar 10 escenarios (campos vacíos, emails duplicados, etc.), se debía rellenar el formulario 10 veces a mano. | Escribir 10 pruebas unitarias independientes y cortas. xUnit las ejecuta en paralelo en menos de un segundo. |
| Ir a SQL Server Management Studio para borrar manualmente el registro de prueba y evitar errores de clave duplicada en el siguiente intento. | Al usar Mocks/Fakes, la persistencia virtual en memoria se destruye y se recrea limpia en cada prueba. No se genera basura en base de datos. |

> 💡 **La gran ventaja es la paz mental:** Al realizar refactorizaciones o cambios críticos en la lógica, ya no es necesario validar manualmente cada flujo de la interfaz de usuario. Un clic ejecuta cientos de pruebas que garantizan que el sistema sigue funcionando exactamente igual.

---

## ⚠️ Consideraciones Críticas para .NET AOT

Al trabajar con compilación por adelantado (**Ahead-Of-Time**), la reflexión dinámica en tiempo de ejecución está restringida. Por lo tanto, librerías de generación de mocks dinámicos tradicionales (como *Moq*) causarán fallos en producción. 

Para mantener el código **AOT-Friendly**, se deben aplicar las siguientes estrategias:
*   **Mocks/Fakes Manuales:** Implementar clases concretas livianas que hereden de las interfaces de infraestructura (muy eficiente y 100% compatible con AOT).
*   **Source Generators:** Herramientas de generación de código en tiempo de compilación (como *Moq.Automated* o generadores nativos basados en Roslyn), los cuales escriben el código de los simulacros antes de compilar a código máquina nativo.

---

## 💻 Ejemplo Práctico: Test Unitario Compilado para Git (.NET AOT Friendly)

A continuación, se detalla la nomenclatura estándar (`[NombreDeClaseAProbar]Tests.cs`) y la estructura de una prueba unitaria limpia basada en el patrón **AAA (Arrange, Act, Assert)**:

```csharp
using Xunit;

public class RegistrarUsuarioTests
{
    [Fact]
    public void Ejecutar_DeberiaGuardarUsuario_CuandoDatosSonValidos()
    {
        // 1. Arrange (Preparar los simulacros / Fakes conformes a AOT)
        var repoFalso = new FakeUserRepository(); 
        var loggerFalso = new FakeLoggerManual(); 
        
        var casoDeUso = new RegistrarUsuario(repoFalso, loggerFalso);
        var datos = new DatosUsuario("Carlos", "carlos@email.com");

        // 2. Act (Ejecutar la lógica pura de negocio)
        var resultado = casoDeUso.Ejecutar(datos);

        // 3. Assert (Verificación automatizada del comportamiento)
        Assert.True(resultado.Exitoso); 
        Assert.Single(repoFalso.UsuariosGuardados); // Valida que se registró exactamente 1 usuario
        Assert.Equal("carlos@email.com", repoFalso.UsuariosGuardados[0].Email); // Valida la integridad del dato
    }
}
```


## 🧩 Anexo para tu Git: Implementaciones Fake (100% compatibles con .NET AOT)

Añade este último bloque de código al final de tu archivo arquitectura_y_pruebas.md para que el ejemplo sea completamente autocontenido:

```csharp

// fakes/FakeUserRepository.cs
// Implementación en memoria que simula la base de datos sin usar reflexión dinámica.
public class FakeUserRepository : IUserRepository
{
    // Lista en la RAM que hace las funciones de tabla física
    public List<Usuario> UsuariosGuardados { get; } = new();
    
    // Interruptor para simular de forma sencilla escenarios de error (caídas de DB)
    public bool SimularExito { get; set; } = true;

    public bool Guardar(Usuario usuario)
    {
        if (!SimularExito) return false;
        
        UsuariosGuardados.Add(usuario);
        return true;
    }
}

// fakes/FakeLoggerManual.cs
// Implementación ligera para simular el Logger sin tocar el disco I/O ni la consola.
public class FakeLoggerManual : ILogger
{
    // Historial para verificar en el test QUÉ mensajes se intentaron escribir
    public List<string> MensajesRegistrados { get; } = new();

    public void LogInfo(string mensaje)
    {
        MensajesRegistrados.Add($"[INFO] {mensaje}");
    }

    public void LogError(string mensaje)
    {
        MensajesRegistrados.Add($"[ERROR] {mensaje}");
    }
}

