using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeploymentRisks.DAL;

namespace DeploymentRisks.Models
{
    public class ReadTaskModel
    {
        public List<ToDoTask> TaskList { get; set; }
    }
}
