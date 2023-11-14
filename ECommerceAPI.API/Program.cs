using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
    ));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()) //Kendi filterımı container'a ekliyorum.
    .AddFluentValidation(configuration =>
        configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()) //Reflection sayesinde kendisi çalıştığı container içerisindeki assemblyde ne kadr validator sınıfı varsa hepsini kullanacağını söylüyoruz. Artık sadece application'ı tarif edene bir sınıf bildirmek yeterli olacaktır.
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);//mevcut olan filtrelemeleri kapatır. Bundan sonra sadece benim yazdığım validasyonları çalıştır diyorum. şu anda controller test edilebilir
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
