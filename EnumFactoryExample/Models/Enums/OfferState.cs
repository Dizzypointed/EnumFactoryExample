using EnumFactoryExample.Models.Factory;

namespace EnumFactoryExample.Models.Enums
{
    [JavaScriptEnum]
    public enum OfferState
    {
        New, 
        Persisted, 
        Published, 
        Deleted
    }
}