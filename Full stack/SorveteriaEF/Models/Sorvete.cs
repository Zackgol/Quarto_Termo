using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorveteriaEF.Models
{

    [Table("Sorvetes")]

    public class Sorvete

    {



        [Key]



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]



        public int Id { get; set; }



        [Required]



        [StringLength(100)]



        public string Nome { get; set; }

        public string Sabor { get; set; }

        public string Tamanho { get; set; }


        [Column(TypeName = "decimal")]


        public decimal Valor { get; set; }



    }

}
