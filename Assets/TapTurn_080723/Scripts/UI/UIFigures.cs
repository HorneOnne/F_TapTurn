using UnityEngine;
using UnityEngine.UI;

namespace TapTurn
{
    public class UIFigures : TapTurnCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button backBtn;
        [SerializeField] private Button leftBtn;
        [SerializeField] private Button rightBtn;

        [Header("Image")]
        [SerializeField] private Image levelIcon;

        private void Start()
        {
            // Update level icon whens start
            levelIcon.sprite = GameManager.Instance.GetLevelIcon(GameManager.Instance.Level);


            backBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayMainMenu(true);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            leftBtn.onClick.AddListener(() =>
            {
                GameManager.Instance.PreviousLevel();
                levelIcon.sprite = GameManager.Instance.GetLevelIcon(GameManager.Instance.Level);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });

            rightBtn.onClick.AddListener(() =>
            {
                GameManager.Instance.NextLevel();
                levelIcon.sprite = GameManager.Instance.GetLevelIcon(GameManager.Instance.Level);
                SFXManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            backBtn.onClick.RemoveAllListeners();
            leftBtn.onClick.RemoveAllListeners();
            rightBtn.onClick.RemoveAllListeners();
        }
    }
}
