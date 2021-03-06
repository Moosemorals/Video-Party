using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Prometheus;

using Serilog;

namespace uk.osric.VideoParty {
    public class Startup {

        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env) {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddHttpContextAccessor();

            services.Configure<ForwardedHeadersOptions>(options => {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                    | ForwardedHeaders.XForwardedProto;
            });

            services.AddMvcCore();

            IMvcBuilder mvcBuilder = services.AddControllersWithViews(
                options => {
                    options.EnableEndpointRouting = false;

                    options.Filters.Add(
                        new AutoValidateAntiforgeryTokenAttribute()
                    );
                }
            );

            if (_env.IsDevelopment()) {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            app.UseSerilogRequestLogging();
            app.UseForwardedHeaders();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = ctx => ctx.Context.Response.Headers.Append("Cache-Control",
                        $"public max-age={TimeSpan.FromHours(27).TotalSeconds}"),
            });


            app.UseRouting();
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });

            app.UseMvc();

        }
    }
}
