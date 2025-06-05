using ConcertTicketSystem.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application-specific services and DbContext with DI
builder.Services.AddConcertTicketSystemServices(builder.Configuration);

var app = builder.Build();

// Middleware pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error"); // Centralized error handling endpoint for production
    app.UseHsts();                    // Enforce HTTPS Strict Transport Security in production
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
