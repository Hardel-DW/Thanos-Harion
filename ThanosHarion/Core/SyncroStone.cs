using Harion.Reactor;
using Hazel;
using System;
using UnityEngine;

namespace ThanosHarion.Core {

    [RegisterInIl2Cpp]
    public class SyncroStone : MonoBehaviour {

        public SyncroStone(IntPtr ptr) : base(ptr) { }

        public bool AmOwner {
            get => OwnerId == AmongUsClient.Instance.ClientId;
        }

        public StoneData ObjectId;
        public int OwnerId;

        public void OnDestroy() {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SyncroDestroy, SendOption.None, -1);
            writer.Write((byte) ObjectId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}
