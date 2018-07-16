﻿using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Ping : MonoBehaviour
{
    Socket socket;

    void Start()
    {
        if (socket == null)
        {
            socket = IO.Socket("http://127.0.0.1:3000");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.Log("connection");

                socket.On("status-pong", (data) =>
                {
                    string str = data.ToString();
                    var ctx = JsonConvert.DeserializeObject<StatusPong>(str);
                    var now = GetTimestamp();
                    var diff = now - ctx.ts;
                    Debug.Log($"ping: {diff}ms");
                });
            });
        }

        RunPingProcess();
    }

    private void OnDestroy()
    {
        if (socket != null)
        {
            socket.Disconnect();
            socket = null;
        }
    }

    struct StatusPing
    {
        public long ts;
    }

    struct StatusPong
    {
        public long ts;
    }

    long GetTimestamp()
    {
        var now = DateTime.UtcNow;
        var zero = new DateTime(1970, 1, 1, 0, 0, 0);
        var diff = now - zero;
        var ts = (long)(diff.TotalMilliseconds);
        return ts;
    }

    void SendPing(Socket socket)
    {
        if (socket == null)
        {
            return;
        }

        var ts = GetTimestamp();
        var ctx = new StatusPing() { ts = ts };
        var json = JsonConvert.SerializeObject(ctx);
        socket.Emit("status-ping", json);
    }

    async void RunPingProcess()
    {
        while (true)
        {
            SendPing(socket);
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}