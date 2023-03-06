using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AISpawner : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private List<GameObject> _npcList;
    [SerializeField] private Transform[] _npcSpawnPositions;
    [SerializeField] private float _respawnSpeed;
    private int _randomIndex;
    private int _randomPosIndex;
    public int _levelNPCCount;
    public int _defeatedEnemyCount;
    public int LevelNPCCount 
    {
        get {return _levelNPCCount; }
        set {_levelNPCCount=value; }
    }
    public int DefeatedEnemyCount
    {
        get { return _defeatedEnemyCount; }
        set { _defeatedEnemyCount = value; }
    }
    private void Start()
    {
        Time.timeScale = 1;
        _levelNPCCount = _npcList.Count;
    }
    public List<GameObject> NPCList
    {
        get { return _npcList; }
        set { _npcList = value; }
    }
    void RandomNPCBorn()
    {
        _randomPosIndex = Random.Range(0, _npcSpawnPositions.Length);
        _randomIndex = Random.Range(0, _npcList.Count);
        _npcList[_randomIndex].SetActive(true);
        _npcList[_randomIndex].transform.position = _npcSpawnPositions[_randomPosIndex].position;
        _npcList.Remove(_npcList[_randomIndex]);
        if (_npcList.Count==0)
        {
            CancelInvoke();
        }
        if (_playerManager.IsDancing)
        {
            GameObject[] _npcs = GameObject.FindGameObjectsWithTag("NPC");
            for (int i = 0; i < _npcs.Length; i++)
            {
                _npcs[i].GetComponent<Animator>().SetBool("isDancing", true);
            }
            CancelInvoke();
        }
    }
    public void CallInvoke()
    {
        InvokeRepeating(nameof(RandomNPCBorn), .55f, _respawnSpeed);
    }
}