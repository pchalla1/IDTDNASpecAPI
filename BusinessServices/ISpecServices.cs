using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// Product Service Contract
    /// </summary>
    public interface ISpecServices
    {
        IEnumerable<object> GetSpec(Spec_Input input);
        IEnumerable<object> GetAllSpecs(Spec_Input input);
        int CreateSpecs(object o);
        IEnumerable<object> UpdateSpec(int SpecDeliveryId,Spec_DeliveryEntity spec);
        bool CancelSpec(int SpecDeliveryId);
    }
}
