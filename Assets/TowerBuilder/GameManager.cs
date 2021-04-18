using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerBuilder
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager s_instance = null;

        public static GameManager Instance
        {
            get
            {
                if (s_instance == null) return null;
                return s_instance;
            }
        }

        [SerializeField] private Camera[] _cameras;
        public bool IsGameEnd { get; private set; } = false;

        [SerializeField] private GameObject _gameoverPanel;

        protected GameManager() { }

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _cameras[0].enabled = true;
            _cameras[1].enabled = false;
        }

        public void GameEnd()
        {
            IsGameEnd = true;
            _gameoverPanel.SetActive(true);
            _cameras[0].enabled = false;
            _cameras[1].enabled = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
