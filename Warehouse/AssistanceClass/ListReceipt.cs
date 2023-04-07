using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.ApplicationData;
using Warehouse.MainPages;

namespace Warehouse.AssistanceClass
{
    class ListReceipt : ObservableCollection<Receipt>
    {
        public ListReceipt()
        {
            DbSet<Receipt> receipt = PageReceipt.DataEntities1.Receipts;
            var queryReceipt = from Id in receipt select Id;
            foreach (Receipt rol in queryReceipt)
            {
                this.Add(rol);
            }
        }
    }
}
