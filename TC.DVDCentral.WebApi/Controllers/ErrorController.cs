using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.DVDCentral.Models;
using TC.DVDCentral.BusinessLogic;
using System.Web.Http;

namespace TC.DVDCentral.WebApi.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public IEnumerable<ErrorRecord> Get()
        {
            using (ErrorLogger logger = new ErrorLogger())
                return logger.GetAll();
        }

        public ErrorRecord Get(int id)
        {
            using (ErrorLogger logger = new ErrorLogger())
                return logger.GetError(id);
        }

        public void Post([FromBody]ErrorRecord error)
        {
            using (ErrorLogger logger = new ErrorLogger())
                logger.LogError(error);
        }
    }
}