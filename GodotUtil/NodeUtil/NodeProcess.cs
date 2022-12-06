using Godot;

namespace Util.GDNode
{

	public static class NodeProcess
	{
		public enum NodeProcessMode
		{
			None,
			Idle,
			Physics,
			Both = Idle | Physics,
		}

		[System.Flags]
		public enum NodeProcessModes
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

		public static void SetProcessModeFlags(Node nd, NodeProcessModes flags)
		{
			nd.SetProcess(flags.HasFlag(NodeProcessModes.Idle));
			nd.SetProcessInternal(flags.HasFlag(NodeProcessModes.IdleInternal));
			nd.SetPhysicsProcess(flags.HasFlag(NodeProcessModes.Physics));
			nd.SetPhysicsProcessInternal(flags.HasFlag(NodeProcessModes.PhysicsInternal));
			nd.SetProcessInput(flags.HasFlag(NodeProcessModes.Input));
			nd.SetProcessUnhandledInput(flags.HasFlag(NodeProcessModes.UnhandledInput));
			nd.SetProcessUnhandledKeyInput(flags.HasFlag(NodeProcessModes.UnhandledKeyInput));
		}

		public static void SetProcessModeFlags(Node nd, NodeProcessModes flags, bool value)
		{
			if (flags.HasFlag(NodeProcessModes.Idle))
				nd.SetProcess(value);
			if (flags.HasFlag(NodeProcessModes.IdleInternal))
				nd.SetProcessInternal(value);
			if (flags.HasFlag(NodeProcessModes.Physics))
				nd.SetPhysicsProcess(value);
			if (flags.HasFlag(NodeProcessModes.PhysicsInternal))
				nd.SetPhysicsProcessInternal(value);
			if (flags.HasFlag(NodeProcessModes.Input))
				nd.SetProcessInput(value);
			if (flags.HasFlag(NodeProcessModes.UnhandledInput))
				nd.SetProcessUnhandledInput(value);
			if (flags.HasFlag(NodeProcessModes.UnhandledKeyInput))
				nd.SetProcessUnhandledKeyInput(value);
		}

		public static NodeProcessModes GetProcessModeFlags(Node nd)
		{
			NodeProcessModes flags = NodeProcessModes.None;
			if (nd.IsProcessing())
				flags |= NodeProcessModes.Idle;
			if (nd.IsProcessingInternal())
				flags |= NodeProcessModes.IdleInternal;
			if (nd.IsPhysicsProcessing())
				flags |= NodeProcessModes.Physics;
			if (nd.IsPhysicsProcessingInternal())
				flags |= NodeProcessModes.PhysicsInternal;
			if (nd.IsProcessingInput())
				flags |= NodeProcessModes.Input;
			if (nd.IsProcessingUnhandledInput())
				flags |= NodeProcessModes.UnhandledInput;
			if (nd.IsProcessingUnhandledKeyInput())
				flags |= NodeProcessModes.UnhandledKeyInput;
			return flags;
		}

		#endregion

	}

}