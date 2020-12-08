using Cart_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_Service.Repository
{
    public class CartRepo : ICartRepo
    {
        private static Dictionary<int, List<MenuItem>> _cart = new Dictionary<int, List<MenuItem>>()
        {
            {1, new List<MenuItem>(){ new MenuItem() { Id=1,Name="Maggie",CategoryName="Snacks",Active=true,DateOfLaunch=new DateTime(2020,2,3),freeDelivery=false,Price=35 },
        new MenuItem() { Id=2,Name="Chicken Manchuria",CategoryName="Starters",Active=true,DateOfLaunch=new DateTime(2020,5,13),freeDelivery=true,Price=158 }}
            },
            {
                2,new List<MenuItem>()
                {
                    new MenuItem() { Id=2,Name="Chicken Manchuria",CategoryName="Starters",Active=true,DateOfLaunch=new DateTime(2020,5,13),freeDelivery=true,Price=158 },
        new MenuItem() { Id=3,Name="Chicken Biryani",CategoryName="Biryani",Active=true,DateOfLaunch=new DateTime(2020,6,6),freeDelivery=true,Price=315 },
        new MenuItem() { Id=4,Name="Veg Manchuria",CategoryName="Starters",Active=true,DateOfLaunch=new DateTime(2020,7,9),freeDelivery=false,Price=100 }
                }
            },
            {
                3,new List<MenuItem>(){ new MenuItem() { Id = 3, Name = "Chicken Biryani", CategoryName = "Biryani", Active = true, DateOfLaunch = new DateTime(2020, 6, 6), freeDelivery = true, Price = 315 } }
            }
            
        };
        public bool Add_Item_Into_Cart(int userId,MenuItem menuItem)
        {
            if (_cart.ContainsKey(userId))
            {
                _cart[userId].Add(menuItem);
            }
            else
            {
                List<MenuItem> menus = new List<MenuItem>() { menuItem };
                _cart.Add(userId, menus);
            }
            
            return true;
        }

        public List<MenuItem> Get_Cart_Of_User(int userId)
        {
            if (_cart.ContainsKey(userId))
            {
                return _cart[userId];
            }
            else
            {
                return null;
            }
        }

        public bool Remove_Item_From_Cart(int userId,int menuItemId)
        {
            if (!_cart.ContainsKey(userId))
            {
                return false;
            }
            int index = _cart[userId].FindIndex(x => x.Id == menuItemId);
            if (index == -1)
            {
                return false;
            }
            _cart[userId].RemoveAt(index);
            return true;
        }
    }
}
