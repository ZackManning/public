﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Store;
using WebApi.Models;
using System.Net;
using System.Linq;

namespace WebApi.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserDataStore UsersDataStore;

        /// <summary>
        /// Construct Controller
        /// </summary>
        /// <param name="dataStore">Injected Data Store</param>
        public UsersController(IUserDataStore dataStore)
        {
            UsersDataStore = dataStore;
        }

        #region GET
        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>A list of users if it exists</returns>
        /// <response code="200">Returns the users</response>
        /// <response code="404">No content</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var users = await UsersDataStore.Get<User>();
            if (users == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(users);
            }
        }

        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <remarks>
        /// Note that the id can be a GUID
        ///  
        ///     GET api/users/77edbd1e-fcbc-4152-a13d-2d228a8a7637
        ///
        /// </remarks>
        /// <returns>The user if it exists</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="400">If there is no id</response>
        /// <response code="404">No Content</response>
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            
            var user = await UsersDataStore.Get<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
        #endregion GET

        #region POST
        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user"></param>
        /// <remarks>
        /// It is recommend you do not supply an id when creating a new user.  If you do not supply an id then one will be generated for you.
        ///     
        ///     POST api/users
        ///     {
        ///         "first": "Brad",
        ///         ...
        ///     }
        ///     
        ///     returns the location in the response header and the user with a auto-generated GUID that was created.
        ///     
        /// </remarks>
        /// <returns>The created user</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var newUserID = await UsersDataStore.Insert(user);

            return CreatedAtRoute("GetUser", new { id = newUserID }, user);
        }
        #endregion POST

        #region PUT
        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <response code="204">If the update was successful.</response>
        /// <response code="400">If there is no id, user, or the id supplied does not match the user id in the body.</response>
        /// <response code="404">If the id supplied is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] User user)
        {
            if (InvalidUpdateModelState(id, user))
            {
                return BadRequest();
            }

            if (await UsersDataStore.Get<User>(id) == null)
            {
                return NotFound();
            }

            await UsersDataStore.Update(id, user);

            return NoContent();
        }
        #endregion PUT

        #region Utilities
        private bool InvalidUpdateModelState(string id, User user)
        {
            return string.IsNullOrWhiteSpace(id) || user == null || user.Id != id;
        }
        #endregion Utilities


        #region DELETE
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">If the delete was successful.</response>
        /// <response code="400">If there is no id supplied.</response>
        /// <response code="404">If the id supplied is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            if (await UsersDataStore.Get<User>(id) == null)
            {
                return NotFound();
            }

            await UsersDataStore.Delete(id);

            return NoContent();
        }
        #endregion DELETE
    }
}
