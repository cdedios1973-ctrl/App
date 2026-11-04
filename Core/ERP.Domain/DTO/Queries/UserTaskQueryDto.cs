using System;
//using System.Text.Json.Serialization;

namespace App.Definitions.DTO;

/// <summary>
/// DTO resultante del JOIN entre UserTask, User y Task. Es una vista
/// Relacion 1 : 1 : Muchos.
/// </summary>

[Serializable()] // TODO: Estudiar y cambiar a [JsonSerializable] cuando se migren los serializadores
public sealed record class UserTaskQueryDto
{

    #region Properties { immutable, locked after creation }

    public long Id { get; init; } = 0;
    public long UserId { get; init; } = 0;
    public long TaskId { get; init; } = 0;
    public string UserName { get; init; } = string.Empty;
    public string TaskName { get; init; } = string.Empty;
    public string TaskObservation { get; init; } = string.Empty;

    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserTaskQueryDto"/>.
    /// </summary>
    /// <param name="id">UserTask.Id (PK)</param>
    /// <param name="userId">UserTask.UserId (FK)</param>
    /// <param name="taskId">UserTask.TaskId (FK)</param>
    /// <param name="userName">User.UserName (Vía INNER JOIN)</param>
    /// <param name="taskName">Task.Name (Vía LEFT JOIN)</param>
    /// <param name="taskObservation">Task.Observation</param>

    #region Constructors

    public UserTaskQueryDto(long id, long userId, long taskId, string userName, string taskName, string taskObservation)
    {
        this.Id = id;
        this.UserId = userId;
        this.TaskId = taskId;
        this.UserName = userName;
        this.TaskName = taskName;
        this.TaskObservation = taskObservation;
    }

    #endregion

}