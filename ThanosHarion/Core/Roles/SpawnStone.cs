using System.Collections.Generic;
using UnityEngine;
using Harion.Utility.Utils;
using System.Linq;

namespace ThanosHarion.Core.Roles {
    public partial class Thanos {

        public Dictionary<GameObject, SystemTypes> Stones { get; set; } = new();

        public void InitStone() {
            Stones = new();
            foreach (StoneInformation StoneData in StoneInformation.StonesData) {
                if (!StoneData.IsActive)
                    continue;

                SetRandomPosition(StoneData.ModelTemplate(AmongUsClient.Instance.ClientId));
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
                
                SyncroStone(Stone);
            } while (!CorrectPositon);
        }

        private void SyncroStone(GameObject Stone) {

        }
    }
}
