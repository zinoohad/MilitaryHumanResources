using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryHumanResources.Model
{
    public class ArmoredVessels
    {
        public int ID { get; set; }

        public string MilitaryID { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string FullName => Name + (MilitaryID == null ? "" : $" [{MilitaryID}]");
    }
}
