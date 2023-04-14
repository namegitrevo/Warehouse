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
    class ListDocuments : ObservableCollection<Document>
    {
        public ListDocuments()
        {
            DbSet<Document> document = PageReceipt.DataEntities1.Documents;
            var queryDocument = from x in document where x.DocumentTypeId==1  select x;
            foreach (Document doc in queryDocument)
            {
                this.Add(doc);
            }
        } 
    }
}
