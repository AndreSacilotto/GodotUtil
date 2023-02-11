namespace Util;

/// <summary>Used by the game by classes to pause the main loop invocation</summary>
public interface IPausable
{
	/// <summary>Prop that can be used to pause the loop execution</summary>
	public bool Paused { get; set; }
}
