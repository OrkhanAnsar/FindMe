using FindMeMobileClient.Models;
using FindMeMobileClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMeMobileClient.Services {
    public class SubscribeService : ISubscribeService {
        private readonly List<Filter> subscribedFilters;

        public SubscribeService() {
            subscribedFilters = new List<Filter>();
        }

        public void AddSubscribedFilter() {
            subscribedFilters.Add(App.Filter);
        }

        /// <summary>
        /// Delete subscribed filter by key
        /// </summary>
        /// <param name="key">key of the object in dictionary with filters</param>
        /// <returns>returns 0 if process is successfull
        /// 1 if process is unsuccessfull</returns>
        public void DeleteSubscribedFilter(DateTime filterDate) {
            subscribedFilters.Remove(subscribedFilters.First(p => p.FilterDate == filterDate));
        }

        public List<Filter> GetSubscribedFilters() {
            return subscribedFilters;
        }
    }
}
