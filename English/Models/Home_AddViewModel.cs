using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace English.Models
{
    public class Home_AddViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string title { get; set; }

        public string english_text { get; set; }

        public string vietnamese_text { get; set; }

        public string keywords { get; set; }

        public DateTime date_created { get; set; }

        public DateTime date_modified { get; set; }

        public int deleted { get; set; }
    }
}
