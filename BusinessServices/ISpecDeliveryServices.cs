using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface ISpecDeliverySevices
    {

        DataTable GetSpecDeliveriesByFilter(List<Spec_Input> specInputs);
        Spec_DeliveryEntity GetSpecDeliveryById(int DeliveryId);
        IEnumerable<Spec_DeliveryEntity> GetAllSpecDeliveries();
        int CreateSpecDelivery(Spec_DeliveryEntity specDelivery);
        bool UpdateSpecDelivery(int deliveryId, Spec_DeliveryEntity specDelivery);
        bool CancelSpecDelivery(List<int> deliveryIds);
    }
}
