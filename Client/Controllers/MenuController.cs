using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{

    public class MenuController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MenuController));
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login", "Login");

            }
            else
            {
                _log4net.Info(HttpContext.Session.GetString("Username") + " requested all menuitems");
                List<MenuItem> menuItems = new List<MenuItem>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44368/api/menu/allitems"))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(apiResponse);
                    }
                }
                _log4net.Info(HttpContext.Session.GetString("Username") + " Successfully got all menuitems");
                return View(menuItems);
            }

        }

        [HttpGet("edit/{menuId}")]
        public async Task<IActionResult> EditMenuItem(int menuId)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login", "Login");

            }
            else
            {
                MenuItem menuItem;
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    using (var response1 = await client.GetAsync("https://localhost:44368/api/menu/" + menuId))
                    {
                        if (!response1.IsSuccessStatusCode)
                        {
                            _log4net.Info("Error in getting menuitem with id " + menuId);
                            return RedirectToAction("Index", "Menu");
                        }
                        var apiResponse = await response1.Content.ReadAsStringAsync();
                        menuItem = JsonConvert.DeserializeObject<MenuItem>(apiResponse);
                        _log4net.Info("Successfully loaded menuitem " + menuItem.Name + " to edit ");
                        return View(menuItem);
                    }

                }
            }
        }
        [HttpPost("edit/{menuId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMenuItem(int menuId, MenuItem menuItem)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login", "Login");

            }
            else
            {

                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(menuItem), Encoding.UTF8, "application/json");
                    using (var response1 = await client.PostAsync("https://localhost:44368/api/menu/edititem/" + menuId, content))
                    {
                        if (!response1.IsSuccessStatusCode)
                        {
                            _log4net.Info("Error in editing menuitem with id " + menuId);

                        }
                        else
                        {
                            _log4net.Info("Successfully edited menuitem with name " + menuItem.Name);
                        }

                        return RedirectToAction("Index", "Menu");

                    }
                }
            }
        }
    }
}