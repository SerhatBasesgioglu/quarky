using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Qaurky is running");

app.MapGet("/api/notes", () =>
{
    var notes = Directory.GetFiles("notes", "*.md")
    .Select(f => Path.GetFileNameWithoutExtension(f));
    return Results.Ok(notes);
});

app.MapGet("/api/notes/{id}", (string id) =>
{
    var path = $"notes/{id}.md";
    if (!File.Exists(path)) return Results.NotFound();
    var content = File.ReadAllText(path);
    return Results.Ok(content);
});

app.MapPost("/api/notes", async ([FromBody] Note note) =>
{
    Directory.CreateDirectory("notes");
    var content = note.ToMarkDown();
    var filename = $"notes/{note.Title}.md";
    await File.WriteAllTextAsync(filename, content);
    return Results.Created($"/api/notes/{note.Id}", null);
});

app.Run();
