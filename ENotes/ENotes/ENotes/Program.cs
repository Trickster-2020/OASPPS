using ENotes.Client.Pages;
using ENotes.Client.Services;
using ENotes.Components;
using ENotes.Components.Account;
using ENotes.Data;
using ENotes.Repository;
using ENotes.shared.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
Extreme.License.Verify("42022-39930-22986-04040");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//registering configuration for EnotesDbContext
builder.Services.AddDbContext<EnotesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnotesConnection"));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddBlazorBootstrap();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IRoleService,RoleRepository>();
builder.Services.AddScoped<ISemesterService,SemesterRepository>();
builder.Services.AddScoped<IScoreServices,ScoreRepository>();
builder.Services.AddScoped<ISubjectService,SubjectRepository>();
builder.Services.AddScoped<ITeacherService,TeacherRepository>();
builder.Services.AddScoped<IStudentService,StudentRepository>();
builder.Services.AddScoped<ITeachService,TeachRepository>();
builder.Services.AddScoped<INoteService, NoteRepository>();
builder.Services.AddScoped<IQuizService, QuizRepository>();
builder.Services.AddScoped<IEnrolledService,EnrollledRepository>();
builder.Services.AddScoped<IQuizScoreService, QuizScoreRepository>();
builder.Services.AddScoped<IRegisterService, RegisterRepository>();

builder.Services.AddFluentUIComponents();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Dashboard).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
