var builder = WebApplication.CreateBuilder(args);

// 🔧 Tohle je důležité – naslouchání na všech IP
builder.WebHost.UseUrls("http://0.0.0.0:5500");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // volitelné

var app = builder.Build();

// ❌ Můžeš zakomentovat nebo smazat, protože https nepoužíváš
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();           // volitelné
app.UseSwaggerUI();         // volitelné

app.Run();
