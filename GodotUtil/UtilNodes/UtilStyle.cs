using Godot;

namespace Util;

public static class UtilStyle
{
	public static void StyleBoxSetContentMargin(this StyleBox box, float left, float top, float right, float bottom)
	{
		box.ContentMarginTop = top;
		box.ContentMarginRight = right;
		box.ContentMarginBottom = bottom;
		box.ContentMarginLeft = left;
	}
	public static void StyleBoxSetContentMargin(this StyleBox box, float vertical, float horizontal)
	{
		box.ContentMarginTop = box.ContentMarginBottom = vertical;
		box.ContentMarginRight = box.ContentMarginLeft = horizontal;
	}
	public static void StyleBoxSetContentMargin(this StyleBox box, float value) =>
		box.ContentMarginTop = box.ContentMarginBottom = box.ContentMarginRight = box.ContentMarginLeft = value;
}