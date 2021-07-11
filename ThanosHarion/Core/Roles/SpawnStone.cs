using System.Collections.Generic;
using UnityEngine;
using Harion.Utility.Utils;
using System.Linq;
using Hazel;

namespace ThanosHarion.Core.Roles {
    public partial class Thanos {

        public Dictionary<StoneData, SystemTypes> Stones { get; set; } = new();

        private void InitStone() {
            foreach (StoneInformation StoneData in StoneInformation.StonesData) {
                if (!StoneData.IsActive)
                    continue;

                GameObject StoneObject = StoneData.CreateStone(AmongUsClient.Instance.ClientId);
                PositionData PositionData = SetRandomPosition(StoneObject, StoneData.StoneType);
                SendStone(StoneObject, StoneData, PositionData);
            }
        }

        private void ResetStonePoessession() {
            Stones = new();
            StoneInformation.StonesData.ForEach(stone => stone.HasStone = false);
        }

        internal void SendStone(GameObject StoneObject, StoneInformation Data, PositionData PositionData) {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.SyncroStone, SendOption.None, -1);
            writer.Write(AmongUsClient.Instance.ClientId);
            writer.WriteVector3(StoneObject.transform.localPosition);
            writer.Write((byte) Data.StoneType);
            writer.Write((byte) PositionData.room);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }

        internal PositionData SetRandomPosition(GameObject Stone, StoneData stoneType) {
            bool CorrectPositon = false;
            PositionData Room = null;

            do {
                List<PlainShipRoom> Rooms = ShipStatus.Instance.AllRooms.ToList().Where(value => !Stones.Values.Any(room => room == value.RoomId)).ToList();
                if (Rooms == null)
                    break;

                PositionData Position = PositionUtils.GetRandomPositionByRoom(PositionUtils.GetCurrentMap(), Rooms.PickRandom().RoomId);
                if (Position == null)
                    continue;

                CorrectPositon = true;
                Stone.transform.localPosition = new Vector3(Position.position.x, Position.position.y);
                Stones.Add(stoneType, Position.room);
                Room = Position;
            } while (!CorrectPositon);

            return Room;
        }
    }
}
