using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;
using Cinemachine;
using ProjectileManager;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _playerParticles;
    [SerializeField] private Transform _particlePoint;
    [SerializeField] private Sounds _sounds;
    [SerializeField] private AISpawner _aIController;
    [SerializeField] private ProjectileThrow _projectileThrow;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ColorCalculation _colorCalculation;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _balls;
    [SerializeField] private List<GameObject> _collectedBalls;
    [SerializeField] private List<GameObject> _extraBalls;
    [SerializeField] private Transform _bag;
    [SerializeField] private Transform _basket;


    [SerializeField] private Transform _sling;
    [SerializeField] private CinemachineVirtualCamera _firstCam;
    [SerializeField] private CinemachineVirtualCamera _secondCam;
    [SerializeField] private GameObject _touchManage;

    private bool _isStart = false;
    private bool _isFinish = false;
    private bool _isDancing = false;
    private bool _isMagnetCollected;
    private GateManager _gateManager;

    public bool IsStart
    {
        get { return _isStart; }
        set { _isStart = value; }
    }
    public bool IsFinish 
    {
        get {return _isFinish; }
        set { _isFinish = value; }
    }
    public Transform Bag
    {
        get { return _bag; }
        set { _bag = value; }
    }
    public Animator PlayerAnimator
    {
        get { return _animator; }
        set { _animator = value; }
    }
    public  List<GameObject> CollectedBalls 
    {
        get {return _collectedBalls; }
        set {_collectedBalls=value; }
    }
    public bool IsDancing 
    {
        get {return _isDancing; }
        set {_isDancing=value; }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.SetParent(_sling.transform);
            transform.GetComponent<ParentConstraint>().enabled = true;
        }
    }
    void FixedUpdate()
    {
        if (_isMagnetCollected)
        {
            for (int i = 0; i < _balls.Length; i++)
            {
                if (_balls[i].CompareTag("Ball") && Vector3.Distance(transform.position, _balls[i].transform.position) < 6f)
                {
                    _balls[i].transform.position = Vector3.MoveTowards(_balls[i].transform.position, transform.position, Time.deltaTime * 10f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (_collectedBalls.Count!=0)
            {
                Vibration.Vibrate(50);
                _playerParticles[1].transform.position = _particlePoint.position;
                _playerParticles[1].Play();
                StartCoroutine(BallDrop(5));
            }    
        }

        else if (other.CompareTag("Obstacle2"))
        {
            if (_collectedBalls.Count != 0)
            {
                Vibration.Vibrate(50);
                _playerParticles[1].transform.position = _particlePoint.position;
                _playerParticles[1].Play();
                StartCoroutine(BallDrop(2));
            }
        }

        else if (other.CompareTag("Obstacle3"))
        {
            if (_collectedBalls.Count != 0)
            {
                Vibration.Vibrate(50);
                _playerParticles[1].transform.position = _particlePoint.position;
                _playerParticles[1].Play();
                StartCoroutine(BallDrop(1));
            }
        }
        else if (other.CompareTag("Gate"))
        {
            _sounds.AudioManagerSource.PlayOneShot(_sounds.GateHitSound);
            _gateManager = other.GetComponent<GateManager>();
            TakeGateSkill();
        }
        else if (other.CompareTag("Magnet"))
        {
            _sounds.AudioManagerSource.PlayOneShot(_sounds.MagnetCollect);
            _playerParticles[2].transform.position = _particlePoint.position;
            _playerParticles[2].Play();
            other.gameObject.SetActive(false);
            _isMagnetCollected = true;
            _uiManager.MagnetTimer();
            Invoke(nameof(MagnetTimeUp), 4.68f);
        }

        else if (other.CompareTag("Finish"))
        {
            if (_collectedBalls.Count==0)
            {
                _isStart = false;
                _isFinish = true;
                _animator.SetBool("isFinish", _isFinish);
                Vibration.Vibrate(50);
                for (int i = 0; i < _balls.Length; i++)
                {
                    _balls[i].SetActive(false);
                }
                for (int i = 0; i < _collectedBalls.Count; i++)
                {
                    _collectedBalls[i].SetActive(false);
                }
                _playerParticles[3].transform.position = _particlePoint.position;
                _playerParticles[3].Play();
                _uiManager.UIPanel.SetActive(false);
                _uiManager.LoosePanel.SetActive(true);
                _uiManager.LoosePanel.transform.DOMove(_uiManager.ExamplePanel.transform.position, 2.25f).OnComplete(() =>
                {
                    _sounds.AudioManagerSource.PlayOneShot(_sounds.LooseSound);
                    gameObject.SetActive(false);
                });
            }
            else
            {
                _isStart = false;
                _isFinish = true;
                _animator.SetBool("isFinish", _isFinish);
                StartCoroutine(EndBasket());
                StartCoroutine(ReachToSling());
            }
           
        }
        else if (other.CompareTag("Ball"))
        {
            _sounds.AudioManagerSource.PlayOneShot(_sounds.BallTake);
            _collectedBalls.Add(other.gameObject);
            other.gameObject.SetActive(false);
            _colorCalculation.ColorBallCount();
            _bag.gameObject.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), .15f).OnComplete(() =>
            {
                _bag.gameObject.transform.DOScale(new Vector3(.8f, .8f, .8f), .15f);
            });
        }
        else if (other.CompareTag("NPC"))
        {
            Vibration.Vibrate(50);
            _isDancing = true;
            GameObject[] _npcs = GameObject.FindGameObjectsWithTag("NPC");
            for (int i = 0; i < _npcs.Length; i++)
            {
                _npcs[i].GetComponent<Animator>().SetBool("isDancing", true);
            }
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i].SetActive(false);
            }
            for (int i = 0; i < _collectedBalls.Count; i++)
            {
                _collectedBalls[i].SetActive(false);
            }
            _sling.gameObject.SetActive(false);
            _playerParticles[3].transform.position = _particlePoint.position;
            _playerParticles[3].Play();
            _uiManager.UIPanel.SetActive(false);
            _uiManager.LoosePanel.SetActive(true);
            _uiManager.LoosePanel.transform.DOMove(_uiManager.ExamplePanel.transform.position, 2.25f).OnComplete(() => 
            {
                _sounds.AudioManagerSource.PlayOneShot(_sounds.LooseSound);
                gameObject.SetActive(false);
            });
        }
    }
    void TakeGateSkill()
    {
        if (_gateManager.GateIndex == 0)
        {
            _projectileThrow.shootRange *= 2;
        }
        else if (_gateManager.GateIndex == 1)
        {
            int firstIndex = _extraBalls.Count;
            for (int i = 0; i < firstIndex; i++)
            {
                _collectedBalls.Add(_extraBalls[0]);
                _extraBalls.RemoveAt(0);
            }
            _colorCalculation.ColorBallCount();
            _bag.gameObject.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), .15f).OnComplete(() =>
            {
                _bag.gameObject.transform.DOScale(new Vector3(.8f, .8f, .8f), .15f);
            });
        }
    }
    IEnumerator BallDrop (int division)
    {
        int dropCount = _collectedBalls.Count / division;
        if (dropCount<1)
        {
            dropCount = 1;
        }
        for (int i = 0; i < dropCount; i++)
        {
            int ballIndex = Random.Range(0, dropCount - i);
            _collectedBalls[ballIndex].tag = "Respawn";
            _collectedBalls[ballIndex].transform.position = transform.position - new Vector3(Random.Range(-.2f, .2f), -1f, Random.Range(.1f, .2f));
            _collectedBalls[ballIndex].SetActive(true);
            StartCoroutine(BallDisappear(_collectedBalls[ballIndex]));
            _collectedBalls.Remove(_collectedBalls[ballIndex]);
            _colorCalculation.ColorBallCount();
            yield return new WaitForSeconds(.075f);
        }
    }
    IEnumerator BallDisappear(GameObject droppedBall)
    {
        yield return new WaitForSeconds(2f);
        droppedBall.SetActive(false);
        StopCoroutine(BallDisappear(droppedBall));
    }
    IEnumerator EndBasket()
    {
        yield return new WaitForSeconds(1.9f);
        int positionOrder = 0;
        for (int i = _collectedBalls.Count-1; i >= 0; i--)
        {
            _collectedBalls[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(.05f);
            _sounds.AudioManagerSource.PlayOneShot(_sounds.BallTake);
            _collectedBalls[i].SetActive(true);
            _collectedBalls[i].transform.position = _basket.position + new Vector3(0,.3f,0) + (.51f * positionOrder * Vector3.up);
            positionOrder++;
        }
        yield return new WaitForSeconds(.5f);
        _collectedBalls[0].GetComponent<Collider>().enabled = false;
        _collectedBalls[0].transform.DOMove(_projectileThrow.shootPoint2.transform.position, .5f).OnComplete(()=> 
        {
            _aIController.CallInvoke();
            _touchManage.SetActive(true);
        });
    }

    IEnumerator ReachToSling()
    {
        _firstCam.Follow = null;
        _secondCam.Priority = 15;
        //_sling.gameObject.SetActive(true);
        transform.SetParent(_sling.transform);
        yield return new WaitForSeconds(1.8f);
        _basket.gameObject.SetActive(true);
        _uiManager.InputPanel.SetActive(false);
        transform.GetComponent<ParentConstraint>().enabled = true;
    }
    void MagnetTimeUp()
    {
        _playerParticles[2].Stop();
        _isMagnetCollected = false;
    }
}