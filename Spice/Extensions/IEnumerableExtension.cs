using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Spice.Extensions
{
    public static class IEnumerableExtension
    {

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, int selectedValue)
        {
            var result = from item in items
                         select new SelectListItem
                         {
                             Text = item.getPropertyValue("Name"),
                             Value = item.getPropertyValue("Id"),
                             Selected = item.getPropertyValue("Id").Equals(selectedValue.ToString())
                         };

            return result;
        }
    }
}
