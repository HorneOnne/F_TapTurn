using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TapTurn
{
    public class UIGameplay : TapTurnCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button homeBtn;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI levelText;

        private void Start()
        {
            homeBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene._0_MainMenuScene, null);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });


            // Set level text.
            levelText.text = GameManager.Instance.Level.ToString();
        }

        private void OnDestroy()
        {
            homeBtn.onClick.RemoveAllListeners();
        }
    }
}
