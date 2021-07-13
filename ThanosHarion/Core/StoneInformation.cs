using Harion.ArrowManagement;
using Harion.CustomRoles;
using Harion.Utility;
using Harion.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using ThanosHarion.Core.Roles;
using UnityEngine;
using Object = UnityEngine.Object;
using ThanosRole = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core {
    public class StoneInformation {

        public static readonly List<StoneInformation> StonesData = new() {
            new StoneInformation(StoneData.Mind, "Mind", ResourceLoader.MindStoneSprite, StonePickup.Thanos, false, StoneVisibility.Nobody, MindRole.Instance, false),
            new StoneInformation(StoneData.Power, "Power", ResourceLoader.PowerStoneSprite, StonePickup.Thanos, false, StoneVisibility.Nobody, PowerRole.Instance, false),
            new StoneInformation(StoneData.Reality, "Reality", ResourceLoader.RealityStoneSprite, StonePickup.Thanos, false, StoneVisibility.Nobody, RealityRole.Instance, false),
            new StoneInformation(StoneData.Soul, "Soul", ResourceLoader.SoulStoneSprite, StonePickup.Everyone, true, StoneVisibility.Crewmate, SoulRole.Instance, true),
            new StoneInformation(StoneData.Space, "Space", ResourceLoader.SpaceStoneSprite, StonePickup.Thanos, false, StoneVisibility.Nobody, SpaceRole.Instance, false),
            new StoneInformation(StoneData.Time, "Time", ResourceLoader.TimeStoneSprite, StonePickup.Thanos, false, StoneVisibility.Nobody, TimeRole.Instance, false),
        };

        public readonly StoneData StoneType;
        public readonly StonePickup StonePickuBy;
        public readonly string Name;
        public readonly Sprite Texture;
        public readonly Action<PlayerControl> OnPickup;
        public readonly bool HasArrow;
        public readonly RoleManager StoneRole;
        public readonly bool EnableStoneRole;
        private List<PlayerControl> PlayerCanSeeStone = new();
        private List<PlayerControl> PlayerCanSeeArrow = new();

        public ArrowManager Arrow { get; private set; }

        public StoneVisibility ArrowVisibleBy { get; set; }

        public GameObject StoneObject { get; set; } = null;

        public bool IsActive { get; set; } = true;

        private StoneVisibility _Visibility;

        public StoneVisibility Visibility { 
            get => _Visibility;
            set {
                _Visibility = value;
                PlayerCanSeeStone = GetListVisibility();
                StoneObject.GetComponent<SpriteRenderer>().enabled = PlayerCanSeeStone.ContainsPlayer(PlayerControl.LocalPlayer);
            }
        }

        public bool HasStone { get; set; } = false;

        public StoneInformation(StoneData stoneType, string name, Sprite texture, StonePickup stonePickuBy, bool hasArrow, StoneVisibility arrowVisibleBy, RoleManager role, bool enableRole) {
            StoneType = stoneType;
            Name = name;
            Texture = texture;
            StonePickuBy = stonePickuBy;
            HasArrow = hasArrow;
            ArrowVisibleBy = arrowVisibleBy;
            StoneRole = role;
            EnableStoneRole = enableRole;
            OnPickup = (PlayerControl Player) => OnPickupAction(Player);
        }

        private void OnPickupAction(PlayerControl Player) {
            if (Player.PlayerId != PlayerControl.LocalPlayer.PlayerId)
                return;

            HasStone = true;
            Object.Destroy(Arrow.Arrow);
            Arrow = null;
            ThanosRole.Instance.Stones.Remove(StoneType);

            if (EnableStoneRole)
                StoneRole.RpcAddPlayer(Player);
        }

        public GameObject CreateStone(int OwnerId, Vector2? Position = null) {
            PlayerCanSeeStone = GetListVisibility();
            PlayerCanSeeArrow = GetListArrowVisibility();

            Position ??= new Vector2(100f, 100f);

            GameObject Stone = new GameObject();
            Stone.name = Name;
            Stone.transform.localPosition = new Vector3(Position.Value.x, Position.Value.y, 0f);
            Stone.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            Stone.SetActive(true);

            SpriteRenderer renderer = Stone.AddComponent<SpriteRenderer>();
            renderer.sprite = Texture;
            renderer.enabled = PlayerCanSeeStone.ContainsPlayer(PlayerControl.LocalPlayer);

            PickupObject Pickup = Stone.AddComponent<PickupObject>();
            Pickup.OnPickup = OnPickup;
            Pickup.PlayersCanPickup = GetListPickupPlayer();

            SyncroStone Syncro = Stone.AddComponent<SyncroStone>();
            Syncro.ObjectId = StoneType;
            Syncro.OwnerId = OwnerId;

            BoxCollider2D collider = Stone.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;

            StoneObject = Stone;
            Arrow = new ArrowManager(StoneObject, true, 0f);
            Arrow.Arrow.SetActive(PlayerCanSeeArrow.ContainsPlayer(PlayerControl.LocalPlayer) && HasArrow);
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay)
                Arrow.Arrow.SetActive(true);

            return StoneObject;
        }
        
        private List<PlayerControl> GetListPickupPlayer() => StonePickuBy switch {
            StonePickup.Everyone => PlayerControl.AllPlayerControls.ToArray().ToList(),
            StonePickup.Thanos => ThanosRole.Instance.AllPlayers,
            StonePickup.Crewmate => PlayerControlUtils.GetCrewmate(),
            _ => new()
        };

        private List<PlayerControl> GetListVisibility() => Visibility switch {
            StoneVisibility.Everyone => PlayerControl.AllPlayerControls.ToArray().ToList(),
            StoneVisibility.Crewmate => PlayerControlUtils.GetCrewmate(),
            StoneVisibility.Thanos => ThanosRole.Instance.AllPlayers,
            _ => new()
        };

        private List<PlayerControl> GetListArrowVisibility() => ArrowVisibleBy switch {
            StoneVisibility.Everyone => PlayerControl.AllPlayerControls.ToArray().ToList(),
            StoneVisibility.Crewmate => PlayerControlUtils.GetCrewmate(),
            StoneVisibility.Thanos => ThanosRole.Instance.AllPlayers,
            _ => new()
        };

        public static StoneInformation GetStoneData(StoneData Data) => StonesData.FirstOrDefault(stone => stone.StoneType == Data);

        public static void ReadSyncroData(StoneData data, Vector3 Position, int OwnerId) {
            StoneInformation StoneInfo = GetStoneData(data);
            StoneInfo.CreateStone(OwnerId, Position);
        }

        public static void ReadDestroyData(StoneData data) {
            StoneInformation StoneInfo = GetStoneData(data);

            if (StoneInfo.StoneObject != null) {
                Object.Destroy(StoneInfo.StoneObject);
                Object.Destroy(StoneInfo.Arrow.Arrow);
                StoneInfo.Arrow = null;
            }
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
        Crewmate,
        Thanos,
        Nobody
    }

    public enum StonePickup {
        Everyone,
        Thanos,
        Crewmate
    }
}
