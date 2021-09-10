using Microsoft.EntityFrameworkCore;
using MinApi;
using MinApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TeamDbContext>(x => x.UseInMemoryDatabase("Teams_DB"));

await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/teamList", async (TeamDbContext context) =>
{
    return await context.Teams.ToListAsync();
});

app.MapGet("/team/{id}", async (int id, TeamDbContext context) => 
{
    return await context.Teams.FirstOrDefaultAsync(a => a.Id == id);
});

app.MapPost("/team", async (Team team, TeamDbContext context) =>
{
    context.Teams.Add(team);
    await context.SaveChangesAsync();

    return team;
});

app.MapPut("/team", async (Team team, TeamDbContext context) =>
{
    context.Entry(team).State = EntityState.Modified;
    await context.SaveChangesAsync();
    return team;
});

app.MapDelete("/team/{id}", async (int id, TeamDbContext context) =>
{
    var pessoa = await context.Teams.FirstOrDefaultAsync(a => a.Id == id);

    context.Teams.Remove(pessoa);
    await context.SaveChangesAsync();
    return;
});

app.Run();