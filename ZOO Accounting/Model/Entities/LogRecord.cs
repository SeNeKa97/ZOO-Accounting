using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_Accounting.Model.Entities
{
    [Table("Log")]
    public class LogRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string Text { get;set; }

        public LogRecord() { }
        public LogRecord(DateTime time, string text) {
            Time = time;
            Text = text;
        }
    }
}
