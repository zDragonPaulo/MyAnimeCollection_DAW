using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models;  // Importando o namespace correto

namespace Models {
    public class UserListAvaliationModel {

        [Key]
        public int UserListAvaliationId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public UserModel? User { get; set; }

        [ForeignKey("UserListId")]
        public int UserListId { get; set; }

        public UserListModel? UserList { get; set; }

        [Range(0, 10)]
        public float Avaliation { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow; // Adicionando a data de criação
    }
}