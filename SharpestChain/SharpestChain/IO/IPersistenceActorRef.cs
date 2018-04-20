namespace Com.Innoq.SharpestChain.Eventing
{
    using Akka.Actor;

    public interface IPersistenceActorRef
    {
        IActorRef GetActorRef();
    }
}
