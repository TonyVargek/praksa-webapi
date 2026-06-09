using Autofac;
using Autofac.Extensions.DependencyInjection;
using Example.Repository;
using Example.Repository.Common;
using Example.Service;
using Example.Service.Common;
using Example.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers();

// builder.Services.AddScoped<IFoodRepository, FoodRepository>();
// builder.Services.AddScoped<IMemberRepository, MemberRepository>();
// builder.Services.AddScoped<IFoodService, FoodService>();
// builder.Services.AddScoped<IMemberService, MemberService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<MemberRepository>().As<IMemberRepository>().InstancePerDependency();
    containerBuilder.RegisterType<FoodRepository>().As<IFoodRepository>().InstancePerDependency();

    containerBuilder.RegisterType<MemberService>().As<IMemberService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<FoodService>().As<IFoodService>().InstancePerLifetimeScope();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();