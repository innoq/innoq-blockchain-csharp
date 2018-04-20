namespace Com.Innoq.SharpestChain.IO
{
    using Akka.Actor;

    /// <summary>
    /// Wrapper class to pass a dedicated actorRef into a controller,
    /// with the dotnet mvc dependency injection.
    /// </summary>
    public class PersistenceActorRef : IPersistenceActorRef
    {
        private readonly IActorRef SharpestChainPersitenceActorWrapper;

        public PersistenceActorRef(IActorRef pSharpestChainPersistenceActorWrapper) =>
                SharpestChainPersitenceActorWrapper = pSharpestChainPersistenceActorWrapper;

        public IActorRef GetActorRef() => SharpestChainPersitenceActorWrapper;
    }
}
