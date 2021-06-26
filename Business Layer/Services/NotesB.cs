using Business_Layer.Interfaces;
using Common_Layer;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    /// </summary>
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// note field of type INotes
        /// </summary>
        private  INotes notesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager" /> class.
        /// </summary>
        /// <param name="notes">notes parameter of type INotes</param>
        public NotesManager(INotes notes)
        {
            this.notesRL = notes;
        }

        /// <summary>
        /// Method to Call AddNewNote() method to create new note
        /// </summary>
        /// <param name="note">note parameter</param>
        /// <returns>boolean result</returns>
        public NotesModel AddNewNote(NotesModel note, long ids)
        {
            try
            {
                this.notesRL.AddNewNote(note,ids);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public bool RemoveNote(int id)
        {
            try
            {
                bool note = this.notesRL.RemoveNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Call UpdateNote() method to update a note 
        /// </summary>
        /// <param name="note">note id</param>
        /// <returns>boolean result</returns>
        public bool UpdateNote(NotesModel note)
        {
            try
            {
                bool result = this.notesRL.UpdateNote(note);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Call GetNoteById() method to remove a note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string note message</returns>
        public IEnumerable<NotesModel> GetNoteById(int id)
        {
            try
            {
                IEnumerable<NotesModel> note = this.notesRL.GetNoteById(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Call PinOrUnpinNote() method to Pin Or Unpin a Note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string note message</returns>
        public string PinOrUnpinNote(int id)
        {
            try
            {
                var note = this.notesRL.PinOrUnpinNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Call PinOrUnpinNote() method to Pin Or Unpin a Note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string note message</returns>
        public string ArchiveOrUnArchiveNote(int id)
        {
            try
            {
                var note = this.notesRL.ArchiveOrUnArchiveNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to get all archived notes
        /// </summary>
        /// <returns>archived notes</returns>
        public IEnumerable<NotesModel> GetAllArchivedNotes()
        {
            try
            {
                var note = this.notesRL.GetAllArchivedNotes();
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Trash or Restore a note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string TrashOrRestoreNote(int id)
        {
            try
            {
                var note = this.notesRL.TrashOrRestoreNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method declaration to get all notes which are in trash
        /// </summary>
        /// <returns>all notes from trash</returns>
        public IEnumerable<NotesModel> GetAllNotesaFromTrash()
        {
            try
            {
                var note = this.notesRL.GetAllNotesaFromTrash();
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method calls SetReminder() method to set reminder for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="reminder">reminder parameter for note</param>
        /// <returns>boolean result</returns>
        public bool SetReminder(int id, string reminder)
        {
            try
            {
                bool result = this.notesRL.SetReminder(id, reminder);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to get all notes for which reminder has set
        /// </summary>
        /// <returns>notes for which reminder has set</returns>
        public IEnumerable<NotesModel> GetAllNotesWhosReminderIsSet()
        {
            try
            {
                IEnumerable<NotesModel> notes = this.notesRL.GetAllNotesWhosReminderIsSet();
                return notes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to unset reminder for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>boolean result</returns>
        public bool UnSetReminder(int id)
        {
            try
            {
                bool result = this.notesRL.UnSetReminder(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to add color for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="image">selected image</param>
        /// <returns>boolean result</returns>
        public bool ChangeColor(int id, string color)
        {
            try
            {
                bool result = this.notesRL.ChangeColor(id, color);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to add image for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="image">selected image</param>
        /// <returns>boolean result</returns>
        public bool AddImage(int id, IFormFile image)
        {
            try
            {
                bool result = this.notesRL.AddImage(id, image);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
