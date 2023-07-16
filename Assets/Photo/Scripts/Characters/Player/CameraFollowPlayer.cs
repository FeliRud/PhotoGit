using Cinemachine;
using UnityEngine;
using Zenject;

namespace Photo
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraFollowPlayer : MonoBehaviour
    {
        private Player _player;
        private CinemachineVirtualCamera _cinemachine;
        
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;

            Init();
        }

        private void Init()
        {
            _cinemachine = GetComponent<CinemachineVirtualCamera>();
            _cinemachine.Follow = _player.transform;
        }
    }
}