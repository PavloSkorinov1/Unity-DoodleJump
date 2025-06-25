using UnityEngine;

namespace Core.Actions
{
    public class SpinningAction : ActionBase
    {
        [SerializeField] private float spinSpeed = 90f; 
        
        private Vector3 spinAxis = new Vector3(0, 0, 1); 

        private bool _isSpinning = false;

        protected void Update()
        {
            if (_isSpinning)
            {
                transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);
            }
        }
        
        protected override void ExecuteAction()
        {
            _isSpinning = true;
        }
    }
}
