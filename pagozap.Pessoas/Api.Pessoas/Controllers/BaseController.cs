using App.Pessoas.Interface;
using CrossCutting.Pessoas;
using Infra.Pessoas.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInjector;
using System;

namespace Api.Pessoas.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Base")]
    public class BaseController : Controller
    {
       /// <summary>
	   /// 
	   /// </summary>
        public BaseController()
        {
            QueueParameters.QueuelName = "Pessoas";
        }

    }
}