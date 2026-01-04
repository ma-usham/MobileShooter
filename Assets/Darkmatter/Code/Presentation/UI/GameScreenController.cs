using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public class GameScreenController : IGameScreenController
    {
        GameScreenView gameScreenView;

        public GameScreenController(GameScreenView gameScreenView)
        {
            this.gameScreenView = gameScreenView;
        }
        public void UpdateFireableBulletCount(int bulletCount)
        {
            gameScreenView.UpdateBulletText(bulletCount);
        }

        public void UpdateRemainingZombiesCount(int zombiesCount)
        {
            gameScreenView.UpdateRemainingZombiesCountText(zombiesCount);
        }

        public void UpdateTotalZombiesCount(int totalZombiesCount)
        {
            gameScreenView.UpdateTotalZombiesCount(totalZombiesCount);
        }

        public void ShowGameOverText()
        {
            gameScreenView.ShowGameOver();
            
        }

        public void ShowPlayerHealth(int health)
        {
           gameScreenView.ShowPlayerHealth(health);
        }
    }
}
