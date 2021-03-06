﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Volunteers_ReadyToHelp.Models;
using Volunteers_ReadyToHelp.ViewModels;

namespace Volunteers_ReadyToHelp.Controllers
{
    public class AdminController : Controller
    {
        public ApplicationDbContext dbContext = new ApplicationDbContext();
        public Country country = new Country();
        public State state = new State();
        // Country Country = new Country();
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard(LoginViewModel model)
        {
            return Content("Done");
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRole(RoleViewModel roleViewModel)
        {
            var roles = ApplicationRoleManager.Create(HttpContext.GetOwinContext());
            if (!await roles.RoleExistsAsync(roleViewModel.RoleName))
            {
                await roles.CreateAsync(new IdentityRole { Name = roleViewModel.RoleName});
                @ViewBag.AddRole = "New role added successfully";
            }
            else
            {
                @ViewBag.AddRole = "Role Already Exists";
            }
            return View();
        }
        
        
        public ActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCountry(CountryViewModel countryViewModel)
        {
            Guid id = Guid.NewGuid();
            country.CountryId = id.ToString();
            country.CountryName = countryViewModel.CountryName;
            dbContext.Country.Add(country);
            dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public ActionResult AddState()
        {
            List<Country> allCountry = new List<Country>();
            allCountry = dbContext.Country.OrderBy(a => a.CountryName).ToList();
            ViewBag.CountryID = new SelectList(allCountry, "CountryID", "CountryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddState(StateViewModel stateViewModel)
        {
            List<Country> allCountry = new List<Country>();
            allCountry = dbContext.Country.OrderBy(a => a.CountryName).ToList();
            ViewBag.CountryID = new SelectList(allCountry, "CountryID", "CountryName");
            Guid id = Guid.NewGuid();
            state.CountryId = stateViewModel.CountryId;
            state.StateId = id.ToString();
            state.StateName = stateViewModel.StateName;
            dbContext.State.Add(state);
            dbContext.SaveChangesAsync();
            
            return View();
        }

        [HttpGet]
        public JsonResult GetStates(RegistrationViewModel registerViewModel)
        {
            List<State> allstates = new List<State>();
            var CountryId = registerViewModel.CountryId;
            string ID = CountryId;
            if (CountryId != null)
            {
                using (ApplicationDbContext dbcontext = new ApplicationDbContext())
                {
                    allstates = dbcontext.State.Where(a => a.CountryId.Equals(ID)).OrderBy(a => a.StateName).ToList();
                }
            }
            if (Request.IsAjaxRequest())
            {
                
                var a = new JsonResult
                {
                    Data = allstates,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return a;
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}