using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaFullStackApp
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
    }
}