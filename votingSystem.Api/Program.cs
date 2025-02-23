using Microsoft.EntityFrameworkCore;
using votingSystem.Api.Features.Candidates;
using votingSystem.Api.Features.Candidates.CreateCandidate;
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
builder.Services.AddScoped<IVoteRepository,VoteRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<CreateCanidadateHandler>();
builder.Services.AddScoped<SubmitVoteHandler>();
builder.Services.AddScoped<ProcessVoteHandler>();
builder.Services.AddHostedService<RabbitMqConsumer>();
builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();



var app = builder.Build();


// We have this to automatically apply the migrations to the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VoteSystemDbContext>();
    context.Database.Migrate();
}

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