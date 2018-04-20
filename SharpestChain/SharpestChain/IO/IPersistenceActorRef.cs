namespace Com.Innoq.SharpestChain.IO
{
    using Akka.Actor;

    public interface IPersistenceActorRef
    {
        IActorRef GetActorRef();
    }
}
