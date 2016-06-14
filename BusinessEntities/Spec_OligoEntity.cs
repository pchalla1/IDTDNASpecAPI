using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Spec_OligoEntity
    {
        public int SPEC_OLIGO_ID { get; set; }
        public System.DateTime CREATE_DTM { get; set; }
        public int SPEC_STATE_ID { get; set; }
        public int OLIGO_TYPE { get; set; }
        public int WORKFLOW_PATH_ID { get; set; }
        public int BRANCH_LOCATION_ID { get; set; }
        public Nullable<int> PROD_ID { get; set; }
        public Nullable<int> REF_ID { get; set; }
        public Nullable<int> SCALE_ID { get; set; }
        public int SPEC_DELIVERY_ID { get; set; }
        public int SPEC_ORDER_ID { get; set; }
        public double GUAR_MIN_SHIP_VAL { get; set; }
        public int GUAR_MIN_SHIP_UOM_ID { get; set; }
        public double GUAR_MIN_SYNTH_VAL { get; set; }
        public int GUAR_MIN_SYNTH_UOM_ID { get; set; }
        public double GUAR_MAX_SYNTH_VAL { get; set; }
        public int GUAR_MAX_SYNTH_UOM_ID { get; set; }
        public Nullable<double> PURITY_GUAR_PCT { get; set; }
        public int PURITY_EXPL_ID { get; set; }
        public bool PURITY_GUAR_USABLE { get; set; }
        public Nullable<int> YIELD_EXPL_ID { get; set; }
        public int PROCESS_PRIORITY { get; set; }
        public Nullable<int> PURIFICATION_ID { get; set; }
        public Nullable<int> PARENT_SPEC_OLIGO_ID { get; set; }
        public int VERSION_NBR { get; set; }
        public System.DateTime VERSION_DTM { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string SEQUENCE { get; set; }
        public string SEQUENCE_DESCRIPTION { get; set; }
        public string SEQUENCE_CODON { get; set; }
        public string EXTERNAL_REF_ID { get; set; }
        public Nullable<int> DIRECTED_TYPE { get; set; }
        public Nullable<bool> IS_SYNTHESIS_REQUIRED { get; set; }

        public int? QUANTITY { get; set; }
    }
}
