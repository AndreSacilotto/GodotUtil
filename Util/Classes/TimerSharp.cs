namespace Util;

public class TimerSharp : IPausable, IClosable, IRequireGameLoop<float>
{
	#region Event

	private Action? onTimeout;
	public event Action OnTimeout
	{
		add {
			onTimeout += value;
			refCount++;
		}
		remove {
			onTimeout -= value;
			if (--refCount <= 0)
			{
				refCount = 0;
				Paused = true;
				Reset();
			}
		}
	}

	#endregion

	private int refCount;

	private float accumulator;

	public TimerSharp(bool autostart = false, bool repeat = false)
	{
		Repeat = repeat;
		if (autostart)
			Start();
	}

	public float Delay { get; set; } = 1f;
	public bool Paused { get; set; } = true;
	public bool Repeat { get; set; }

	public float Time => accumulator;
	public float Percentage => accumulator / Delay;

	public void Step(float delta)
	{
		if (Paused)
			return;
		accumulator += delta;
		if (accumulator > Delay)
		{
			onTimeout?.Invoke();
			if (Repeat)
				Reset();
			Paused = !Repeat;
		}
	}

	public void Reset() => accumulator = 0f;

	public void Start()
	{
		Reset();
		Paused = false;
	}
	public void Start(float delay)
	{
		Start();
		Delay = delay;
	}

	public void Close() => onTimeout = null;

}
