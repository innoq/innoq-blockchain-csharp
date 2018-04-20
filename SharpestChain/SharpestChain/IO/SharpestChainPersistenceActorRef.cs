namespace Com.Innoq.SharpestChain.IO
{
    using Akka.Actor;

    using Eventing;

    /// <summary>
    /// Wrapper class to pass a dedicated actorRef into a controller,
    /// with the dotnet mvc dependency injection.
    /// </summary>
    public class SharpestChainPersistenceActorRef : ISharpestChainPersistenceActorRef
    {
        private readonly IActorRef SharpestChainPersitenceActorWrapper;

        public SharpestChainPersistenceActorRef(IActorRef pSharpestChainPersistenceActorWrapper) => 
                SharpestChainPersitenceActorWrapper = pSharpestChainPersistenceActorWrapper;

        public IActorRef GetActorRef() => SharpestChainPersitenceActorWrapper;
    }
}
