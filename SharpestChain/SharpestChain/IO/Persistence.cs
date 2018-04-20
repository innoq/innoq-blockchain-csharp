namespace Com.Innoq.SharpestChain.IO
{
    using System;
    using System.Collections.Generic;

    using Akka.Actor;

    using Data;

    public partial class Persistence : UntypedActor
    {
        public static Props props(IActorRef connectionHolderActorRef) => Props.Create(() => new Persistence(connectionHolderActorRef));

        public static readonly Block GENESIS_BLOCK =
                new Block(1, 0, 1917336,
                          new[]
                          {
                                  new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0,
                                                  "I am Heribert Innoq")
                          }, "0");

        private readonly List<Block> _blocks;

        private readonly List<Transaction> _unconfirmedTransactions;

        private readonly IActorRef _connectionHolder;

        public Persistence(IActorRef connectionHolderActorRef)
        {
            _connectionHolder = connectionHolderActorRef;
            _blocks = new List<Block> {GENESIS_BLOCK};
            _unconfirmedTransactions = new List<Transaction>();
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case Block block:
                    _blocks.Add(block);
                    _unconfirmedTransactions.RemoveAll(transaction => block.Transactions.Contains(transaction));
                    _connectionHolder.Tell(block);
                    break;
                case GetBlocks _:
                    Sender.Tell(_blocks.AsReadOnly());
                    break;
                case GetTransactions _:
                    Sender.Tell(AllTransactions().AsReadOnly());
                    break;
                case GetUnconfirmedTransactions _:
                    Sender.Tell(_unconfirmedTransactions.AsReadOnly());
                    break;
                case GetTransaction msg:
                    Sender.Tell(AllTransactions().Find(t => t.Id == new Guid(msg.Id)));
                    break;
                case Transaction transaction:
                    _unconfirmedTransactions.Add(transaction);
                    _connectionHolder.Tell(transaction);
                    break;
            }
        }

        private List<Transaction> AllTransactions()
        {
            var transactions = new List<Transaction>();
            foreach (var block in _blocks)
            {
                transactions.AddRange(block.Transactions);
            }

            transactions.AddRange(_unconfirmedTransactions);
            return transactions;
        }
    }
}
