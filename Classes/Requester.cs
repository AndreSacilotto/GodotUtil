﻿using System;

namespace Util
{
    public class Requester : IDisposable
    {
        public event Action OnFirstRequest;
        public event Action OnNoRequest;

        private int requestCount;

        public Requester() { }
        public Requester(Action first, Action no)
        {
            if(first != null)
                OnFirstRequest = first;
            if(no != null)
                OnNoRequest = no;
        }

        public int Count => requestCount;
        public bool InRequest => requestCount > 0;

        public void AddRequest()
        {
            requestCount++;
            if (OnFirstRequest != null && requestCount == 1)
                OnFirstRequest();
        }

        public void RemoveRequest()
        {
            if (requestCount == 0)
                return;

            requestCount--;
            if (OnNoRequest != null && requestCount == 0)
                OnNoRequest();
        }

        public void Dispose()
        {
            requestCount = 0;
            OnFirstRequest = null;
            OnNoRequest = null;
        }

    }
}