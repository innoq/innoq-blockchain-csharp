namespace Com.Innoq.SharpestChain.Eventing
{
    using Akka.Actor;

    public interface IEventConnectionHolderActorRef
    {
        IActorRef GetActorRef();
    }
}
