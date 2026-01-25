using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

internal static class Program
{
    public static Task<int> Main(string[] args)
    {
        return SynthesisPipeline.Instance
            .AddPatch<ISkyrimMod, ISkyrimModGetter>(state => new Patcher(state).Run())
            .SetTypicalOpen(GameRelease.SkyrimSE, ModKeys.DefaultOutput)
            .Run(args);
    }
}
