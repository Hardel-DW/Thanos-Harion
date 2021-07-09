using System.Collections.Generic;
using UnityEngine;
using Harion.Utility.Utils;
using System.Linq;
using Hazel;

namespace ThanosHarion.Core.Roles {
    public partial class Thanos {

        public Dictionary<GameObject, SystemTypes> Stones { get; set; } = new();

        public void InitStone() {
            Stones = new();
            foreach (StoneInformation StoneData in StoneInformation.StonesData) {
                if (!StoneData.IsActive)
                    continue;

                GameObject StoneObject = StoneData.CreateStone(AmongUsClient.Instance.ClientId);
                SetRandomPosition(StoneObject);
                SendStone(StoneObject, StoneData);
            }
        }

        private void ResetStonePoessession() => StoneInformation.StonesData.ForEach(stone => stone.HasStone = false);
        
        private void SetRandomPosition(GameObject Stone) {
            bool CorrectPositon = false;

            do {
                List<PlainShipRoom> Rooms = ShipStatus.Instance.AllRooms.ToList().Where(value => !Stones.Values.Any(room => room == value.RoomId)).ToList();
                if (Rooms == null)
                    break;

                PositionData Position = PositionUtils.GetRandomPositionByRoom(PositionUtils.GetCurrentMap(), Rooms.PickRandom().RoomId);
                if (Position == null)
                    continue;

                CorrectPositon = true;
                Stone.transform.localPosition = new Vector3(Position.position.x, Position.position.y);
                Stones.Add(Stone, Position.room);
            } while (!CorrectPositon);
        }

        private void SendStone(GameObject StoneObject, StoneInformation Data) {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SyncroStone, SendOption.None, -1);
            writer.Write(AmongUsClient.Instance.ClientId);
            writer.WriteVector3(StoneObject.transform.localPosition);
            writer.Write((byte) Data.StoneType);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}
