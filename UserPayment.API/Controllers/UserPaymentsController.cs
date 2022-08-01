using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UserPayment.API.Entities;
using UserPayment.API.Models;
using UserPayment.API.Services;

namespace UserPayment.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/userpayments")]

    public class UserPaymentsController : ControllerBase
    {
        private readonly ILogger<UserPaymentsController> logger;
        private readonly IUserPaymentInfoRepository userPaymentInfoRepository;
        private readonly IMapper mapper;
        const int maxUsersPageSize = 20;

        public UserPaymentsController(ILogger<UserPaymentsController> logger, IUserPaymentInfoRepository paymentInfoRepository, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.userPaymentInfoRepository = paymentInfoRepository ?? throw new ArgumentNullException(nameof(paymentInfoRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithoutPaymentsDto>>> GetUsers([FromQuery] string name = null, [FromQuery] string searchQuery = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var city = User.Claims.FirstOrDefault(c => c.Type.ToLower() == "city").Value;

            if (pageSize > maxUsersPageSize)
            {
                pageSize = maxUsersPageSize;
            }
            var (userEntities, pageMeta) = await userPaymentInfoRepository.GetUsersAsync(name, searchQuery, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageMeta));
            return Ok(mapper.Map<IEnumerable<UserWithoutPaymentsDto>>(userEntities));
        }
        [HttpGet("{AccountNumber}", Name = "GetSingleUser")]
        public async Task<IActionResult> GetSingleUser(string AccountNumber, bool includePaymentsMade = false)
        {
            var userToReturn = await userPaymentInfoRepository.GetUserAsync(AccountNumber, includePaymentsMade);
            if (userToReturn == null)
            {
                logger.LogInformation($"No user found for account number {AccountNumber}");
                return NotFound();
            }
            if (includePaymentsMade)
            {
                return Ok(mapper.Map<UserDto>(userToReturn));
            }
            else
            {
                return Ok(mapper.Map<UserWithoutPaymentsDto>(userToReturn));
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto user)
        {
            var finalUser = mapper.Map<User>(user);

            userPaymentInfoRepository.AddUser(finalUser);

            await userPaymentInfoRepository.SaveChangesAsync();

            var createdUserToReturn = mapper.Map<UserDto>(finalUser);

            return CreatedAtRoute("GetSingleUser", new { createdUserToReturn.AccountNumber }, createdUserToReturn);
        }
        [HttpPut("{accountnumber}")]
        public async Task<ActionResult> UpdateUser(string AccountNumber, UserForUpdateDto user)
        {
            var userEntity = await userPaymentInfoRepository.GetUserAsync(AccountNumber, true);
            if (userEntity == null)
            {
                return NotFound();
            }
            mapper.Map(user, userEntity);

            await userPaymentInfoRepository.SaveChangesAsync();

            return NoContent();

        }
        [HttpPatch("{accountNumber}")]
        public async Task<ActionResult> PartiallyUpdateUser(string accountNumber, JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            var userEntity = await userPaymentInfoRepository.GetUserAsync(accountNumber, true);

            var userToPatch = mapper.Map<UserForUpdateDto>(userEntity);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                logger.LogWarning($"Invalid request body format");
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(userToPatch))
            {
                logger.LogInformation($"Failed to create a user with account with the provided information, please check the format of the request");
                return BadRequest(ModelState);
            }
            mapper.Map(userToPatch, userEntity);
            await userPaymentInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{accountNumber}")]
        public async Task<ActionResult> DeleteUser(string accountNumber)
        {
            var userEntity = await userPaymentInfoRepository.GetUserAsync(accountNumber, true);
            if (userEntity == null)
            {
                logger.LogInformation($"User with {accountNumber} couldn't be deleted as it does not exist");
                return NotFound();
            }
            userPaymentInfoRepository.DeleteUser(userEntity);
            await userPaymentInfoRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
