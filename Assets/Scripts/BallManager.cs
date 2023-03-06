using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BallManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Sounds _sounds;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private AISpawner _aiController;
    [SerializeField] private Material _green;
    [SerializeField] private Material _red;
    [SerializeField] private Material _blue;
    [SerializeField] private ParticleSystem _confettiParticle;
    [SerializeField] private ParticleSystem[] _ballParticles;
    //public static int _defeatedEnemyCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Material ballmaterial = transform.GetComponent<MeshRenderer>().sharedMaterial;
            Material aiMaterial = other.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMaterial;
            _sounds.AudioManagerSource.PlayOneShot(_sounds.BallImpactSound);
            PlayBallExplodeParticle();
            if (ballmaterial == aiMaterial)
            {
                _aiController.DefeatedEnemyCount++;
                other.gameObject.SetActive(false);
                WinCheck();
                gameObject.SetActive(false);
            }
            else if (ballmaterial != aiMaterial)
            {
                other.GetComponent<NavMeshAgent>().speed /= 2;
                if (other.GetComponent<Animator>().GetBool("isHitted"))
                {
                    other.gameObject.SetActive(false);
                    _aiController.DefeatedEnemyCount++;
                    WinCheck();
                }
                other.GetComponent<Animator>().SetBool("isHitted", true);
                gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && _playerManager.IsFinish)
        {
            PlayBallExplodeParticle();
            _sounds.AudioManagerSource.PlayOneShot(_sounds.BallImpactSound);
            if (_playerManager.CollectedBalls.Count==1)
            {
                GameObject[] allNPCs = GameObject.FindGameObjectsWithTag("NPC");
                for (int i = 0; i < allNPCs.Length; i++)
                {
                    allNPCs[i].GetComponent<NavMeshAgent>().speed += 5f;
                }

                for (int i = 0; i < _aiController.NPCList.Count; i++)
                {
                    _aiController.NPCList[i].GetComponent<NavMeshAgent>().speed += 5f;
                }
            }
            gameObject.SetActive(false);
        }
    }
    void PlayBallExplodeParticle()
    {
        Material material = transform.GetComponent<MeshRenderer>().sharedMaterial;
        if (material==_green)
        {
            _ballParticles[0].transform.position = transform.position;
            _ballParticles[0].Play();
        }
        else if (material==_red)
        {
            _ballParticles[1].transform.position = transform.position;
            _ballParticles[1].Play();
        }
        else
        {
            _ballParticles[2].transform.position = transform.position;
            _ballParticles[2].Play();
        }
    }
    void WinCheck()
    {
        if (_aiController.DefeatedEnemyCount == _aiController.LevelNPCCount)
        {
            //int lastBallCount = _playerManager.CollectedBalls.Count;
            //Debug.Log(lastBallCount);
            _confettiParticle.Play();
            _uiManager.UIPanel.SetActive(false);
            _uiManager.WinPanel.SetActive(true);
            _uiManager.WinPanel.transform.DOMove(_uiManager.ExamplePanel.transform.position, 3f).OnComplete(() =>
            {
                _sounds.AudioManagerSource.PlayOneShot(_sounds.WinSound);
                Time.timeScale = 0;
            });
        }
    }
}