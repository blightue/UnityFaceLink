using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FaceLink.Data
{
    [CreateAssetMenu(menuName = "FaceLink/Source/LiveLinkStream", fileName = "New LiveLink Stream Config")]
    public class BSLLStreamSource: BSSourceBase
    {
        [SerializeField] protected int port = 11111;
        [SerializeField] protected bool isPlaying;

        protected UdpClient udpClient;

        protected CancellationTokenSource listenCTS;

        protected CancellationToken listenCT => listenCTS.Token;
        
        public override void Play()
        {
            isPlaying = true;
            listenCTS = new CancellationTokenSource();
            if (udpClient == null) Task.Run(StartListen, listenCT);
        }

        public override void Stop()
        {
            isPlaying = false;
            listenCTS.Cancel();
            udpClient.Close();
            udpClient = null;
        }

        public override void Pause()
        {
            isPlaying = false;
        }

        protected async void StartListen()
        {
            try
            {
                udpClient = new UdpClient(port);
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                return;
            }

            while (true)
            {
                UdpReceiveResult receivedResult = await udpClient.ReceiveAsync();
                if (isPlaying)
                {
                    Byte[] receivedBytes = receivedResult.Buffer;
                    if (receivedBytes.Length < 244) continue;

                    float[] bsValues = BytesToFloatArray(receivedBytes.TakeLast(244).ToArray());
                    OnBSChanged?.Invoke(bsValues);
                }
            }
        }
        
        public static float[] BytesToFloatArray(byte[] byteArray)
        {
            if (byteArray.Length % 4 != 0)
            {
                throw new ArgumentException("The byte array length must be a multiple of 4.");
            }
            
            int floatCount = byteArray.Length / 4;
            float[] floatArray = new float[floatCount];


                for (int i = 0; i < floatCount; i++)
                {
                    int lIndex = i * 4;
                    byte[] floatBytes = new byte[]
                    {
                        byteArray[lIndex + 3],
                        byteArray[lIndex + 2],
                        byteArray[lIndex + 1],
                        byteArray[lIndex]
                    };
                    floatArray[i] = BitConverter.ToSingle(floatBytes, 0);
                }
            
            return floatArray;
        }
    }
}