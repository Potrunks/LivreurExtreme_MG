using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Sources.Components
{
    public class GenericMenuButtonSystem : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }

        public void Retry()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Play()
        {
            SceneManager.LoadScene("Paris");
        }
    }
}
