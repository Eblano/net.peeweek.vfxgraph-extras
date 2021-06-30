using UnityEngine.VFX;

namespace UnityEngine.VFX.Utility
{
    [VFXBinder("Camera/Camera Depth Binder")]
    public class HDRPCameraDepthBinder : VFXBinderBase
    {
        [VFXPropertyBinding("UnityEngine.Texture2D")]
        public ExposedProperty DepthTextureProperty = "DepthTexture";
        [VFXPropertyBinding("UnityEditor.VFX.CameraType")]
        public ExposedProperty CameraProperty = "Camera";
        public HDRPCameraDepthLayer CameraDepthLayer;

        ExposedProperty position;
        ExposedProperty angles;
        ExposedProperty scale;
        ExposedProperty fieldOfView;
        ExposedProperty nearPlane;
        ExposedProperty farPlane;
        ExposedProperty aspectRatio;
        ExposedProperty pixelDimensions;

        void UpdateSubparameters()
        {
            position = CameraProperty + "_transform_position";
            angles = CameraProperty + "_transform_angles";
            scale = CameraProperty + "_transform_scale";
            fieldOfView = CameraProperty + "_fieldOfView";
            nearPlane = CameraProperty + "_nearPlane";
            farPlane = CameraProperty + "_farPlane";
            aspectRatio = CameraProperty + "_aspectRatio";
            pixelDimensions = CameraProperty + "_pixelDimensions";
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateSubparameters();
        }

        private void OnValidate()
        {
            UpdateSubparameters();
        }

        public override bool IsValid(VisualEffect component)
        {
            return CameraDepthLayer != null && 
                CameraDepthLayer.Camera != null &&
                component.HasTexture(DepthTextureProperty) &&
                component.HasVector3(position) &&
                component.HasVector3(angles) &&
                component.HasVector3(scale) &&
                component.HasFloat(fieldOfView) &&
                component.HasFloat(nearPlane) &&
                component.HasFloat(farPlane) &&
                component.HasFloat(aspectRatio) &&
                component.HasVector2(pixelDimensions);
        }

        public override void UpdateBinding(VisualEffect component)
        {
            component.SetTexture(DepthTextureProperty, CameraDepthLayer.DepthTexture);

            component.SetVector3(position, CameraDepthLayer.Camera.transform.position);
            component.SetVector3(angles, CameraDepthLayer.Camera.transform.eulerAngles);
            component.SetVector3(scale, CameraDepthLayer.Camera.transform.lossyScale);
            component.SetFloat(fieldOfView, CameraDepthLayer.Camera.fieldOfView * Mathf.Deg2Rad);
            component.SetFloat(nearPlane, CameraDepthLayer.Camera.nearClipPlane);
            component.SetFloat(farPlane, CameraDepthLayer.Camera.farClipPlane);
            component.SetFloat(aspectRatio, CameraDepthLayer.Camera.aspect);
            component.SetVector2(pixelDimensions, new Vector2(CameraDepthLayer.Camera.pixelWidth, CameraDepthLayer.Camera.pixelHeight));
        }
    }
}
