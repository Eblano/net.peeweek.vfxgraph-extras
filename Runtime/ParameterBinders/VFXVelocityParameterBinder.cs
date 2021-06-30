using UnityEngine.VFX;

namespace UnityEngine.VFX.Utility
{

[VFXBinder("Utility/Velocity")]
public class VFXVelocityPropertyBinder : VFXBinderBase
{
    [VFXPropertyBinding("UnityEngine.Vector3")]
    public ExposedProperty VelocityProperty = "OwnerVelocity";

    Vector3 velocity;
    Vector3 oldPosition;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        oldPosition = gameObject.transform.position;
    }

    public override bool IsValid(VisualEffect component)
    {
        return component.HasVector3(VelocityProperty);
    }

    public override void UpdateBinding(VisualEffect component)
    {
        Vector3 position = gameObject.transform.position;
        velocity = (oldPosition - position) * Time.deltaTime;

        component.SetVector3(VelocityProperty, velocity);

        oldPosition = position;
    }

    public override string ToString()
    {
        return "Velocity : " + VelocityProperty.ToString();
        }
    }
}
