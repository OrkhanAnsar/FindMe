using FindMeMobileClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindMeMobileClient.Services.Interfaces {
    public interface ISubscribeService {
        List<Filter> GetSubscribedFilters();
        void AddSubscribedFilter();
        void DeleteSubscribedFilter(DateTime filterDate);
    }
}
