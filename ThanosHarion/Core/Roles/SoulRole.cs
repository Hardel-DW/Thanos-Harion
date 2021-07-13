using Harion.CustomRoles;
using Harion.Enumerations;
using Harion.Utility.Utils;
using UnityEngine;

namespace ThanosHarion.Core.Roles {

    [RegisterInCustomRoles(typeof(SoulRole))]
    public partial class SoulRole : CustomRole<SoulRole> {
        // Color: #A88932FF
        public SoulRole() : base() {
            NumberPlayers = 0;
            RoleActive = true;
            IsMainRole = false;
            ShowIntroCutScene = false;
            Team = Team.Everyone;
            TeamFolder = FreeplayFolder.Undefined;
            GiveRoleAt = Moment.Never;
            GiveTasksAt = Moment.Never;
            RoleVisibleBy = VisibleBy.Everyone;
            Color = new Color(0.658f, 0.537f, 0.196f, 1f);
            Name = "Possessor of soul stone";
            TasksDescription = "<color=#A88932FF>Objective: You have the soul stone,\n you must keep it as long as\npossible to prevent Thanos from collecting\n all the stones.</color>";
        }

        public override void OnLocalDie(PlayerControl Player) {
            if (!HasRole(Player))
                return;

            RpcRemovePlayer(Player);

            // Replace Soul Stone
            StoneInformation SoulStone = StoneInformation.GetStoneData(StoneData.Soul);
            GameObject StoneObject = SoulStone.CreateStone(AmongUsClient.Instance.ClientId);
            PositionData positionData = Thanos.Instance.SetRandomPosition(StoneObject, StoneData.Soul);
            Thanos.Instance.SendStone(StoneObject, SoulStone, positionData);
            SoulStone.HasStone = false;
        }
    }
}
