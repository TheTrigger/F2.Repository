using Microsoft.EntityFrameworkCore.ChangeTracking;
using F2.Repository.Abstracts;
using F2.Repository.Demo.Models;
using System;

namespace F2.Repository.Demo.Repositories
{
    /// <summary>
    /// Repositories scope
    /// </summary>
    public class LibraryDbScope : DbContextScope<LibraryContext>
    {
        public LibraryDbScope(LibraryContext context, BookRepository bookRepository, AuthorRepository authorRepository) : base(context)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;

            context.ChangeTracker.Tracked += ChangeTracker_Tracked;
        }

        private void ChangeTracker_StateChanged(object sender, EntityStateChangedEventArgs e)
        {
            var student = e.Entry.Entity;
            // e.Entry.State
        }

        private void ChangeTracker_Tracked(object sender, EntityTrackedEventArgs e)
        {
            // create dto: state, context, entity ?
            var entity = e.Entry.Entity;
            //throw new NotImplementedException();
        }


        public BookRepository BookRepository { get; }
        public AuthorRepository AuthorRepository { get; }
    }
}