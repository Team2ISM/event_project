using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using Foolproof;
using Events.Business.Models;
namespace team2project.Models
{
    public class EventListViewModel
    {
        public EventListViewModel(List<EventViewModel> list, string location = null)
        {
            this.EventsList = list;
            this.Location = location;
        }

        public List<EventViewModel> EventsList
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

    }
}