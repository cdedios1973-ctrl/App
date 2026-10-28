using System;
//using System.Text.Json.Serialization;

namespace App.Definitions.DTO;

/// <summary>
/// DTO de la tabla maestra 'Task' (Vista).
/// </summary>

[Serializable()] // TODO: Estudiar y cambiar a [JsonSerializable] cuando se migren los serializadores
public sealed record class TaskDto
{

    #region Properties { immutable, locked after creation }

    public long Id { get; init; } = 0;
    public string Name { get; init; } = string.Empty;
    public string Observation { get; init; } = string.Empty;

    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="TaskDto"/>.
    /// </summary>
    /// <param name="id">Task.Id (PK)</param>
    /// <param name="name">Task.Name</param>
    /// <param name="observation">Task.Observation</param>

    #region Constructor

    public TaskDto(long id, string name, string observation)
    { 
        this.Id = id;
        this.Name = name;
        this.Observation = observation;
    }

    #endregion

}