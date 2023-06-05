using UnityEngine;
using System.Collections;

namespace Pathfinding
{
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
    public class AIDestinationSetter : VersionedMonoBehaviour
    {
        public Transform target;
        IAstarAI ai;

        public Vector2 MoveDirection
        {
            get
            {
                if (ai != null && target != null)
                {
                    Vector2 direction = target.position - transform.position;
                    return direction.normalized;
                }
                return Vector2.zero;
            }
        }

        public Vector2 CurrentTargetPosition
        {
            get
            {
                if (ai != null && target != null)
                {
                    return target.position;
                }
                return Vector2.zero;
            }
        }

        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        void Update()
        {
            if (target != null && ai != null) ai.destination = target.position;
        }
    }
}
