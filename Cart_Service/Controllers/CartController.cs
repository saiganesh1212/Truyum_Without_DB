using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart_Service.Models;
using Cart_Service.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart_Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CartController));
        private ICartRepo _cartRepo;
        public CartController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }
        [HttpGet("{userId}")]
        public IActionResult Get_User_Cart(int userId)
        {
            var menuList = _cartRepo.Get_Cart_Of_User(userId);
            if (menuList != null)
            {
                _log4net.Info("User with id " + userId + " successfully got cart details");
                return Ok(menuList);
            }
            return NotFound();
        }
        // GET: api/Cart
        [HttpPost("addtocart/{userId}")]
        public IActionResult Add_Into_Cart(int userId,MenuItem menuItem)
        {
            _cartRepo.Add_Item_Into_Cart(userId, menuItem);
            
             _log4net.Info("MenuItem with name " + menuItem.Name + " successfully added into cart of user " + userId);
             return Ok();
            
        }

        [HttpDelete("removeitem/{id}/{menuItemId}")]
        public IActionResult Delete_From_Cart(int id,int menuItemId)
        {
            if (_cartRepo.Remove_Item_From_Cart(id, menuItemId))
            {
                _log4net.Info("Menuitem item with id " + menuItemId + " successfully deleted");
                return Ok();
            }
            return NotFound();
        }
        
    }
}
