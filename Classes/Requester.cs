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

	public class RequesterEvents : IClosable
	{
		public event Action OnFirstRequest;
		public event Action OnNoRequest;

		private bool requested = false;
		private int requestCount = 0;

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
			if (requestCount > 0)
			{
				if (OnNoRequest != null && requestCount == 1)
					OnNoRequest();
				requestCount--;
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

	public class RequesterEventsUnsafe : IClosable
	{
		public event Action OnFirstRequest;
		public event Action OnNoRequest;

		private bool requested = false;
		private int requestCount = 0;

		public int Count => requestCount;
		public bool InRequest => requested;

		public void AddRequest()
		{
			requestCount++;
			requested = true;
			if (requestCount == 1)
				OnFirstRequest();
		}

		public void RemoveRequest()
		{
			if (requestCount > 0)
			{
				if (requestCount == 1)
					OnNoRequest();
				requestCount--;
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