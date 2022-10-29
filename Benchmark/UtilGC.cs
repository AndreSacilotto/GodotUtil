using System;

namespace Util.Test
{
	public static class UtilGC
	{
		public static void CollectImmediately()
		{
			GC.Collect(GC.MaxGeneration);
			GC.WaitForPendingFinalizers();
		}

		public static void CollectEverthing()
		{
			GC.Collect(GC.MaxGeneration);
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		public static long AllocateSomeNonReferencedMemory()
		{
			int loops = 64;
			int size = 1024;
			for (int i = 0; i < loops; i++)
			{
				int[] array = new int[size];
				array[0] = 1;
			}
			return loops * size * sizeof(int); // int is 32-bits (4 bytes)
		}

	}
}
