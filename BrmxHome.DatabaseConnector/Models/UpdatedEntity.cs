using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrmxHome.DatabaseConnector.Models
{
    public class UpdatedEntity: AuditedEntity
    {
        public DateTime Updated { get; set; }
    }
}
