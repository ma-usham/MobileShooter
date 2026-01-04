using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Darkmatter.Presentation
{
    public class GameScreenView : MonoBehaviour
    {
        public TextMeshProUGUI fireableBulletText;
        public TextMeshProUGUI remainingZombiesCountText;
        public TextMeshProUGUI totalZombiesCountText;
        public TextMeshProUGUI playerHealthText;
        public GameObject GameOverObject;

        public void UpdateBulletText(int bulletCount)
        {
            fireableBulletText.text = bulletCount.ToString();
        }

        public void UpdateRemainingZombiesCountText(int zombiesCount)
        {
            remainingZombiesCountText.text = zombiesCount.ToString();
        }

        public void UpdateTotalZombiesCount(int totalZombies)
        {
            totalZombiesCountText.text = totalZombies.ToString();   
        }
        public void ShowPlayerHealth(int health)
        {
            playerHealthText.text = health.ToString();
        }
       public void ShowGameOver()
        {
            GameOverObject.SetActive(true);
            Invoke("ChangeScene", 2f);
        }

        void ChangeScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
