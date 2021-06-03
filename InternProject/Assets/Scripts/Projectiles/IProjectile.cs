public interface IProjectile
{
    public int Damage { get; set; }
    public float KnockBack { get; set; }
    public float Lifetime { get; set; }
    public float BulletSpeed { get; set; }
    public bool IsActivated { get; set; }
}