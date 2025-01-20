using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models;  // Importando o namespace correto

namespace Models {
    public class UserListModel {
        [Key]
        public int UserListId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public UserModel? User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        // Lista de IDs de Animes
        public List<int> AnimeIds { get; set; } = new List<int>();
    }
}