using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _ballImpact;
    [SerializeField] private AudioClip _gateHit;
    [SerializeField] private AudioClip _stretch;
    [SerializeField] private AudioClip _shooting;
    [SerializeField] private AudioClip _magnetCollect;
    [SerializeField] private AudioClip _magnetEnd;
    [SerializeField] private AudioClip _ballTake;
    [SerializeField] private AudioClip _looseSound;
    [SerializeField] private AudioClip _winSound;

    public AudioSource AudioManagerSource
    {
        get { return _audioSource; }
        set { _audioSource = value; }
    }
    public AudioClip BallImpactSound 
    {
        get {return _ballImpact; }
        set { _ballImpact = value; }
    }
    public AudioClip GateHitSound
    {
        get { return _gateHit; }
        set { _gateHit = value; }
    }
    public AudioClip StretchSound
    {
        get { return _stretch; }
        set { _stretch = value; }
    }
    public AudioClip ShootingSound
    {
        get { return _shooting; }
        set { _shooting = value; }
    }
    public AudioClip MagnetCollect
    {
        get { return _magnetCollect; }
        set { _magnetCollect = value; }
    }
    public AudioClip MagnetEnd
    {
        get { return _magnetEnd; }
        set { _magnetEnd = value; }
    }
    public AudioClip BallTake
    {
        get { return _ballTake; }
        set { _ballTake = value; }
    }
    public AudioClip LooseSound
    {
        get { return _looseSound; }
        set { _looseSound = value; }
    }
    public AudioClip WinSound
    {
        get { return _winSound; }
        set { _winSound = value; }
    }
}