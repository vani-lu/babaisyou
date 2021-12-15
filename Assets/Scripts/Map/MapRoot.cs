using UnityEngine;

namespace Gfen.Game.Map
{
    public class MapRoot : MonoBehaviour
    {
        public Vector2Int size;

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            // Boundaries
            Gizmos.DrawLine(Vector3.zero, new Vector3(size.x, 0f, 0f));
            Gizmos.DrawLine(new Vector3(size.x, 0f, 0f), new Vector3(size.x, size.y, 0f));
            Gizmos.DrawLine(new Vector3(size.x, size.y, 0f), new Vector3(0f, size.y, 0f));
            Gizmos.DrawLine(new Vector3(0f, size.y, 0f), Vector3.zero);
            // Half
            Gizmos.DrawLine(new Vector3(size.x/2, 0f, 0f), new Vector3(size.x/2, size.y, 0f));
            Gizmos.DrawLine(new Vector3(size.x, size.y/2, 0f), new Vector3(0f, size.y/2, 0f));
        }
    }
}
