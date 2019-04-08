﻿using Documaster.Model.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.Entities
{
    public class UserProfile : BaseEntity
    {
        [MaxLength(25)]
        public string UserName { get; set; }

        [MaxLength(128)]
        public string Password { get; set; }
    }
}
