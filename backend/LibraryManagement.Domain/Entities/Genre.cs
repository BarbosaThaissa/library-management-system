using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    
    
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
