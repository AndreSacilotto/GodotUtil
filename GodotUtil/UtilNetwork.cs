using System;
using Godot;
using static Godot.NetworkedMultiplayerENet;
using static Godot.NetworkedMultiplayerPeer;

namespace Util
{
    public static class UtilNetwork
    {
        public const int MAX_PORT = 65535;
        public const int SERVER_ID = 1;

        public static void SeparateFullAddress(string fullAddress, out string address, out int port, char separator = ':')
        {
            var s = fullAddress.Trim().Split(separator);
            address = s[1];
            port = s[2].ToInt();
        }

        public static string MakeFullAddress(string address, int port, char separator = ':')
        {
            return address + separator + port;
        }

        public static NetworkedMultiplayerENet CreateENetPeer(bool refuseConnections = false, TransferModeEnum transferMode = TransferModeEnum.Reliable, bool ordered = false, int channels = 3, CompressionModeEnum compression = CompressionModeEnum.RangeCoder, bool relay = true, int transferChannel = -1, string dtlsHostname = "", bool dtlsVerify = true, bool dtlsUse = false) => new NetworkedMultiplayerENet
        {
            RefuseNewConnections = refuseConnections,
            TransferMode = transferMode,
            AlwaysOrdered = ordered,
            ChannelCount = channels,
            CompressionMode = compression,
            ServerRelay = relay,
            TransferChannel = transferChannel,
            DtlsHostname = dtlsHostname,
            DtlsVerify = dtlsVerify,
            UseDtls = dtlsUse,
        };

        public static NetworkedMultiplayerCustom NetworkPeerCustom(this SceneTree tree) => tree.NetworkPeer as NetworkedMultiplayerCustom;
        public static NetworkedMultiplayerENet NetworkPeerENet(this SceneTree tree) => tree.NetworkPeer as NetworkedMultiplayerENet;
        public static WebRTCMultiplayer NetworkPeerWebRTC(this SceneTree tree) => tree.NetworkPeer as WebRTCMultiplayer;
        public static WebSocketMultiplayerPeer NetworkPeerWebSocket(this SceneTree tree) => tree.NetworkPeer as WebSocketMultiplayerPeer;

        public static void CloseENet(SceneTree tree)
        {
            (tree.NetworkPeer as NetworkedMultiplayerENet).CloseConnection();
            tree.NetworkPeer = null;
        }
        public static void CloseWebRTC(SceneTree tree)
        {
            (tree.NetworkPeer as WebRTCMultiplayer).Close();
            tree.NetworkPeer = null;
        }

        public static int[] GetPeersWithServer(SceneTree tree)
        {
            var peers = tree.GetNetworkConnectedPeers();
            var peersPlus = new int[peers.Length + 1];
            Array.Copy(peers, 0, peersPlus, 1, peers.Length);
            peersPlus[0] = tree.GetNetworkUniqueId();
            return peersPlus;
        }

        public static string[] GetIPs()
        {
            return UtilGD.GodotArray<string>(IP.GetLocalAddresses());
        }

        public static void OpenTerminals(int count, string godot, string project, string scene)
        {
            var args = new string[] { "--path", project, scene };
            for (int i = 0; i < count; i++)
                OS.Execute(godot, args, false);
        }        
        
        public static void OpenTerminals(int count, string scene = "")
        {
            var godotPath = OS.GetExecutablePath();
            var projectPath = ProjectSettings.GlobalizePath("res://");
            if(string.IsNullOrWhiteSpace(scene))
                scene = (string)ProjectSettings.GetSetting("application/run/main_scene");
            OpenTerminals(count, godotPath, projectPath, scene);
        }


    }
}