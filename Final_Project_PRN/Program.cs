using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Final_Project_PRN.Models;
using Final_Project_PRN.Services;
using Final_Project_PRN.Pages.Validate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PRN221ProjectContext>();
builder.Services.AddTransient<ScheduleService>();
builder.Services.AddTransient<TeacherServices>();
builder.Services.AddTransient<SubjectService>();
builder.Services.AddTransient<UniversityClassService>();
builder.Services.AddTransient<CourseSessionService>();
builder.Services.AddTransient<ValidateSchedule>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
