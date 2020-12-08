using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Menu_Service.Models;
using Menu_Service.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MenuController));
        private IMenuRepo _menuRepo;
        public MenuController(IMenuRepo menuRepo)
        {
            _menuRepo = menuRepo;
        }
        // GET: api/Menu
        [HttpGet("allitems")]
        public IActionResult Get_All_MenuItems()
        {
            _log4net.Info("Getting all menuitems");
            var res=_menuRepo.Get_All_Menu_Items();
            return Ok(res);
        }

        [HttpPost("edititem/{id}")]
        public IActionResult Edit_MenuItem(int id,MenuItem menuItem)
        {
            if (_menuRepo.Edit_MenuItem(id, menuItem))
            {
                _log4net.Info("Successfully edited menuitem with id " + id);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get_Menu_Item_By_Id(int id)
        {
            MenuItem menuItem = _menuRepo.Get_Item_By_Id(id);
            if ( menuItem!= null)
            {
                return Ok(menuItem);
            }
            return NotFound();
        }
    }
}
