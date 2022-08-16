﻿using Godot;
using static Godot.NetworkedMultiplayerENet;

namespace Util
{
    public static class UtilNetwork
    {

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

        public static NetworkedMultiplayerENet CreateENetPeer(bool ordered = false, int channels = 3, CompressionModeEnum compression = CompressionModeEnum.RangeCoder, bool relay = true, int transferChannel = -1, string dtlsHostname = "", bool dtlsVerify = true, bool dtlsUse = false) => new NetworkedMultiplayerENet
        {
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

        public static string[] GetIPs()
        {
            return UtilGD.GodotArray<string>(IP.GetLocalAddresses());
        }

    }
}