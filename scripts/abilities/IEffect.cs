using System.Threading.Tasks;

public interface IEffect
{
    bool IsStateModifier { get; }
    Task Apply(DamageContext ctx);
}
