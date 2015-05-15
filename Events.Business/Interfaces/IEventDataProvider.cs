﻿using System.Collections.Generic;
using Events.Business.Models;

namespace Events.Business.Interfaces
{
    public interface IEventDataProvider
    {
        IList<EventModel> GetList();

        EventModel GetById(string id);

        int Create(EventModel evnt);

        //void Update(TModel model);

        //void Delete(TModel model);
    }
}
