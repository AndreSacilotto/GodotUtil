namespace Util.Interpolation;

public abstract class TweenerSharpBase : IClosable, IRequireGameLoop<float>
{
	public event Action<TweenerSharpBase>? OnTweenerEnd;

	/// <summary>Time of the tweener animation</summary>
	public float Duration { get; set; } = 1f;

	/// <summary>Time spent doing the tweener animation</summary>
	public float Accumulator { get; set; } = 0f;

	protected bool complete;

	/// <summary>True if the tweener has completed his animation, false otherwise</summary>
	public bool Complete => complete;

	/// <summary>Reset the tweener to be able to play again</summary>
	public void Reset()
	{
		Accumulator = 0f;
		complete = false;
	}

	protected void TryEnd()
	{
		if (Accumulator > Duration)
		{
			OnTweenerEnd?.Invoke(this);
			complete = true;
		}
	}

	public abstract void Step(float delta);

	public virtual void Close()
	{
		OnTweenerEnd = null;
	}

}
