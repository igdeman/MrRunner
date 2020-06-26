using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public class UnionFind
    {
        public int[] id;
        public int[] size;

        public UnionFind(int N)
        {
            id = new int[N];
            size = new int[N];
            for (int i = 0; i < N; i++)
            {
                id[i] = i;
                size[i] = 1;
            }
        }

        public void SetRoot(int i, int root)
        {
            id[i] = root;
        }

        public int Root(int i)
        {
            while (i != id[i])
            {
                id[i] = id[id[i]];
                i = id[i];
            }
            return i;
        }

        public bool IsConnected(int v1, int v2)
        {
            return Root(v1) == Root(v2);
        }

        public void Union(int v1, int v2, int preferedRoot)
        {
            int v1Root = Root(v1);
            int v2Root = Root(v2);

            if (v1Root == preferedRoot)
            {
                id[v2Root] = id[v1Root];
            }
            else
            {
                id[v1Root] = id[v2Root];
            }
        }

        public void Union(int v1, int v2)
        {
            int v1Root = Root(v1);
            int v2Root = Root(v2);

            if (size[v1Root] > size[v2Root])
            {
                id[v2Root] = id[v1Root];
                size[v2Root] += size[v1Root];
            }
            else
            {
                id[v1Root] = id[v2Root];
                size[v1Root] += size[v2Root];
            }
        }
    }
}
