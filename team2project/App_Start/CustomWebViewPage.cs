using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Business.Classes;
using Events.Business.Models;
using Events.NHibernateDataProvider;
using Events.NHibernateDataProvider.NHibernateCore;
using team2project.Models;

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
                list.Add(new SelectListItem() { Text = "Все", Value = "-1" });
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
                var cities = AutoMapper.Mapper.Map<List<CitiesViewModel>>(cityManager.GetList());
                var list = new List<SelectListItem>(cities.Count+1);
                //list.Add(new SelectListItem() { Text = "Все", Value = "" });
                foreach (var city in cities) {
                    list.Add(new SelectListItem() { Text = city.Name, Value = city.Name });
                }
                return list;
            }
        }

    }
}