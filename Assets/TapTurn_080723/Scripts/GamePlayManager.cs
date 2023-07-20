using UnityEngine;
using UnityEngine.SceneManagement;

namespace TapTurn
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;

        public enum GameState
        {
            LOADING,
            PLAYING,
            WIN,
            GAMEOVER,
            PAUSE,
        }


        [Header("Properties")]
        public GameState currentState;
        [SerializeField] private float waitTimeBeforePlaying = 0.5f;



        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            OnStateChanged += SwitchState;
        }

        private void OnDisable()
        {
            OnStateChanged -= SwitchState;
        }

        private void Start()
        {
            currentState = GameState.LOADING;

            // Start the fade-in transition when the scene starts
            FadeTransition.Instance.FadeIn(() =>
            {
                StartCoroutine(Utilities.WaitAfter(waitTimeBeforePlaying, () =>
                {
                    ChangeGameState(GameState.PLAYING);
                }));               
            });
        }

     

        public void ChangeGameState(GameState state)
        {
            currentState = state;
            OnStateChanged?.Invoke();
        }

        private void SwitchState()
        {
            switch(currentState)
            {
                default: break;
                case GameState.PLAYING:
                    break;
                case GameState.WIN:
                    SFXManager.Instance.PlaySound(SoundType.Win, false);
                    StartCoroutine(Utilities.WaitAfter(1.5f, () =>
                    {
                        GameManager.Instance.NextLevel();
                        Loader.Load(Loader.Scene._0_MainMenuScene, null);

                    }));
                    break;
                case GameState.GAMEOVER:
                    SFXManager.Instance.PlaySound(SoundType.GameOver, false);
                    StartCoroutine(Utilities.WaitAfter(1.0f, () =>
                    {
                        Loader.Load(Loader.Scene._1_GameOverScene, null);
                    }));
                    break;
                case GameState.PAUSE:
                    break;
            }
        }
    }       
}
