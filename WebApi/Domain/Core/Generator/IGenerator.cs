namespace Core.Generator;

public interface IGenerator<TActivity>
{
    List<TActivity> Generate(TActivity activity);
}