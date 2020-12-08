using Menu_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menu_Service.Repository
{
    public interface IMenuRepo
    {
        List<MenuItem> Get_All_Menu_Items();
        bool Edit_MenuItem(int id,MenuItem menuItem);
        MenuItem Get_Item_By_Id(int id);
    }
}
