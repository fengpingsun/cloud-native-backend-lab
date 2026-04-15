var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

var orders = new List<Order>();

app.MapGet("/orders", () => orders);

app.MapPost("/orders", (Order order) =>
{
    order.Id = Guid.NewGuid();
    orders.Add(order);
    return Results.Ok(order);
});

app.Run();

record Order
{
    public Guid Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
}