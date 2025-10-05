using System.Collections.Generic;

public interface IHasConditionalEffects
{
    List<IConditionalEffect> GetConditionalEffects();
}
