using System.ComponentModel.Composition;
using System.Data.Entity;
using DataAccess.UnitOfWork;
using DependencyResolver;

namespace DataModel
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        /// <summary>
        /// Registers the component
        /// </summary>
        /// <param name="registerComponent">Component to register</param>
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork,UnitOfWork>();
        }
    }
}
