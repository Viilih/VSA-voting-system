using Microsoft.EntityFrameworkCore;
using votingSystem.Api.Features.Votes;
using votingSystem.Api.Features.Votes.ProcessVote;
using votingSystem.Api.Features.Votes.SubmitVote;
using votingSystem.Api.Infrastructure.DbContext;
using votingSystem.Api.Infrastructure.Messaging.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<VoteSystemDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("votingSystem.Api")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<VoteRepository>();
builder.Services.AddScoped<SubmitVoteHandler>();
builder.Services.AddScoped<ProcessVoteHandler>();
builder.Services.AddHostedService<RabbitMqConsumer>();
builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();



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