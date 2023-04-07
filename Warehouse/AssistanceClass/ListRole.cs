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
    class ListRole : ObservableCollection<Role>
    {
        public ListRole()
        {
            DbSet<Role> roles = PageEmployees.DataEntities1.Roles;
            var queryRoles = from Id in roles select Id;
            foreach (Role rol in queryRoles)
            {
                this.Add(rol);
            }
        }
    }
}
