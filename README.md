# socket.io-unity-sample

socket.io sample with Unity game client.

[floatinghotpot/socket.io-unity](https://github.com/floatinghotpot/socket.io-unity) is good project.
But it doesn't support [UniRx](https://github.com/neuecc/UniRx).
So, I make new boilerplate project.

## svr (server)

node.js + socket.io

```bash
cd svr
yarn install
yarn start
```

## cli (client)

* unity 2018.1.8f1
* .Net : 4.x

see `cli/Assets/Ping.cs`

### libraries

* SocketIoClientDotNet.dll
	* https://github.com/Quobject/SocketIoClientDotNet
	* use 1.0.7.1 (.Net 4.5)
* EngineIoClientDotNet.dll
	* https://github.com/Quobject/EngineIoClientDotNet
	* use 1.0.7 (.Net 4.5)
* Newtonsoft.Json.dll
	* https://github.com/JamesNK/Newtonsoft.Json/releases
	* use 11.0.2  (.Net 4.5)
* System.Collections.Immutable
	* https://www.nuget.org/packages/System.Collections.Immutable
	* use 1.5.0 (.Net 4.5)
* WebSocket4Net.dll / SuperSocket.ClientEngine.dll
	* https://github.com/kerryjiang/WebSocket4Net
	* use commit [9a5e11600ab520ec1ce0a725c6df12bf6d8e0504](https://github.com/kerryjiang/WebSocket4Net/commit/9a5e11600ab520ec1ce0a725c6df12bf6d8e0504)
	* use WebSocket4Net.sln + .Net 4.5 built dll