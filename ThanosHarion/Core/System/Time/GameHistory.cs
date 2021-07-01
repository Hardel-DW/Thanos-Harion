using System;
using UnityEngine;

namespace ThanosHarion.Core.System.Time {
    public class GameHistory {
        public Vector3 position;
        public DateTime positionTime;
        public Vector2 velocity;

        public GameHistory(Vector3 position, DateTime positionTime, Vector2 velocity) {
            this.position = position;
            this.positionTime = positionTime;
            this.velocity = velocity;
        }
    }
}
