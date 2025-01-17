using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models;  // Importando o namespace correto

namespace Models {
    public class AnimeListModel {
        [Key]
        public int AnimeListId { get; set; }

        public int AnimeId { get; set; }

        public ICollection<UserListModel>? UserList { get; set; }
    }
}