using Newtonsoft.Json.Serialization;
using OrderAPI.Command;
using OrderAPI.DataBase;
using OrderAPI.Hub;
using OrderAPI.Requests;
using OrderAPI.Responses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddScoped<ICommandResolver,CommandResolver>();


builder.Services.AddTransient<ICommand<LoginAppRequest, LoginAppResponse>, GetUserByIdPasswordCommand>();
builder.Services.AddTransient<ICommand<GetAllItemsRequest, GetAllItemsResponse>, GetAllItemsCommand>();


builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");

app.MapControllers();
app.MapHub<MessageHub>("/order");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
