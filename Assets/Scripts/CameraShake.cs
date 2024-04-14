using Cinemachine;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _noise;

    private int _drillCount;
    
    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        
        _noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
    }

    public int DrillCount
    {
        get { return _drillCount; }
        set
        {
            _drillCount = value;

            OnDrillCountChanged();
        }
    }

    private void OnDrillCountChanged()
    {
        if (_drillCount > 0)
        {
            _noise.m_AmplitudeGain = 0.5f;
        }

        else
        {
            _noise.m_AmplitudeGain = 0f;
        }
    }
}
