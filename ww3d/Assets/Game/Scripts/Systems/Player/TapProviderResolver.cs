using System.Collections.Generic;
using System.Linq;
using VContainer;
using VContainer.Unity;

[LevelScope]
public class TapProviderResolver : ISelfRegisterable, IInitializable {

    [Inject]
    private IEnumerable<ITapProvider> providers;

    public void Initialize() {
        providers = providers.OrderBy(p => p.Order).ToList();
    }

    public ITapProvider Resolve(in TapContext ctx) {
        foreach (var p in providers) {
            if (p.CanHandle(ctx)) {
                return p;
            }
        }

        return null;
    }

}