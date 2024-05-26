using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model.DataModels;
using SchoolRegister.EF;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.Services.Configuration.AutoMapperProfiles;
using SchoolRegister.Services.Interfaces;
using Services.ConcreteServices;
using Services.Interfaces;
using Tests;


namespace Tests;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MainProfile));
        services.AddEntityFrameworkInMemoryDatabase()
        .AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("InMemoryDb")
        );
        services.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddRoleManager<RoleManager<Role>>()
        .AddUserManager<UserManager<User>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddTransient(typeof(ILogger), typeof(Logger<Startup>));
        services.AddTransient<ISubjectService, SubjectService>();
        services.AddTransient<IGradeService, GradeService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<ITeacherService, TeacherService>();
        services.SeedData();
    }
}