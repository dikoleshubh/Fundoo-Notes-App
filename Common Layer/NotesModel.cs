using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Common_Layer
{
   public  class NotesModel
    {
        [Key]
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
       
        public string Color { get; set; }
        public string Image { get; set; }
        //public string Lable { get; set; }

        [ForeignKey("User")]
        public long ID { get; set; }
       
      //  public virtual User User { get; set; }

        [DefaultValue(false)]
        public bool Pin { get; set; }

        [DefaultValue(false)]
        public bool Archieve { get; set; }

        [DefaultValue(false)]
        public bool Is_Trash { get; set; }
    }
}
