﻿using Common_Layer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    /// </summary>
    public interface INotesManager
    {
        /// <summary>
        /// Method declaration to add new note
        /// </summary>
        /// <param name="note">note parameter</param>
        /// <returns>string message</returns>
        public NotesModel AddNewNote(NotesModel note, long ids);

        /// <summary>
        /// Method declaration to retrieve all note
        /// </summary>
        /// <returns>all notes</returns>
       // public IEnumerable<NotesModel> RetrievNote(long ID);

        /// <summary>
        /// Method declaration to remove a note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>boolean result</returns>
        public bool RemoveNote(int id);

        /// <summary>
        /// Method declaration to update a note
        /// </summary>
        /// <param name="note">note parameter</param>
        /// <returns>boolean result</returns>
        public bool UpdateNote(NotesModel note);
       // void AddNewNote(NotesModel note, long ids);


        /// <summary>
        /// Method declaration to get a note by its Id
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public IEnumerable<NotesModel> GetNoteById(int id);

        
        public string PinOrUnpinNote(int id);

        
        public string ArchiveOrUnArchiveNote(int id);


        /// <summary>
        /// Method declaration to get all notes which are archived
        /// </summary>
        /// <returns>archived notes</returns>
        public IEnumerable<NotesModel> GetAllArchivedNotes();

        /// <summary>
        /// Method Declaration to Trash or Restore a note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string TrashOrRestoreNote(int id);

        /// <summary>
        /// Method declaration to get all notes which are in trash
        /// </summary>
        /// <returns>all notes from trash</returns>
        public IEnumerable<NotesModel> GetAllNotesaFromTrash();

        /// <summary>
        /// Method declaration to set reminder for perticulat note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="reminder">reminder parameter for note</param>
        /// <returns>boolean result</returns>
        public bool SetReminder(int id, string reminder);

        /// <summary>
        /// Method declaration to get all notes for which reminder has set
        /// </summary>
        /// <returns>notes for which reminder has set</returns>
        public IEnumerable<NotesModel> GetAllNotesWhosReminderIsSet();

        /// <summary>
        /// Method declaration to unset reminder for perticular note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>boolean result</returns>
        public bool UnSetReminder(int id);

        /// <summary>
        /// Method declaration to add color for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="color">color name</param>
        /// <returns>boolean result</returns>
        public bool ChangeColor(int id, string color);

        /// <summary>
        /// Method to add image for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="image">selected image</param>
        /// <returns>boolean result</returns>
        public bool AddImage(int id, IFormFile image);
    }
}
