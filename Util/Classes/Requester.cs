using System;

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

	public class RequesterEvents : IClosable
	{
		public event Action? OnFirstRequest;
		public event Action? OnNoRequest;

		private bool requested = false;
		private int requestCount = 0;

		public RequesterEvents() { }
		public RequesterEvents(Action OnFirstRequest) 
		{ 
			this.OnFirstRequest = OnFirstRequest; 
		}
		public RequesterEvents(Action OnFirstRequest, Action OnNoRequest) 
		{ 
			this.OnFirstRequest = OnFirstRequest; 
			this.OnNoRequest = OnNoRequest; 
		}

		public int Count => requestCount;
		public bool InRequest => requested;

		public void AddRequest()
		{
			requestCount++;
			requested = true;
			if (requestCount == 1 && OnFirstRequest != null)
				OnFirstRequest();
		}

		public void RemoveRequest()
		{
			if (requestCount > 0)
			{
				requestCount--;
				if (requestCount == 0 && OnNoRequest != null)
					OnNoRequest();
			}
			else
				requestCount = 0;
		}

		public void Close()
		{
			OnFirstRequest = null;
			OnNoRequest = null;
		}

	}


}