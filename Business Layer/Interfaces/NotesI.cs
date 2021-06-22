using System;
using System.Collections.Generic;
using System.Text;
using Common_Layer;
using Microsoft.AspNetCore.Http;

namespace Business_Layer.Interfaces
{

    public interface INotesBL
    {
        
        public bool AddNewNote(NotesModel note);

        public IEnumerable<NotesModel> RetrievNote();

       
        public bool RemoveNote(int id);

        
        public bool UpdateNote(NotesModel note);


        
        public IEnumerable<NotesModel> GetNoteById(int id);

        public string PinOrUnpinNote(int id);

        public string ArchiveOrUnArchiveNote(int id);


        public IEnumerable<NotesModel> GetAllArchivedNotes();

     
        public string TrashOrRestoreNote(int id);

       
        public IEnumerable<NotesModel> GetAllNotesaFromTrash();

        
        public bool SetReminder(int id, string reminder);

      
        public IEnumerable<NotesModel> GetAllNotesWhosReminderIsSet();

        
        public bool UnSetReminder(int id);

        
        public bool ChangeColor(int id, string color);

       
        public bool AddImage(int id, IFormFile image);
    }
}
