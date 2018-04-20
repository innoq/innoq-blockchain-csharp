namespace Com.Innoq.SharpestChain
{
    using System.IO;

    using Akka.Actor;
    
    using Akka.Streams;

    using Eventing;

    using IO;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ConnectionHolder = Eventing.ConnectionHolder;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            string appConfig = File.ReadAllText("app.config");
            var system = ActorSystem.Create("reservieren", appConfig);
            
            var eventConnectionHolder = system.ActorOf(ConnectionHolder.props(), "event-connection-holder");
            var persistence = system.ActorOf(Persistence.props(eventConnectionHolder), "persistence");
            
            services.AddTransient(typeof(IEventConnectionHolderActorRef), pServiceProvider => new EventConnectionHolderActorRefActorRef(eventConnectionHolder));
            services.AddTransient(typeof(IPersistenceActorRef),  pServiceProvider => new PersistenceActorRef(persistence));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
