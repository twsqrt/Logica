using UnityEngine;

namespace View.WindowLogic
{
    public class Window : MonoBehaviour
    {
        public virtual void Open()
            => gameObject.SetActive(true);

        public virtual void Close()
            => gameObject.SetActive(false);
        
        public void OpenCloseToggle()
        {
            bool currentActive = gameObject.activeSelf;
            gameObject.SetActive(currentActive == false);
        }
    }
}