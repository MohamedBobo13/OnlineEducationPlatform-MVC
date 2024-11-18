using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.BLL.AutoMapper.AnswerAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.AnswerResultAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.CourseAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.EnrollmnentAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.ExamMappingProfile;
using OnlineEducationPlatform.BLL.AutoMapper.ExamResultMapper;
using OnlineEducationPlatform.BLL.AutoMapper.InstructorAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.LectureAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.QuesionAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.QuizAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.QuizResultAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.StudentAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper.VideoAutoMapper;
using OnlineEducationPlatform.BLL.AutoMapper;
using OnlineEducationPlatform.DAL.Data.DBHelper;
using OnlineEducationPlatform.BLL.Manager.Answermanager;
using OnlineEducationPlatform.BLL.Manager.EnrollmentManager;
using OnlineEducationPlatform.BLL.Manager.ExamManager;
using OnlineEducationPlatform.BLL.Manager.ExamResultmanager;
using OnlineEducationPlatform.BLL.Manager.InstructorManager;
using OnlineEducationPlatform.BLL.Manager.Questionmanager;
using OnlineEducationPlatform.BLL.Manager.QuizManager;
using OnlineEducationPlatform.BLL.Manager.quizresultmanager;
using OnlineEducationPlatform.BLL.Manager.StudentManager;
using OnlineEducationPlatform.BLL.Manager;
using OnlineEducationPlatform.DAL.Repo.AnswerRepo;
using OnlineEducationPlatform.DAL.Repo.AnswerResultRepo;
using OnlineEducationPlatform.DAL.Repo.EnrollmentRepo;
using OnlineEducationPlatform.DAL.Repo.Iexamrepo;
using OnlineEducationPlatform.DAL.Repo.InstructorRepo;
using OnlineEducationPlatform.DAL.Repo.QuestionRepo;
using OnlineEducationPlatform.DAL.Repo.QuizRepo;
using OnlineEducationPlatform.DAL.Repo.StudentRepo;
using OnlineEducationPlatform.DAL.Repositories;
using OnlineEducationPlatform.BLL.Manager.Account_Manager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace OnlineEducationPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EducationPlatformContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            }
          );

			builder.Services.AddAutoMapper(map => map.AddProfile(new Examresultmappingprofile()));

			builder.Services.AddAutoMapper(map => map.AddProfile(new LectureMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new PdfFileMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new VedioMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new CourseMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new QuizResultMappingProfile()));

			builder.Services.AddAutoMapper(map => map.AddProfile(new AnswerMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new AnswerResultMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new QuestionMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new EnrollmentMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new StudentMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new InstructorMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new QuizMappingProfile()));
			builder.Services.AddAutoMapper(map => map.AddProfile(new ExamMappingProfile()));

            RegisterServices(builder.Services);
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<EducationPlatformContext>()
              .AddDefaultTokenProviders();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjusted session timeout to 30 minutes
                options.Cookie.HttpOnly = true; // Ensure the session cookie is HTTP-only
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // Redirect to login if not authenticated
                options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to access denied page if unauthorized
                options.ExpireTimeSpan = TimeSpan.FromDays(7); // Cookie expires after 7 days
                options.SlidingExpiration = true; // Extend cookie expiration on activity
                options.Cookie.HttpOnly = true; // Ensure cookie cannot be accessed by JavaScript
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // Use Cookie-based authentication

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        private static void RegisterServices(IServiceCollection Services)
		{
           Services.AddScoped<IEnrollmentRepo, EnrollmentRepo>();
                Services.AddScoped<IenrollmentManager, EnrollmentManager>();

                Services.AddScoped<IQuizResultRepo, QuizResultRepo>();
            Services.AddScoped<IQuizResultManager, QuizResultManager>();

            Services.AddScoped<IExamResultRepo, ExamResultRepo>();
           Services.AddScoped<IExamResultmanager, examresultmanager>();

            Services.AddScoped<IStudentRepo, StudentRepo>();
                Services.AddScoped<Istudentmanager, Stuentmanager>();
            Services.AddScoped<IInstructorRepo, InstructorRepo>();
                 Services.AddScoped<IInstructorManager, instructorManager>();

           Services.AddScoped<IQuizRepo, QuizRepo>();
           Services.AddScoped<IQuizManager, QuizManager>();
            //builder.Services.AddScoped<IQuizManager, QuizManager>();
          Services.AddScoped<Iexamrepo, examrepo>();
           Services.AddScoped<IExamManager, ExamManager>();






            Services.AddScoped<IAnswerRepo, AnswerRepo>();
            Services.AddScoped<IAnswerManager, AnswerManager>();

            Services.AddScoped<IAnswerResultRepo, AnswerResultRepo>();
            Services.AddScoped<IAnswerResultManager, AnswerResultManager>();

                Services.AddScoped<IQuestionRepo, QuestionRepo>();
            Services.AddScoped<IQuestionManager, QuestionManager>();

            Services.AddScoped<ILectureRepo, LectureRepo>();
            Services.AddScoped<ILectureManager, LectureManager>();
            Services.AddScoped<IPdfFileRepo, PdfFileRepo>();
            Services.AddScoped<IPdfFileManager, PdfFileManager>();
            Services.AddScoped<IVedioRepo, VedioRepo>();
          Services.AddScoped<IVedioManager, VedioManager>();
              Services.AddScoped<ICourseRepo, CourseRepo>();
            Services.AddScoped<ICourseManager, CourseManager>();
            Services.AddScoped<IAccountManager, AccountManager>();



        }
    }
}
