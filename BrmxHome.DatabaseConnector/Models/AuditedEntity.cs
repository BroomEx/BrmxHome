using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrmxHome.DatabaseConnector.Models
{
    abstract public class AuditedEntity: Entity
    {
        public DateTime CreationTime { get; set; }
    }
}
