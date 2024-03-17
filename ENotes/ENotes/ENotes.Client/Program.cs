using ENotes.Client;
using ENotes.Client.Services;
using ENotes.shared.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazorBootstrap();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<IRoleService,RoleServices>();
builder.Services.AddScoped<ISemesterService,SemesterService>();
builder.Services.AddScoped<IScoreServices,ScoreServices>();
builder.Services.AddScoped<ISubjectService, SubjectServices>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IStudentService,StudentServices>();
builder.Services.AddScoped<ITeachService, TeachServices>();
builder.Services.AddScoped<INoteService, NoteServices>();
builder.Services.AddScoped<IQuizService, QuizServices>();


builder.Services.AddFluentUIComponents();
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    }
);

await builder.Build().RunAsync();
