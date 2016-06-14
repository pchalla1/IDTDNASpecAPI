using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface ISpecOligoServices
    {


        DataTable GetSpecOligosByFilter(List<Spec_Input> specInputs);
        Spec_OligoEntity GetSpecOligoById(int DeliveryId);
        IEnumerable<Spec_OligoEntity> GetAllSpecOligos();
        int CreateSpecOligos(Spec_OligoEntity specOligo);
        bool UpdateSpecOligos(int oligoId, Spec_OligoEntity SpecOligo);
        bool CancelSpecOligo(List<int> oligoIds);
    }
}
