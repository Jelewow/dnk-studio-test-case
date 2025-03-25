using UnityEngine;
using UnityEngine.AI;

namespace Jelewow.DNK.Player.MonoBehaviours
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField]
        public NavMeshAgent NavMeshAgent { get; private set; }
        
        [field: SerializeField]
        public Animator Animator { get; private set; }

        public float Speed => NavMeshAgent.velocity.magnitude;
    }
}