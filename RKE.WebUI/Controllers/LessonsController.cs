﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Internal;
using Castle.MicroKernel.ModelBuilder.Descriptors;
using RKE.BL.Abstract;
using RKE.UIModels.RozkladModelForStudents;
using RKE.UIModels.RozkladModelsForTeachers;
using StringExtensions = Bender.Extensions.StringExtensions;

namespace RKE.WebUI.Controllers
{
    public class LessonsController : Controller
    {
        private readonly IRozkladHandler _logic;
        public LessonsController(IRozkladHandler logic)
        {
            _logic = logic;
        }

        public async Task<ActionResult>  Index(string text)
        {
            if (!text.IsNullOrEmpty())
            {
                Regex regex = new Regex(@"\d");
                Regex regexForExternal = new Regex(@"-з\d");
                MatchCollection match = regex.Matches(text);

                if (regexForExternal.IsMatch(text))
                {
                    var re = await _logic.GetByExternalGroup(text);
                    return View("LessonsForExternalStudents",re);

                }
                else if (match.IsNullOrEmpty())
                {
                    RozkladModelForTeachersRozkladModel re = await _logic.GetByNameOfTeacher(text);

                    if (StringExtensions.IsNullOrEmpty(re))
                    {
                        return View("Index");
                    }
                    else
                    {
                        return View("LessonsForTeachers",re);
                    }
                }
                else
                {
                    RozkladModelForStudentsRozkladModel re = await _logic.GetByGroup(text);

                    if (StringExtensions.IsNullOrEmpty(re))
                    {
                        return View("Index");
                    }
                    else
                    {
                        return View("LessonsForStudents", re);
                    }
                }
            }
            else
            {
                return View("Index");
            }
        }

        public async Task<ActionResult> Teacher(int id) {
            RozkladModelForTeachersRozkladModel re = await _logic.GetByIdOfTeacher(id);

            if (StringExtensions.IsNullOrEmpty(re))
            {
                return View("Index");
            }
            else
            {
                return View("LessonsForTeachers", re);
            }

        }
    }
}