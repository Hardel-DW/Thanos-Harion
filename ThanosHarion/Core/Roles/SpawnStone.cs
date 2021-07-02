using System.Collections.Generic;
using UnityEngine;
using ThanosHarion.Core.Buttons;
using Harion.Utility;
using Harion.Utility.Utils;
using Harion.Utility.Helper;
using System.Linq;
using Random = System.Random;

namespace ThanosHarion.Core.Roles {
    public partial class Thanos {

        private static Random random = new Random();
        public List<GameObject> Stones { get; set; } = new();

        public void InitStone() {
            Stones = new();
            PlaceRealityStone();
            PlaceMindStone();
            PlaceSpaceStone();
            PlaceTimeStone();
            PlacePowerStone();
            PlaceSoulStone();
        }

        private GameObject ModelStone(string name, Sprite sprite) {
            GameObject Stone = new GameObject();
            Stone.name = name;
            Stone.transform.localPosition = new Vector3(100f, 100f, 0f);
            Stone.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            Stone.SetActive(true);

            SpriteRenderer renderer = Stone.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;

            PickupObject Pickup = Stone.AddComponent<PickupObject>();
            BoxCollider2D collider = Stone.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;

            return Stone;
        }

        private void PlaceRealityStone() {
            Sprite sprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.reality.png", 300f);
            GameObject Stone = ModelStone("Reality", sprite);
            PickupObject pickup = Stone.GetComponent<PickupObject>();
            pickup.OnPickup = () => RealityButton.Instance.HasStone = true;
            Stones.Add(Stone);
        }

        private void PlaceMindStone() {
            Sprite sprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.mind.png", 300f);
            GameObject Stone = ModelStone("Mind", sprite);
            PickupObject pickup = Stone.GetComponent<PickupObject>();
            pickup.OnPickup = () => MindButton.Instance.HasStone = true;
            Stones.Add(Stone);
        }

        private void PlaceSpaceStone() {
            Sprite sprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.space.png", 300f);
            GameObject Stone = ModelStone("Space", sprite);
            PickupObject pickup = Stone.GetComponent<PickupObject>();
            pickup.OnPickup = () => SpaceButton.Instance.HasStone = true;
            Stones.Add(Stone);
        }

        private void PlaceTimeStone() {
            Sprite sprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.time.png", 300f);
            GameObject Stone = ModelStone("Time", sprite);
            PickupObject pickup = Stone.GetComponent<PickupObject>();
            pickup.OnPickup = () => TimeButton.Instance.HasStone = true;
            Stones.Add(Stone);
        }

        private void PlacePowerStone() {
            Sprite sprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.power.png", 300f);
            GameObject Stone = ModelStone("Power", sprite);
            PickupObject pickup = Stone.GetComponent<PickupObject>();
            pickup.OnPickup = () => PowerButton.Instance.HasStone = true;
            Stones.Add(Stone);
        }

        private void PlaceSoulStone() {
            Sprite sprite = SpriteHelper.LoadSpriteFromEmbeddedResources("ThanosHarion.Resources.soul.png", 300f);
            GameObject Stone = ModelStone("Soul", sprite);
            PickupObject pickup = Stone.GetComponent<PickupObject>();
            pickup.OnPickup = () => SoulButton.Instance.HasStone = true;
            pickup.PlayersCanPickup = PlayerControl.AllPlayerControls.ToArray().ToList();
            Stones.Add(Stone);
        }

        private void ResetStonePoessession() {
            MindButton.Instance.HasStone = false;
            RealityButton.Instance.HasStone = false;
            PowerButton.Instance.HasStone = false;
            SpaceButton.Instance.HasStone = false;
            TimeButton.Instance.HasStone = false;
            SoulButton.Instance.HasStone = false;
        }

        private Vector2? GetRandomPosition() {
            List<PlainShipRoom> Rooms = ShipStatus.Instance.AllRooms.ToArray().ToList();
            int maxTurn = 0;
            bool CorrectPositon = false;
            Vector2? ValueReturn = null;

            do {
                maxTurn++;
                PlainShipRoom room = Rooms.PickRandom();
                Vector2 RandomPositionInRoom = (Vector2) room.transform.localPosition + new Vector2(UnityEngine.Random.Range(room.roomArea.bounds.min.x, room.roomArea.bounds.max.x), UnityEngine.Random.Range(room.roomArea.bounds.min.y, room.roomArea.bounds.max.y));
                bool InRoom = room.roomArea.OverlapPoint(RandomPositionInRoom);
                if (!InRoom)
                    continue;

                UnhollowerBaseLib.Il2CppReferenceArray<Collider2D> Collider = Physics2D.OverlapAreaAll(RandomPositionInRoom += Vector2.one, RandomPositionInRoom -= Vector2.one);
                if (Collider.Count > 0)
                    continue;

                ValueReturn = RandomPositionInRoom;
                CorrectPositon = true;
            } while (maxTurn < 100 && CorrectPositon == false);

            return ValueReturn;
        }
    }
}
