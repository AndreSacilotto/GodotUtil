using Godot;

namespace Util.GDNode
{
	public enum NodeProcessMode
	{
		None,
		Idle,
		Physics,
		Both = Idle | Physics,
	}

	[System.Flags]
	public enum NodeProcessesFlags
	{
		None = 0,
		Idle = 1 << 0,
		IdleInternal = 1 << 1,
		Physics = 1 << 2,
		PhysicsInternal = 1 << 3,
		Input = 1 << 4,
		UnhandledInput = 1 << 5,
		UnhandledKeyInput = 1 << 6,
	}

	public static class NodeProcess
	{
		public static NodeProcessMode GetProcessMode(Node nd)
		{
			var p = nd.IsProcessing();
			var pp = nd.IsPhysicsProcessing();
			if (p && pp)
				return NodeProcessMode.Both;
			if (p)
				return NodeProcessMode.Idle;
			if (pp)
				return NodeProcessMode.Physics;
			return NodeProcessMode.None;
		}

		public static void SetProcessMode(Node nd, NodeProcessMode mode, bool value)
		{
			var b = mode == NodeProcessMode.Both;
			if (b || mode == NodeProcessMode.Idle)
				nd.SetProcess(value);
			if (b || mode == NodeProcessMode.Physics)
				nd.SetPhysicsProcess(value);
		}
		public static void SetProcessMode(Node nd, NodeProcessMode mode)
		{
			var b = mode == NodeProcessMode.Both;
			nd.SetProcess(b || mode == NodeProcessMode.Idle);
			nd.SetPhysicsProcess(b || mode == NodeProcessMode.Physics);
		}

		#region Flags

		public static void SetProcessModeFlags(Node nd, NodeProcessesFlags flags)
		{
			nd.SetProcess(flags.HasFlag(NodeProcessesFlags.Idle));
			nd.SetProcessInternal(flags.HasFlag(NodeProcessesFlags.IdleInternal));
			nd.SetPhysicsProcess(flags.HasFlag(NodeProcessesFlags.Physics));
			nd.SetPhysicsProcessInternal(flags.HasFlag(NodeProcessesFlags.PhysicsInternal));
			nd.SetProcessInput(flags.HasFlag(NodeProcessesFlags.Input));
			nd.SetProcessUnhandledInput(flags.HasFlag(NodeProcessesFlags.UnhandledInput));
			nd.SetProcessUnhandledKeyInput(flags.HasFlag(NodeProcessesFlags.UnhandledKeyInput));
		}

		public static void SetProcessModeFlags(Node nd, NodeProcessesFlags flags, bool value)
		{
			if (flags.HasFlag(NodeProcessesFlags.Idle))
				nd.SetProcess(value);
			if (flags.HasFlag(NodeProcessesFlags.IdleInternal))
				nd.SetProcessInternal(value);
			if (flags.HasFlag(NodeProcessesFlags.Physics))
				nd.SetPhysicsProcess(value);
			if (flags.HasFlag(NodeProcessesFlags.PhysicsInternal))
				nd.SetPhysicsProcessInternal(value);
			if (flags.HasFlag(NodeProcessesFlags.Input))
				nd.SetProcessInput(value);
			if (flags.HasFlag(NodeProcessesFlags.UnhandledInput))
				nd.SetProcessUnhandledInput(value);
			if (flags.HasFlag(NodeProcessesFlags.UnhandledKeyInput))
				nd.SetProcessUnhandledKeyInput(value);
		}

		public static NodeProcessesFlags GetProcessModeFlags(Node nd)
		{
			NodeProcessesFlags flags = NodeProcessesFlags.None;
			if (nd.IsProcessing())
				flags |= NodeProcessesFlags.Idle;
			if (nd.IsProcessingInternal())
				flags |= NodeProcessesFlags.IdleInternal;
			if (nd.IsPhysicsProcessing())
				flags |= NodeProcessesFlags.Physics;
			if (nd.IsPhysicsProcessingInternal())
				flags |= NodeProcessesFlags.PhysicsInternal;
			if (nd.IsProcessingInput())
				flags |= NodeProcessesFlags.Input;
			if (nd.IsProcessingUnhandledInput())
				flags |= NodeProcessesFlags.UnhandledInput;
			if (nd.IsProcessingUnhandledKeyInput())
				flags |= NodeProcessesFlags.UnhandledKeyInput;
			return flags;
		}

		#endregion

	}

}