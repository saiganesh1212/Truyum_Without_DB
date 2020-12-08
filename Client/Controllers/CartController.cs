using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class CartController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CartController));
        // GET: Cart
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login", "Login");

            }
            else
            {
                _log4net.Info("Getting cart of user " + HttpContext.Session.GetString("Username"));
                List<MenuItem> menuItems = new List<MenuItem>();
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response1 = await client.GetAsync("https://localhost:44315/api/cart/" + HttpContext.Session.GetString("id")))
                    {
                        if (!response1.IsSuccessStatusCode)
                        {
                            _log4net.Info("User with id " + HttpContext.Session.GetString("id") + " has empty cart");
                            ViewBag.message = "Cart Empty";
                            return View();
                        }

                        var apiResponse = await response1.Content.ReadAsStringAsync();
                        menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(apiResponse);
                        _log4net.Info("User has " + menuItems.Count + " items in his cart");
                    }
                }
                return View(menuItems);
            }
        }

        // GET: Cart/Details/5

        // POST: Cart/Create
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
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

                    using (var response = await client.GetAsync("https://localhost:44368/api/menu/" + id.ToString()))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            _log4net.Info("Menuitem with id " + id + " does not exist");
                            return RedirectToAction("Index", "Menu");
                        }
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        menuItem = JsonConvert.DeserializeObject<MenuItem>(apiResponse);
                    }

                    StringContent content = new StringContent(JsonConvert.SerializeObject(menuItem), Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    using (var response1 = await client.PostAsync("https://localhost:44315/api/cart/addtocart/" + HttpContext.Session.GetString("id"), content))
                    {
                        if (!response1.IsSuccessStatusCode)
                        {
                            _log4net.Info("Error in adding " + menuItem.Name + " to cart");

                        }
                        else
                        {
                            _log4net.Info("Successfully added " + menuItem.Name + " to cart of user " + HttpContext.Session.GetString("Username"));
                        }
                        
                    }

                }
                return RedirectToAction("Cart", "Cart");
            }
        }



        // POST: Cart/Delete/5
        [HttpPost("delete/{menuItemId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFromCart(int menuItemId)
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
                    StringContent content = new StringContent(JsonConvert.SerializeObject(menuItemId), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    using (var response1 = await client.DeleteAsync("https://localhost:44315/api/cart/removeitem/" + HttpContext.Session.GetString("id") + "/" + menuItemId.ToString()))
                    {
                        if (!response1.IsSuccessStatusCode)
                        {
                            _log4net.Info("Error in deleting menuItem with id " + menuItemId);
                            
                        }
                        else
                        {
                            _log4net.Info("Successfully deleted menuItem with id " + menuItemId.ToString() + " to cart of user " + HttpContext.Session.GetString("Username"));
                        }
                        
                    }
                }
                return RedirectToAction("Cart", "Cart");
            }

        }

        
    }
}