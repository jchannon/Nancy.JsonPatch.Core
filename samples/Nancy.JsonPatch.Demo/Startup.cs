namespace Nancy.JsonPatch.Demo
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Nancy.Owin;
    
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(opts => opts.Bootstrapper = new DemoBootstrapper()));
        }
    }
}
