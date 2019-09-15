using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace English.Data
{
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MySqlCharset("utf8")]
        public string title { get; set; }

        public string english_text { get; set; }

        [MySqlCharset("utf8")]
        public string vietnamese_text { get; set; }

        public string keywords { get; set; }

        public DateTime date_created { get; set; }

        public DateTime date_modified { get; set; }

        public int viewed { get; set; }

        public int deleted { get; set; }
    }
}
