using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FlappyBird
{
    public sealed class Game
    {
        private static Game instance = null;

        public static bool IsDead = false;
        public static bool IsWindows = true;

        private Game() {
            Score.Initialize();
            PanelScore.Initialize();
            Medal.Initialize();
        }

        public static Game GetInstance() {
            if (instance == null) {
                instance = new Game();
            }
            return instance;
        }

        public static bool HasInput() {
            if (IsWindows == true) {
                return Input.GetMouseButtonDown(0);
            }
            else{
                return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
            }
        }

        public static Vector2 GetInputPosition() {
            if (IsWindows == true) {
                return Input.mousePosition;
            }
            else {
                return Input.GetTouch(0).position;
            }
        }
    }
}
