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

        public string ObjectId;
        public SendOption sendMode = SendOption.None;
        public int OwnerId;

        public void OnDisable() {

        }

        public void OnEnable() {

        }

        public void OnDestroy() {

        }

        public void PlaceStone(string name, Vector2 Position, int OwnerId, ) {

        }
    }
}
