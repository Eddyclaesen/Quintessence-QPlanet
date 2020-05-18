using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Quintessence.QCandidate.Controllers
{
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Options()
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);

            return Ok();
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a value with the specified identifier.
        /// </summary>
        /// <param name="projectId">The value's identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
#pragma warning disable IDE0060 // Remove unused parameter
        public IActionResult Options(int projectId)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                //HttpMethods.Patch,
                HttpMethods.Post,
                HttpMethods.Put);

            return Ok();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async IAsyncEnumerable<string> Get([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            yield return string.Empty;
        }

        [HttpGet("{projectId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public Task<IActionResult> Get(int projectId, CancellationToken cancellationToken)
        {
            return Task.FromResult((IActionResult)Ok());
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public Task<IActionResult> Post([FromBody] string project, CancellationToken cancellationToken)
        {
            return Task.FromResult((IActionResult)Created(new Uri(""), null));
        }

        [HttpPut("{projectId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> Put(int projectId, [FromBody] string project, CancellationToken cancellationToken)
        {
            return Task.FromResult((IActionResult)NoContent());
        }

        [HttpDelete("{projectId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public Task<IActionResult> Delete(int projectId, CancellationToken cancellationToken)
        {
            return Task.FromResult((IActionResult)Ok());
        }
    }
}

