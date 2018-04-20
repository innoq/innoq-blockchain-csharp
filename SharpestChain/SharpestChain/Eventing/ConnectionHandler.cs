namespace Com.Innoq.SharpestChain.Eventing
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading;

    using Akka.Actor;

    using Data;

    using IO;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Writes received events from the persistent query stream as Server Sent Events to the response
    /// stream of the connection.
    /// </summary>
    public partial class ConnectionHandler : UntypedActor
    {
        public static Props props(Stream pStream, CancellationToken pCancellationToken, IActorRef pPersistence)
            => Props.Create(() => new ConnectionHandler(pStream, pCancellationToken, pPersistence));

        private readonly CancellationToken _cancellationToken;

        private readonly StreamWriter _writer;

        private readonly IActorRef _persistence;

        public ConnectionHandler(Stream pStream, CancellationToken pCancellationToken, IActorRef pPersitence)
        {
            _cancellationToken = pCancellationToken;
            _persistence = pPersitence;
            _writer = new StreamWriter(pStream)
                      {
                              NewLine = "\n"
                      };
        }

        protected override void PreStart()
        {
            base.PreStart();
            ValidateConnection();
            KeepAliveConnection();
            Self.Tell(new Replay());
        }

        protected override void OnReceive(object pMessage)
        {
            switch (pMessage)
            {
                case Block block:
                    if (!ConnectionIsClosed())
                    {
                        _writer.WriteLine($"id:{block.Index}");
                        _writer.WriteLine("event: new_block");
                        _writer.WriteLine($"data: {JsonConvert.SerializeObject(block)}");
                        _writer.WriteLine();
                        _writer.Flush();
                    }

                    break;

                case Transaction transaction:
                    if (!ConnectionIsClosed())
                    {
                        _writer.WriteLine($"id:{transaction.Id}");
                        _writer.WriteLine("event: new_transaction");
                        _writer.WriteLine($"data: {JsonConvert.SerializeObject(transaction)}");
                        _writer.WriteLine();
                        _writer.Flush();
                    }

                    break;

                case Replay _:
                    if (!ConnectionIsClosed())
                    {
                        var currentBlocks = _persistence
                                            .Ask<ReadOnlyCollection<Block>>(
                                                    new Persistence.GetBlocks(), TimeSpan.FromSeconds(5)).Result;
                        foreach (var block in currentBlocks)
                        {
                            _writer.WriteLine($"id:{block.Index}");
                            _writer.WriteLine("event: new_block");
                            _writer.WriteLine($"data: {JsonConvert.SerializeObject(block)}");
                            _writer.WriteLine();
                            _writer.Flush();
                        }
                    }

                    break;
                case CheckConnection _:
                    if (!ConnectionIsClosed())
                    {
                        ValidateConnection();
                    }

                    break;
                case HeartBeat _:
                    if (!ConnectionIsClosed())
                    {
                        _writer.WriteLine(":heartbeat");
                        _writer.WriteLine();
                        _writer.Flush();

                        KeepAliveConnection();
                    }

                    break;
            }
        }

        private void ValidateConnection()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(5), Self, new CheckConnection(),
                                                      ActorRefs.NoSender);
        }

        private void KeepAliveConnection()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(60), Self, new HeartBeat(),
                                                      ActorRefs.NoSender);
        }

        private bool ConnectionIsClosed()
        {
            bool isClosed = false;
            if (_cancellationToken.IsCancellationRequested)
            {
                _writer.Dispose();
                Self.Tell(PoisonPill.Instance);
                isClosed = true;
            }

            return isClosed;
        }
    }
}
