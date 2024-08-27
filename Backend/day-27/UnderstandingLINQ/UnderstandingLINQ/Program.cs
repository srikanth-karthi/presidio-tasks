using System;
using System.Linq;
using UnderstandingLINQ.Models;

namespace UnderstandingLINQ
{
    internal class Program
    {
        void PrintTheBooksPublisherwise()
        {
            using (PubsContext context = new PubsContext())
            {
                var books = context.Titles
                            .GroupBy(t => t.PubId)
                            .Select(t => new { Key = t.Key, TitleCount = t.Count() });

                foreach (var book in books)
                {
                    Console.Write(book.Key);
                    Console.WriteLine(" - " + book.TitleCount);
                }
            }
        }

        void PrintTheBooksPublisherwiseDetailed()
        {
            using (PubsContext context = new PubsContext())
            {
                var books = context.Titles
                            .GroupBy(t => t.PubId)
                            .Select(t => new
                            {
                                PublisherId = t.Key,
                                TitleCount = t.Count(),
                                Titles = t.Select(title => new
                                {
                                    BookName = title.Title1,
                                    BookPrice = title.Price
                                })
                            });

                foreach (var book in books)
                {
                    Console.Write(book.PublisherId);
                    Console.WriteLine(" - " + book.TitleCount);
                    foreach (var title in book.Titles)
                    {
                        Console.WriteLine("\t" + title.BookName + " " + title.BookPrice);
                    }
                }
            }
        }

        void PrintNumberOfBooksFromType(string type)
        {
            using (PubsContext context = new PubsContext())
            {
                var bookCount = context.Titles.Where(t => t.Type == type).Count();
                Console.WriteLine($"There are {bookCount} books in the type {type}");
            }
        }

        void PrintAuthorNames()
        {
            using (PubsContext context = new PubsContext())
            {
                var authors = context.Authors.ToList();
                foreach (var author in authors)
                {
                    Console.WriteLine(author.AuFname + " " + author.AuLname);
                }
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.PrintAuthorNames();
            //program.PrintNumberOfBooksFromType("mod_cook");
            program.PrintTheBooksPublisherwise();
            //program.PrintTheBooksPublisherwiseDetailed();
        }
    }
}
