using GuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Repositories
{
    public interface IGuestbookRepository
    {
        bool AddGuestBook(GuestbookEntry guestbookEntry);
        List<GuestbookEntry> List();
    }
}
