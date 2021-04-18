using UnityEngine;
using UnityEngine.UI;

namespace TowerBuilder
{
    public class TowerGenerator : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _brickPrefab;
        [SerializeField] private GameObject _towerPools;
        [SerializeField] private Text _scoreText;

        [SerializeField] private float _moveDistance = 50f;
        [SerializeField, Range(0, 1)] private float _speed = 0.2f;

        private GameObject _currentBrick;

        private bool _moveLeft = false;
        private float _nextHeight;
        private bool _moveUp = false;
        private int _floor = 0;
        private float _brickSize;
        private float _brickHeight;
        private float _moveUpDistance;

        private void Awake()
        {
            _brickSize = _brickPrefab.transform.localScale.x;
            _brickHeight = _brickPrefab.transform.localScale.y;
            _moveUpDistance = (_brickHeight / 2) * 4;
        }

        // Start is called before the first frame update
        private void Start()
        {
            transform.position = new Vector3(transform.position.x, _moveUpDistance, transform.position.z);

            CreateNewBrick();
            _scoreText.text = $"{_floor} floor";
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.IsGameEnd) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DropBrick();
                CreateNewBrick();

                _nextHeight = transform.position.y + _moveUpDistance;
                _moveUp = true;

                _floor++;
                _scoreText.text = $"{_floor} floor";
            }
        }
        private void FixedUpdate()
        {
            transform.Translate((_moveLeft ? Vector3.left : Vector3.right) * _speed);
            if (Mathf.Abs(transform.position.x) > _moveDistance) _moveLeft = !_moveLeft;

            if (_moveUp)
            {
                transform.Translate(Vector3.up);
                _camera.transform.Translate(Vector3.back);
                if (transform.position.y >= _nextHeight) _moveUp = false;
            }
        }

        private void CreateNewBrick()
        {
            _currentBrick = Instantiate(_brickPrefab, transform.position, Quaternion.identity, transform);
            _currentBrick.transform.localScale = new Vector3(_brickSize, _brickHeight, _brickSize);
            _brickSize -= 0.1f;
            _currentBrick.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void DropBrick()
        {
            _currentBrick.transform.position -= new Vector3(0, -_brickHeight, 0);

            _currentBrick.transform.parent = _towerPools.transform;
            _currentBrick.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
