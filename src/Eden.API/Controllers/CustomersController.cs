using Eden.API.Models;
using Eden.Application.Interfaces;
using Eden.Core.Entities;
using Eden.Infrastructure.Repositories;
using Eden.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;

namespace Eden.API.Controllers
{
    [ApiVersion("1.0")]

    public class CustomersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiResponse = new ApiResponse<List<Customer>>();

            try
            {
                var data = await _unitOfWork.Customers.GetAllAsync();
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
        [Route("customerNumber")]
        public async Task<IActionResult> GetCustomerByCustomerNumber(int customerId)
        {
            var apiResponse = new ApiResponse<Customer>();

            if (customerId < 0)
            {
                apiResponse.Success = false;
                apiResponse.Message = "Customer Number must be more than 0.";
                Logger.Instance.Error("Customer Number was found empty");
                return BadRequest(apiResponse);
            }

            try
            {
                //var data = await _unitOfWork.Customers.GetCustomerById(customerId);
                apiResponse.Success = true;
                //apiResponse.Result = data;
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
        public async Task<IActionResult> CreateNewCustomer(Customer customer)
        {
            var apiResponse = new ApiResponse<Customer>();
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _unitOfWork.Customers.AddAsync(customer);
                    if (data > 0)
                    {
                        apiResponse.Success = true;
                        apiResponse.Result = customer;
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


            } else
            {
                apiResponse.Success = false;
                apiResponse.Message ="Error with passed values";
                return BadRequest(apiResponse);
            }

            return Ok(apiResponse);
        }


    }
}
