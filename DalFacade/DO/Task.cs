﻿

using System.Data;

namespace DO;
/// <summary>
///  Task Entity represents a task with all its props
/// </summary>
/// <param name="Id">the ID of the task</param>
/// <param name="Description">The description of the task</param>
/// <param name="Alias">The alias of the task</param>
/// <param name="Milestone">The Milestone of the task</param>
/// <param name="CratedAt">The creation date of the task</param>
/// <param name="Start">The start date of the task</param>
/// <param name="ForecastDate">Estimated date for completion of the task</param>
/// <param name="Deadline">Last date for completing the task</param>
/// <param name="Complete">Actual task completion date</param>
/// <param name="Deliverables">The product of the task</param>
/// <param name="Remarks">Remarks about the task</param>
/// <param name="EngineerId">The ID of the engineer in charge of the task</param>
/// <param name="ComplexityLevel">Complexity level of the task</param>

public record Task
(
     int Id,
     string Description,
     string Alias,
     bool Milestone,
     DateTime CratedAt,
     DateTime Start,
     DateTime ForecastDate,
     DateTime Deadline,
     DateTime Complete,
     string Deliverables,
     string Remarks,
     int EngineerId,
     EngineerExperience ComplexityLevel
)
{
}
