using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private AISpawner _aiController;
    public static int _defeatedEnemyCount;
    public int _levelNPCCount;

    private void Start()
    {
        _levelNPCCount = _aiController.NPCList.Count;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Material ballmaterial = transform.GetComponent<MeshRenderer>().sharedMaterial;
            Material aiMaterial = other.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMaterial;

            if (ballmaterial == aiMaterial)
            {
                _defeatedEnemyCount++;
                other.gameObject.SetActive(false);
                WinCheck();
                gameObject.SetActive(false);
            }
            else if (ballmaterial != aiMaterial)
            {
                other.transform.localScale -= new Vector3(.25f, .25f, .25f);
                if (other.transform.localScale.x < 1.1f)
                {
                    other.gameObject.SetActive(false);
                    _defeatedEnemyCount++;
                    WinCheck();
                }
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("Ground") && _playerManager.IsFinish)
        {
            if (_playerManager.CollectedBalls.Count==0)
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
    void WinCheck()
    {
        if (_defeatedEnemyCount==_levelNPCCount)
        {
            Debug.Log("win");
        }
    }
}