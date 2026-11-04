using System;

/// <summary>
/// POCO de la tabla maestra 'Task' (Logic - Persistence).
/// </summary>

namespace ERP.Domain.Entity;

internal sealed record class Task
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Observation { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="Task"/>.
    /// </summary>
    /// <param name="id">Task.Id (PK)</param>
    /// <param name="name">Task.Name</param>
    /// <param name="observation">Task.Observation</param>
    public Task(long id, string name, string observation) 
    {
        this.Id = id;
        this.Name = name;
        this.Observation = observation;
    }
}