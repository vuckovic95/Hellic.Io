using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public static class GlobalManager
    {
        private static PoolManager poolManager;
        public static PoolManager PoolManager
        {
            get
            {
                if (poolManager == null)
                {
                    Debug.LogError("PoolManager does not exist in the scene.");
                }
                return poolManager;

            }
            set
            {
                poolManager = value;
            }
        }

        private static Coroutines coroutines;
        public static Coroutines Coroutines
        {
            get
            {
                if (poolManager == null)
                {
                    Debug.LogError("Coroutines does not exist in the scene.");
                }
                return coroutines;

            }
            set
            {
                coroutines = value;
            }
        }

        private static GameManager gameManager;
        public static GameManager GameManager
        {
            get
            {
                if (gameManager == null)
                {
                    Debug.LogError("GameManager does not exist in the scene.");
                }
                return gameManager;

            }
            set
            {
                gameManager = value;
            }
        }

        private static LevelManager levelManager;
        public static LevelManager LevelManager
        {
            get
            {
                if (levelManager == null)
                {
                    Debug.LogError("LevelManager does not exist in the scene.");
                }
                return levelManager;

            }
            set
            {
                levelManager = value;
            }
        }

        private static SaveData saveData;
        public static SaveData SaveData
        {
            get
            {
                if (saveData == null)
                {
                    Debug.LogError("SaveData does not exist in the scene.");
                }
                return saveData;

            }
            set
            {
                saveData = value;
            }
        }

        private static UI_Manager uiManager;
        public static UI_Manager UI_Manager
        {
            get
            {
                if (uiManager == null)
                {
                    Debug.LogError("UI_Manager does not exist in the scene.");
                }
                return uiManager;

            }
            set
            {
                uiManager = value;
            }
        }

        private static AudioManager audioManager;
        public static AudioManager AudioManager
        {
            get
            {
                if (audioManager == null)
                {
                    Debug.LogError("AudioManager does not exist in the scene.");
                }
                return audioManager;

            }
            set
            {
                audioManager = value;
            }
        }

        //Singleton
        private static PlayerController player;
        public static PlayerController Player
        {
            get
            {
                if (player == null)
                {
                    Debug.LogError("Player Controller does not exist in the scene.");
                }
                return player;

            }
            set
            {
                player = value;
            }
        }
    }
}
