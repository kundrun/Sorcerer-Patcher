using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;

internal partial class Patcher
{
    private void PatchSoulGemRecords()
    {
        Console.WriteLine("Processing soul gems.");

        var soulGems = _state.LoadOrder.PriorityOrder.SoulGem().WinningOverrides();
        foreach (var soulGem in soulGems)
        {
            if (!SoulGemValues.TryGetValue(soulGem.FormKey, out var expectedValue) ||
                soulGem.Value == expectedValue)
            {
                continue;
            }

            var patchedSoulGem = _state.PatchMod.SoulGems.GetOrAddAsOverride(soulGem);
            patchedSoulGem.Value = expectedValue;

            Console.WriteLine($">>> Patched soul gem {soulGem.EditorID}.");
        }
    }
}
