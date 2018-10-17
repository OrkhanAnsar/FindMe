using FindMePrism.Models;
using Prism.Events;
using System.Collections.Generic;

namespace FindMePrism.Events
{
    public class InstTypesEvent : PubSubEvent<IEnumerable<InstitutionType>> { }
}
