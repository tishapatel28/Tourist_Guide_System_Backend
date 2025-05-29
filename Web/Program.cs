using Infrastructure.ContextClass;
using Infrastructure.Repositaries;
using Infrastructure.Service.CustomService.CarBookings;
using Infrastructure.Service.CustomService.Cars;
using Infrastructure.Service.CustomService.Flights;
using Infrastructure.Service.CustomService.Hotels;
using Infrastructure.Service.CustomService.Locations;
using Infrastructure.Service.CustomService.RestaurantBookings;
using Infrastructure.Service.CustomService.Restaurants;
using Infrastructure.Service.CustomService.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepositary<>), typeof(Repositary<>));
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<ICarBookingService, CarBookingService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IRestaurantBookingService, RestaurantBookingService>();

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
