using System;
using System.Collections.Generic;

namespace Util
{
    public class Requester
    {
        private bool requested;
        private int requestCount;

        public int Count => requestCount;
        public bool InRequest => requested;

        public void AddRequest()
        {
            requestCount++;
            requested = true;
        }

        public void RemoveRequest()
        {
            if (--requestCount <= 0)
            { 
                requestCount = 0;
                requested = false;
            }
        }

    }

    public class RequesterEvents
    {
        public event Action OnFirstRequest;
        public event Action OnNoRequest;

        private bool requested;
        private int requestCount;

        public RequesterEvents() { }
        public RequesterEvents(Action first, Action no)
        {
            if(first != null)
                OnFirstRequest = first;
            if(no != null)
                OnNoRequest = no;
        }

        public int Count => requestCount;
        public bool InRequest => requested;

        public void AddRequest()
        {
            requestCount++;
            requested = true;
            if (OnFirstRequest != null && requestCount == 1)
                OnFirstRequest();
        }

        public void RemoveRequest()
        {
            requestCount--;
            if (OnNoRequest != null && requestCount == 0)
                OnNoRequest();
            if (requestCount <= 0)
            { 
                requestCount = 0;
                requested = false;
            }
        }

	}

}