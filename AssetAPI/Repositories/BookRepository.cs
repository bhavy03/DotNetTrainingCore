using AssetAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetAPI.Repositories
{
    public class BookRepository : IAssetRepository<Book>
    {
        private readonly AssetContext _context;

        public BookRepository(AssetContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book? Search(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            if (book != null) return book;
            else return null;

        }

        public Book Add(Book asset)
        {
            _context.Books.Add(asset);
            _context.SaveChanges();
            return asset;
        }

        public bool Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public Book? Update(int id, Book asset)
        {
            var existing = _context.Books.Find(id);
            if (existing == null) return null;

            existing.Name = asset.Name;
            existing.Author = asset.Author;
            existing.PublishDate = asset.PublishDate;
            existing.QuantityAvailable = asset.QuantityAvailable;

            _context.SaveChanges();
            return existing;
        }
    }
}