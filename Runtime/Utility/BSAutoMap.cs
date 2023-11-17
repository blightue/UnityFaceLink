using System;
using System.Collections.Generic;
using System.Linq;
using JacksonDunstan.NativeCollections;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace FaceLink.Utility
{
    public class BSAutoMap
    {
        public static int[] ComputeBSMap(string[] originals, string[] targets)
        {
            int[] result = new int[originals.Length];
            HashSet<string> targetSet = targets.ToHashSet();
            for (var i = 0; i < originals.Length; i++)
            {
                string original = originals[i];
                string closestText = ClosestText(original, targetSet);
                result[i] = Array.IndexOf(targets, closestText);
                targetSet.Remove(closestText);
            }

            return result;
        }

        private static string ClosestText(string original, IEnumerable<string> targets)
        {
            int closestDistance = int.MaxValue;
            string closestText = null;

            foreach (string target in targets)
            {
                int distance = LevenshteinDistance(original, target);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestText = target;
                }
            }
            return closestText;
        }

        private static int LevenshteinDistance(string text1, string text2)
        {
            int len1 = text1.Length;
            int len2 = text2.Length;

            int[,] dp = new int[len1 + 1, len2 + 1];
            for (int i = 0; i <= len1; i++)
                dp[i, 0] = i;
            for (int j = 0; j <= len2; j++)
                dp[0, j] = j;

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    if (char.ToUpperInvariant(text1[i - 1]) == char.ToUpperInvariant(text2[j - 1]))
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] = Mathf.Min(dp[i - 1, j], Mathf.Min(dp[i, j - 1], dp[i - 1, j - 1])) + 1;
                    }
                }
            }
            return dp[len1, len2];
        }
    }

    [BurstCompile]
    public struct LSDistanceJob : IJobParallelFor
    {
        [ReadOnly]
        public FixedString128Bytes textA;
        [ReadOnly]
        public NativeArray<FixedString128Bytes> textArray;

        public NativeArray<int> result;
        
        [BurstCompile]
        public void Execute(int index)
        {
            result[index] = ComputeLSDistance(textA, textArray[index]);
        }

        [BurstCompile]
        private int ComputeLSDistance(FixedString128Bytes text1, FixedString128Bytes text2)
        {
            int length1 = text1.Length;
            int length2 = text2.Length;

            NativeArray2D<int> map2d = new NativeArray2D<int>(length1 + 1, length2 + 1, Allocator.Temp);

            for (int i = 0; i <= length1; i++)
                map2d[i, 0] = i;
            for (int j = 0; j <= length2; j++)
                map2d[0, j] = j;
            

            for (int i = 1; i <= length1; i++)
            {
                for (int j = 1; j < length2; j++)
                {
                    if (text1[i - 1] == text2[j - 1])
                        map2d[i, j] = map2d[i - 1, j - 1];
                    else
                        map2d[i, j] = math.min(map2d[i - 1, j], math.min(map2d[i - 1, j - 1], map2d[i, j - 1])) + 1;
                }
            }
            
            int distance = map2d[length1, length2];

            map2d.Dispose();
            
            return distance;
        }
    }
}