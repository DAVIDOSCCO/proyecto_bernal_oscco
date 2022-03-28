using App.Ventas.UI.MVC.ActionFilters;
using App.Ventas.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Ventas.UI.MVC.Controllers
{
    [ErrorActionFilter] //Anottations -> ActionFilter
    //[Authorize]
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unit;
        protected readonly ILog _log;
        public BaseController(IUnitOfWork unit, ILog log = null)
        {
            _unit = unit;
            _log = log;
        }
    }
}