using Harion.CustomKeyBinds;
using Harion.CustomOptions;
using Harion.CustomRoles;
using Harion.CustomRoles.Abilities;
using Harion.CustomRoles.Abilities.UsableVent;
using Harion.Enumerations;
using System.Collections.Generic;
using UnityEngine;
using RealityButton = ThanosHarion.Core.Buttons.RealityButton;
using PowerButton = ThanosHarion.Core.Buttons.PowerButton;
using SpaceButton = ThanosHarion.Core.Buttons.SpaceButton;
using MindButton = ThanosHarion.Core.Buttons.MindButton;
using TimeButton = ThanosHarion.Core.Buttons.TimeButton;
using TimeCore = ThanosHarion.Core.System.Time.Time;

namespace ThanosHarion.Core.Roles {

    [RegisterInCustomRoles(typeof(Thanos))]
    public class Thanos : CustomRole<Thanos> {
        // Color: #BD00AFFF
        public static CustomNumberOption ThanosPercent = CustomOption.AddNumber("Thanos ", "<color=#BD00AFFF>Thanos Apparition</color>", 0f, 0f, 100f, 5f, GenericGameOption.ThanosHolder);
        public static CustomNumberOption NumberThanos = CustomOption.AddNumber("Number Thanos", 1f, 1f, 10f, 1f, GenericGameOption.ThanosHolder);

        public static CustomNumberOption CooldownTimeStone = CustomOption.AddNumber("Cooldown Time Stone", 10f, 10f, 60f, 2.5f, GenericGameOption.TimeStoneHolder);
        public static CustomNumberOption CooldownRealityStone = CustomOption.AddNumber("Cooldown Reality Stone", 10f, 10f, 60f, 2.5f, GenericGameOption.RealityStoneHolder);
        public static CustomNumberOption CooldownSpaceStone = CustomOption.AddNumber("Cooldown Space Stone", 10f, 10f, 60f, 2.5f, GenericGameOption.SpaceStoneHolder);
        public static CustomNumberOption CooldownMindStone = CustomOption.AddNumber("Cooldown Mind Stone", 10f, 10f, 60f, 2.5f, GenericGameOption.MindStoneHolder);
        public static CustomNumberOption CooldownSoulStone = CustomOption.AddNumber("Cooldown Soul Stone", 10f, 10f, 60f, 2.5f, GenericGameOption.SoulStoneHolder);
        public static CustomNumberOption CooldownPowerStone = CustomOption.AddNumber("Cooldown Power Stone", 10f, 10f, 60f, 2.5f, GenericGameOption.PowerStoneHolder);

        public static CustomStringOption VisibilityTimeStone = CustomOption.AddString("Visibility Time Stone", GenericGameOption.TimeStoneHolder, "Everyone", "Crewmate", "Impostor");
        public static CustomStringOption VisibilityRealityStone = CustomOption.AddString("Visibility Reality Stone", GenericGameOption.RealityStoneHolder, "Everyone", "Crewmate", "Impostor");
        public static CustomStringOption VisibilitySpaceStone = CustomOption.AddString("Visibility Space Stone", GenericGameOption.SpaceStoneHolder, "Everyone", "Crewmate", "Impostor");
        public static CustomStringOption VisibilityMindStone = CustomOption.AddString("Visibility Mind Stone", GenericGameOption.MindStoneHolder, "Everyone", "Crewmate", "Impostor");
        public static CustomStringOption VisibilitySoulStone = CustomOption.AddString("Visibility Soul Stone", GenericGameOption.SoulStoneHolder, "Everyone", "Crewmate", "Impostor");
        public static CustomStringOption VisibilityPowerStone = CustomOption.AddString("Visibility Power Stone", GenericGameOption.PowerStoneHolder, "Everyone", "Crewmate", "Impostor");

        public static CustomNumberOption DurationTimeStone = CustomOption.AddNumber("Duration Time Stone", 5f, 2.5f, 30f, 2.5f, GenericGameOption.TimeStoneHolder);
        public static CustomNumberOption DurationRealityStone = CustomOption.AddNumber("Duration Reality Stone", 5f, 5f, 60f, 2.5f, GenericGameOption.RealityStoneHolder);
        public static CustomNumberOption DurationMindStone = CustomOption.AddNumber("Duration Mind Stone", 5f, 5f, 60f, 2.5f, GenericGameOption.MindStoneHolder);
        public static CustomNumberOption MaxPortal = CustomOption.AddNumber("Max Portal", 4f, 1f, 10f, 1f, GenericGameOption.SpaceStoneHolder);
        public static CustomToggleOption EnableReviveTime = CustomOption.AddToggle("Enable Rivive during rewind", false, GenericGameOption.TimeStoneHolder);
        public static CustomToggleOption UsableVitals = CustomOption.AddToggle("Thanos can use vitals", true, GenericGameOption.TimeStoneHolder);
        public static CustomToggleOption EnableSnap = CustomOption.AddToggle("Enable Snap Ability", true, GenericGameOption.SnapHolder);

        public static CustomKeyBind KeyBindTime = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha1, "Time", "Thanos");
        public static CustomKeyBind KeyBindReality = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha2, "Reality", "Thanos");
        public static CustomKeyBind KeyBindSpace = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha3, "Space", "Thanos");
        public static CustomKeyBind KeyBindPower = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha4, "Power", "Thanos");
        public static CustomKeyBind KeyBindMind = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha5, "Mind", "Thanos");
        public static CustomKeyBind KeyBindSoul = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha6, "Soul", "Thanos");
        public static CustomKeyBind KeyBindSnap = CustomKeyBind.AddCustomKeyBind(KeyCode.Alpha7, "Snap", "Thanos");

        public override List<Ability> Abilities { get; set; } = new List<Ability>() {
            new VentAbility() { CanVent = true }
        };

        public Thanos() : base() {
            GameOptionFormat();
            RoleActive = true;
            Side = PlayerSide.Impostor;
            RoleType = RoleType.Impostor;
            GiveRoleAt = Moment.StartGame;
            GiveTasksAt = Moment.StartGame;
            Color = new Color(0.741f, 0f, 0.686f, 1f);
            Name = "Thanos";
            IntroDescription = "Find the stones, and Challenge the crewmates";
            TasksDescription = "<color=#FFFFFFFF>Objective: Find the stones to get the snap.</color>\n\n<color=#808080FF>Snap:</color> Ends the game.\n<color=#008516FF>Time stone:</color> Allows to go back in time.\n<color=#822FA8FF>Power Stone:</color> Allows you to kill in a zone.\n<color=#C46f1AFF>Soul Stone:</color> Crewmates can pick it up.\n<color=#A6A02EFF>Spirit Stone:</color> Allows you to transform into someone.\n<color=#3482BAFF>Space Stone:</color> Sets up portals.\n<color=#D43D3DFF>Reality Stone:</color> Allows you to make yourself invisible.";
            OutroDescription = "Thanos Win";
        }

        public override void OnInfectedStart() {
            PercentApparition = (int) ThanosPercent.GetValue();
            NumberPlayers = (int) NumberThanos.GetValue();
        }

        public override void OnGameStarted() {
            TimeCore.recordTime = DurationTimeStone.GetValue() * 2;
            TimeButton.Instance.EffectDuration = DurationTimeStone.GetValue() / 2;
            TimeButton.Instance.MaxTimer = CooldownTimeStone.GetValue();
            TimeButton.Instance.UseNumber = 4;
            TimeCore.ClearGameHistory();

            MindButton.Instance.MaxTimer = CooldownMindStone.GetValue();
            MindButton.Instance.EffectDuration = DurationMindStone.GetValue();

            SpaceButton.Instance.MaxTimer = CooldownSpaceStone.GetValue();
            PowerButton.Instance.MaxTimer = CooldownPowerStone.GetValue();

            RealityButton.Instance.MaxTimer = CooldownRealityStone.GetValue();
            RealityButton.Instance.EffectDuration = DurationRealityStone.GetValue();
        }

        public override void OnMeetingStart(MeetingHud instance) {
            TimeCore.StopRewind();
        }

        public override void OnGameEnded() {
            TimeCore.ClearGameHistory();
        }

        private void GameOptionFormat() {
            ThanosPercent.ShowChildrenConidtion = () => ThanosPercent.GetValue() > 0;
            ThanosPercent.ValueStringFormat = (option, value) => $"{value}%";
            NumberThanos.ValueStringFormat = (option, value) => $"{value} players";

            DurationTimeStone.ValueStringFormat = (option, value) => $"{value}s";
            DurationRealityStone.ValueStringFormat = (option, value) => $"{value}s";
            DurationMindStone.ValueStringFormat = (option, value) => $"{value}s";
            CooldownTimeStone.ValueStringFormat = (option, value) => $"{value}s";
            CooldownRealityStone.ValueStringFormat = (option, value) => $"{value}s";
            CooldownSpaceStone.ValueStringFormat = (option, value) => $"{value}s";
            CooldownMindStone.ValueStringFormat = (option, value) => $"{value}s";
            CooldownSoulStone.ValueStringFormat = (option, value) => $"{value}s";
            CooldownPowerStone.ValueStringFormat = (option, value) => $"{value}s";
        }
    }
}
