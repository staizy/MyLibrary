using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    public class Book
    {
        public Book(short article, string author, string name, int age, int count)
        {
            Article = article;
            Author = author;
            Name = name;
            Age = age;
            Count = count;
        }

        public short Article { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Count { get; set; }
    }
}
