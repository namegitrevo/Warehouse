using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.AdminPages;
using Warehouse.ApplicationData;

namespace Warehouse.AssistanceClass
{
    class ListStorage : ObservableCollection<Storage>
    {
        public ListStorage()
        {
            DbSet<Storage> storages = PageSupplies.DataEntities1.Storages;
            var queryStorage = from Id in storages select Id;
            foreach (Storage sto in queryStorage)
            {
                this.Add(sto);
            }
        }
    }
}
