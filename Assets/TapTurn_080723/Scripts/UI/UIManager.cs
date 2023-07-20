using UnityEngine;

namespace TapTurn
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu uiMainMenu;
        public UISettings uiSettings;
        public UIFigures uiFigures;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplaySettingsMenu(false);
            DisplayFiguresMenu(false);
        }

        public void DisplayMainMenu(bool isActive)
        {
            uiMainMenu.DisplayCanvas(isActive);
        }

        public void DisplaySettingsMenu(bool isActive)
        {
            uiSettings.DisplayCanvas(isActive);
        }

        public void DisplayFiguresMenu(bool isActive)
        {
            uiFigures.DisplayCanvas(isActive);
        }
    }
}
