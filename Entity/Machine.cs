using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    
    public class ATMMachine:IEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50,ErrorMessage ="Lokasyon 50 karakterden fazla olamaz")]
        [Column(name:"Lokasyon")]

        public string Location { get; set; }
        [Column(name:"ToplamPara",TypeName ="smallint")]
        public short BirTL { get; set; }
        public short BesTL { get; set; }
        public short OnTL { get; set; }
        public short YirmiTL { get; set; }
        public short ElliTL { get; set; }
        public short YuzTL { get; set; }
        public short IkiyuzTL { get; set; }
      
        public int TotalMoney { get; set; }

    }
}
