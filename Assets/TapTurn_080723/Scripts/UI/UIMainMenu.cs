using UnityEngine;
using UnityEngine.UI;

namespace TapTurn
{
    public class UIMainMenu : TapTurnCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button playBtn;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button figuresBtn;
        [SerializeField] private Button privacyBtn;


        private void Start()
        {
            playBtn.onClick.AddListener(() =>
            {
                Loader.LoadLevel(GameManager.Instance.Level, null);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            settingsBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplaySettingsMenu(true);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            figuresBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayFiguresMenu(true);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            privacyBtn.onClick.AddListener(() =>
            {
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            playBtn.onClick.RemoveAllListeners();
            settingsBtn.onClick.RemoveAllListeners();
            figuresBtn.onClick.RemoveAllListeners();
            privacyBtn.onClick.RemoveAllListeners();  
        }
    }
}
