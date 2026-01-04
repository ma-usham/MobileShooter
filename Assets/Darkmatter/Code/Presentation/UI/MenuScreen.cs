using UnityEngine;
using UnityEngine.SceneManagement;

namespace Darkmatter.Presentation
{
    public class MenuScreen : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }
        public void OnPlayBtnClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}
