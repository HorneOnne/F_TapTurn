using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TapTurn
{

    public class UIGameOver : TapTurnCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button replayBtn;
        [SerializeField] private Button menuBtn;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestText;

        private void Start()
        {
            replayBtn.onClick.AddListener(() =>
            {
                Loader.LoadLevel(GameManager.Instance.Level, null);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            menuBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene._0_MainMenuScene, null);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            scoreText.text = $"SCORE: {GameManager.Instance.GetScore()}";
            bestText.text = $"BEST: {GameManager.Instance.GetBestScore()}";
            GameManager.Instance.ResetScore();
        }



        private void OnDestroy()
        {
            replayBtn.onClick.RemoveAllListeners();
            menuBtn.onClick.RemoveAllListeners();
        }
    }
}
