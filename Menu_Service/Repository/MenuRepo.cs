using Menu_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menu_Service.Repository
{
    public class MenuRepo : IMenuRepo
    {
        private static List<MenuItem> menuItems = new List<MenuItem>() { new MenuItem() { Id=1,Name="Maggie",CategoryName="Snacks",Active=true,DateOfLaunch=new DateTime(2020,2,3),freeDelivery=false,Price=35 },
        new MenuItem() { Id=2,Name="Chicken Manchuria",CategoryName="Starters",Active=true,DateOfLaunch=new DateTime(2020,5,13),freeDelivery=true,Price=158 },
        new MenuItem() { Id=3,Name="Chicken Biryani",CategoryName="Biryani",Active=true,DateOfLaunch=new DateTime(2020,6,6),freeDelivery=true,Price=315 },
        new MenuItem() { Id=4,Name="Veg Manchuria",CategoryName="Starters",Active=true,DateOfLaunch=new DateTime(2020,7,9),freeDelivery=false,Price=100 }
        };

        public bool Edit_MenuItem(int menuId,MenuItem menuItem)
        {
            int index = menuItems.FindIndex(x => x.Id == menuId);
            if (index == -1)
            {
                return false;
            }
            menuItems[index].Name = menuItem.Name;
            menuItems[index].Price = menuItem.Price;
            menuItems[index].freeDelivery = menuItem.freeDelivery;
            menuItems[index].DateOfLaunch = menuItem.DateOfLaunch;
            menuItems[index].CategoryName = menuItem.CategoryName;
            menuItems[index].Active = menuItem.Active;
            
            return true;
        }

        public List<MenuItem> Get_All_Menu_Items()
        {
            return menuItems;
        }

        public MenuItem Get_Item_By_Id(int id)
        {
            int index = menuItems.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return null;
            }
            return menuItems[index];
        }
    }
}
