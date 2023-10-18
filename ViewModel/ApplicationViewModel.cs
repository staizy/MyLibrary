using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using MyLibrary.Model;
using System.Windows;

namespace MyLibrary.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        private Book selectedBook;
        private Book selectedUserBook;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Book> Books { get; set; }


        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
                OnPropertyChanged("FilteredUserBooks");
            }
        }

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        public Book SelectedUserBook
        {
            get { return selectedUserBook; }
            set
            {
                selectedUserBook = value;
                OnPropertyChanged(nameof(SelectedUserBook));
            }
        }

        private RelayComand addCommand;
        public RelayComand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayComand(obj =>
                  {
                      if (selectedUser == null)
                      {
                          MessageBox.Show("Не выбран пользователь в меню слева, которому Вы собираетесь добавить выбранную книгу!", "Внимание!");
                      }
                      if (selectedBook == null)
                      {
                          MessageBox.Show("Не выбрана книга в меню справа, которую Вы собираетесь добавить выбранному пользователю!", "Внимание!");
                      }
                      else
                      {
                          if (selectedBook.Count == 0) MessageBox.Show("Эта книга закончилась на складе!", "Внимание!");
                          else
                          {                       
                              selectedUser.UserBooks.Insert(0, selectedBook);
                              selectedBook.Count--;
                          }
                      }
                  }));
            }
        }

        private RelayComand removeCommand;
        public RelayComand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayComand(obj =>
                  {
                      if (selectedUser == null)
                      {
                          MessageBox.Show("Не выбран пользователь в меню слева, у которого Вы собираетесь удалить выбранную книгу!", "Внимание!");
                      }
                      if (selectedUserBook == null)
                      {
                          MessageBox.Show("Не выбрана книга в меню посередине, которую Вы собираетесь удалить у выбранного пользователя!", "Внимание!");
                      }
                      else
                      {
                          selectedUserBook.Count++;
                          selectedUser.UserBooks.Remove(selectedUserBook);
                      }
                  }));
            }
        }

        public ObservableCollection<Book> FilteredBooks
        {
            get
            {
                if (searchBookText != null)
                {
                    return new ObservableCollection<Book>(Books.Where(x => x.Name.Contains(SearchBookText, StringComparison.OrdinalIgnoreCase) || 
                    x.Author.Contains(SearchBookText, StringComparison.OrdinalIgnoreCase) || Convert.ToString(x.Article).Contains(SearchBookText) ||
                    Convert.ToString(x.Age).Contains(SearchBookText)));
                }
                else
                {
                    return Books;
                }
            }
        }

        private string searchBookText;
        public string SearchBookText
        {
            get { return searchBookText; }
            set
            {
                searchBookText = value;
                OnPropertyChanged("SearchBookText");
                OnPropertyChanged("FilteredBooks");
            }
        }

        public ObservableCollection<Book> FilteredUserBooks
        {
            get
            {
                if (searchUserBookText != null && selectedUser != null)
                {
                    return new ObservableCollection<Book>(SelectedUser.UserBooks.Where(x => x.Name.Contains(SearchUserBookText, StringComparison.OrdinalIgnoreCase) ||
                    x.Author.Contains(SearchUserBookText, StringComparison.OrdinalIgnoreCase)
                    || Convert.ToString(x.Article).Contains(SearchUserBookText) || Convert.ToString(x.Age).Contains(SearchUserBookText)));
                }

                if (selectedUser != null)
                {
                    return SelectedUser.UserBooks;
                }
                else return new ObservableCollection<Book>();
            }
        }

        private string searchUserBookText;
        public string SearchUserBookText
        {
            get { return searchUserBookText; }
            set
            {
                searchUserBookText = value;
                OnPropertyChanged("SearchUserBookText");
                OnPropertyChanged("FilteredUserBooks");
            }
        }

        public ObservableCollection<User> FilteredUsers
        {
            get
            {
                if (searchUserText != null)
                {
                    return new ObservableCollection<User>(Users.Where(x => x.Name.Contains(SearchUserText, StringComparison.OrdinalIgnoreCase) ||
                    x.Surname.Contains(SearchUserText, StringComparison.OrdinalIgnoreCase) || Convert.ToString(x.Id).Contains(SearchUserText)));
                }
                else
                {
                    return Users;
                }
            }
        }

        private string searchUserText;
        public string SearchUserText
        {
            get { return searchUserText; }
            set
            {
                searchUserText = value;
                OnPropertyChanged("SearchUserText");
                OnPropertyChanged("FilteredUsers");
            }
        }

        public ApplicationViewModel()
        {
            Books = new ObservableCollection<Book>
            {
                new Book { Article = 3431, Author = "Александр Пушкин", Name = "Евгений Онегин", Age = 1921, Count = 7 },
                new Book { Article = 9352, Author = "Николай Гоголь", Name = "Мастер и Маргарита", Age = 1913, Count = 11 },
                new Book { Article = 2243, Author = "Идущий к реке", Name = "Записи из черновика", Age = 2021, Count = 5 },
                new Book { Article = 1044, Author = "Андрей Писатель", Name = "Добрый Вечер", Age = 2023, Count = 3 },
                new Book { Article = 5699, Author = "Маргарет Этвуд", Name = "Рассказ служанки", Age = 1985, Count = 9 },
                new Book { Article = 9021, Author = "Джордж Оруэлл", Name = "1984", Age = 1949, Count = 10 },
                new Book { Article = 3457, Author = "Эрих Мария Ремарк", Name = "Три товарища", Age = 1936, Count = 8 },
                new Book { Article = 7890, Author = "Федор Достоевский", Name = "Преступление и наказание", Age = 1866, Count = 12 },
                new Book { Article = 5678, Author = "Маргарет Этвуд", Name = "Основание", Age = 2020, Count = 8 },
                new Book { Article = 9022, Author = "Джордж Оруэлл", Name = "Скотный двор", Age = 1945, Count = 9 },
                new Book { Article = 3450, Author = "Эрих Мария Ремарк", Name = "Триумфальная арка", Age = 1945, Count = 7 },
                new Book { Article = 7891, Author = "Федор Достоевский", Name = "Идиот", Age = 1869, Count = 11 },
                new Book { Article = 3432, Author = "Александр Пушкин", Name = "Капитанская дочка", Age = 1836, Count = 10 },
                new Book { Article = 1358, Author = "Николай Гоголь", Name = "Ревизор", Age = 1836, Count = 8 }
            };

            Users = new ObservableCollection<User>
            {
                new User {Id = 0, Name = "Дмитрий", Surname = "Кризо", UserBooks = { Books[0], Books[1], Books[4], Books[5] } },
                new User {Id = 1, Name = "Саша", Surname = "Белый", UserBooks = { Books[2], Books[3] } },
                new User {Id = 2, Name = "Володя", Surname = "Черный", UserBooks = { Books[6], Books[5], Books[4], Books[3], } },
                new User {Id = 3, Name = "Андрей", Surname = "Чикатило", UserBooks = { } },
                new User {Id = 4, Name = "Кирилл", Surname = "Ллирик", UserBooks = { Books[8], Books[9] } },
                new User {Id = 5, Name = "Юрий", Surname = "Дудь", UserBooks = { Books[5], Books[6], Books[7], Books[8], Books[9], Books[10], } },
                new User {Id = 6, Name = "Юлий", Surname = "Цезарь", UserBooks = { Books[0] } },
                new User {Id = 7, Name = "Дмитрий", Surname = "Цезарь", UserBooks = { } }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
