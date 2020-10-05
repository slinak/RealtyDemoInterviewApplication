using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealtyDemo.Startup))]
namespace RealtyDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
