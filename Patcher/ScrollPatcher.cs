using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

internal partial class Patcher
{
    private void PatchScrollRecords()
    {
        Console.WriteLine("Processing scrolls.");

        var scrolls = _state.LoadOrder.PriorityOrder.Scroll().WinningOverrides()
            .Where(x => !ExcludedScrollMods.Contains(x.FormKey.ModKey))
            .Where(x => !ExcludedScrollKeys.Contains(x.FormKey))
            .Where(x => x.EditorID is { } editorId &&
                        !editorId.StartsWith("MAG_"));

        foreach (var scroll in scrolls)
        {
            if (GetScrollInfo(scroll) is not { } scrollInfo)
            {
                continue;
            }

            Scroll? patchedScroll = null;

            var expectedValue = ScrollValues(scrollInfo.SkillLevel);

            if (expectedValue != scroll.Value)
            {
                patchedScroll ??= _state.PatchMod.Scrolls.GetOrAddAsOverride(scroll);
                patchedScroll.Value = expectedValue;
            }

            var expectedSkill = ScrollSkills(scrollInfo.MagicSkill);

            if (!expectedSkill.IsNull && !scroll.HasKeyword(expectedSkill))
            {
                patchedScroll ??= _state.PatchMod.Scrolls.GetOrAddAsOverride(scroll);
                (patchedScroll.Keywords ?? []).Add(expectedSkill);
            }

            if (patchedScroll != null)
            {
                Console.WriteLine($">>> Patched scroll {scroll.EditorID}.");
            }
        }
    }

    private ScrollInfo? GetScrollInfo(IScrollGetter scroll)
    {
        var primaryEffect = scroll.Effects
            .Select(x => TryResolve(x.BaseEffect))
            .MaxBy(x => x?.BaseCost);

        return primaryEffect != null
            ? new ScrollInfo(primaryEffect.MinimumSkillLevel, primaryEffect.MagicSkill)
            : null;
    }

    private record ScrollInfo(uint SkillLevel, ActorValue MagicSkill);
}
