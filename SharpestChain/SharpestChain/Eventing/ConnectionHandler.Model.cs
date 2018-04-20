namespace Com.Innoq.SharpestChain.Eventing
{
    public partial class ConnectionHandler
    {
        public sealed class BookReservedEvent
        {
            public readonly string Isbn;

            public readonly bool Reserved;

            public BookReservedEvent(string pIsbn, bool pReserved)
            {
                Isbn = pIsbn;
                Reserved = pReserved;
            }
        }   
    }
}
