using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDatabase.Models
{
    public class DBComboLeg
    {
        public static int SAME = 0;
        public static int OPEN = 1;
        public static int CLOSE = 2;
        public static int UNKNOWN = 3;

        [Key]
        public int Id { get; set; }
        [Required]
        public int ConId { get; set;}

        public int Ratio { get; set;}
        [MaxLength(10)]
        public string Action { get; set;}
        [MaxLength(10)]
        public string Exchange { get; set;}
        public int OpenClose { get; set;}
        public int ShortSaleSlot { get; set;}
        [MaxLength(50)]
        public string DesignatedLocation { get; set;}
        public int ExemptCode { get; set;}
        public DBContract DbContract { get; set; }
    }
}
