using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserPayment.API.Models;

namespace UserPayment.API.Controllers
{
    [ApiController]
    [Route("api/userpayments")]
    public class UserPaymentsController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetUserPayments()
        {
           return new JsonResult(UsersDataStore.Current.Users);
        }
        [HttpGet("{AccountNumber}", Name ="GetSingleUser")]
        public ActionResult<UserDto> GetSingleUser(string AccountNumber)
        {
            var UserToReturn = UsersDataStore.Current.Users.FirstOrDefault(u => u.AccountNumber == AccountNumber);

            if (UserToReturn == null)
            {
                return NotFound();
            }

            return Ok(UserToReturn);
        }
        [HttpPost]
        public ActionResult<UserDto> CreateUser(UserForCreationDto user)
        {

            var finalUser = new UserDto()
            {
                AccountName = user.AccountName,
                AccountNumber = user.AccountNumber,
                AdeptReference = user.AdeptReference,
                Balance = user.Balance,
                DateOfBirth = user.DateOfBirth
            };
            UsersDataStore.Current.Users.Add(finalUser);

            return CreatedAtRoute("GetSingleUser", new { finalUser.AccountNumber }, finalUser);
        }
        [HttpPut("{accountnumber}")]
        public ActionResult UpdateUser(string AccountNumber, UserForUpdateDto user)
        {
            var userFromStore = UsersDataStore.Current.Users.FirstOrDefault(u => u.AccountNumber == AccountNumber);
            if (userFromStore == null)
            {
                return NotFound();
            }

            userFromStore.AccountName = user.AccountName;
            userFromStore.AdeptReference = user.AdeptReference;
            userFromStore.Balance = user.Balance;
            userFromStore.DateOfBirth = user.DateOfBirth;

            return NoContent();

        }
        [HttpPatch]
        public ActionResult PartiallyUpdateUser(string accountNumber, JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            var userFromStore = UsersDataStore.Current.Users.FirstOrDefault(u => u.AccountNumber == accountNumber);
            if (userFromStore == null)
            {
                return NotFound();
            }

            var userToPatch = new UserForUpdateDto() { AccountName = userFromStore.AccountName, AdeptReference = userFromStore.AdeptReference, Balance = userFromStore.Balance, DateOfBirth = userFromStore.DateOfBirth };

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(userToPatch))
            {
                return BadRequest(ModelState);
            }

            userFromStore.AccountName = userToPatch.AccountName;
            userFromStore.AdeptReference = userToPatch.AdeptReference;
            userFromStore.Balance = userToPatch.Balance;
            userFromStore.DateOfBirth = userToPatch.DateOfBirth;

            return NoContent();
        }
    }
}
