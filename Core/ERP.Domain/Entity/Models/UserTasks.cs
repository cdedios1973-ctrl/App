using System;

/// <summary>
/// POCO de la tabla maestra 'UserTasks' (Logic - Persistence).
/// </summary>

namespace App.Definitions.Entity;

internal sealed record class UserTasks
{
    internal long Id { get; init; }
    internal long UserId { get; init; }
    internal long TaskId { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia <see cref="UserTasks"/>.
    /// </summary>
    /// <param name="id">UserTasks.Id (PK)</param>
    /// <param name="userId">UserTasks.UserId (resticcion combinada con UserTasks.TaskId)</param>
    /// <param name="taskId">UserTasks.TaskId (resticcion combinada con UserTasks.UserId)</param>
    internal UserTasks(long id, long userId, long taskId)
    {
        this.Id = id;
        this.UserId = userId;
        this.TaskId = taskId;
    }
}