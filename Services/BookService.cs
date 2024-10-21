using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreDB.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BookService(IOptions<BookStoreDBSettings> bookStoreDatabaseSettings) 
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetBook() => await _booksCollection.Find(_ => true).ToListAsync();
        public async Task<Book?> GetBook(string id) => await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync(); 
        public async Task CreateBook(Book newBook) => await _booksCollection.InsertOneAsync(newBook);
        public async Task UpdateBook(string id, Book updateBook) => await _booksCollection.ReplaceOneAsync(b => b.Id == id, updateBook); 
        public async Task DeleteBook(string id) => await _booksCollection.DeleteOneAsync(b => b.Id == id);
    }
}