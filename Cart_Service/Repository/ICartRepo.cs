using Cart_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_Service.Repository
{
    public interface ICartRepo
    {
        bool Add_Item_Into_Cart(int userId,MenuItem menuItem);
        bool Remove_Item_From_Cart(int userId,int menuItemId);
        List<MenuItem> Get_Cart_Of_User(int userId);
    }
}
