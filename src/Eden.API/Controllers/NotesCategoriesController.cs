using Eden.API.Models;
using Eden.Application.Interfaces;
using Eden.Core.Entities.Notes;
using Eden.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Eden.API.Controllers
{
    public class NotesCategoriesController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;

        public NotesCategoriesController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var apiResponse = new ApiResponse<List<NotesCategory>>();

            try
            {
                var data = await _unitOfWork.NotesCategories.GetAllAsync();
                apiResponse.Success = true;
                apiResponse.Result = data.ToList();
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                Logger.Instance.Error("SQL Exception:", ex);
                return StatusCode(500, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                Logger.Instance.Error("Exception:", ex);
                return StatusCode(500, apiResponse);
            }

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("guid")]
        public async Task<IActionResult> GetByUniqueId(string guid)
        {
            var apiResponse = new ApiResponse<NotesCategory>();

            try
            {
                var data = await _unitOfWork.NotesCategories.GetByUniqueIdAsync(guid);
                apiResponse.Success = true;
                apiResponse.Result = data;
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                Logger.Instance.Error("SQL Exception:", ex);
                return StatusCode(500, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                Logger.Instance.Error("Exception:", ex);
                return StatusCode(500, apiResponse);
            }

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetByUniqueId(int id)
        {
            var apiResponse = new ApiResponse<NotesCategory>();

            try
            {
                var data = await _unitOfWork.NotesCategories.GetByIdAsync(id);
                apiResponse.Success = true;
                apiResponse.Result = data;
            }
            catch (SqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                Logger.Instance.Error("SQL Exception:", ex);
                return StatusCode(500, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                Logger.Instance.Error("Exception:", ex);
                return StatusCode(500, apiResponse);
            }

            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotesCategory(NotesCategory notesCategory)
        {
            var apiResponse = new ApiResponse<NotesCategory>();
            if (ModelState.IsValid)
            {
                try
                {
                    var result  = await _unitOfWork.NotesCategories.AddAsync(notesCategory);
                    if (result == 1)
                    {
                        apiResponse.Success = true;
                        apiResponse.Result = notesCategory;
                        return Ok(apiResponse);
                    }
                    else
                    {
                        apiResponse.Success = false;
                        apiResponse.Message = "Unable to add note category.";
                        return StatusCode(500, apiResponse);
                    }

                }
                catch (SqlException ex)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = ex.Message;
                    Logger.Instance.Error("SQL Exception:", ex);
                    return StatusCode(500, apiResponse);

                }
                catch (Exception ex)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = ex.Message;
                    Logger.Instance.Error("Exception:", ex);
                    return StatusCode(500, apiResponse);

                }


            }
            else
            {
                apiResponse.Success = false;
                apiResponse.Message = "Error with passed values";
                return BadRequest(apiResponse);
            }

            return Ok(apiResponse);
        }


    }
}
