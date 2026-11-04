using System;
//using System.Text.Json.Serialization;

/// <summary>
/// DTO de la tabla maestra 'Task' (Vista).
/// </summary>

namespace App.Definitions.DTO;

[Serializable()] // TODO: Estudiar y cambiar a [JsonSerializable] cuando se migren los serializadores
internal sealed record class TaskDto
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Observation { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="TaskDto"/>.
    /// </summary>
    /// <param name="id">Task.Id (PK)</param>
    /// <param name="name">Task.Name</param>
    /// <param name="observation">Task.Observation</param>
    public TaskDto(long id, string name, string observation)
    { 
        this.Id = id;
        this.Name = name;
        this.Observation = observation;
    }
}