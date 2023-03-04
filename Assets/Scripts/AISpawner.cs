using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AISpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _npcList;
    [SerializeField] private Transform[] _npcSpawnPositions;
    [SerializeField] private float _respawnSpeed;
    private int _randomIndex;
    public List<GameObject> NPCList
    {
        get { return _npcList; }
        set { _npcList = value; }
    }
    void RandomNPCBorn()
    {
        _randomIndex = Random.Range(0, _npcList.Count);
        _npcList[_randomIndex].SetActive(true);
        _npcList[_randomIndex].transform.position = _npcSpawnPositions[_randomIndex].position;
        _npcList.Remove(_npcList[_randomIndex]);
        if (_npcList.Count==0)
        {
            CancelInvoke();
        }
    }
    public void CallInvoke()
    {
        InvokeRepeating(nameof(RandomNPCBorn), .55f, _respawnSpeed);
    }
}