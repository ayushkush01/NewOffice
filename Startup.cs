using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Office.Startup))]
namespace Office
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
