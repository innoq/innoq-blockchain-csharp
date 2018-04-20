namespace Com.Innoq.SharpestChain.Controllers
{
    using System.Threading.Tasks;

    using Akka.Actor;

    using Eventing;

    using IO;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    /// <inheritdoc />
    /// <summary>
    /// Provides the ActionResult for the controller.
    /// To keep the connection open until a client is closing the connection
    /// a task is returned which never ends.
    /// </summary>
    public class PushActorStreamResult : IActionResult
    {
        private readonly string _contentType;

        private readonly IEventConnectionHolderActorRef _connectionHolderActorRef;

        private readonly IPersistenceActorRef _persistence;

        public PushActorStreamResult(IEventConnectionHolderActorRef pConnectionHolderActorRef, string pContentType,
                                     IPersistenceActorRef pPersistence)
        {
            _contentType = pContentType;
            _connectionHolderActorRef = pConnectionHolderActorRef;
            _persistence = pPersistence;
        }

        public Task ExecuteResultAsync(ActionContext pContext)
        {
            var stream = pContext.HttpContext.Response.Body;
            pContext.HttpContext.Response.GetTypedHeaders().ContentType = new MediaTypeHeaderValue(_contentType);

            return _connectionHolderActorRef.GetActorRef()
                                            .Ask(new ConnectionHolder.NewConnection(
                                                         stream, pContext.HttpContext.RequestAborted,
                                                         _persistence.GetActorRef()));
        }
    }
}
