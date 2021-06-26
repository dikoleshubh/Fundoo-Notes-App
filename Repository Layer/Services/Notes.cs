using Common_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Repository_Layer.Services
{
    public class NotesRepository : INotes
    {
        
        private  UserContext _userDbContext;


        private  IConfiguration configuration;
       // ICollection<NotesModel> notemodel;


        public NotesRepository(UserContext userDbContext, IConfiguration configuration)
        {
            _userDbContext = userDbContext;
            this.configuration = configuration;
        }
        
        /// <summary>
        /// Method to add/create new Note to the database
        /// </summary>
        /// <param name="note">note parameter of type NotesModel</param>
        /// <returns>string message</returns>
        public NotesModel AddNewNote(NotesModel note, long ids)
        {
            try
            {
                //bool result;
                //if (note != null)
                //{
                //    _userDbContext.FundooNotes.Add(note);
                //    _userDbContext.SaveChanges();
                //    result = true;
                //    return result;
                //}

                //result = false;
                //return result;
              //  var res = _userDbContext.Users.FirstOrDefault(e => e.ids == note.ids);
                //if (res ==  null)
                
                    var NewNote = new NotesModel
                    {

                        ID = ids,
                       //NotesId = note.NotesId,
                        Title = note.Title,
                        Description = note.Description,
                        Color = note.Color,
                        Image = note.Image,
                        Reminder = note.Reminder,
                        Pin = note.Pin,
                        Archieve = note.Archieve,
                        Is_Trash = note.Is_Trash,

                    };
                    _userDbContext.FundooNotes.Add(NewNote);

                    _userDbContext.SaveChangesAsync();

                    return (note) ;
                
                
                
             
                }

            
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Retrieve all notes from database
        /// </summary>
        /// <returns>all notes from database</returns>
        //public IEnumerable<NotesModel> RetrievNote(long ID)
        //{
        //    try
        //    {
        //        IEnumerable<NotesModel> result;
        //        IEnumerable<NotesModel> note = _userDbContext.FundooNotes;
        //        if (note != null)
        //        {
        //            result = note;
        //        }
        //        else
        //        {
        //            result = null;
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        /// <summary>
        /// Method to Remove Note from Database using id
        /// </summary>
        /// <param name="id">notes integer id</param>
        /// <returns>boolean result</returns>
        public bool RemoveNote(int id)
        {
            try
            {
                bool result;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    if (note.Is_Trash == true)
                    {
                        _userDbContext.FundooNotes.Remove(note);
                        _userDbContext.SaveChanges();
                        result = true;
                        return result;
                    }
                }

                result = false;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Update Note 
        /// </summary>
        /// <param name="note">note parameter of type NotesModel</param>
        /// <returns>boolean result</returns>
        public bool UpdateNote(NotesModel note)
        {
            try
            {
                bool result;
                if (note.NotesId > 0)
                {
                    _userDbContext.Entry(note).State = EntityState.Modified;
                    _userDbContext.SaveChanges();
                    result = true;
                    return true;
                }

                result = false;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Retrieve Note by Id 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>user note</returns>
        public IEnumerable<NotesModel> GetNoteById(int id)
        {
            try
            {
                IEnumerable<NotesModel> result;
                if (id > 0)
                {
                    var note = _userDbContext.FundooNotes.Where(x => x.NotesId == id);
                    result = note;
                    return result;
                }
                result = null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Pin Or Unpin the Note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string PinOrUnpinNote(int id)
        {
            try
            {
                string message;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    if (note.Pin == false)
                    {
                        note.Pin = true;
                        _userDbContext.Entry(note).State = EntityState.Modified;
                        _userDbContext.SaveChanges();
                        message = "Note Pinned";
                        return message;
                    }
                    if (note.Pin == true)
                    {
                        note.Pin = false;
                        _userDbContext.Entry(note).State = EntityState.Modified;
                        _userDbContext.SaveChanges();
                        message = "Note Unpinned";
                        return message;
                    }
                }

                return message = "Unable to pin or unpin note.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Archive or unarchive the note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        public string ArchiveOrUnArchiveNote(int id)
        {
            try
            {
                string message;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    if (note.Archieve == false)
                    {
                        note.Archieve = true;
                        _userDbContext.Entry(note).State = EntityState.Modified;
                        _userDbContext.SaveChanges();
                        message = "Note Archived";
                        return message;
                    }
                    if (note.Archieve == true)
                    {
                        note.Archieve = false;
                        _userDbContext.Entry(note).State = EntityState.Modified;
                        _userDbContext.SaveChanges();
                        message = "Note Unarchived";
                        return message;
                    }
                }

                return message = "Unable to archive or unarchive note.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to get all Archived notes
        /// </summary>
        /// <returns>all archived notes</returns>
        public IEnumerable<NotesModel> GetAllArchivedNotes()
        {
            try
            {
                IEnumerable<NotesModel> result;
                IEnumerable<NotesModel> note = _userDbContext.FundooNotes.Where(x => x.Archieve == true);
                if (note != null)
                {
                    result = note;
                }
                else
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Trash Or Restore Note
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>string message</returns>
        public string TrashOrRestoreNote(int id)
        {
            try
            {
                string message;
                var note = _userDbContext.FundooNotes.Where(x => x.NotesId == id).SingleOrDefault();
                if (note != null)
                {
                    if (note.Is_Trash == false)
                    {
                        note.Is_Trash = true;
                        _userDbContext.Entry(note).State = EntityState.Modified;
                        _userDbContext.SaveChanges();
                        message = "Note Trashed";
                        return message;
                    }
                    if (note.Is_Trash == true)
                    {
                        note.Is_Trash = false;
                        _userDbContext.Entry(note).State = EntityState.Modified;
                        _userDbContext.SaveChanges();
                        message = "Note Restored";
                        return message;
                    }
                }

                return message = "Unable to Restore or Trash note.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to get all Archived notes
        /// </summary>
        /// <returns>all archived notes</returns>
        public IEnumerable<NotesModel> GetAllNotesaFromTrash()
        {
            try
            {
                IEnumerable<NotesModel> result;
                IEnumerable<NotesModel> note = _userDbContext.FundooNotes.Where(x => x.Is_Trash == true);
                if (note != null)
                {
                    result = note;
                }
                else
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to set reminder for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="reminder">reminder parameter for note</param>
        /// <returns>boolean result</returns>
        public bool SetReminder(int id, string reminder)
        {
            try
            {
                bool result;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    note.Reminder = reminder;
                    _userDbContext.Entry(note).State = EntityState.Modified;
                    _userDbContext.SaveChanges();
                    result = true;
                    return result;
                }

                result = false;
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
                IEnumerable<NotesModel> result;
                result = _userDbContext.FundooNotes.Where(x => x.Reminder.Length > 0);
                if (result != null)
                {
                    return result;
                }
                result = null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to set reminder for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>boolean result</returns>
        public bool UnSetReminder(int id)
        {
            try
            {
                bool result;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    note.Reminder = null;
                    _userDbContext.Entry(note).State = EntityState.Modified;
                    _userDbContext.SaveChanges();
                    result = true;
                    return result;
                }

                result = false;
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
        /// <param name="color">color name</param>
        /// <returns>boolean result</returns>
        public bool ChangeColor(int id, string color)
        {
            try
            {
                bool result;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    note.Color = color;
                    _userDbContext.Entry(note).State = EntityState.Modified;
                    _userDbContext.SaveChanges();
                    result = true;
                    return result;
                }

                result = false;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to set image as background for a note 
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="image">selected image</param>
        /// <returns>boolean result</returns>
        public bool AddImage(int id, IFormFile image)
        {
            try
            {
                bool result;
                var note = _userDbContext.FundooNotes.Find(id);
                if (note != null)
                {
                    Account account = new Account(
                        configuration["CloudinaryAccount:CloudName"],
                        configuration["CloudinaryAccount:ApiKey"],
                        configuration["CloudinaryAccount:ApiSecret"]
                    );
                    var path = image.OpenReadStream();
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    note.Image = uploadResult.Url.ToString();
                    _userDbContext.Entry(note).State = EntityState.Modified;
                    _userDbContext.SaveChanges();
                    result = true;
                    return result;
                }

                result = false;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
