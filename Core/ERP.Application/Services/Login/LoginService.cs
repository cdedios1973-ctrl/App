using ERP.Application.DTO;      // DTO de la vista/UI
using ERP.Application.Interfaces.Persistence;
using ERP.Domain.Queries;       // Tus POCOs de consulta (Read Models)

namespace ERP.Application.Services;

// 1. Tu Excepción Personalizada de Negocio
public class BusinessRuleException : Exception
{
    // Puedes pasarle una lista de errores o un texto acumulado
    public BusinessRuleException(string message) : base(message) { }
}

public class LoginService
{
    private readonly IAuthUserReadRepository _authRepository;

    public LoginService(IAuthUserReadRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public void Execute(UserLoginQueryDto dtoFromUi)
    {
        // --- SECCIÓN FAIL-FAST ---
        // Acumular posibles errores antes de tocar la base de datos
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(dtoFromUi.UserName))
            errores.Add("El campo 'Usuario' es obligatorio.");

        if (string.IsNullOrWhiteSpace(dtoFromUi.Password))
            errores.Add("El campo 'Contraseña' es obligatorio.");

        // Si falla algo, Custom Exception
        if (errores.Any())
        {
            string mensajeCompleto = string.Join(Environment.NewLine, errores);
            throw new BusinessRuleException(mensajeCompleto);
        }

        // --- CONTINÚA EL FLUJO NORMAL ---
        // Si pasa el Fail-Fast, llamar seguro persistencia
        var queryResult = _authRepository.Find(dtoFromUi.UserName, dtoFromUi.Password);

        if (queryResult == null)
            throw new BusinessRuleException("El usuario o la contraseña no son válidos.");
    }
}
