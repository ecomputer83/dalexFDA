using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Effects
{
    [Preserve(AllMembers = true)]
    public class RemoveBorderEffect : RoutingEffect
    {
        public RemoveBorderEffect() : base(EffectIds.RemoveBorderEffect)
        {
        }
    }
}
