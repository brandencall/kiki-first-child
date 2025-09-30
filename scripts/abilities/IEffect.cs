using System.Threading.Tasks;

public interface IEffect
{
    Task Apply(BaseEnemy target);
}
