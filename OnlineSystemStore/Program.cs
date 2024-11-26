using Microsoft.EntityFrameworkCore;
using OnlineSystemStore.DAL.DB;
using OnlineSystemStore.DAL.RepositoryImplementation;
using OnlineSystemStore.Domain.InterfaceRepository;
using OnlineSystemStore.Domain.MainProfile;
using OnlineSystemStore.Services.ServiceImplementation;
using OnlineSystemStore.Services.ServiceInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OnlineDataStore>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

builder.Services.AddScoped(typeof(IMainRepository<>), typeof(MainRepository<>));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
