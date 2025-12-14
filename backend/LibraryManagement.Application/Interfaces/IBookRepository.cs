using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
}
