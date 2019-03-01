﻿using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public interface IProjectService
    {
        Project CreateProject(Project project);
        Project GetProjectById(int id);
        bool DeleteProject(Project project);
        Project GetProjectByProjectId(int id);
        Project GetProjectByNoteId(int id);
        bool UpdateProject(Project project);
    }
}
