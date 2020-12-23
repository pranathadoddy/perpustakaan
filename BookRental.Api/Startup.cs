using AutoMapper;
using BookRental.DataAccess;
using BookRental.DataAccess.Application;
using BookRental.Repository.Common;
using BookRental.RepositoryContract.Common;
using BookRental.Service.Common;
using BookRental.ServiceContract.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.Api
{
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
            services.AddControllers();

            var databaseConnectionString = this.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BookRentalContext>(context => context.UseSqlServer(databaseConnectionString));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            var a = this.Configuration.GetSection("AllowOriginUrl").Value;
            services.AddCors(c=> { 
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                .AllowAnyMethod()
               .AllowAnyHeader()); 
            });

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRentalService, RentalService>();


            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
