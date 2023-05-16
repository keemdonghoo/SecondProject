using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Domain
{
    [Table Board]
    public class Board
    {
        public long BoardUID { get; set; } 

        public string Name { get; set; }    
    }
}
