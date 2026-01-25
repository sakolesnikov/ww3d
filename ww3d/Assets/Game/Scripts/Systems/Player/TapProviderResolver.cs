using System.Collections.Generic;
using VContainer;

[LevelScope]
public class TapProviderResolver : ISelfRegisterable {

    [Inject]
    private readonly IEnumerable<ITapProvider> providers;

    public ITapProvider Resolve(in TapContext ctx) {
        foreach (var p in providers) {
            if (p.CanHandle(ctx)) {
                return p;
            }
        }

        return null;
    }

}