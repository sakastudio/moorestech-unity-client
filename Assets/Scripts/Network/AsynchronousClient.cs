using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Threading;  
using System.Text;
using UnityEngine;

// リモートデバイスからデータを受信するためのステートオブジェクト。 
public class StateObject {  
    // Client socket.  
    public Socket workSocket = null;  
    // Size of receive buffer.  
    public const int BufferSize = 256;  
    // Receive buffer.  
    public byte[] buffer = new byte[BufferSize];  
    // Received data string.  
    public StringBuilder sb = new StringBuilder();  
}  
  
public class AsynchronousClient {  
    // リモートデバイスのポート番号です。 
    private const int port = 11000;  
  
    // ManualResetEventインスタンスが完了を知らせます。 
    private static ManualResetEvent connectDone =
        new ManualResetEvent(false);  
    private static ManualResetEvent sendDone =
        new ManualResetEvent(false);  
    private static ManualResetEvent receiveDone =
        new ManualResetEvent(false);  
  
    // リモートデバイスからの応答です。 
    private static String response = String.Empty;

    private static Socket client;
  
    public static void StartClient() {  
        // リモートデバイスに接続します。  
        try {  
            // ソケットのリモートエンドポイントを確立します。 
            // リモートデバイスの名前は「host.contoso.com」です。 
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");  
            IPAddress ipAddress = ipHostInfo.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);  
  
            // Create a TCP/IP socket.  
            client = new Socket(ipAddress.AddressFamily,  
                SocketType.Stream, ProtocolType.Tcp);  
  
            // リモートエンドポイントに接続します。  
            client.BeginConnect( remoteEP,
                new AsyncCallback(ConnectCallback), client);  
            connectDone.WaitOne();

            // リモートデバイスからのレスポンスを受信します。 
            Receive(client);  
            receiveDone.WaitOne();
        } catch (Exception e) {  
            Debug.Log(e.ToString());  
        }  
    }  
  
    private static void ConnectCallback(IAsyncResult ar) {  
        try {  
            // stateオブジェクトからソケットを取得します。 
            Socket client = (Socket) ar.AsyncState;  
  
            // 接続を完了します。 
            client.EndConnect(ar);  
  
            Debug.Log($"Socket connected to {client.RemoteEndPoint.ToString()}");  
  
            // 接続が完了したことを知らせる信号です。 
            connectDone.Set();  
        } catch (Exception e) {  
            Debug.Log(e.ToString());  
        }  
    }

    private static void CloseSocket()
    {
        client.Shutdown(SocketShutdown.Both);  
        client.Close();  
    }
    
    private static void Receive(Socket client) {  
        try {  
            // Stateオブジェクトを作成します。 
            StateObject state = new StateObject();  
            state.workSocket = client;  
  
            // リモートデバイスからのデータ受信を開始します。 
            client.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,  
                new AsyncCallback(ReceiveCallback), state);  
        } catch (Exception e) {  
            Debug.Log(e.ToString());  
        }  
    }  
  
    private static void ReceiveCallback( IAsyncResult ar ) {  
        try {  
            // 非同期ステートオブジェクトから、ステートオブジェクトとクライアントソケットを取得します。 
            StateObject state = (StateObject) ar.AsyncState;
            var payload = state.buffer;
            Console.WriteLine(string.Join(",",payload));
        } catch (Exception e) {  
            Debug.Log(e.ToString());  
        }  
    }  
  
    public static void Send(byte[] byteData) {
        // リモートデバイスへのデータ送信を開始します。 
        client.BeginSend(byteData, 0, byteData.Length, 0,  
            new AsyncCallback(SendCallback), client);  
    }  
  
    private static void SendCallback(IAsyncResult ar) {  
        try {  
            // stateオブジェクトからソケットを取得します。 
            Socket client = (Socket) ar.AsyncState;  
  
            // リモートデバイスへのデータ送信完了  
            int bytesSent = client.EndSend(ar);  
            Debug.Log($"Sent {bytesSent} bytes to server.");  
  
            // すべてのバイトが送信されたことを知らせる信号です。 
            sendDone.Set();  
        } catch (Exception e) {  
            Debug.Log(e.ToString());  
        }  
    }  
}