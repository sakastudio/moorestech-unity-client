using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Network
{
    public class PacketHandler
    {
        private static Socket socket;
        public static void StartSocketClient()
        {
            //IPアドレスやポートを設定している。
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);


            Debug.Log("サーバーに接続します");
            //ソケットを作成
            socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //接続する。失敗するとエラーで落ちる。
            socket.Connect(remoteEP);
            Debug.Log("サーバーに接続しました");
            byte[] bytes = new byte[1024];
            while (true)
            {
                //Receiveで受信
                var len = socket.Receive(bytes);
                Debug.Log("データ受信 "+len+"バイト");
                //解析を行う
                ResponseAnalysis.GetPacketResponse(bytes);
            }
        }

        public static void SendMessages(byte[] sendData)
        {
            Debug.Log("データ送信 "+sendData.Length+"バイト");
            socket.Send(sendData);
        }
        
    }
}