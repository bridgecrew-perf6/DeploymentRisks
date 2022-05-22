using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentRisks.DAL
{
    public class ToDoTask
    {
        [Required]
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
