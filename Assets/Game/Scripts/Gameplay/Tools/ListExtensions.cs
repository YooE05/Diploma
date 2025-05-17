using System.Collections.Generic;
using UnityEngine;

namespace YooE
{
    public static class ListExtensions
    {
        public static List<T> GetRandomItemsFisherYates<T>(this IList<T> source, int count)
        {
            var n = source.Count;
            count = Mathf.Clamp(count, 0, n);

            var indices = new int[n];
            for (var i = 0; i < n; i++) indices[i] = i;

            for (var i = 0; i < count; i++)
            {
                var j = Random.Range(i, n);

                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            var result = new List<T>(count);
            for (var i = 0; i < count; i++)
            {
                result.Add(source[indices[i]]);
            }

            return result;
        }
    }
}