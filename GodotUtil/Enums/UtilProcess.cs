using Godot;

namespace Util;

public static class UtilProcess
{
	[System.Flags]
	public enum Mode : byte
	{
		None = 0,
		Idle,
		Physics,
		Both = Idle | Physics,
	}

	#region Modes

	public static Mode GetProcessMode(Node nd)
	{
		var p = nd.IsProcessing();
		var pp = nd.IsPhysicsProcessing();
		if (p && pp)
			return Mode.Both;
		if (p)
			return Mode.Idle;
		if (pp)
			return Mode.Physics;
		return Mode.None;
	}

	public static Mode GetProcessModeInternal(Node nd)
	{
		var p = nd.IsProcessingInternal();
		var pp = nd.IsPhysicsProcessingInternal();
		if (p && pp)
			return Mode.Both;
		if (p)
			return Mode.Idle;
		if (pp)
			return Mode.Physics;
		return Mode.None;
	}

	public static void SetProcessMode(Node nd, Mode mode)
	{
		nd.SetProcess(mode.HasFlag(Mode.Idle));
		nd.SetPhysicsProcess(mode.HasFlag(Mode.Physics));
	}

	public static void SetProcessModeInternal(Node nd, Mode mode)
	{
		nd.SetProcessInternal(mode.HasFlag(Mode.Idle));
		nd.SetPhysicsProcessInternal(mode.HasFlag(Mode.Physics));
	}

	#endregion

}