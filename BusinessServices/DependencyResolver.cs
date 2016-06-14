using System.ComponentModel.Composition;
using DataAccess;
using DataAccess.UnitOfWork;
using DependencyResolver;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            //registerComponent.RegisterType<ISpecServices, SpecServices>();
            registerComponent.RegisterType<ISpecDeliverySevices, SpecDeliveryServices>();
            registerComponent.RegisterType<ISpecOligoServices, SpecOligoServices>();
        }
    }
}
