namespace Com.Innoq.SharpestChain.Eventing
{
    using System.IO;
    using System.Threading;

    using Akka.Actor;

    using IO;

    public partial class ConnectionHolder
    {
        public sealed class NewConnection
        {
            public readonly Stream Stream;

            public readonly CancellationToken CancellationToken;

            public readonly IActorRef PersistenceActorRef;

            public NewConnection(Stream pStream, CancellationToken pCancellationToken, IActorRef pPersistence)
            {
                Stream = pStream;
                CancellationToken = pCancellationToken;
                PersistenceActorRef = pPersistence;
            }
        }
    }
}
