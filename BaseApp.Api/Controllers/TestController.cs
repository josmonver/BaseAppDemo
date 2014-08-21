using BaseApp.Data;
using BaseApp.Data.Contracts;
using System.Web.Http;

namespace BaseApp.Web.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private IUoW _uow;
        RepositoryProvider _repo;
        public TestController(RepositoryProvider repo)
        {
            _repo = repo;
           // _uow = uow;
        }

        [Authorize]
        [Route("")]
        public string Get()
        {
            return "OK!";
        }

    }
}