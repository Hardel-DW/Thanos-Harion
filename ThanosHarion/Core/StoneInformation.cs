using Harion.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThanosHarion.Core {
    public class StoneInformation {

        public static readonly List<StoneInformation> StonesData = new() {
            new StoneInformation(StoneData.Mind, "Mind", ResourceLoader.MindStoneSprite, StonePickup.Thanos),
            new StoneInformation(StoneData.Power, "Power", ResourceLoader.PowerStoneSprite, StonePickup.Thanos),
            new StoneInformation(StoneData.Reality, "Reality", ResourceLoader.RealityStoneSprite, StonePickup.Thanos),
            new StoneInformation(StoneData.Soul, "Soul", ResourceLoader.SoulStoneSprite, StonePickup.Everyone),
            new StoneInformation(StoneData.Space, "Space", ResourceLoader.SpaceStoneSprite, StonePickup.Thanos),
            new StoneInformation(StoneData.Time, "Time", ResourceLoader.TimeStoneSprite, StonePickup.Thanos),
        };

        public readonly StoneData StoneType;
        public readonly StoneVisibility StoneVisibilty;
        public readonly StonePickup StonePickuBy;
        public readonly string Name;
        public readonly Sprite Texture;
        public readonly Action<PlayerControl> OnPickup;

        public bool IsActive { get; set; } = true;

        public StoneVisibility Visibility { get; set; }

        public bool HasStone { get; set; } = false;

        public StoneInformation(StoneData stoneType, string name, Sprite texture, StonePickup stonePickuBy) {
            StoneType = stoneType;
            Name = name;
            Texture = texture;
            StonePickuBy = stonePickuBy;
            OnPickup = (PlayerControl Player) => HasStone = true;
        }

        public GameObject CreateStone(int OwnerId, Vector2? Position = null) {    
            Position ??= new Vector2(100f, 100f);

            GameObject Stone = new GameObject();
            Stone.name = Name;
            Stone.transform.localPosition = new Vector3(Position.Value.x, Position.Value.y, 0f);
            Stone.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            Stone.SetActive(true);

            SpriteRenderer renderer = Stone.AddComponent<SpriteRenderer>();
            renderer.sprite = Texture;

            PickupObject Pickup = Stone.AddComponent<PickupObject>();
            SyncroStone Syncro = Stone.AddComponent<SyncroStone>();
            Syncro.ObjectId = StoneType;
            Syncro.OwnerId = OwnerId;

            BoxCollider2D collider = Stone.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;

            return Stone;
        }
        
        public static StoneInformation GetStoneData(StoneData Data) => StonesData.FirstOrDefault(stone => stone.StoneType == Data);

        public static void ReadSyncroData(StoneData data, Vector3 Position, int OwnerId) {
            StoneInformation StoneInfo = GetStoneData(data);
            StoneInfo.CreateStone(OwnerId, Position);
        }
    }

    public enum StoneData : byte {
        Reality,
        Space,
        Time,
        Soul,
        Power,
        Mind
    }

    public enum StoneVisibility {
        Everyone,
        Thanos,
        Crewmate,
        Nobody
    }

    public enum StonePickup {
        Everyone,
        Thanos,
        Crewmate
    }
}
