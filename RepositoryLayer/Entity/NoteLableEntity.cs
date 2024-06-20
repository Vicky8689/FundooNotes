using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NoteLableEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteLabelId { get; set; }

        public int LabelID { get; set; }
        [ForeignKey(nameof(LabelID))]
        public LabelEntity Label { get; set; }

        public int NoteId { get; set; }
        [ForeignKey(nameof(NoteId))]
        public NotesEntity Notes { get; set; }
    }
}
