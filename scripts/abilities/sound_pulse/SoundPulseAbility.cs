using Godot;

public partial class SoundPulseAbility : HitboxComponent 
{
    public  float WaveSpeed { get; set; } = 200f;
    [Export] 
    private ColorRect _waveRect;
    [Export]
    private CollisionShape2D _collisionShape;
    private ShaderMaterial _material;
    private float _maxRadius = 250f;
    private float _radius = 0f;
    private float _collisionShapeDisabledRadius = 100.0f;

    public override void _Ready()
    {
        _material = (ShaderMaterial)_waveRect.Material;
        _waveRect.Visible = true;
    }

    public override void _Process(double delta)
    {
        _radius += WaveSpeed * (float)delta;
        _material.SetShaderParameter("radius", _radius);

        if (_radius < _collisionShapeDisabledRadius)
        {
            _collisionShape.Disabled = true;
        }
        else
        {
            _collisionShape.Disabled = false;
        }

        if (_radius > _maxRadius)
        {
            _radius = 0.0f;
        }
    }

}
