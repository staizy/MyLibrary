using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyLibrary.Model;

namespace MyLibrary
{
    public partial class MainWindow : Window
    {

        public List<User> Users = new List<User>()
        {
            new User(0, "Дмитрий", "Кризо", 
                new List<Book>()
                {
                new Book(3431, "Александр Пушкин", "Евгений Онегин", 1921, 7),
                new Book(9352, "Николай Гоголь", "Мастер и Маргарита", 1913, 11),
                }
                ),
            new User(1, "Саня", "Ржавый",
                new List<Book>()
                {
                new Book(3431, "Александр Пушкин", "Евгений Онегин", 1921, 7)
                }
                ),
            new User(2, "Лёха", "Белый", new List<Book>()),
            new User(3, "Володя", "Черный", new List<Book>()),
            new User(4, "Андрей", "Чикатило", new List<Book>()
            {
                new Book(1044, "Андрей Писатель", "Добрый Вечер", 2023, 3),
            new Book(5699, "Маргарет Этвуд", "Рассказ служанки", 1985, 9),
            new Book(9021, "Джордж Оруэлл", "1984", 1949, 10),
            new Book(3457, "Эрих Мария Ремарк", "Три товарища", 1936, 8),
            new Book(7890, "Федор Достоевский", "Преступление и наказание", 1866, 12),
            new Book(5678, "Маргарет Этвуд", "Основание", 2020, 8),
            new Book(9022, "Джордж Оруэлл", "Скотный двор", 1945, 9),
            new Book(3450, "Эрих Мария Ремарк", "Триумфальная арка", 1945, 7),
            new Book(7891, "Федор Достоевский", "Идиот", 1869, 11)
            }),
            new User(5, "Кирилл", "Ллирик", new List<Book>()),
            new User(6, "Юрий", "Дудь", new List<Book>()
            {
                new Book(7890, "Федор Достоевский", "Преступление и наказание", 1866, 12),
                new Book(5678, "Маргарет Этвуд", "Основание", 2020, 8),
                new Book(9022, "Джордж Оруэлл", "Скотный двор", 1945, 9)
            }),
            new User(7, "Юлий", "Цезарь", new List<Book>()
            {
                new Book(3432, "Александр Пушкин", "Капитанская дочка", 1836, 10),
                new Book(1358, "Николай Гоголь", "Ревизор", 1836, 8)
            })
        };

        public List<Book> Books = new List<Book>()
        {
            new Book(3431, "Александр Пушкин", "Евгений Онегин", 1921, 7),
            new Book(9352, "Николай Гоголь", "Мастер и Маргарита", 1913, 11),
            new Book(2243, "Идущий к реке", "Записи из черновика", 2021, 5),
            new Book(1044, "Андрей Писатель", "Добрый Вечер", 2023, 3),
            new Book(5699, "Маргарет Этвуд", "Рассказ служанки", 1985, 9),
            new Book(9021, "Джордж Оруэлл", "1984", 1949, 10),
            new Book(3457, "Эрих Мария Ремарк", "Три товарища", 1936, 8),
            new Book(7890, "Федор Достоевский", "Преступление и наказание", 1866, 12),
            new Book(5678, "Маргарет Этвуд", "Основание", 2020, 8),
            new Book(9022, "Джордж Оруэлл", "Скотный двор", 1945, 9),
            new Book(3450, "Эрих Мария Ремарк", "Триумфальная арка", 1945, 7),
            new Book(7891, "Федор Достоевский", "Идиот", 1869, 11),
            new Book(3432, "Александр Пушкин", "Капитанская дочка", 1836, 10),
            new Book(1358, "Николай Гоголь", "Ревизор", 1836, 8)
        };

        public MainWindow()
        {
            InitializeComponent();
            UserNameList.ItemsSource = Users;
            AllBooksList.ItemsSource = Books;
        }

        private void UserNameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var books = new List<Book>();
            User user = UserNameList.SelectedItem as User;
            if (user != null)
            {
                foreach (var i in user.BookList)
                    foreach (var book in Books)
                        if (book.Article == i.Article)
                            books.Add(book);
            }
            UserBooksList.ItemsSource = books;
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            User CurrentUser = UserNameList.SelectedItem as User;
            Book CurrentBook = AllBooksList.SelectedItem as Book;
            if (CurrentBook == null) MessageBox.Show("Выберите книгу!");
            if (CurrentUser == null) MessageBox.Show("Выберите пользователя!");
            if (CurrentUser != null && CurrentBook != null)
            {
                if (CurrentBook.Count > 0)
                {
                    CurrentUser.BookList.Add(CurrentBook);
                    CurrentBook.Count--;
                }
                else
                {
                    MessageBox.Show("Эта книга закончилась на складе!");
                }
                var books = new List<Book>();
                if (CurrentUser != null)
                {
                    foreach (var i in CurrentUser.BookList)
                        foreach (var book in Books)
                            if (book.Article == i.Article)
                                books.Add(book);
                }
                UserBooksList.ItemsSource = books;
                AllBooksList.Items.Refresh();
            }
        }

        private void FindUser_Click(object sender, RoutedEventArgs e)
        {
            var newusers = from u in Users
                           where ((u.Name.Contains(NameSearch.Text)) || string.IsNullOrEmpty(NameSearch.Text)) &&
                                 ((u.Surname.Contains(SurnameSearch.Text)) || string.IsNullOrEmpty(SurnameSearch.Text)) &&
                                 ((Convert.ToString(u.Id) == IdSearch.Text) || string.IsNullOrEmpty(IdSearch.Text))
                           select u;
            UserNameList.ItemsSource = newusers;
            UserNameList.Items.Refresh();
        }

        private void FindUserBooks_Button_Click(object sender, RoutedEventArgs e)
        {
            User CurrentUser = UserNameList.SelectedItem as User;
            var books = new List<Book>();
            if (CurrentUser != null)
            {
                foreach (var i in CurrentUser.BookList)
                    foreach (var book in Books)
                        if (book.Article == i.Article)
                            books.Add(book);
            }
            var userbooks = from u in books
                            where ((Convert.ToString(u.Article).Contains(UserBookArticleSearch.Text)) || string.IsNullOrEmpty(UserBookArticleSearch.Text)) &&
                                  ((u.Author.Contains(UserBookAuthorSearch.Text)) || string.IsNullOrEmpty(UserBookAuthorSearch.Text)) &&
                                  ((u.Name.Contains(UserBookNameSearch.Text)) || string.IsNullOrEmpty(UserBookNameSearch.Text)) &&
                                  ((Convert.ToString(u.Age) == UserBookAgeSearch.Text) || string.IsNullOrEmpty(UserBookAgeSearch.Text))
                            select u;
            UserBooksList.ItemsSource = userbooks;
            UserBooksList.Items.Refresh();
        }

        private void FindBook_Button_Click(object sender, RoutedEventArgs e)
        {
            var allbooks = from u in Books
                            where ((Convert.ToString(u.Article).Contains(BookArticleSearch.Text)) || string.IsNullOrEmpty(BookArticleSearch.Text)) &&
                                  ((u.Author.Contains(BookAuthorSearch.Text)) || string.IsNullOrEmpty(BookAuthorSearch.Text)) &&
                                  ((u.Name.Contains(BookNameSearch.Text)) || string.IsNullOrEmpty(BookNameSearch.Text)) &&
                                  ((Convert.ToString(u.Age) == BookAgeSearch.Text) || string.IsNullOrEmpty(BookAgeSearch.Text))
                            select u;
            AllBooksList.ItemsSource = allbooks;
            AllBooksList.Items.Refresh();
        }
    }
}
