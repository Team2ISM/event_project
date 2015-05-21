using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cities.Business.Classes;
using Cities.Business.Models;
using Cities.NHibernateDataProvider;

namespace team2project.App_Start
{
    public abstract class CustomWebViewPage : WebViewPage
    {
        CitiesManager cityManager;

        public CustomWebViewPage() {
            this.cityManager = new CitiesManager(new NHibernateCitiesDataProvider(), new Events.RuntimeCache.RuntimeCacheManager());
        }
        public List<SelectListItem> AvailableLocations {
            get {
                var cities = cityManager.GetList();
                var list = new List<SelectListItem>(cities.Count+1);
                list.Add(new SelectListItem() { Text = "Все", Value = "" });
                foreach(var city in cities){
                    list.Add(new SelectListItem(){ Text = city.Name, Value=city.Id.ToString()});
                }
                return list;
            }
        }
    }

    public abstract class CustomWebViewPage<T> : WebViewPage<T>
    {
        CitiesManager cityManager;
        public CustomWebViewPage() {
            this.cityManager = new CitiesManager(new NHibernateCitiesDataProvider(), new Events.RuntimeCache.RuntimeCacheManager());
        }
        public List<SelectListItem> AvailableLocations {
            get {
                var cities = cityManager.GetList();
                var list = new List<SelectListItem>(cities.Count+1);
                list.Add(new SelectListItem() { Text = "Все", Value = "" });
                foreach (var city in cities) {
                    list.Add(new SelectListItem() { Text = city.Name, Value = city.Id.ToString() });
                }
                return list;
            }
        }

    }
}