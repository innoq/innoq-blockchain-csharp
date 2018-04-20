namespace Com.Innoq.SharpestChain.Eventing
{
    using Akka.Actor;

    public interface ISharpestChainPersistenceActorRef
    {
        IActorRef GetActorRef();
    }
}
