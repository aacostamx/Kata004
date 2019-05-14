using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Exam : IEntity
    {
        [Column("ExamID")]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Exam Date")]
        public DateTime Date { get; set; }
    }
}
