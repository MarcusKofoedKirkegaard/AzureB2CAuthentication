/*
    THIS APPLICATION IS CREATED BASED ON THE MICROSOFT GUIDE AT:
    https://learn.microsoft.com/en-us/aspnet/core/security/authentication/azure-ad-b2c?view=aspnetcore-7.0
*/

using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureADB2C"));

// builder.Services.AddAntiforgery(options =>
// {
//     options.Cookie.SameSite = SameSiteMode.None; // Adjust the SameSite mode as needed
//     options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Set to Always for production deployments with HTTPS
// });

// builder.Services.Configure<CookiePolicyOptions>(options =>
//     {
//         options.MinimumSameSitePolicy = SameSiteMode.None;
//         options.HttpOnly = HttpOnlyPolicy.None;
//         options.Secure = CookieSecurePolicy.Always; // Set to Always for production deployments with HTTPS
//         options.MinimumSameSitePolicy = SameSiteMode.None;
//     });

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to 
    // the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Index");
})
.AddMvcOptions(options => { })
.AddMicrosoftIdentityUI();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();




