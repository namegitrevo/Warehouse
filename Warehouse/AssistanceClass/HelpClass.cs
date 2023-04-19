using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.AssistanceClass
{
    class HelpClass
    {
        public static int Id { get; set; }
        public static int UId { get; set; }
        public static int UnitId { get; set; }
        public static int SuppId { get; set; }
        public static int reqId { get; set; }
        public static int DocId { get; set; }//Для кнопок редаетирования
        public static int DocItId { get; set; }
        public static int RoleId { get; set; }
        public static string DocItemAddId { get; set; }
        public static int DocumentAddId { get; set; } //возможно стоит совместить с DocId
        public static int DocId2 { get; set; }//Для кнопок редактирования, костыль
        public static string DocItemAddId2 { get; set; }//костыль

    }
}
