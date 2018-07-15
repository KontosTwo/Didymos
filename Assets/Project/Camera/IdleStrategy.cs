using UnityEngine;

public partial class GameplayCamera
{
    private class IdleStrategy : IMovementStrategy
    {
        private Camera cam;
        public IdleStrategy(Camera cam)
        {
            this.cam = cam;
        }

        public void Cleanup()
        {

        }

        public Vector3 GetDirection()
        {
            return cam.transform.forward;
        }


        public Vector3 GetLocation()
        {
            return cam.transform.position;
        }

        public void Initialize()
        {

        }

        public void Update()
        {

        }
    }



}
