using GuestBook.Data;
using GuestBook.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuestBook.Repositories
{
    public class GuestbookRepository : IGuestbookRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GuestbookRepository> _logger;
        private readonly IConfiguration _configuration;
        public GuestbookRepository(AppDbContext context,ILogger<GuestbookRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public bool AddGuestBook(GuestbookEntry guestbookEntry)
        {
            try
            {
                guestbookEntry.DateAdded = DateTime.Now;
                _context.Add(guestbookEntry);
                _context.SaveChanges();
                SaveToFile();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GuestbookRepository Add");
                return false;
            }
        }
        public List<GuestbookEntry> List()
        {
            try
            {
                System.Threading.Thread.Sleep(20000);
                var res = _context.GuestbookEntries.ToList();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GuestbookRepository List");
                return new List<GuestbookEntry>();
            }
        }
        private void SaveToFile()
        {
            string path = _configuration.GetSection("Paths").GetSection("FilePath").Value;
            var list = JsonSerializer.Serialize(List());
            File.WriteAllText(path, list);
        }
    }
    
}
