using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections;
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
                    var ctx = (data as JObject).ToObject<StatusPong>();
                    var now = GetTimestamp();
                    var diff = now - ctx.ts;
                    Debug.Log($"ping: {diff}ms");
                });
            });
        }

        StartCoroutine(BeginPingLoop());
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
        socket.Emit("status-ping", JObject.FromObject(ctx));
    }

    IEnumerator BeginPingLoop()
    {
        while (true)
        {
            SendPing(socket);
            yield return new WaitForSeconds(1);
        }
    }
}
