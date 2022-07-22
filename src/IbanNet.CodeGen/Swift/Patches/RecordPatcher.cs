namespace IbanNet.CodeGen.Swift.Patches;

public abstract class RecordPatcher
{
    private static IList<RecordPatcher>? _patches;

    protected abstract SwiftCsvRecord Apply(SwiftCsvRecord record);

    public static SwiftCsvRecord ApplyAll(SwiftCsvRecord record)
    {
        return GetPatches().Aggregate(record, (current, patch) => patch.Apply(current));
    }

    private static IEnumerable<RecordPatcher> GetPatches()
    {
        return _patches ??=
            typeof(RecordPatcher).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && typeof(RecordPatcher).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<RecordPatcher>()
                .ToList();
    }
}
