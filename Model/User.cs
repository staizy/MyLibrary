using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    public class User
    {
        public User(int id, string? name, string? surname, List<Book> bookList)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BookList = bookList;            
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public List<Book> BookList { get; set; }
    }
}
