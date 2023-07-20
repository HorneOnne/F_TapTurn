using TMPro;
using UnityEngine;

namespace TapTurn
{
    public class UIOther : TapTurnCanvas
    {
        [SerializeField] private TextMeshProUGUI bestText;

        private void Start()
        {
            bestText.text = $"BEST: {GameManager.Instance.GetBestScore()}";
        }
    }
}
