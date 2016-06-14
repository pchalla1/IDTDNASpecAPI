using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Spec_DeliveryEntity
    {
        public int SPEC_DELIVERY_ID{get;set;}
        public DateTime CREATE_DTM { get; set; }
        public int SPEC_STATE_ID { get; set; }
        public DateTime? GUAR_COMPLETE_DTM { get; set; }
        public int SPEC_ORDER_ID { get; set; }
        public int? PARENT_SPEC_DELIVERY_ID { get; set; }
        public int PROCESS_PRIORITY { get; set; }
        public int? REF_ID { get; set; }
        public decimal DECLARED_VALUE { get; set; }
        public int VERSION_NBR { get; set; }
        public DateTime VERSION_DTM { get; set; }
        public string PROJECT_NAME { get; set; }
        public bool? REMAKE { get; set; }
        public string EXTERNAL_REF_ID { get; set; }
        public int? DIRECTED_TYPE { get; set; }
        public bool? DO_SHIP { get; set; }
        public decimal? DECLARED_VALUE_NATURAL { get; set; }


    }
}
