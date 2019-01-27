using Accounting.Module.BusinessObjects;

namespace Accounting.Module
{
    public interface ISupportDefaultVatRate
    {
        VatRate DefaultVatRate { get; set; }
    }
}