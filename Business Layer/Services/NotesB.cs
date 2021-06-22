using System;
using System.Collections.Generic;
using System.Text;
using Business_Layer.Interfaces;
using Common_Layer;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Interfaces;

namespace Business_Layer.Services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesBL notes;

        public NotesBL(INotesBL notes)
        {
            this.notes = notes;
        }

       
        public bool AddNewNote(NotesModel note)
        {
            try
            {
                bool result = this.notes.AddNewNote(note);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        ///  Method to Call RetrievNote() method to retriev
        
        public IEnumerable<NotesModel> RetrievNote()
        {
            try
            {
                IEnumerable<NotesModel> note = this.notes.RetrievNote();
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Call RemoveNote() method to remove a note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string note message</returns>
        public bool RemoveNote(int id)
        {
            try
            {
                bool note = this.notes.RemoveNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    
        /// Method to update a note 
        
        public bool UpdateNote(NotesModel note)
        {
            try
            {
                bool result = this.notes.UpdateNote(note);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        /// Method to Call GetNoteById() method to remove a note 
        
        public IEnumerable<NotesModel> GetNoteById(int id)
        {
            try
            {
                IEnumerable<NotesModel> note = this.notes.GetNoteById(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        /// Method to Pin Or Unpin a Note 
        
        public string PinOrUnpinNote(int id)
        {
            try
            {
                var note = this.notes.PinOrUnpinNote(id);
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Call PinOrUnpinNote() method to Pin Or Unpin a Note 
        
        public string ArchiveOrUnArchiveNote(int id)
        {
            try
            {
                var note = this.notes.ArchiveOrUnArchiveNote(id);
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
                var note = this.notes.GetAllArchivedNotes();
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      
        /// Method to Trash or Restore a note
       
        public string TrashOrRestoreNote(int id)
        {
            try
            {
                var note = this.notes.TrashOrRestoreNote(id);
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
                var note = this.notes.GetAllNotesaFromTrash();
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
                bool result = this.notes.SetReminder(id, reminder);
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
                IEnumerable<NotesModel> notes = this.notes.GetAllNotesWhosReminderIsSet();
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
                bool result = this.notes.UnSetReminder(id);
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
                bool result = this.notes.ChangeColor(id, color);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Method to add image for note
       
        public bool AddImage(int id, IFormFile image)
        {
            try
            {
                bool result = this.notes.AddImage(id, image);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
