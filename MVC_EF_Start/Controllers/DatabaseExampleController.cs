using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
  public class DatabaseExampleController : Controller
  {
    public ApplicationDbContext dbContext;

    public DatabaseExampleController(ApplicationDbContext context)
    {
      dbContext = context;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<ViewResult> DatabaseOperations()
    {
      // CREATE operation
      Company MyCompany = new Company();
      MyCompany.symbol = "MCOB";
      MyCompany.name = "ISM";
      MyCompany.date = "ISM";
      MyCompany.isEnabled = true;
      MyCompany.type = "ISM";
      MyCompany.iexId = "ISM";

      Quote MyCompanyQuote1 = new Quote();
      //MyCompanyQuote1.EquityId = 123;
      MyCompanyQuote1.date = "11-23-2018";
      MyCompanyQuote1.open = 46.13F;
      MyCompanyQuote1.high = 47.18F;
      MyCompanyQuote1.low = 44.67F;
      MyCompanyQuote1.close = 47.01F;
      MyCompanyQuote1.volume = 37654000;
      MyCompanyQuote1.unadjustedVolume = 37654000;
      MyCompanyQuote1.change = 1.43F;
      MyCompanyQuote1.changePercent = 0.03F;
      MyCompanyQuote1.vwap = 9.76F;
      MyCompanyQuote1.label = "Nov 23";
      MyCompanyQuote1.changeOverTime = 0.56F;
      MyCompanyQuote1.symbol = "MCOB";

      Quote MyCompanyQuote2 = new Quote();
      //MyCompanyQuote1.EquityId = 123;
      MyCompanyQuote2.date = "11-23-2018";
      MyCompanyQuote2.open = 46.13F;
      MyCompanyQuote2.high = 47.18F;
      MyCompanyQuote2.low = 44.67F;
      MyCompanyQuote2.close = 47.01F;
      MyCompanyQuote2.volume = 37654000;
      MyCompanyQuote2.unadjustedVolume = 37654000;
      MyCompanyQuote2.change = 1.43F;
      MyCompanyQuote2.changePercent = 0.03F;
      MyCompanyQuote2.vwap = 9.76F;
      MyCompanyQuote2.label = "Nov 23";
      MyCompanyQuote2.changeOverTime = 0.56F;
      MyCompanyQuote2.symbol = "MCOB";

      Student oneStudent = new Student();
            oneStudent.Id = 111;
            oneStudent.Name = "Bibhas K Bera";

      Course onecourse = new Course();
            onecourse.Id = 11;
            onecourse.Name = "MSBAIS";

      Enrolment oneenrollment = new Enrolment();
            oneenrollment.Id = 1;
            oneenrollment.course = onecourse;
            oneenrollment.student = oneStudent;
            oneenrollment.grade = "A++";





      dbContext.Companies.Add(MyCompany);
      dbContext.Quotes.Add(MyCompanyQuote1);
      dbContext.Quotes.Add(MyCompanyQuote2);
            dbContext.Students.Add(oneStudent);
            dbContext.Courses.Add(onecourse);
            dbContext.Enrolments.Add(oneenrollment);

            //dbContext.App_Models.Add(oneappmodel);

            dbContext.SaveChanges();

     App_Models oneappmodel = new App_Models();
            oneappmodel.enrolments = oneenrollment;
            oneappmodel.companies = MyCompany;
            oneappmodel.quotes = new List<Quote>();

            oneappmodel.quotes.Add(MyCompanyQuote1);
            oneappmodel.quotes.Add(MyCompanyQuote2);
          


            // READ operation
            Company CompanyRead1 = dbContext.Companies
                              .Where(c => c.symbol == "MCOB")
                              .First();

      Company CompanyRead2 = dbContext.Companies
                              .Include(c => c.Quotes)
                              .Where(c => c.symbol == "MCOB")
                              .First();

      // UPDATE operation
      CompanyRead1.iexId = "MCOB";
      dbContext.Companies.Update(CompanyRead1);
      //dbContext.SaveChanges();
      await dbContext.SaveChangesAsync();
      
      // DELETE operation
      //dbContext.Companies.Remove(CompanyRead1);
      //await dbContext.SaveChangesAsync();

      return View(oneappmodel);
    }

    public ViewResult LINQOperations()
    {

            Enrolment enrolment1 = dbContext.Enrolments
                                    .Include(e => e.student)
                                    .Include(e => e.course )
                                    .Where(e => e.Id == 1)
                                    .First();

            Company CompanyRead1 = dbContext.Companies
                                      .Where(c => c.symbol == "MCOB")
                                      .First();


            Quote Quote1 = dbContext.Companies
                              .Include(c => c.Quotes)
                              .Where(c => c.symbol == "MCOB")
                              .FirstOrDefault()
                              .Quotes
                              .FirstOrDefault();


            App_Models oneappmodel = new App_Models();
            oneappmodel.enrolments = enrolment1;
            oneappmodel.companies = CompanyRead1;
            oneappmodel.quotes = new List<Quote>();

            oneappmodel.quotes.Add(Quote1);
            //oneappmodel.quotes.Add(MyCompanyQuote2);

            return View(oneappmodel);
    }

  }
}