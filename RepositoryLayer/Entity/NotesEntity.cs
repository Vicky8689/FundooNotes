using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]    
        public string Color { get; set; } 
        [Required]
        public bool IsArchive { get; set; } = false;
        [Required]
        public bool IsTrash { get; set; } = false;

        [Required]
        public int UserId {  get; set; }
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }


    }
}
