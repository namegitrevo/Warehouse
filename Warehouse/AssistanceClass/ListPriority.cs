using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.AdminPages;
using Warehouse.ApplicationData;
using Warehouse.MainPages;

namespace Warehouse.AssistanceClass
{
    class ListPriority : ObservableCollection<Priority>
    {
        public ListPriority()
        {
            DbSet<Priority> priority = PageRequests.DataEntities1.Priorities;
            var queryPriority = from Id in priority select Id;
            foreach (Priority pri in queryPriority)
            {
                this.Add(pri);
            }
        }
    }
}
