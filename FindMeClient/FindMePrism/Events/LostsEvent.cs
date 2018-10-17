using FindMePrism.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMePrism.Events
{
    public class LostsEvent : PubSubEvent<IEnumerable<Lost>> {}
}
