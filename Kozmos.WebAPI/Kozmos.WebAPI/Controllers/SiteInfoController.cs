using Kozmos.WebAPI.Data.Services;
using Kozmos.WebAPI.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kozmos.WebAPI.Controllers
{




    [Route("api/[controller]")]
    [ApiController]




    public class SiteInfoController : ControllerBase
    {

        public SiteInfoServices _siteInfoServices;
        public SiteInfoController(SiteInfoServices siteInfoServices)
        {
            _siteInfoServices = siteInfoServices;
        }



        [HttpGet]
        //[Authorize]
        public async Task<ActionResult> GetSiteInfoController()
        {
            var siteInfo = await _siteInfoServices.getSiteInfoService();

            return Ok(siteInfo);
        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateSiteInformation(SiteInfoVM vm)
        {
            await _siteInfoServices.UpdateSiteInformationService(vm);

            return Ok("Güncelleme başarıyla tamamlandı.");
        }
    }
}
