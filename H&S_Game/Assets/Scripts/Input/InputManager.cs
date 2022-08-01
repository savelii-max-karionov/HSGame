using UnityEngine;

namespace HS
{
    public abstract class InputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public virtual float horizontal { get; }
        public virtual float vertical { get; }
    }

}
