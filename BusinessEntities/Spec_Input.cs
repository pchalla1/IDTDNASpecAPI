using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Spec_Input
    {

        public string Table { get; set; }
        public string FieldName { get; set; }
        public List<string> Values { get; set; }
        public string Condition { get; set; } // < between like
        public string Operation { get; set; } //AND

        public string WhoAreYou { get; set; }

        public string WhereAreYou { get; set; }

    }
}
