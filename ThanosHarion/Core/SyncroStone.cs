using Harion.Reactor;
using Hazel;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ThanosHarion.Core {

    [RegisterInIl2Cpp]
    public class SyncroStone : MonoBehaviour {

        public SyncroStone(IntPtr ptr) : base(ptr) { }

        [HideFromIl2Cpp]
        public bool AmOwner {
            get => OwnerId == AmongUsClient.Instance.ClientId;
        }

        [HideFromIl2Cpp]
        public StoneData ObjectId { get; set; }

        [HideFromIl2Cpp]
        public int OwnerId { get; set; }

        public void OnDestroy() {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SyncroDestroy, SendOption.None, -1);
            writer.Write((byte) ObjectId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}
