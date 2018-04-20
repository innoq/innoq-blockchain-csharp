﻿namespace Com.Innoq.SharpestChain.IO
{
    using System;
    using System.Collections.Generic;

    using Akka.Actor;

    using Data;

    using Eventing;

    public partial class Persistence : UntypedActor
    {
        public static Props props(IActorRef connectionHolderActorRef) => Props.Create(() => new Persistence(connectionHolderActorRef));

        public static readonly Block GENESIS_BLOCK = 
                new Block(1, 0, 1917336, new []{new Transaction(new Guid("b3c973e2-db05-4eb5-9668-3e81c7389a6d"), 0, "I am Heribert Innoq")}, "0");
        
        private readonly List<Block> _blocks;

        private  readonly IActorRef _connectionHolder;
        public Persistence(IActorRef connectionHolderActorRef)
        {
            _connectionHolder = connectionHolderActorRef;
            _blocks = new List<Block>{GENESIS_BLOCK};
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case Block block :
                    _blocks.Add(block);
                    _connectionHolder.Tell(block);
                    break;
                case GetBlocks _:
                    Sender.Tell(_blocks.AsReadOnly());
                    break;
                    
            }

        }
    }
}
