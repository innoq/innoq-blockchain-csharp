# Sharpest Chain
The most beautiful blockchain implementation of all blockchains

Actually, it is just an attempt to implement a very basic blockchain.
This project was created in a one-and-a-half day hackathon at INNOQ,
with the purpose to explain the concept of a blockchain and to teach beginners
c# skills or refresh the skills of those who already knew c#.
Created by a group of four people. Feel free to improve the code or be inspired
by it. 

Licensed under MIT license.

## Technology
We used simple asp.net core 2.0 and added nuget packages for Akka.NET
(used for server sent events), nunit (unit testing), Newtonsoft's JSON framework
(used to parse JSON objects) and Jetbrains Annotations. 

## Execute
Run the project and the endpoints will be reachable under 

### Existing endpoints
Endpoint (GET) | explanation
--- | --- 
/ | Information about the current node (your laptop, pc, process)
/blocks | Lists all existing blocks. If you haven't mined any block yet, only the genesis block will be displayed
/mine | Mine a new block for the chain. This might take a few seconds
/transactions | List all available transactions that will be stored in a block
/transactions/{id} | Show details of specific transaction, identified by {id}
/events | List all events that have happened so far

Endpoint (POST) | explanation
--- | --- 
/transactions | Allows you to store a transaction. A transaction may only contain a payload (string)

