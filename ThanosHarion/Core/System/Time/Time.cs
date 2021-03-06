using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Button = ThanosHarion.Core.Buttons.TimeButton;
using ThanosRole = ThanosHarion.Core.Roles.Thanos;

namespace ThanosHarion.Core.System.Time {
    public static class Time {
        public static Dictionary<byte, List<GameHistory>> PlayersPositions = new Dictionary<byte, List<GameHistory>>();
        public static Dictionary<byte, DateTime> DeadPlayers = new Dictionary<byte, DateTime>();

        public static float recordTime = 5f;
        public static bool isRewinding = false;

        public static void Record() {
            if (PlayerControl.AllPlayerControls != null && PlayerControl.AllPlayerControls.Count > 0) {
                foreach (var player in PlayerControl.AllPlayerControls) {
                    if (!PlayersPositions.ContainsKey(player.PlayerId))
                        PlayersPositions[player.PlayerId] = new List<GameHistory>();

                    var currentPlayer = PlayersPositions.FirstOrDefault(d => d.Key == player.PlayerId);
                    if (currentPlayer.Value != null && currentPlayer.Value.Count > 0)
                        while (currentPlayer.Value.Count >= Mathf.Round(recordTime / UnityEngine.Time.fixedDeltaTime))
                            currentPlayer.Value.RemoveAt(currentPlayer.Value.Count - 1);

                    if (player.moveable)
                        currentPlayer.Value.Insert(0, new GameHistory(player.transform.position, DateTime.UtcNow, player.gameObject.GetComponent<Rigidbody2D>().velocity));
                    else if (currentPlayer.Value != null && currentPlayer.Value.Count > 0)
                        currentPlayer.Value.Insert(0, new GameHistory(currentPlayer.Value[0].position, DateTime.UtcNow, currentPlayer.Value[0].velocity));

                    if (player.Data.IsDead && !DeadPlayers.ContainsKey(player.PlayerId))
                        DeadPlayers[player.PlayerId] = DateTime.UtcNow;
                }
            }
        }

        public static void Rewind() {
            foreach (var player in PlayerControl.AllPlayerControls) {
                if (!PlayersPositions.ContainsKey(player.PlayerId))
                    continue;

                List<GameHistory> gameHistory = PlayersPositions.FirstOrDefault(d => d.Key == player.PlayerId).Value;

                if (gameHistory.Count > 0) {
                    if (!player.inVent) {
                        var currentGemeHistory = gameHistory[0];
                        player.transform.position = currentGemeHistory.position;
                        player.gameObject.GetComponent<Rigidbody2D>().velocity = currentGemeHistory.velocity;

                        if (ThanosRole.EnableReviveTime.GetValue() && player.Data.IsDead)
                            if (DeadPlayers.ContainsKey(player.PlayerId))
                                if (currentGemeHistory.positionTime < DeadPlayers[player.PlayerId])
                                    TimeMasterRevive(player.PlayerId);

                        if (Minigame.Instance) {
                            try {
                                Minigame.Instance.Close();
                            } catch { }
                        }
                    }

                    gameHistory.RemoveAt(0);
                }
            }

            bool CanStopRewind = true;
            foreach (var player in PlayerControl.AllPlayerControls) {
                if (!PlayersPositions.ContainsKey(player.PlayerId))
                    continue;

                List<GameHistory> gameHistory = PlayersPositions.FirstOrDefault(d => d.Key == player.PlayerId).Value;
                if (gameHistory.Count != 0 || gameHistory == null)
                    CanStopRewind = false;
            }

            if (CanStopRewind)
                StopRewind();
        }

        public static void StartRewind() {
            isRewinding = true;
            PlayerControl.LocalPlayer.moveable = false;
            HudManager.Instance.FullScreen.color = new Color(0f, 0.5f, 0.8f, 0.3f);
            HudManager.Instance.FullScreen.enabled = true;

            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.TimeRewind, SendOption.None, -1);
            AmongUsClient.Instance.FinishRpcImmediately(write);
        }

        public static void StopRewind() {
            isRewinding = false;
            PlayerControl.LocalPlayer.moveable = true;
            HudManager.Instance.FullScreen.enabled = false;
            HudManager.Instance.FullScreen.color = new Color(1f, 0f, 0f, 0.3f);
        }

        public static void TimeMasterRevive(byte playerId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == playerId) {
                    player.Revive();
                    DeadBody body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == playerId);
                    if (body != null)
                        UnityEngine.Object.Destroy(body.gameObject);

                    if (DeadPlayers.ContainsKey(player.PlayerId))
                        DeadPlayers.Remove(player.PlayerId);
                }
            }
        }

        public static void ClearGameHistory() {
            PlayersPositions.Clear();
            DeadPlayers.Clear();
        }
    }
}
