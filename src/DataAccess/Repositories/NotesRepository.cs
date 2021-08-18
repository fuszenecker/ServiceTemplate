using System;
using System.Collections.Generic;
using DigitalThinkers.DataAccess.Contexts;
using DigitalThinkers.DataAccess.Entities;
using DigitalThinkers.Domain.Interfaces;

namespace DigitalThinkers.DataAccess.Repositories
{
    public class NotesRepository : INotesRepository, IDisposable
    {
        private NotesContext context = new();

        public NotesRepository()
        {
            this.context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            if (context is not null)
            {
                context.Dispose();
                context = null;
            }
        }

        public IDictionary<uint, uint> GetNotes()
        {
            var result = new Dictionary<uint, uint>();

            foreach (var note in context.Notes)
            {
                result[note.Denominator] = note.Count;
            }

            return result;
        }

        public void StoreNotes(IDictionary<uint, uint> notes)
        {
            // Delete all existing items, and add the new items,
            foreach (var item in context.Notes)
            {
                context.Notes.Remove(item);
            }

            foreach (var note in notes)
            {
                context.Notes.Add(new Note()
                {
                    Denominator = note.Key,
                    Count = note.Value
                });
            }

            context.SaveChanges();
        }

        public void Transaction(Action action)
        {
            using var transaction = context.Database.BeginTransaction();
            action();
            transaction.Commit();
        }
    }
}