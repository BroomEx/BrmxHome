using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrmxHome.DatabaseConnector.Models
{
    public class Sensor: FullyAuditedEntity
    {
        public string Name { get; set; }
    }
}
