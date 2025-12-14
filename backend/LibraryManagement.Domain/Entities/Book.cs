using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int AuthorId { get; set; }
    public int GenreId { get; set; }


    public Author Author { get; set; } = default!;
    public Genre Genre { get; set; } = default!;
}
