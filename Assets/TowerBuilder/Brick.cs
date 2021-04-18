using UnityEngine;

namespace TowerBuilder
{
    public class Brick : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                print("Collision entered");
                GameManager.Instance.GameEnd();
            }
        }
    }
}
