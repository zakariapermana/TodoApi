using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        //public ValuesController(IDataAccessProvider dataAccessProvider)
        //{
        //    _dataAccessProvider = dataAccessProvider;
        //}
    }
}
