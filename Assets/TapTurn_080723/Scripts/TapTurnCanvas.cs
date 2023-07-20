using UnityEngine;

namespace TapTurn
{
    public class TapTurnCanvas : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;

        #region Properties
        public Canvas Canvas { get { return _canvas; } }
        #endregion

        public void DisplayCanvas(bool isDisplay)
        {
            _canvas.enabled = isDisplay;
        }
    }
}
