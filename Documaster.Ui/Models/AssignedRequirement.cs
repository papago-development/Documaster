﻿namespace Documaster.Ui.Models
{
    public class AssignedRequirement
    {
        public int Id { get; set; }
        public int RequirementId { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}