using Kozmos.WebAPI.Data.Services;
using Kozmos.WebAPI.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kozmos.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMessageController : ControllerBase
    {
        public CustomerMessageServices _customerMessageServices;
        public CustomerMessageController(CustomerMessageServices customerMessageServices)
        {
            _customerMessageServices = customerMessageServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CustomerMessageGetVM>>> GetAllCustomerMessagesController()
        {
            var list = await _customerMessageServices.GetAllCustomerMessagesService();
            return Ok(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateCustomerMessageController(CustomerMessageAddVM vm)
        {
            bool response = await _customerMessageServices.CreateUserMessage(vm);
            if (response)
            {
                return Ok("Mesajınız başarıyla oluşturuldu");
            }
            else
            {
                return BadRequest("Mesajınız oluşturulurken bir hata meydana geldi");
            };
            
        }

        [HttpPut("changeresolved/{id}")]
        [Authorize]
        public async Task<ActionResult> ChangeFeaturedController(int id)
        {
            bool result = await _customerMessageServices.ChangeIsResolvedService(id);
            if (result)
            {
                return Ok("Çözüme ulaşma durumu başarıyla değiştirildi");
            }
            return BadRequest("İşlem gerçekleştirilemedi");
        }

    }
}
