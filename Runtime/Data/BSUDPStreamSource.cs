using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FaceLink.Data
{
    [Serializable]
    public class BSUDPStreamSource: BSSourceBase
    {
        [SerializeField] protected int port = 11111;
        [SerializeField] protected bool isPlaying;

        protected bool isStop;
        
        protected UdpClient udpClient;
        
        public override void Play()
        {
            isPlaying = true;
            isStop = false;
            if (udpClient == null) Task.Run(StartListen);
        }

        public override void Stop()
        {
            isPlaying = false;
            isStop = true;
        }

        public override void Pause()
        {
            isPlaying = false;
        }

        protected async void StartListen()
        {
            udpClient = new UdpClient(port);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);

            while (!isStop)
            {
                UdpReceiveResult receivedResult = await udpClient.ReceiveAsync();
                if (isPlaying)
                {
                    Byte[] receivedBytes = receivedResult.Buffer;

                    if (receivedBytes.Length < 244) continue;

                    float[] bsValues = BytesToFloatArray(receivedBytes.TakeLast(244).ToArray());
                    // Debug.Log(bsValues.Length);
                    OnBSChanged?.Invoke(bsValues);
                }
            }
            
            udpClient.Close();
            udpClient = null;
        }
        
        public static float[] BytesToFloatArray(byte[] byteArray)
        {
            if (byteArray.Length % 4 != 0)
            {
                throw new ArgumentException("The byte array length must be a multiple of 4.");
            }
            
            // Debug.Log(string.Join(',',byteArray.Select(b => b.ToString())));
            //
            int floatCount = byteArray.Length / 4;
            float[] floatArray = new float[floatCount];
            //
            // for (int i = 0; i < floatCount; i++)
            // {
            //     floatArray[i] = BitConverter.ToSingle(byteArray, i * 4);
            //     
            // }
            
            List<List<Byte>> chunkedBytes = byteArray
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 4)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            // Process each float in out chunked out list
            foreach (var item in chunkedBytes.Select((value, i) => new { i, value }))
            {
                // First, reverse the list because the data will be in big endian, then convert it to a float
                item.value.Reverse();
                floatArray[item.i] = BitConverter.ToSingle(item.value.ToArray(), 0);
            }

            return floatArray;
        }
    }
}