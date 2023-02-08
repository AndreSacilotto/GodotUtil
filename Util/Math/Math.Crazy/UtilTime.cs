using System.Runtime.CompilerServices;

namespace Util.MathCrazy
{
	public static class UtilTime
	{
		[MethodImpl(UtilShared.INLINE)] public static float NsToMs(float ms) => ms / 1e+6f;

		#region Milliseconds to

		[MethodImpl(UtilShared.INLINE)] public static float MsToNs(float ms) => ms * 1e+6f;

		/// <summary>Milisencods to Microsencond</summary>
		[MethodImpl(UtilShared.INLINE)] public static float MsToUs(float ms) => ms * 1000;

		[MethodImpl(UtilShared.INLINE)] public static float MsToSec(float ms) => ms / 1000f;

		#endregion

		#region Seconds to

		[MethodImpl(UtilShared.INLINE)] public static float SecToMs(float sec) => sec * 1000f;
		[MethodImpl(UtilShared.INLINE)] public static float SecToMinute(float sec) => sec / 60f;
		[MethodImpl(UtilShared.INLINE)] public static float SecToHour(float sec) => sec / 3600f;

		#endregion

		#region Minute to

		[MethodImpl(UtilShared.INLINE)] public static float MinuteToSec(float min) => min * 60f;
		[MethodImpl(UtilShared.INLINE)] public static float MinuteToHour(float min) => min / 60f;

		#endregion

		#region Hour to

		[MethodImpl(UtilShared.INLINE)] public static float HourToSec(float hour) => hour * 3600f;
		[MethodImpl(UtilShared.INLINE)] public static float HourToMinute(float hour) => hour * 60f;
		[MethodImpl(UtilShared.INLINE)] public static float HourToDays(float hour) => hour / 24f;

		#endregion

		#region Days to

		[MethodImpl(UtilShared.INLINE)] public static float DaysToHour(float days) => days * 24f;

		#endregion


	}
}