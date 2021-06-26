using Business_Layer.Interfaces;
using Common_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Fundo_app.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
       
        private readonly INotesManager notes;

        public NotesController(INotesManager notes)
        {
            this.notes = notes;
        }

      
        [HttpPost]
        [Route("CreateNewNote")]
        public IActionResult Notes([FromBody] NotesModel note)
        {
            try
            {

                var identity = User.Identity as ClaimsIdentity;
                var user = HttpContext.User;
                if (identity != null)
                {
                    NotesModel notesDB = new NotesModel();

                    IEnumerable<Claim> claims = identity.Claims;
                    long ID = Convert.ToInt64(user.Claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    notesDB=this.notes.AddNewNote(note, ID);
                    return this.Ok(new { success = true, user = ID});
                }

                return this.BadRequest(new ResponseModel<NotesModel>() { Status = false, Message = "Failed to Add New Note to Database" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<NotesModel>() { Status = false, Message = ex.Message });
            }
        }

       /* [HttpGet]
        public IActionResult GetActiveNotes()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    var result = this.notes.RetrievNote(ID);
                    return this.Ok(new { success = true, user = Email, Notes = result });
                }
                return BadRequest(new { success = false, Message = "No active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.InnerException });
            }
        }*/


        [HttpDelete]
        [Route("RemoveNoteById/{id}")]
        public IActionResult RemoveNoteById(int id)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long ID = Convert.ToInt64(claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    // var result = this.notes.RemoveNote(id);
                    var result = this.notes.TrashOrRestoreNote(id);
                    if (result != null)
                    {
                        return this.Ok(new ResponseModel<int>() { Status = true, Message = "Note Deleted Successfully", Data = id });
                    }
                    else
                    {
                        return this.BadRequest(new ResponseModel<int>() { Status = false, Message = "First trash a note then try to delete it. " });
                    }
                 }
                return BadRequest(new { success = false, Message = "No active please login" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<int>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API to Update note
        /// </summary>
        /// <param name="note">NotesModel note parameter</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote([FromBody] NotesModel note)
        {
            try
            {
                var result = this.notes.UpdateNote(note);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Note updated Successfully !", Data = note });
                }

                return this.BadRequest(new ResponseModel<NotesModel>() { Status = false, Message = "Error While updating note" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<NotesModel>() { Status = false, Message = ex.Message });
            }
        }

     
        [HttpGet]
        [Route("GetNoteById")]
        public IActionResult GetNoteeById(int id)
        {
            try
            {
                var result = this.notes.GetNoteById(id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Note Retrived Successfully", Data = result });
                }

                return this.BadRequest(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = "Unable to Retrieve Note" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller Method call method PinOrUnpinNote() method to Pin Or unpin the note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        [HttpPut]
        [Route("PinOrUnpinNote")]
        public IActionResult PinOrUnpinNote(int id)
        {
            try
            {
                var result = this.notes.PinOrUnpinNote(id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller Method call method ArchiveOrUnArchiveNote() method to Archive Or Unarchive the note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("ArchiveOrUnArchiveNote")]
        public IActionResult ArchiveOrUnarchive(int id)
        {
            try
            {
                var result = this.notes.ArchiveOrUnArchiveNote(id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API to Retrieve all notes from database
        /// </summary>
        /// <returns>response data</returns>
        [HttpGet]
        [Route("GetAllArchivedNotes")]
        public IActionResult GetAllArchivedNotes()
        {
            try
            {
                IEnumerable<NotesModel> result = this.notes.GetAllArchivedNotes();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Archived Notes retrieved Successfully", Data = result });
                }

                return this.BadRequest(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = "Unable to retrieve notes." });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method to Trash Or Restore a Note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>string message</returns>
        [HttpPut]
        [Route("TrashOrRestoreNote")]
        public IActionResult TrashOrRestoreNote(int id)
        {
            try
            {
                var result = this.notes.TrashOrRestoreNote(id);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API to Retrieve all notes from database
        /// </summary>
        /// <returns>response data</returns>
        [HttpGet]
        [Route("GetAllNotesaFromTrash")]
        public IActionResult GetAllNotesaFromTrash()
        {
            try
            {
                IEnumerable<NotesModel> result = this.notes.GetAllNotesaFromTrash();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Archived Notes retrieved Successfully", Data = result });
                }

                return this.BadRequest(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = "Unable to retrieve notes." });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = ex.Message });
            }
        }
      
        /// <summary>
        /// Controller method to set reminder for controller
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="reminder">reminder parameter for note</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("SetReminder")]
        public IActionResult SetReminder(int id, string reminder)
        {
            try
            {
                var result = this.notes.SetReminder(id, reminder);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reminder is set for this Note Successfully !", Data = reminder });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Error While setting reminder for this note" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method to get all nots for which reminder is set
        /// </summary>
        /// <returns>response data</returns>
        [HttpGet]
        [Route("GetAllNotesWhosReminderIsSet")]
        public IActionResult GetNotesWithReminders()
        {
            try
            {
                IEnumerable<NotesModel> result = this.notes.GetAllNotesWhosReminderIsSet();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Note who's reminder is set are retrieved successfully !", Data = result });
                }

                return this.BadRequest(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = "Unable to retrieve Note who's reminder is set." });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<IEnumerable<NotesModel>>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method to get all nots for which reminder is set
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("UnsetReminder")]
        public IActionResult UnSetReminder(int id)
        {
            try
            {
                var result = this.notes.UnSetReminder(id);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<int>() { Status = true, Message = "You have unset Reminder this Note !", Data = id });
                }

                return this.BadRequest(new ResponseModel<int>() { Status = false, Message = "Error While unsetting reminder for this note" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<int>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method to add color for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="color">color name</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("AddColor")]
        public IActionResult ChangeColor(int id, string color)
        {
            try
            {
                var message = this.notes.ChangeColor(id, color);
                if (message.Equals("New Color has set to this note !"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "New Color has set to this note !", Data = color });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Error While changing color for this note" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method to add image for note
        /// </summary>
        /// <param name="id">note id</param>
        /// <param name="image">selected image</param>
        /// <returns>response data</returns>
        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(int id, IFormFile image)
        {
            try
            {
                var result = this.notes.AddImage(id, image);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<int>() { Status = true, Message = "Image has Added for this Note Successfully!", Data = id });
                }

                return this.BadRequest(new ResponseModel<int>() { Status = false, Message = "Error While Adding image for this note" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<int>() { Status = false, Message = ex.Message });
            }
        }
    }
}

//System.AggregateException
//HResult = 0x80131500
//  Message = Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Business_Layer.Interfaces.INotesBL Lifetime: Transient ImplementationType: Business_Layer.Services.NotesBL': A circular dependency was detected for the service of type 'Business_Layer.Interfaces.INotesBL'.
//Business_Layer.Interfaces.INotesBL(Business_Layer.Services.NotesBL) -> Business_Layer.Interfaces.INotesBL)
//  Source = Microsoft.Extensions.DependencyInjection
//  StackTrace:
//at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(IEnumerable`1 serviceDescriptors, ServiceProviderOptions options)
//   at Microsoft.Extensions.DependencyInjection.ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(IServiceCollection services, ServiceProviderOptions options)
//   at Microsoft.Extensions.DependencyInjection.DefaultServiceProviderFactory.CreateServiceProvider(IServiceCollection containerBuilder)
//   at Microsoft.Extensions.Hosting.Internal.ServiceFactoryAdapter`1.CreateServiceProvider(Object containerBuilder)
//   at Microsoft.Extensions.Hosting.HostBuilder.CreateServiceProvider()
//   at Microsoft.Extensions.Hosting.HostBuilder.Build()
//   at Fundo_app.Program.Main(String[] args) in C:\Users\Admin\source\repos\Fundo app\Program.cs:line 16

//  This exception was originally thrown at this call stack:
//    [External Code]

//Inner Exception 1:
//InvalidOperationException: Error while validating the service descriptor 'ServiceType: Business_Layer.Interfaces.INotesBL Lifetime: Transient ImplementationType: Business_Layer.Services.NotesBL': A circular dependency was detected for the service of type 'Business_Layer.Interfaces.INotesBL'.
//Business_Layer.Interfaces.INotesBL(Business_Layer.Services.NotesBL) -> Business_Layer.Interfaces.INotesBL

//Inner Exception 2:
//InvalidOperationException: A circular dependency was detected for the service of type 'Business_Layer.Interfaces.INotesBL'.
//Business_Layer.Interfaces.INotesBL(Business_Layer.Services.NotesBL) -> Business_Layer.Interfaces.INotesBL
