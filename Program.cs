using GoTravnikApi.Data;
using GoTravnikApi.IRepositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GoTravnikApi.Helper;
using GoTravnikApi.Services;
using GoTravnikApi.IServices;
using GoTravnikApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IAccommodationRepository, AccommodationRepository>();    
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IAttractionRepository, AttractionRepository>();
builder.Services.AddScoped<IFoodAndDrinkRepository, FoodAndDrinkRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>(); 
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IAttractionService, AttractionService>();
builder.Services.AddScoped<IFoodAndDrinkService, FoodAndDrinkService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
