using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopfy_Mobile.Models
{
    public class CreateCityModel
    {
        public string CityName { get; set; }
    }

    public class UpdateCityModel : CreateCityModel
    {

    }

    public class ViewByCityNameModel : CreateCityModel
    {

    }

    public class CityModel : CreateCityModel
    {
        public int Id { get; set; }

        public virtual IList<SalePriceModel> SalePrices { get; set; }
    }
}