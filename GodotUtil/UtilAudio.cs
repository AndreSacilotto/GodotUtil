using Godot;
using System.Threading.Tasks;
using Util;

namespace GodotUtil;

public static class UtilAudio
{
    //public const float PRESSURE = 30f; // Godot mute by default mute at -60db

    public static float PRESSURE { get; } = (float)Math.Abs(ProjectSettings.GetSetting("audio/buses/channel_disable_threshold_db").AsDouble() / 2d);

    public static float DecibelToPercent(float dB) => MathF.Pow(10f, dB / PRESSURE);

    public static float PercentToDecibel(float percent) => percent != 0 ? PRESSURE * MathF.Log10(percent) : PRESSURE * -2.5f;

    #region AudioStreamPlayer Extension
    public static T GetAudioStream<T>(this AudioStreamPlayer player) where T : AudioStream => (T)player.Stream;
    public static void SetVolume(this AudioStreamPlayer player, float percent)
    {
        player.VolumeDb = PercentToDecibel(percent);
    }
    public static double GetAudioLength(this AudioStreamPlayer player)
    {
        var audio = player.Stream;
        if (audio == null)
            return -1d;
        return audio.GetLength();
    }
    public static void Play(this AudioStreamPlayer player, AudioStream audio)
    {
        player.Stream = audio;
        player.Play();
    }
    public static async Task PlayAndWait(this AudioStreamPlayer player)
    {
        player.Play();
        await player.ToSignal(player, AudioStreamPlayer.SignalName.Finished);
    }
    public static async Task PlayAndWait(this AudioStreamPlayer player, AudioStream audio)
    {
        player.Stream = audio;
        await player.PlayAndWait();
    }
    #endregion

    #region AudioStreamPlayer2D Extension
    public static T GetAudioStream<T>(this AudioStreamPlayer2D player) where T : AudioStream => (T)player.Stream;
    public static void SetVolume(this AudioStreamPlayer2D player, float percent)
    {
        player.VolumeDb = PercentToDecibel(percent);
    }
    public static double GetAudioLength(this AudioStreamPlayer2D player)
    {
        var audio = player.Stream;
        if (audio == null)
            return -1d;
        return audio.GetLength();
    }
    public static void Play(this AudioStreamPlayer2D player, AudioStream audio)
    {
        player.Stream = audio;
        player.Play();
    }
    public static async Task PlayAndWait(this AudioStreamPlayer2D player)
    {
        player.Play();
        await player.ToSignal(player, AudioStreamPlayer2D.SignalName.Finished);
    }
    public static async Task PlayAndWait(this AudioStreamPlayer2D player, AudioStream audio)
    {
        player.Stream = audio;
        await player.PlayAndWait();
    }
    #endregion

    #region AudioStreamPlayer3D Extension
    public static T GetAudioStream<T>(this AudioStreamPlayer3D player) where T : AudioStream => (T)player.Stream;
    public static void SetVolume(this AudioStreamPlayer3D player, float percent)
    {
        player.VolumeDb = PercentToDecibel(percent);
    }
    public static double GetAudioLength(this AudioStreamPlayer3D player)
    {
        var audio = player.Stream;
        if (audio == null)
            return -1d;
        return audio.GetLength();
    }
    public static void Play(this AudioStreamPlayer3D player, AudioStream audio)
    {
        player.Stream = audio;
        player.Play();
    }
    public static async Task PlayAndWait(this AudioStreamPlayer3D player)
    {
        player.Play();
        await player.ToSignal(player, AudioStreamPlayer3D.SignalName.Finished);
    }
    public static async Task PlayAndWait(this AudioStreamPlayer3D player, AudioStream audio)
    {
        player.Stream = audio;
        await player.PlayAndWait();
    }
    #endregion

    public static double GetLoopOffset(AudioStream stream) 
    {
        if (stream is AudioStreamOggVorbis ogg)
            return ogg.LoopOffset;
        if (stream is AudioStreamMP3 mp3)
            return mp3.LoopOffset;
        if (stream is AudioStreamWav wav)
            return wav.LoopBegin;
        return -1d;
    }

}