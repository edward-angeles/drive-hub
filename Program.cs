
using DriveHub.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// MVC + RAZOR PAGES
// ========================================

// Liam's MVC marketplace
builder.Services.AddControllersWithViews();

// External APIs
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<VehicleApiService>();

// ========================================
// FIREBASE CONFIGURATION
// ========================================

var projectId = builder.Configuration["Firebase:ProjectId"];

if (string.IsNullOrWhiteSpace(projectId))
{
    throw new InvalidOperationException(
        "Set Firebase:ProjectId using dotnet user-secrets before running.");
}

var credentialPath = builder.Configuration["Firebase:CredentialPath"];

if (string.IsNullOrWhiteSpace(credentialPath))
{
    credentialPath = Environment.GetEnvironmentVariable(
        "GOOGLE_APPLICATION_CREDENTIALS");
}

if (string.IsNullOrWhiteSpace(credentialPath) || !File.Exists(credentialPath))
{
    throw new InvalidOperationException(
        "Set Firebase:CredentialPath to your service account JSON file.");
}

var credential = CredentialFactory
    .FromFile<ServiceAccountCredential>(credentialPath)
    .ToGoogleCredential();

// Firestore database
builder.Services.AddSingleton(new FirestoreDbBuilder
{
    ProjectId = projectId,
    GoogleCredential = credential
}.Build());

// Firestore repositories
builder.Services.AddSingleton<PartRepository>();
builder.Services.AddSingleton<OrderRepository>();

// Firebase Admin SDK
builder.Services.AddSingleton(provider =>
{
    return FirebaseApp.DefaultInstance ??
        FirebaseApp.Create(new AppOptions
        {
            Credential = credential,
            ProjectId = projectId
        });
});

// ========================================
// AUTHENTICATION
// ========================================

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";

        options.Cookie.Name = "DriveHub.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// ========================================
// BUILD APPLICATION
// ========================================

var app = builder.Build();

// ========================================
// HTTP PIPELINE
// ========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve existing wwwroot files
app.UseStaticFiles();

app.UseRouting();

// Authentication must precede authorization
app.UseAuthentication();
app.UseAuthorization();

// ========================================
// ROUTING
// ========================================

// Edward's static asset mapping
app.MapStaticAssets();

// Liam's MVC controllers
app.MapControllers();

// Marketplace homepage
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Parts}/{action=Index}/{id?}");

// ========================================
// START APPLICATION
// ========================================

Console.WriteLine(
    $"Environment: {app.Environment.EnvironmentName}");

Console.WriteLine(
    $"Firebase Project ID configured: {!string.IsNullOrWhiteSpace(projectId)}");

app.Run();
