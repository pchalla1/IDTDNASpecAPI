using DependencyResolver;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace IDTDNASpecAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

 ComponentLoader.LoadContainer(container, ".\\bin", "IDTDNASpecAPI.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "BusinessServices.dll"); 


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}