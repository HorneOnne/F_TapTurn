using UnityEngine;
using UnityEngine.UI;

namespace TapTurn
{
    public class UISettings : TapTurnCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button backBtn;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider soundFXSlider;

        private void Start()
        {
            // Update slider value 
            soundFXSlider.value = SFXManager.Instance.sfxVolume;
            musicSlider.value = SFXManager.Instance.backgroundVolume;

            backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            soundFXSlider.onValueChanged.AddListener((value) =>
            {
                SFXManager.Instance.sfxVolume= value;
            });

            musicSlider.onValueChanged.AddListener((value) =>
            {
                SFXManager.Instance.backgroundVolume = value;
                SFXManager.Instance.UpdateBackgroundVolume();
            });
        }

        private void OnDestroy()
        {
            backBtn.onClick.RemoveAllListeners();
            soundFXSlider.onValueChanged.RemoveAllListeners();
        }
    }
}
