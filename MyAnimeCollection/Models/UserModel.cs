using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models;  // Importando o namespace correto


namespace Models {
    public class UserModel {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(300)]
        public string Biography { get; set; }

        public int Age { get; set; }

        public string ImageUrl { get; set; } 

        public ICollection<UserListModel>? UserList { get; set; }

        public ICollection<UserListAvaliationModel>? UserListAvaliation { get; set; }

        public ICollection<UserAnimeAvaliationModel>? UserAnimeAvaliation { get; set; }
    }
}